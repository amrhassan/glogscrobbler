// DeviceDetector.cs
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
using System.Collections.Generic;

namespace GLogScrobbler.Core
{
	
	
	public static class DeviceDetector
	{
		
		public static string[] GetDevicesWithLogs()
		{
			// A list of all available devices
			List<string> devices = new List<string>(System.Environment.GetLogicalDrives());
			devices.AddRange(SettingsProxy.RecentMountPoints.Split(';'));
			
			// Filter them to ones with logs
			List<string> devicesWithLogs = new List<string>();
			
			foreach (string device in devices)
				if (PathHasLog(device) && !devicesWithLogs.Contains(device))
					devicesWithLogs.Add(device);
			
			return devicesWithLogs.ToArray();
		}
		
		public static bool PathHasLog(string path)
		{
			if (File.Exists(Path.Combine(path, ".scrobbler.log")))
				return true;
			else
				return false;
		}
	}
}
