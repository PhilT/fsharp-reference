echo "Building docs..."
mkdir -f output/electricvisions/assets | Out-Null
mkdir -f output/fsharp-reference/assets | Out-Null
mkdir -f output/matter-game/assets | Out-Null

# TODO: How to check for file existence before attempting delete?
rm output/electricvisions/index.html
rm output/matter-game/index.html

cp -r -force assets/* output/electricvisions/assets
cp -r -force assets/* output/fsharp-reference/assets
cp -r -force assets/* output/matter-game/assets
cp -r -force matter-game/assets output/matter-game

dotnet fsi build.fsx
echo Done.
