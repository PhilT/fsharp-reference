---
title: Installing Chocolatey
description: Every operating system should have a package manager. A canonical way to install any piece of software. Windows has [Chocolatey](https://chocolatey.org).
keywords: package manager
---

While Windows developers catch up to the idea of distributing via a package
manager it does have most of the popular software in there.

The official way to install Chocolatey is to open PowerShell and run the
following command:

```powershell
Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).
  DownloadString('https://chocolatey.org/install.ps1'))
```

Or check out [https://chocolatey.org/install](https://chocolatey.org/install)
for full install instructions.
