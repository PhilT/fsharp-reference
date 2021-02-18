open Expecto
open System.Reflection

let testsFromModules =
  let assembly = Assembly.GetExecutingAssembly()

  assembly.GetTypes()
  |> Seq.filter (fun t -> t.Name <> "Main") // Ignore current file
  |> Seq.map (fun t ->
    t.GetMethods()
    |> Array.tryFind(fun m -> m.Name = "get_tests")
  )
  |> Seq.choose id
  |> Seq.map (fun func -> ((box (func.Invoke(null, [||]))) :?> Test))
  |> Seq.toList

let tests =
  testList "Matter" testsFromModules
  |> Test.shuffle "--"

[<EntryPoint>]
let main argv =

  let config = {
    defaultConfig with
      //verbosity = Logging.LogLevel.Verbose
      colour = Logging.Colour256
  }

  runTestsWithArgs config argv tests
