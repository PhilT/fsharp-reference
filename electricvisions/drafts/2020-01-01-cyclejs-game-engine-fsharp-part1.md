---
title: CycleJS Style Game Engine in F# Part 1: Analysis
description: Recently I took another look at CycleJS to see if I can take some inspiration for the design of my game engine.
created: 2020-01-01
updated:
keywords: f# reactiveprogramming observables cyclejs
---

I first discovered CycleJS when I touched on functional and reactive programming
while still working on web applications. The simple dataflow concept immediately
struck a cord with me and I enjoyed playing around with some simple apps.

After realising I'd have to learn reactive programming, I'll now take another
look at CycleJS to see if I can reuse some of the concepts. The main (CycleJS.org)[cyclejs.org]
website has a great overview and docs guide which I recommend reading first.
The [videos on egghead.io](https://egghead.io/courses/cycle-js-fundamentals) are
also first rate and were really helpful in understanding how things work under
the hood.

Do note that in reactive programming the correct term for event streams are
**observables**. I have to agree with Staltz here and prefer to use the term,
**stream**.

## A summary of key features of CycleJS

I'll now try to summarize here for the purpose of assessing it's suitability.

A basic CycleJS app looks like this:

    //--- logic
    function main(sources) {
      const input$ = sources.DOM.select('.field').events('input')

      const name$ = input$.map(ev => ev.target.value).startWith('')

      const vdom$ = name$.map(name =>
        div([
          label('Name:'),
          input('.field', {attrs: {type: 'text'}}),
          hr(),
          h1('Hello ' + name),
        ])
      )

      return { DOM: vdom$ }
    }

    run(main, { DOM: makeDOMDriver('#app') })

It's a pure function that takes *sources* and returns *sinks*. Sources contain
a bunch of user action streams keyed on a driver and the HTML element
(for example the DOM driver and in this case the input named `.field`). Sinks are
streams of view data which are returned back to CycleJS for rendering.

All the side-effects happen in the drivers. The main function is data in, data
out making it really easy to test.

The DOM driver is where streams are subscribed to and hence why they need to be
hot. This is where the DOM is written to and events read. CycleJS
uses a virtual DOM but that's not relevant for our purposes.

Signature for creating a driver:

    function myDriver(sink$, name /* optional */)

E.g. a simplified DOM driver

    //--- effects
    function makeDOMDriver(root) {
      function domDriver(text$) {
        text$: subscribe({
          next: str => {
            const elem = document.querySelector(root)
            elem.textContent = str
          }
        })
        const domSource = fromEvent(document, 'click')
        return domSource
      }
    }

Components can be created in the same way as `main`. Sources are passed in and
sinks are returned. In this case properties or a `props` key can be passed in
via the sources object and used to set some initial state. A `value` key can
be returned in the sinks object which the containing component or app can use
to complete some additional calculation. This is combined with the DOM sink of
the component and added to the main app. `props` and `value` are just arbitary
key names. Anything can be used.

Components are also isolated. They shouldn't be able to detect events from other
components nor leak it's output into other components. When passing `sources`
down to a component, only include events relevant to the component. `sinks` need
to be have the same class name added to the vdom output to uniquely identify them.

## Is CycleJS relevant for a reactive game engine?

Well the logic in a CycleJS app are just pure functions so any language that
supports pure functions and has a stream library will do.

In a game engine, you'd have a scene graph. This is analogous to the DOM in
CycleJS. Components would be groups of related objects but I think I want to
explore this in a little more detail to see if it fits.

Everything is a stream. This is one of the hardest parts coming from an OO
imperative background. Before I can program my game I have to reprogram myself!
Still, I've already written some code to handle mouse input in a reactive way
and found it really quite beautiful.

