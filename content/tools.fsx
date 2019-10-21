(**
## Tools and Platforms

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

### Git Bash for Windows

At this time of writing Windows Subsystem for Linux is too slow to do real work.
Version 2 is coming which will address the IO issues but we don't need a whole
separate operating system. Powershell is too verbose and CMD is lacking sensible
commands (just try converting a bunch of DOS files to Unix line-endings). GitBash
allows us to run some commands, build stuff and it also means our scripts work
cross-platform.

Some commands are still better run in a native shell. For these the new Windows
Terminal can be used. You can install it via the Windows App Store. It'll run
either Powershell or CMD.

 *)
