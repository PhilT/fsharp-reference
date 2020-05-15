window.addEventListener('DOMContentLoaded', () => {
  addTitleToKeywords()

  let menuDiv = document.getElementById('subjects')
  renderMenu(menuDiv, window.menu)

  let page = document.location.pathname.replace(/\.html$/, '')
  page = page == '/' ? (window.mainPage || '/main/index') : page

  loadPage(page)
  let title = findTitle(page)
  window.history.replaceState({ id: page, title }, title, page + '.html')
  highlightMenuItem(page)
  scrollToMenuItem(page)

  let input = document.querySelector('#search input')
  if (input) {
    input.addEventListener('keyup', event => {
      renderMenu(menuDiv, filterTitles(event.target.value))
    })
  }

  menuDiv.addEventListener('click', event => {
    let id = event.target.id
    event.preventDefault()
    loadPage(id)
    let title = findTitle(id)
    window.history.pushState({ id, title }, title, id + '.html')
    highlightMenuItem(id)
  })
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

window.addEventListener('popstate', event => {
  let id = event.state.id
  loadPage(id)
  highlightMenuItem(id)

})

async function loadPage(id) {
  let response = await fetch(`/content${id}.html`)
  let content = document.getElementById('content')
  document.title = `${window.location.host} - ${window.history.state.title}`
  content.innerHTML = await response.text()
  document.body.scrollIntoView()
  addSocialLinksTo(content)
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
    return section.pages.find(page => page.id == relativeId)
  }).filter( a => a )

  return pages.length > 0 && pages[0].name
}

function scrollToMenuItem(page) {
  document.getElementById(page).scrollIntoView()
}
