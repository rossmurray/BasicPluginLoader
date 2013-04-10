using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace BasicPluginLoader
{
	public static class PluginLoader
	{
		public static IEnumerable<Lazy<T>> GetPlugins<T>(FileInfo assembly) where T : class
		{
			return GetPlugins<T>(new[] { assembly });
		}

		public static IEnumerable<Lazy<T>> GetPlugins<T>(IEnumerable<FileInfo> assemblies) where T : class
		{
			return GetLazyExports<T>(assemblies);
		}

		public static IEnumerable<T> LoadPlugins<T>(FileInfo assembly) where T : class
		{
			return LoadPlugins<T>(new[] { assembly });
		}

		public static IEnumerable<T> LoadPlugins<T>(IEnumerable<FileInfo> assemblies) where T : class
		{
			var lazyExports = GetLazyExports<T>(assemblies);
			return lazyExports.Select(x => x.Value);
		}

		private static IEnumerable<Lazy<T>> GetLazyExports<T>(IEnumerable<FileInfo> assemblies) where T : class
		{
			var assemblyCatalogs = assemblies.Where(a => a != null).Select(a => new AssemblyCatalog(a.FullName));
			var container = new CompositionContainer(new AggregateCatalog(assemblyCatalogs));
			return container.GetExports<T>();
		}
	}
}
