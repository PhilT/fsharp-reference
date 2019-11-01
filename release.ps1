echo 'Uploading docs to S3...'
aws s3 sync output/electricvisions s3://electricvisions.com --delete
aws s3 sync output/fsharp-reference s3://fsharp-reference.com --delete
aws s3 sync output/matter-game s3://matter-game.com --delete
echo 'Done.'
