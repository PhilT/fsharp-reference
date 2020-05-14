---
title: Neovim
description: Neovim gives you 16 million colour support and originally had some other advantages but Vim caught up. However, for Windows, I still find Neovim to be the better option right now.
created: 2019-11-13
keywords: ide development editor vscode visual studio
---

I was, until recently, using Neovim-QT. However, after having some trouble with a
plugin not rendering floating windows correctly I took a look at neovim in
Windows Terminal. Turns out it's noticeably faster, especially on startup. This
coupled with Windows Terminal's support for splitting panes and I'm sticking with
the console.

Setting up Vim for F# development can be slightly harder than say VS Code or
Visual Studio but I've found less issues with it once running. I find VS Code and
Visual Studio sluggish and buggy in comparison. Vim emulators can be found in
most editors these days but if you're a real Vim user you'll find them lacking.

Once [Chocolatey is installed](chocolatey.html) you can install Neovim very easily.
Open PowerShell Admin console and type:

```
cinst neovim
```

It's best to run Neovim from CMD, PowerShell or a shortcut as Bash has some strange behaviour.
I've had crashes and certain plugins not working correctly (e.g. FZF). This is probably due to
the plugins expecting Linux path names or just oddness with MinGW.

Run it with `nvim`.

## Neovim Plugins

Setting the `XDG_CONFIG_HOME` environment variable allows the Neovim configuration
file to be placed in a sensible location.

```
setx XDG_CONFIG_HOME %USERPROFILE%\.config
```

coc-fsharp seems to be the most reliable syntax checker for Vim/Neovim and .NET
Core 3. Others had various issues but these will probably get resolved at some point.
In `${XDG_CONFIG_HOME}/nvim/init.vim`:

```
Plug 'neoclide/coc.nvim', {'branch': 'release'} " Intellisense engine with support for VSCode plugins
```

Install the coc-fsharp plugin with the following:

```
nvim -c 'CocInstall -sync coc-fsharp|q'
```

Also, for spellchecked multi-line comments, add the following:

```
autocmd bufread *.fsx,*.fs syn region fsharpMultiLineComment start="(\*" end="\*)" contains=fsharpTodo,@Spell
```

