window.addEventListener('DOMContentLoaded', () => {
  addTitleToKeywords()

  let menuDiv = document.getElementById('subjects')
  renderMenu(menuDiv, window.menu)

  let page = document.location.pathname.replace(/\.html$/, '')
  page = page == '/' ? '/main/index' : page

  loadPage(page)
  window.history.replaceState({ id: page }, findTitle(page), page + '.html')
  highlightMenuItem(page)
  scrollToMenuItem(page)

  let input = document.querySelector('#search input')
  input.addEventListener('keyup', event => {
    renderMenu(menuDiv, filterTitles(event.target.value))
  })

  menuDiv.addEventListener('click', event => {
    let id = event.target.id
    event.preventDefault()
    loadPage(id)
    window.history.pushState({ id: id }, findTitle(id), id + '.html')
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
  let sections = window.menu.map(section => {
    console.log(section)
    let pages = section.pages.filter(page => page.keywords.some(k => k.match(term || ".*")))
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
  console.log("Load: " + `/content${id}.html`)
  let response = await fetch(`/content${id}.html`)
  let content = document.getElementById('content')
  content.innerHTML = await response.text()
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
    return section.pages.find(page => page.id == id)
  }).filter( a => a )

  return pages.length > 0 && pages[0].name
}

function scrollToMenuItem(page) {
  document.getElementById(page).scrollIntoView()
}
