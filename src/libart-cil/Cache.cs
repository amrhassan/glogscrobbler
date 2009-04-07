// Cache.cs
// 
// Copyright (C) 2009 Amr Hassan
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using System.Net;
using log4net;

namespace Art
{
	public class Cache
	{
		private static object threadLock = new object();
		
		private static string rootDirectory;		
		public static string RootDirectory
		{
			get
			{
				// Default is "~/.cache/art"
				if (rootDirectory == null || rootDirectory == "")
					rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".cache/art/");
				
				// Create if not exists
				if (!Directory.Exists(rootDirectory))
				    Directory.CreateDirectory(rootDirectory);
				
				return rootDirectory;
			}
			
			set { rootDirectory = value; }
		}
		
		public static bool IsCached(string type, string key)
		{
			lock (threadLock)
			{
				string path = Path.Combine(Cache.RootDirectory, type + "/" + key);
				if (File.Exists(path) && (new FileInfo(path)).Length > 0)
					return true;
				else
					if (File.Exists(path)) File.Delete(path); return false;
			}
		}
		
		public static void Add(string type, string url, string key)
		{
			lock (threadLock)
			{
				WebClient client = new WebClient();
				string path = Path.Combine(RootDirectory, type + "/" + key);
				Directory.CreateDirectory(Path.Combine(RootDirectory, type));
				
				ILog log = LogManager.GetLogger("Art.Cache");
				log.Info("Downloading " + url + " to " + path);
				client.DownloadFile(url, path);
			}
		}
		
		public static string GetPath(string type, string key)
		{
			lock (threadLock)
			{
				string path = Path.Combine(RootDirectory, type + "/" + key);
				return path;
			}
		}
	}
}