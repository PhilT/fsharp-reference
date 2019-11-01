---
title: Create a Static Website on AWS with Custom Domain over HTTPS
description: Some notes on quickly setting up a static website with a custom apex & www domain over HTTPS/TLS using Amazon S3, Route 53, CloudFront and Let's Encrypt Certificate Authority.
created: 2019-10-28
updated:
categories:
---

It's a great way to start on your Serverless journey.

## Prerequisites

Make sure you're logged in to your AWS account **https://console.aws.amazon.com**.
If you've ever used Amazon, AWS uses the same credentials.

## Register a domain

I've recently switched to https://namecheap.com. They seem to be the cheapest (
their WHOIS privacy is free), their interface is pretty simple to use and
I've not had any issues in the last few months of using them.

I used them to register this website **electricvisions.com**.

## Create S3 Buckets and upload files

### Apex domain bucket

Amazon requires the bucket name to match the domain name.
So for the apex domain our bucket will be called **electricvisions.com**.

1. Browse to **https://s3.console.aws.amazon.com**
1. Click **+ Create Bucket**
1. *Name and region*, enter **Bucket name** which should match the domain name e.g. `electricvisions.com` and click **Next**
1. *Configure options*, click **Next**
1. *Permissions*, Uncheck **Block all public access** and click **Next**
1. *Review*, click **Create Bucket**
1. Click **Upload** and drag in your HTML files
1. Click the Bucket name, in our case **electricvisions.com**, to edit the settings
1. Click **Properties** tab
1. Click **Static website hosting** card
1. Click **Use this bucket to host a website**
1. Enter your root document in **Index document** (usually `index.html`)
1. Enter your error document if you have one
1. Click **Save**
1. Click **Permissions** tab
1. Click **Bucket Policy**
1. Enter the following into the editor changing `electricvisions` to your domain:
    ```
    {
        "Version": "2012-10-17",
        "Statement": [
            {
                "Effect": "Allow",
                "Principal": "*",
                "Action": "s3:GetObject",
                "Resource": "arn:aws:s3:::electricvisions.com/*"
            }
        ]
    }
    ```
   and click **Save**

### Www domain bucket

This will just redirect to the apex domain.

1. Click **+ Create Bucket**
1. *Name and region*, enter **Bucket name** which should match the domain name e.g. `www.electricvisions.com` and click **Next**
1. *Configure options*, click **Next**
1. *Permissions*, this time you can leave the defaults and click **Next**
1. *Review*, click **Create Bucket**
1. Click the Bucket name, in our case **www.electricvisions.com**, to edit the settings
1. Click **Properties** tab
1. Click **Static website hosting** card
1. Click **Redirect requests**
1. Enter `electricvisions.com` for the **Target bucket or domain**
1. Click **Save**

## Create a CloudFront distribution

1. Browse to **https://console.aws.amazon.com/cloudfront**
1. Click **Create Distribution**
1. Under *Web* click **Get Started**
1. Click in the text box next to **Origin Domain Name** and select
   **electricvisions.com.s3.amazonaws.com** from the list
1. Click **Redirect HTTPS to HTTPS** (You always want to have secure connections)
1. Click **Create Distribution**

## Create TLS certificates

We'll now add TLS certificates to CloudFront using letsencrypt.org's free
service.

Diego Lapiduz (@dlapiduz) created a great script to automate the verification
and upload of the certificates. I've added a little script to make it easy for
apex and www domains on Windows.

```
git clone https://github.com/PhilT/certbot-s3front
docker build . -t certbot-s3front
cp env.example.ps1 env.ps1
```

Fill in the details in `env.ps1`. Run the following and follow the
instructions:

```
./env
./install
```

## Configure Route 53

1. Browse to **https://console.aws.amazon.com/route53**
1. Click **Create Hosted Zone**
1. Domain Name: **electricvisions.com**
1. Click **Create**
1. Click **Create Record Set**
1. Check *Alias* **Yes**
1. Click text box **Alias Target** and select the CloudFront domain
1. Click **Save Record Set**
1. Click **Create Record Set**
1. For *Name* enter **www**
1. Check *Alias* **Yes**
1. Click text box **Alias Target** and select the CloudFront domain
1. Click **Save Record Set**
1. Make a note of the 4 NS records
1. Go to **https://namecheap.com**
1. Login and click **Manage** on *electricvisions.com*
1. Under *NAMESERVERS* select **Custom DNS** from the list
1. Add the 4 NS records noted previously

## Back to CloudFront

1. Click the **ID of your distribution**
1. Click **Edit**
1. In *Alternate Domain Names (CNAMEs)* add:
    ```
    electricvisions.com
    www.electricvisions.com
    ```
1. Click **Yes, Edit**
