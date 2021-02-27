# Websites

All the websites I run, in one place!

* electricvisions.com - Company website, General tech blog, Windows, F#, .NET, Ruby
* matter-game.com - Matter, the game I'm working on

## Building

#### `./build`
Compile the F# project in `src`

#### `./run`
Converts FSX files to HTML and places them in `output/`. Also, generates the
article index for electricvisions and matter-game and the index menu data for
fsharp-reference.

#### `./start <website>`
Run a local development web server on one of the following ports:

| website | port |
| ------- | -------- |
| `electricvisions`| 9000 |
| `matter-game`| 9001 |

### `./release`

Uploads `output/` files to S3

## Dependencies

Add new dependencies to paket.dependencies and run `dotnet paket install`

