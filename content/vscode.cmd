REM # Visual Studio Code Setup

REM ## Setup VS Code to give type info and run FSI

echo 'Install `ionide-fsharp` extension'
pause

echo Open settings.json and add:
echo '"FSharp.fsacRuntime": "netcore",'
echo '"FSharp.fsiFilePath": "C:\\Users\\Phil\\bin\\startfsi.cmd",'
pause

REM Create the `startfsi.cmd` referenced in the line above
echo '"C:\Program Files\dotnet\dotnet.exe" fsi %1 > C:\Users\Phil\bin\startfsi.cmd'

REM Run selected text in FSX files with `Alt+Enter`.