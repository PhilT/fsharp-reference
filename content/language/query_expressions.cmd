REM Install SQLite3 with Chocolatey:
cinst sqlite

REM Install NuGet packages (No existing project file):
dotnet new console --name query_expressions -lang F#
dotnet add package SQLProvider
dotnet add package System.Data.SQLite

REM Or, if using the existing query_expressions.fsproj file:
dotnet restore

REM Make the interop libraries available to SQLProvider:
set SQLITE_BASEDIR=packages\system.data.sqlite.core\1.0.111
set INTEROP_DLL=native\netstandard2.0\SQLite.Interop.dll
mkdir %SQLITE_BASEDIR%\lib\netstandard2.0\x86
mkdir %SQLITE_BASEDIR%\lib\netstandard2.0\x64
copy %SQLITE_BASEDIR%\runtimes\win-x86\%INTEROP_DLL% %SQLITE_BASEDIR%\lib\netstandard2.0\x86
copy %SQLITE_BASEDIR%\runtimes\win-x64\%INTEROP_DLL% %SQLITE_BASEDIR%\lib\netstandard2.0\x64


REM Create a customers table:
sqlite3 query_expressions.db < query_expressions.sql
