echo "Building docs..."
mkdir -f output/electricvisions/assets | Out-Null
mkdir -f output/fsharp-reference/assets | Out-Null
mkdir -f output/matter-game/assets | Out-Null

cp -r -force assets/* output/electricvisions/assets
cp -r -force assets/* output/fsharp-reference/assets
cp -r -force assets/* output/matter-game/assets
cp -r -force matter-game/assets output/matter-game
cp -r -force electricvisions/assets output/electricvisions

cp -force electricvisions/favicon.ico output/electricvisions
cp -force fsharp-reference/favicon.ico output/fsharp-reference
cp -force matter-game/favicon.ico output/matter-game

dotnet fsi src/build.fsx
echo Done.
