(**

## Install Neovim QT via Chocolatey

Once Chocolatey is installed you can install Neovim QT very easily. Open Powershell Admin console:

```
cinst neovim
```

It's best to run Neovim from CMD, Powershell or a shortcut as Bash has some strange behaviour.
I've had crashes and certain plugins not working correctly (e.g. FZF). This is probably due to
the plugins expecting Linux path names.

Run it with `nvim-qt`.

## Neovim Plugins

Setting the `XDG_CONFIG_HOME` environment variable allows the neovim config
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

Also, for spellchecked multi-line comments, add the following (remove the
space in `\* )`, it's just needed to format this document correctly):

```
autocmd bufread *.fsx,*.fs syn region fsharpMultiLineComment start="(\*" end="\* )" contains=fsharpTodo,@Spell
```

 *)
