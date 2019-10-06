REM Install .NET Core 3.0 Preview 9 SDK

REM .NET Core 3.0 SDK includes F# Interactive (fsi.exe). This is a REPL like
REM irb or node. Previous versions of .NET Core did not come with FSI.

echo "Download and install .NET Core 3 from"
echo "https://dotnet.microsoft.com/download/dotnet-core/3.0"
pause

REM Once released you will be able to install it from Chocolatey:
REM cinst dotnetcore-sdk

REM Check the correct version is installed
dotnet --version

REM Run the FSI Shell (quit with `#quit;;`)
dotnet fsi

REM Run a saved script
dotnet fsi example.fsi

REM To create a new F# console project:
mkdir newapp
cd newapp
dotnet new console -lang F#
dotnet run
