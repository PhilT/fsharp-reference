---
title: Windows 10 and PowerShell
description: It's the original home of .NET and it's currently the best platform for writing and playing games. This summary has currently been tested on Windows 10 only. Setup instructions assume Windows and PowerShell.
---


### Windows Terminal

I launch nvim directly which uses cmd by default. However, I have shortcuts to
launch a powershell terminal in Neovim. However, Windows Terminal development is
progressing nicely.

There is a preview in the Windows App Store of Windows Terminal which gets
updated. GPU text rendering, a modern look & themes, tabs, split panes(!) and
UTF-8 appear to be the standout features. It's a pretty early build but seems
to work well enough. It's currently missing search and there are some redraw
issues when resizing the terminal
([tracking issue](https://github.com/microsoft/terminal/issues/4200))
particularly in *Vim*.

### PowerShell

I come from a Linux background (Well it was more like Windows -> Linux/Windows).
So I'm a Bash guy. However, on Windows it's just better to run things in CMD or
PowerShell sometimes. CMD is very restrictive and some commands in PowerShell are
verbose. However, I'd prefer to use one shell rather than keep switching. I still
use Git Bash for Windows for some things (`dos2unix` anyone?!) but slowly figuring
out things in PowerShell.

Powershell aliases a lot of commands from Linux which helps with the transition.
Notice that I'm also using forward slashes. Windows is fine with this and will
accept forward or it's native original backslash path separators.

Some commands also have a `-filter` option hence, you need to use at least one
more character to tell PS which option you want. I prefer to just make it obvious
especially in scripts.

Here are a list of commands that got me going with PowerShell:

Remove directory and all files and subdirectories recursively. Works without
`-force` unless you're dealing with directories that start with a `.` (dot).

```powershell
rm -r -force <directory>
```

Echo a string to the end of a file

```powershell
ac <path> "<string>"
```

Display the contents of a file (exactly like Bash)

```powershell
cat <path>
```

Create a directory - don't care if it exists

```powershell
mkdir -f <directory/subdirectory/subsubdir> | Out-Null
```

Get help for a command (don't use `<command> --help` this will do something)

```powershell
man <command>

# e.g.

man mkdir
```

Copy over existing file

```powershell
cp -r -force <source> <destination>
```

Run a PowerShell script called `build.ps1` in the current directory

```powershell
./build
```

Permanently set an environment variable

```powershell
setx TERM xterm-256color
```


