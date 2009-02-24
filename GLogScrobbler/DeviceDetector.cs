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
using Gtk;

namespace GLogScrobbler
{
	
	
	public static class DeviceDetector
	{
		
		public static string GetMountPoint(Window parent)
		{
			List<string> devices = new List<string>(System.Environment.GetLogicalDrives());
			devices.AddRange(SettingsProxy.RecentMountPoints.Split(';'));
			
			List<string> devicesWithLogs = new List<string>();
			string mountPoint = "";
			
			foreach (string device in devices)
				if (PathHasLog(device) && !devicesWithLogs.Contains(device))
					devicesWithLogs.Add(device);
			
			if (devicesWithLogs.Count == 0)
			{
				ChooseDeviceDialog dialog = new ChooseDeviceDialog(parent);
				if ((ResponseType)dialog.Run() == ResponseType.Ok)
					mountPoint = dialog.GetSelectedPath();
			}
			else if (devicesWithLogs.Count == 1)
			{
				mountPoint = devicesWithLogs[0];
			}
			else if(devicesWithLogs.Count > 1)
			{
				ChooseDeviceDialog dialog = new ChooseDeviceDialog(parent, devicesWithLogs.ToArray());
				if ((ResponseType)dialog.Run() == ResponseType.Ok)
					mountPoint = dialog.GetSelectedPath();
			}
			
			if (mountPoint.Length > 0)
			{
				SettingsProxy.AddRecentMountpoint(mountPoint);
				return mountPoint;
			}
			else
			{
				return "";
			}
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
