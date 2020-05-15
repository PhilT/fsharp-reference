window.addEventListener('DOMContentLoaded', () => {
  addTitleToKeywords()

  let menuDiv = document.getElementById('subjects')
  renderMenu(menuDiv, window.menu)

  let id = document.location.pathname.replace(/\.html$/, '')
  id = id == '/' ? (window.mainPage || '/main/index') : id

  // TODO DRY up this code
  let page = findTitle(id)
  loadPage(page)
  window.history.replaceState(page, page.title, id + '.html')
  highlightMenuItem(id)
  scrollToMenuItem(id)

  let input = document.querySelector('#search input')
  if (input) {
    input.addEventListener('keyup', event => {
      renderMenu(menuDiv, filterTitles(event.target.value))
    })
  }

  menuDiv.addEventListener('click', event => {
    let id = event.target.id
    event.preventDefault()

    // TODO DRY up this code with above code
    let page = findTitle(id)
    loadPage(page)
    window.history.pushState(page, page.title, id + '.html')
    highlightMenuItem(id)
  })
})

window.addEventListener('popstate', event => {
  loadPage(event.state)
  highlightMenuItem(event.state.id)
})

// TODO move this to F# build side
function addTitleToKeywords() {
  window.menu.map(section => {
    section.pages.map(page => {
      page.name.toLowerCase().split(' ').forEach(word => page.keywords.push(word))
    })
  })
}

function filterTitles(term) {
  term = term.toLowerCase()
  let sections = window.menu.map(section => {
    let pages = section.pages.filter(page =>
      page.keywords.some(k => k.match(term || ".*")))
    return { ...section, pages }
  }).filter( section => section.pages.length > 0 )

  return sections
}

function renderMenu(menuDiv, menu) {
  menuDiv.innerHTML = ''
  menu.forEach(section => {
    let h2 = document.createElement('h2')
    let ul = document.createElement('ul')
    h2.innerHTML = section.heading
    menuDiv.appendChild(h2)
    menuDiv.appendChild(ul)

    section.pages.forEach(page => {
      let item = document.createElement('li')
      let link = document.createElement('a')

      link.id = `/${section.path}${page.id}`
      let subid = page.subid === undefined ? '' : `#${page.subid}`
      link.href = `/${section.path}${page.id}.html${subid}`
      link.innerHTML = page.name
      item.appendChild(link)
      ul.appendChild(item)
      menuDiv.appendChild(ul)
    })
  })
}

async function loadPage({id, section, title}) {
  let response = await fetch(`/content${id}.html`)
  let content = document.getElementById('content')
  document.title = `${window.location.host} - ${title}`
  content.innerHTML = await response.text()
  document.body.scrollIntoView()

  if (window.socialLinks && section !== 'Main') {
    addSocialLinksTo(content)
  }
}

function addSocialLinksTo(content) {
  let socialDiv = document.createElement('div')
  let link = document.createElement('a')

  socialDiv.id = 'social'
  link.classList.add("twitter-share-button")
  link.href = "https://twitter.com/intent/tweet"
  link.setAttribute('data-size', 'large')
  link.innerHTML = 'Tweet'
  socialDiv.appendChild(link)
  content.appendChild(socialDiv)

  if (twttr) { twttr.widgets.load(link) }
}

function highlightMenuItem(id) {
  let current = document.getElementsByClassName('current')[0]
  if (current !== undefined) {
    current.classList.remove('current')
  }

  let link = document.getElementById(id)
  link.classList.add('current')
}

function findTitle(id) {
  let pages = window.menu.map(section => {
    let relativeId = id.replace('/' + section.path, '')
    let page = section.pages.find(page => page.id == relativeId)
    return page ? { id, title: page.name, section: section.heading } : false
  }).filter( a => a )

  return pages.length > 0 && pages[0]
}

function scrollToMenuItem(page) {
  document.getElementById(page).scrollIntoView()
}
