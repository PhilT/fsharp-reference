# Websites

All the websites I run, in one place!

* electricvisions.com - Company website, General tech blog, Windows, F#, .NET, Ruby
* fsharp-reference.com - Quick reference of the F# language
* matter-game.com - Matter, the game I'm working on, progress, learnings, graphics engine, etc

## Building

* `./build` - Converts FSX files to HTML and places them into `output/`
* `./release` - Uploads `output/` files to S3
* `./start <website>` - Run a local development web server
    * website | port
    * `electricvisions` | 9000
    * `fsharp-reference` | 9001
    * `matter-game` | 9002
