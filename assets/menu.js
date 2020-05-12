window.addEventListener('DOMContentLoaded', () => {
  let menu = document.getElementById('subjects')
  window.menu.forEach(section => {
    let h2 = document.createElement('h2')
    let ul = document.createElement('ul')
    h2.innerHTML = section.heading
    menu.appendChild(h2)
    menu.appendChild(ul)

    section.pages.forEach(page => {
      let item = document.createElement('li')
      let link = document.createElement('a')

      link.id = `/${section.path}${page.id}`
      let subid = page.subid === undefined ? '' : `#${page.subid}`
      link.href = `/${section.path}${page.id}.html${subid}`
      link.innerHTML = page.name
      item.appendChild(link)
      ul.appendChild(item)
      menu.appendChild(ul)
    })
  })

  let page = document.location.pathname.replace(/\.html$/, '')
  page = page == '/' ? window.mainPage : page

  loadPage(page)
  window.history.replaceState({ id: page }, findTitle(page), page + '.html')
  highlightMenuItem(page)
  scrollToMenuItem(page)

  menu.addEventListener('click', event => {
    let id = event.target.id
    event.preventDefault()
    loadPage(id)
    window.history.pushState({ id: id }, findTitle(id), id + '.html')
    highlightMenuItem(id)
  })
})

window.addEventListener('popstate', event => {
  let id = event.state.id
  loadPage(id)
  highlightMenuItem(id)

})

async function loadPage(id) {
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
