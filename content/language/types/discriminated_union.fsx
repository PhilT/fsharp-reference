(**
# Discriminated Unions

Can be any one of a set of possible types. Can be used instead of small
object hierarchies.

Can also be a struct type with `[<Struct>]` attribute. These cannot use a
recursive type definition.

Members such as properties and interfaces can also be added.

Here, `Shape` can be one of `Rectangle`, `Circle`, `Prism` case identifiers:
 *)

type Shape =
  | Rectangle of width: float * length: float
  | Circle of radius: float
  | Prism of width: float * float * height: float // 2 named and 1 anonymous field

let rect = Rectangle(length = 1.3, width = 10.0)
let circle = Circle(1.0)
let prism = Prism(5., 2.0, height = 3.0)

(**
The `Option` core type is a discriminated union.

The case identifiers can be used in pattern matching:
 *)

let getShapeHeight shape =
  match shape with
  | Rectangle(length = l) -> l
  | Circle(radius = r) -> 2. * r
  | Prism(height = h) -> h

(**
Unwrapping the underlying value for a single type:
 *)

type ShaderProgram = | ShaderProgram of id: int
let someFuncUsingShader shaderProgram =
  let (ShaderProgram id) = shaderProgram
  id

(**
Unwrapping the value in function parameters:
 *)
let someFunction (ShaderProgram id) =
  () // function body

(**
Tree data structures using `rec`

Binary tree:
 *)

type Tree =
  | Tip
  | Node of int * Tree * Tree

let rec sumTree tree =
  match tree with
  | Tip -> 0
  | Node(value, left, right) ->
    value + sumTree(left) + sumTree(right)

let myTree = Node(0, Node(1, Node(2, Tip, Tip), Node(3, Tip, Tip)), Node(4, Tip, Tip))
let resultSumTree = sumTree myTree

(**
Discriminated unions work well if the nodes in the tree are heterogeneous (of
different types).
 *)
