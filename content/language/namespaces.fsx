// # Namespaces

// .NET construct to attach a name to a group of program elements.
// can contain modules and types. Namespaces cannot contain
// functions. Only modules contain functions.

// namespace Widgets

type MyWidget1 =
  member this.WidgetName = "Widget1"

module WidgetsModule =
  let widgetName = "Widget2"