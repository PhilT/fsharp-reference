#!/usr/bin/env bash

echo Building docs...
mkdir -p output
cp -f source/scripts.js output
cp -f source/styles.css output
source/packages/fsharp.formatting.commandtool/3.1.0/tools/fsformatting literate --processDirectory --inputDirectory content --outputDirectory output --templateFile source/template.html
echo Done.
