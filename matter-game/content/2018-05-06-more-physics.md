---
title: More Physics
description: Physics will play an important role in this game and the performance of the physics engine is a crucial factor. I've spent a bit more time tweaking the comparison and trying out ideas.
created: 2018-05-06
updated:
categories: javascript physics, 3d
---

What I've realised is that decoupling of physics from the display is required if I ever want to render large worlds server-side. It'll also help with separation of concerns. When building large programs this will help keep issues localised.

So, for now, that means Energy.js is out. It's performance is right up there and it has some amazing features but currently it depends on BabylonJS which means I'd have to get BabylonJS running in node which is something I'd like to avoid. It's also still in development but I'll be keeping an eye on it.

Ammo is pretty complicated and Cannon is the slowest of the bunch. Oimo seemed pretty decent and simple so I added another tab to the comparison to test Oimo directly without going through BabylonJS for potential use on the server. It also allowed me to tweak performance a bit as well.

![Collapsing wall physics engine test](/assets/enginetest/engine_test2.png)

[Here is the comparison](/assets/enginetest) again updated using Oimo.js directly.

I'm pretty happy with the results. I've spent a fair bit of time thinking about this stuff and procrastinating with low-level details, productivity tools and functional programming.

Now, it's time to get serious and start building out a proper demo.

