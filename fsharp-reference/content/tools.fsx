(**
---
title: # Tools and Platforms
description: Operating system and tool choice and why.
---

### F#

Currently runs under version 4.7 but will be updated as new versions are
released. It's shipped with .NET Core.

### .NET Core 3 SDK

It appears to be the future direction of .NET. It makes sense in order to
support multiple operating systems. One of the great features of version 3
is that it ships with F# Interactive, a shell to run F# scripts without first
compiling them.

* [Install instructions](dotnetsdk.html)

### Windows 10

It's the original home of .NET and it's currently the best platform for writing and
playing games. Other operating systems should work but this summary has not currently
been tested outside of Windows 10. Setup instructions assume Windows.

### Chocolatey

Every operating system should have a package manager. A single way to install any
piece of software. Windows has [Chocolatey](https://chocolatey.org) and while
Windows developers catch up to the idea of distributing via a package manager it
does have most popular software there.

* [Install Chocolatey](chocolatey.html)

### Neovim

Neovim QT gives you 16 million colour support and had some other advantages, however,
Vim has caught up but for Windows I still find Neovim QT to be the better option on
Windows. Setting up Vim for F# development can be slightly harder than say VS Code or
Visual Studio but I've found less issues with it once running. I find VS Code and
Visual Studio sluggish and buggy in comparison. Finally, by choosing to work the way
Vim does the emulations in other editors never seem quite as complete.

* [Install Neovim & plugins for F#](neovim.html)

### PowerShell & Windows Terminal

I come from a Linux background (Well it was more like Windows -> Linux/Windows). So
I have Bash built-in. However, on Windows it's just better to run things in CMD or
PowerShell sometimes. CMD is very restrictive and some commands in PowerShell are verbose.
However, I'd prefer to have one shell than having to keep switching. I still use
Git Bash for Windows for some things (`dos2unix` anyone?!) but slowly figuring out
things in PowerShell.

There is a preview in the Windows App Store of Windows Terminal. GPU text rendering,
a modern look & themes, tabs and UTF-8 appear to be the standout features. It's a pretty early build
but seems to work well enough. For me, it's currently missing search.

 *)
