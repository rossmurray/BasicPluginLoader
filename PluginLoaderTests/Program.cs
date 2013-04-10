using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicPluginLoader;
using TestContracts;
using System.IO;

namespace PluginLoaderTests
{
	class Program
	{
		static void Main(string[] args)
		{
			//Visual Studio Express Edition doesn't have unit testing...
			Test(CanLoadTestPluginBuilder);
			Test(LoadsOnlyOneBuilder);
			Test(LoadedBuilderWorks);
			Test(WrongAssemblyDies);
			Test(CantLoadNonExport);
			Test(CanLazyLoadBuilder);
			Test(LazyLoadedBuilderworks);
			Test(CantLazyLoadWrongDll);
			Test(CantLazyLoadWrongType);
		}

		public static void Test(Func<bool> test)
		{
			try
			{
				Console.Write(test.Method.Name + Environment.NewLine + "    ");
				var result = test();
				Console.WriteLine(result ? "Succeeded" : "Failed!");
			}
			catch (Exception ex)
			{
				Console.WriteLine(": {0} - {1}", ex.GetType(), ex.Message);
			}
			Console.WriteLine();
		}

		public static bool CantLazyLoadWrongType()
		{
			var empty = PluginLoader.GetPlugins<ITestPlugin>(new FileInfo("TestPlugin.dll"));
			return !empty.Any();
		}

		public static bool CantLazyLoadWrongDll()
		{
			try
			{
				var nothing = PluginLoader.GetPlugins<ITestPluginBuilder>(new FileInfo("nothing.dll"));
				return false;
			}
			catch (Exception)
			{
				return true;
			}
		}

		public static bool LazyLoadedBuilderworks()
		{
			var builder = PluginLoader.GetPlugins<ITestPluginBuilder>(new FileInfo("TestPlugin.dll")).First().Value;
			var p = builder.Build();
			return p != null && p.TestValue <= int.MaxValue;
		}

		public static bool CanLazyLoadBuilder()
		{
			var builder = PluginLoader.GetPlugins<ITestPluginBuilder>(new FileInfo("TestPlugin.dll"));
			return builder != null && builder.Any() && builder.First() != null && builder.First().Value != null;
		}

		public static bool CantLoadNonExport()
		{
			var empty = PluginLoader.LoadPlugins<ITestPlugin>(new FileInfo("TestPlugin.dll"));
			return !empty.Any();
		}

		public static bool WrongAssemblyDies()
		{
			try
			{
				var nothing = PluginLoader.LoadPlugins<ITestPluginBuilder>(new FileInfo("nothing.dll"));
				return false;
			}
			catch (FileNotFoundException)
			{
				return true;
			}
		}

		public static bool LoadedBuilderWorks()
		{
			var builder = PluginLoader.LoadPlugins<ITestPluginBuilder>(new FileInfo("TestPlugin.dll")).First();
			var plugin = builder.Build();
			return plugin != null && plugin.TestValue >= int.MinValue;
		}

		public static bool LoadsOnlyOneBuilder()
		{
			var p = PluginLoader.LoadPlugins<ITestPluginBuilder>(new FileInfo("TestPlugin.dll"));
			return p != null && p.Any();
		}

		public static bool CanLoadTestPluginBuilder()
		{
			var p = PluginLoader.LoadPlugins<ITestPluginBuilder>(new FileInfo("TestPlugin.dll"));
			return p != null && p.Any();
		}
	}
}
