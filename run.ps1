echo "Building docs..."
mkdir -f output/electricvisions/assets | Out-Null
mkdir -f output/matter-game/assets | Out-Null
mkdir -f output/electricvisions/content | Out-Null
mkdir -f output/matter-game/content | Out-Null

cp -r -force assets/* output/electricvisions/assets
cp -r -force assets/* output/matter-game/assets
cp -r -force electricvisions/assets output/electricvisions
cp -r -force matter-game/assets output/matter-game

cp -force electricvisions/favicon.ico output/electricvisions
cp -force matter-game/favicon.ico output/matter-game

dotnet run -p src
echo Done.
