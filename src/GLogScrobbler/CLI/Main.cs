// Main.cs
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
using System.Collections.Generic;
using Lastfm.Scrobbling;

namespace GLogScrobbler.CLI
{	
	public class Main
	{	
		public static void Init(string[] args)
		{
			if (args[0] == "auth" && args.Length >= 3)
				doAuth(args[1], args[2]);
			else if (args[0] == "scrobble" && args.Length >= 2)
				doScrobble(args[1]);
			else if (args[0] == "scrobble")
				doScrobble("");
			else
				doHelp();
		}
		
		private static void doScrobble(string mountpoint)
		{
			if (SettingsProxy.Username.Length == 0 || SettingsProxy.Password.Length == 0)
			{
				Console.WriteLine("Authenticate a Last.fm account first by running:\n" +
				                  "glogscrobbler auth your_username your_passowrd");
				return;
			}
			
			if (mountpoint.Length > 0)
			{
				if (!Core.DeviceDetector.PathHasLog(mountpoint))
				{
					Console.WriteLine("The provided mount point contains no valid log file.\n" +
					                  "Now trying to guess the path...");
				}
				else
				{
					scrobble(mountpoint);
					return;
				}
			} 
			
			string[] paths = Core.DeviceDetector.GetDevicesWithLogs();
			if (paths.Length > 0)
			{
				scrobble(paths[0]);
				return;
			}
			
			Console.WriteLine("Could not find a valid log file.");
		}

		private static void doAuth(string username, string password)
		{
			SettingsProxy.Username = username;
			SettingsProxy.Password = Lastfm.Utilities.md5(password);
		}
		
		private static void doHelp()
		{
			Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			string[] messages = new string[] {
				"GLogScrobbler " + v,
				"A Last.fm log scrobbler for GNOME",
				"Copyright © 2009 Amr Hassan",
				"",
				"Usage:",
				"  glogscrobbler auth username password",
				"  glogscrobbler scrobble [mounnt_point]",
				"",
				"Commands:",
				"  auth: Authenticate your account with a valied Last.fm username and password.",
				"  scrobble: Submit a device log to Last.fm. A mount point can be provided or left blank for guessing."
			};
			
			foreach (string msg in messages)
				Console.WriteLine(msg);
		}
		
		private static void scrobble(string path)
		{
			SettingsProxy.AddRecentMountpoint(path);
			
			Core.ScrobblesLog log = new GLogScrobbler.Core.ScrobblesLog(path);
			Console.WriteLine("Scrobbling " + log.PlayedTracks.Count + " tracks from " + log.MountPoint + ".");
			
			// Create the Connection object
			Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Connection connection = new Connection("glo", v.ToString(), SettingsProxy.Username,
			                            SettingsProxy.Password);
			
			print("Initializing connection...");
			connection.Initialize();
			
			int i = 1;
			foreach(Entry track in log.PlayedTracks)
			{
				print("Scrobbling (" + i++ + " of " + log.PlayedTracks.Count + "): " + track);
				
				connection.Scrobble(track);
			}
						
			print("All tracks were submitted successfully.");
			
			try {
				log.RemoveFromDevice();
			} catch
			{
				print ("The log however could not be removed from your device for some unknown reason.");
			}
		}
				
		private static void print(object obj)
		{
			Console.WriteLine(obj);
		}
	}
}
