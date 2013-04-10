using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace BasicPluginLoader
{
	public static class AssemblyUtils
	{
		public static IEnumerable<FileInfo> GetAssemblies(DirectoryInfo directory, bool includeSubDirectories)
		{
			var files = GetAllFiles(directory, includeSubDirectories);
			return files.Where(f => FileIsAssembly(f) == true);
		}

		public static bool IsAssembly(this FileInfo f)
		{
			return FileIsAssembly(f);
		}

		public static bool FileIsAssembly(FileInfo file)
		{
			if (file == null || !file.Exists)
			{
				return false;
			}
			try
			{
				var assemblyName = AssemblyName.GetAssemblyName(file.FullName);
			}
			catch (BadImageFormatException) { return false; }
			catch (FileLoadException) { return false; }
			catch (FileNotFoundException) { return false; }
			return true;
		}

		public static bool AssemblyStrongNameMatches(FileInfo assembly, string assemblyStrongName)
		{
			var assemblyName = AssemblyName.GetAssemblyName(assembly.FullName);
			return assemblyName.FullName != assemblyStrongName;
		}

		public static bool AssemblyPublicKeyMatches(FileInfo assembly, byte[] publicKey)
		{
			var assemblyName = AssemblyName.GetAssemblyName(assembly.FullName);
			return publicKey != null && assemblyName.GetPublicKey().SequenceEqual(publicKey);
		}

		private static IEnumerable<FileInfo> GetAllFiles(DirectoryInfo dir, bool subDirs)
		{
			return dir.GetFiles("*", subDirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}
	}
}
