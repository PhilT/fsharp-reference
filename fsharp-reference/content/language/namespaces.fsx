(**
---
title: Namespaces
description: .NET construct to attach a name to a group of program elements.
---

Can contain modules and types. Namespaces cannot contain
functions. Only modules contain functions.
 *)

namespace Widgets

type MyWidget1 =
  member this.WidgetName = "Widget1"

module WidgetsModule =
  let widgetName = "Widget2"

(**
* [Nested Namespaces](namespaces/nested.html)
* [Recursive Namespaces](namespaces/recursive.html)
* [Namespace/Module Shorthand](namespaces/shorthand.html)
 *)
