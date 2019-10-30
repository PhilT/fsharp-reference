(**
# Access Control

The following access levels can be applied to modules, type, methods, value
definitions, functions, properties and explicit fields [TODO: What's an explicit field?]

* `public` - the entity can be accessed by all callers
* `internal` - the entity can only be accessed from the same assembly
* `private` - the entity can only be accessed from the enclosing type or module
* `protected` - not used by F# but when overriding a protected method it remains
                accessible only within the class and its descendants

Specifiers are placed just before the name of the entity except

Access specifiers cannot be used with `inherit` as they will have the same
accessibility as the enclosing type.

TODO: Accessibility for Discriminated Unions

TODO: Accessibility for fields of a record type once page is fixed on docs.microsoft.com
*)

module AccessControl =
  module Module1 =
    // Visible within module only
    type private MyPrivateType() =
      let x = 5 // private by default
      member private this.X() = 10 // marked private
      member this.Z() = x * 100

    type internal MyInternalType() =
      let x = 5
      member private this.X() = 10
      member this.Z() = x * 100

    let private myPrivateObj = MyPrivateType() // private needed to match type
    let internal myInternalObj = MyInternalType() // internal needed to match type

    let result1 = myPrivateObj.Z // public by default
    let result2 = myInternalObj.Z // public by default

  module Module2 =
    open Module1

    // The following commented out code would error as it
    // cannot be accessed from another module:
    // `let private myPrivateObject = MyPrivateType()`

    let internal myInternalObj = MyInternalType()
    let result = myInternalObj.Z
