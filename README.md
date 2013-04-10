BasicPluginLoader
=================

.NET library providing very basic functionality for loading plugins at runtime via [MEF](http://en.wikipedia.org/wiki/Managed_Extensibility_Framework).

Usage:

Load and instantiate all `IPlugin` exports in all assemblies in a certain directory:

    var assemblies = Directory.GetFiles("plugins/", "*.dll");
    var plugins = PluginLoader.LoadPlugins<IPlugin>(assemblies);

To tag a class as an export, simply decorate it with the `[Export]` attribute. Optionally: `[Export(typeof(IPlugin))]`

.NET 4.0. Project file is Visual Studio 2010 format.
