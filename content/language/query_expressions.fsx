// # Query Expressions

// Query a data source using LINQ

#r @"packages\sqlprovider\1.1.68\lib\netstandard2.0\FSharp.Data.SqlProvider.dll"
open FSharp.Data.Sql

[<Literal>]
let RESOLUTION_PATH = __SOURCE_DIRECTORY__ + @".\packages\system.data.sqlite.core\1.0.111\lib\netstandard2.0"

[<Literal>]
let CONN_STRING = 
  "Data Source=" + __SOURCE_DIRECTORY__ + @"/query_expressions.db;" + "Version=3;foreign keys=true"

type Sql = SqlDataProvider<
                Common.DatabaseProviderTypes.SQLITE, 
                SQLiteLibrary = Common.SQLiteLibrary.SystemDataSQLite,
                ConnectionString = CONN_STRING, 
                ResolutionPath = RESOLUTION_PATH, 
                CaseSensitivityChange = Common.CaseSensitivityChange.ORIGINAL>


let db = Sql.GetDataContext()

// Table definition
type Customer = {
  Name: string
}

// Add some data
let customers = db.Main.Customers
let row = customers.Create()
row.Name <- "Joe Bloggs"
db.SubmitUpdates()

// Read some data
let customerList = 
  db.Main.Customers
  |> Seq.map(fun c -> (c.Id, c.Name))
  |> Seq.toList

// Finally, using LINQ with query expressions
let customersQuery =
  query {
    for customer in db.Main.Customers do
      select customer.Name
  }
  |> Seq.toList

// Clean up
row.Delete()
db.SubmitUpdates()
