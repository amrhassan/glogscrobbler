// SettingsProxy.cs
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
using GConf;

namespace GLogScrobbler
{
	public static class SettingsProxy
	{
		static string APP_PATH = "/apps/glogscrobber/";
		
		public static object getValue(string name, object defaultValue)
		{
			try
			{
				Client client = new Client();
				return client.Get(APP_PATH + name);
			}
			catch
			{
				setValue(name, defaultValue);
				return defaultValue;
			}
		}
		
		public static void setValue(string name, object val)
		{
			Client client = new Client();
			client.Set(APP_PATH + name, val);
		}
		
		public static string Username
		{
			get {
				return (string)getValue("username", "");
			}
			set {
				setValue("username", value);
			}
		}
		
		public static string Password
		{
			get {
				return (string)getValue("password", "");
			}
			set {
				setValue("password", value);
			}
		}
		
		public static bool DeleteLog
		{
			get {
				return (bool)getValue("delete_log_after_successful_scrobbling", true);
			}
			set {
				setValue("delete_log_after_successful_scrobbling", value);
			}
		}
		
		public static string RecentMountPoints
		{
			get {
				return (string)getValue("recent_mountpoints", ";");
			}
			set {
				setValue("recent_mountpoints", value);
			}
		}
		
		public static bool NewestFirst
		{
			get {
				return (bool)getValue("show_newest_first", true);
			}
			set {
				setValue("show_newest_first", value);
			}
		}
		
		public static bool ShowArt
		{
			get {
				return (bool)getValue("show_art", true);
			}
			set {
				setValue("show_art", value);
			}
		}
		
		internal static string APIKey
		{
			get { return "a62925b0aba35e85dd7542921b143bfb"; }
		}
		
		internal static string APISecret
		{
			get { return "c15894a9581ea31c8ab8b8d09f69ea26"; }
		}
		
		public static void AddRecentMountpoint(string path)
		{
			if (! RecentMountPoints.Contains(path))
				RecentMountPoints += path + ";";
		}
	}
}
