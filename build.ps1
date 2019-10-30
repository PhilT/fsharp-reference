echo "Building docs..."
mkdir -f output | Out-Null
cp -r -force source/assets output
cd source
dotnet fake run build.fsx
cd ..
echo Done.
