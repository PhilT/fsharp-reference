echo "Building docs..."
mkdir -f output/electricvisions/assets | Out-Null
mkdir -f output/fsharp-reference/assets | Out-Null
mkdir -f output/matter-game/assets | Out-Null
mkdir -f output/electricvisions/content | Out-Null
mkdir -f output/fsharp-reference/content | Out-Null
mkdir -f output/matter-game/content | Out-Null

cp -r -force assets/* output/electricvisions/assets
cp -r -force assets/* output/fsharp-reference/assets
cp -r -force assets/* output/matter-game/assets
cp -r -force electricvisions/assets output/electricvisions
cp -r -force fsharp-reference/assets output/fsharp-reference
cp -r -force matter-game/assets output/matter-game

cp -force electricvisions/favicon.ico,electricvisions/index.html output/electricvisions
cp -force fsharp-reference/favicon.ico,fsharp-reference/index.html output/fsharp-reference
cp -force matter-game/favicon.ico,matter-game/index.html output/matter-game

dotnet fsi src/build.fsx
echo Done.
