echo "Building docs..."
mkdir -f output | Out-Null
cp -force source/scripts.js output
cp -force source/styles.css output
cd source
dotnet fake run build.fsx
cd ..
echo Done.
