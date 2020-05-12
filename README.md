# Websites

All the websites I run, in one place!

* electricvisions.com - Company website, General tech blog, Windows, F#, .NET, Ruby
* fsharp-reference.com - Quick reference of the F# language
* matter-game.com - Matter, the game I'm working on, progress, learnings, graphics engine, etc

## Building

### `./build`

Converts FSX files to HTML and places them in `output/`

### `./start <website>`

Run a local development web server on one of the following ports:

| website | port |
| ------- | -------- |
| `electricvisions`| 9000 |
| `fsharp-reference`| 9001 |
| `matter-game`| 9002 |

### `./release`

Uploads `output/` files to S3

## Dependencies

Add new dependencies to paket.dependencies and run `dotnet paket install`

