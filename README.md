[![NuGet](https://img.shields.io/nuget/v/AutoAssemblyResolver.svg?logo=nuget)](https://www.nuget.org/packages/AutoAssemblyResolver/)

# AutoAssemblyResolver

This project helps with automatically resolving assemblies (dependencies) at runtime by file name alone.

This is helpfull, if the build in assembly version bindings and redirects aren't working for you and you don't want to write your own resolver.

## How it works

The nuget package includes a source generator, which will generate an `AssemblyResolve` event for you. So you don't have to. Depending on the version of dotnet, it will use the `ModuleInitializer` attribute to let the compiler create a module initializer or it will create the module initializer on each build using a packaged tool [Injector](https://github.com/KhaosCoders/ModuleInitializer).

This way you project will automatically load any assembly by name alone, the version doesn't matter.

## Motivation

I help build a large desktop solution, which has a ton of modules each with its own dependencies. This results in a dependencies hell situation, where one dependency is brought into the solution in multiple versions. To fix this issue the dotnet compiler will normaly create assembly version redirects for you. Then the runtime will know which dependency to load once accessed. Sadly this doesn't work when the main process isn't a dotnet process and modules are accessed via COM only. One way to fix this is to add an `AssemblyResolve` resolver to each module by hand. Having this many modules, this is not an option. 

## Warning

This will load `dll` files in the programs directory, if any code will try to access it.
Depending on the use case this can be a huge security issue.

## Acknowledgement

This project builds on [Creating a module initializer in .NET](https://www.coengoedegebure.com/module-initializers-in-dotnet/) from 2017.

# Support this ❤️

If you like my work, please support this project!  
Donate via [PayPal](https://www.paypal.com/donate?hosted_button_id=37PBGZPHXY8EC)
or become a [Sponsor on GitHub](https://github.com/sponsors/Khaos66)