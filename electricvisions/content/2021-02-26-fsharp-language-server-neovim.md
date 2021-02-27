---
title: F# Language server and Neovim 0.5
description: This week was interesting. I've been using Neovim 0.5 (nightly) for several months now and I think I've finally found the setup I want.
keywords: f# neovim lsp
created: 2021-02-26
---

Previously I'd been using Neovim and [coc.nvim](github.com/neoclide/coc.nvim) along with the [coc-fsharp](github.com/coc-extensions/coc-fsharp) plugin which was working well. However, .NET 5 is out and the [fsharp-language-server](github.com/fsprojects/fsharp-language-server) which is the upstream project of coc-fsharp is in need of some love. It's also not, what I would call, the official language server for F#. That title goes to [fsautocomplete](github.com/fsharp/fsautocomplete). However, [vim-fsharp](github.com/fsharp/vim-fsharp)'s support for fsautocomplete seems like it no longer works (having not been updated for some time) and other projects such as [Ionide](github.com/ionide/ionide-vim) only support specific clients such as [LanguageClient-Neovim](github.com/autozimu/LanguageClient-neovim). Again, this plugin has had multiple issues and certainly does not work out of the box in my case and others have experienced similar issues. I'd previously seen some success using fsharp-language-server directly but again it wasn't reliable and had stopped working. Though that could be down to changes in the dev version of Neovim 0.5.

Thing were bleak, at least from my F# development perspective.

## Do It Yourself

So I decided to take another stab at getting an LSP working in Neovim 0.5. This time I'd take the still actively developed FsAutoComplete. It has an LSP mode so I must just be able to plug it in and go.

And I was! With a bit of configuration* to get FsAutoComplete to automatically load the workspace* I had diagnostics, autocompletion, goto method, the works. FsAutoComplete does have some LSP extensions which I'd like to try out but I've not delved too deeply into Neovim's LSP capabilities to know if this is possible.

I'm currently using [my own](https://github.com/PhilT/vim-fsharp) indent plugin with syntax highlighting taken from Ionide. vim-fsharp is a bit long in the tooth and doesn't support some newer syntax. It's indent support seemed spotty too so I built one test first to ensure all the rules played well and could easily added to. However, I'd like to see if I can leverage the formatting support in FsAutoComplete which uses Fantomas. Will it be as performant as the simple vimscript one. Maybe not but possibly more correct and something that could be used in addition for on-save changes.

## Why Not Use an IDE?

I actually downloaded Jetbrains Rider the other day. I installed it, clicked around a bit, typed some code and then promptly exited it and went back to Neovim. For me the lag was enough to put me off. Yes, I've invested a many years into getting my `vimrc`* tailored to my needs so I'm not about to throw it away and start again. For me, (Neo)Vim, with it's quirks* and infinite customization is worth the investment. I took the decision to switch in 2012* and I haven't regretted it.



* It provides some extensions to LSP which I've not looked into beyond this
* `init_options = { AutomaticWorkspaceInit = true }`
* or rather `init.lua`, more on that in a future post
* Lua is a much more sensible language and the fact that Neovim 0.5 provides first class support for it makes it a compelling alternative to Vimscript. Of course it will cause further splintering of the ecosystem but it's progress
* Hadn't realised it was that long. I should know Vim better!
