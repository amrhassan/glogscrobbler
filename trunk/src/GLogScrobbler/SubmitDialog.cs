// SubmitDialog.cs
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
using Gtk;
using Lastfm.Scrobbling;
using System.Threading;

namespace GLogScrobbler
{
	public partial class SubmitDialog : Gtk.Dialog
	{
		ScrobblesLog log;
		Thread thread;
		
		public SubmitDialog(Window parent, ScrobblesLog log)
			:base("", parent, DialogFlags.DestroyWithParent)
		{
			this.Build();
			
			this.log = log;
			
			thread = new Thread(new ThreadStart(scrobble));
			thread.Start();
		}
		
		private void updateStatus(string status)
		{
			progressBar.Text = status +"...";
		}
		
		private void updateProgressBar(int progress, int max)
		{
			progressBar.Fraction = (Convert.ToDouble(progress) / Convert.ToDouble(max));
		}
		
		private void scrobble()
		{
			Connection connection = new Connection("glo", "0.1.1",
			                                       SettingsProxy.Username, 
			                                       SettingsProxy.Password);
			
			int progress = 0;
			int max = log.PlayedCount;
			foreach(PlayedTrack track in log.PlayedTracks)
			{
				if (track.Mode == ScrobbleMode.Played)
				{
					Gdk.Threads.Enter();
					updateStatus("Scrobbling " + track.Artist + " - " + track.Title);
					Gdk.Threads.Leave();
					connection.Scrobble(track);
					Gdk.Threads.Enter();
					updateProgressBar(++progress, max);
					Gdk.Threads.Leave();
				}
			}
			
			Gdk.Threads.Enter();
			this.Respond(ResponseType.Ok);
			Gdk.Threads.Leave();
		}

		protected virtual void OnResponse (object o, Gtk.ResponseArgs args)
		{
			if (args.ResponseId == ResponseType.Cancel)
				thread.Abort();
			
			this.Destroy();
		}
	}
}
