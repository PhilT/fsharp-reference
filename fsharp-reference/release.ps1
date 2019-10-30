echo 'Uploading docs to S3...'
aws s3 sync output s3://fsharp-reference.com --delete
echo 'Done.'
