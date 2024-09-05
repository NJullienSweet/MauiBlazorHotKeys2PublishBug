using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WixSharp;
using WixSharp.CommonTasks;

namespace WixSharpSetup
{
	internal static class Program
	{
		static void Main()
		{
			const string Manufacturer = "Foo";
			const string ProductName = "Bar";
			const string Configuration = "Release";

			var project = new Project("MyProduct", new Dir($@"%ProgramFiles%\{Manufacturer}\{ProductName}"));
			project.GUID = new Guid("dc8462cf-9bdd-41ca-aac0-88d242f66ac8");

			string rootPath = $@"..\..\..\..\MauiBlazorHotKeys2PublishBug\bin\{Configuration}\net8.0-windows10.0.19041.0\win10-x64\publish";
			var mainInstallDirectory = new Dir($@"%ProgramFiles%\{Manufacturer}\{ProductName}", CollectFiles(rootPath));
			project.AddDir(mainInstallDirectory);

			project.BuildMsi();
		}

		/// <summary>
		/// Collects all files and directories in the given path.
		/// </summary>
		/// <param name="path">Must be the complete path to the directory if it is the installation root, otherwise the name of the subdirectory.</param>
		/// <returns>The entites to install.</returns>
		private static WixEntity[] CollectFiles(string path)
		{
			var entities = new List<WixEntity>();
			var files = Directory.EnumerateFiles(path, "*");
			entities.AddRange(files.Select(f => new WixSharp.File(f)).ToArray());

			foreach (string directory in Directory.EnumerateDirectories(path))
			{
				string directoryName = Path.GetFileName(directory);
				entities.Add(new Dir(directoryName, CollectFiles(directory)));
			}
			return entities.ToArray();
		}
	}
}