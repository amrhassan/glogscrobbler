// ScrobblesLog.cs
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
using Lastfm.Scrobbling;
using System.Collections.Generic;
using System.IO;

namespace GLogScrobbler
{
	
	
	public class ScrobblesLog
	{
		public PlayedTrack[] PlayedTracks {get; private set;}
		public string DeviceString {get; private set;}
		public string MountPoint {get; private set;}
		
		public int PlayedCount
		{ get {
				int count = 0;
				foreach(PlayedTrack track in PlayedTracks)
					if (track.Mode == ScrobbleMode.Played)
						count += 1;
				
				return count;
			}
		}
		
		public int SkippedCount
		{ get {
				int count = 0;
				foreach(PlayedTrack track in PlayedTracks)
					if (track.Mode == ScrobbleMode.Skipped)
						count += 1;
				
				return count;
			}
		}
		
		public ScrobblesLog(string mountPoint)
		{
			MountPoint = mountPoint;
			StreamReader reader = new StreamReader(Path.Combine(MountPoint, ".scrobbler.log"), true);
			List<PlayedTrack> tracks = new List<PlayedTrack>();
			
			// Check the Log protocol version
			if (reader.ReadLine() != "#AUDIOSCROBBLER/1.1")
				throw new Exception("Unsupported Logscrobbler protocol. Please update your application.");
			
			// Get timezone
			DateTimeKind kind;
			string timezoneLine = reader.ReadLine();
			if (timezoneLine == "#TZ/UTC")
				kind = DateTimeKind.Utc;
			else
				kind = DateTimeKind.Local;
			
			// Get device string
			DeviceString = reader.ReadLine().Split('/')[1];
			
			// Read the tracks
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				string[] values = line.Split('\t');
				
				ScrobbleMode mode = ScrobbleMode.Played;
				if (values[5] == "L")
					mode = ScrobbleMode.Played;
				else if (values[5] == "S")
					mode = ScrobbleMode.Skipped;
				
				PlayedTrack track = new PlayedTrack(values[0], values[2], 
				                                    Lastfm.Utilities.TimestampToDateTime(long.Parse(values[6]), kind),
				                                    PlaybackSource.User, new TimeSpan(0, 0, int.Parse(values[4])),
				                                    mode, values[1], int.Parse(values[3]),
				                                    values[7]);
				
				tracks.Add(track);
			}
			
			PlayedTracks = tracks.ToArray();
		}
		
		public void RemoveFromDevice()
		{
			// Logs are removed from the device into a ~/.glogscrobbler/old_logs for
			// backup purposes.
			
			string cacheDir = 
				Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".glogscrobbler/old_logs");
			
			if (!Directory.Exists(cacheDir))
				Directory.CreateDirectory(cacheDir);
			
			try
			{
				File.Move(Path.Combine(MountPoint, ".scrobbler.log"),
				          Path.Combine(cacheDir, Lastfm.Utilities.DateTimeToUTCTimestamp(DateTime.Now).ToString()));
			} catch {
				MessageHandler.ShowError(null, "Error Removing Log", 
				                         "For some reason, the log file from " + this.MountPoint + " could not be removed." +
				                         "\nPlease remove it manually.");
			}
		}
	}
}
