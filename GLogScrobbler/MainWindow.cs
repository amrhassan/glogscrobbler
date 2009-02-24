// MainWindow.cs
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

namespace GLogScrobbler
{
	public partial class MainWindow: Gtk.Window
	{
		ScrobblesLog log;
		ListStore treeModel;
		
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();
			
			initList();
			updateList();
		}
		
		private void initList()
		{
			CellRendererText artistCell = new CellRendererText();
			CellRendererText titleCell = new CellRendererText();
			CellRendererText albumCell = new CellRendererText();
			CellRendererText numberCell = new CellRendererText();
			CellRendererText durationCell = new CellRendererText();
			CellRendererText modeCell = new CellRendererText();
			CellRendererText timeCell = new CellRendererText();
			CellRendererText mbidCell = new CellRendererText();
			
			TracksTreeView.AppendColumn("Artist", artistCell);
			TracksTreeView.AppendColumn("Title", titleCell);
			TracksTreeView.AppendColumn("Album", albumCell);
			TracksTreeView.AppendColumn("#", numberCell);
			TracksTreeView.AppendColumn("Duration", durationCell);
			TracksTreeView.AppendColumn("Played", modeCell);
			TracksTreeView.AppendColumn("Time Started", timeCell);
			TracksTreeView.AppendColumn("MBID", mbidCell);
			
			TracksTreeView.Columns[0].AddAttribute(artistCell, "text", 0);
			TracksTreeView.Columns[1].AddAttribute(titleCell, "text", 1);
			TracksTreeView.Columns[2].AddAttribute(albumCell, "text", 2);
			TracksTreeView.Columns[3].AddAttribute(numberCell, "text", 3);
			TracksTreeView.Columns[4].AddAttribute(durationCell, "text", 4);
			TracksTreeView.Columns[5].AddAttribute(modeCell, "text", 5);
			TracksTreeView.Columns[6].AddAttribute(timeCell, "text", 6);
			TracksTreeView.Columns[7].AddAttribute(mbidCell, "text", 7);
			
			log = null;
			
		}
		
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected virtual void OnOpenActionActivated (object sender, System.EventArgs e)
		{
			string mountPoint = DeviceDetector.GetMountPoint(this);
			
			if (mountPoint == "")
				return;
				
			log = new ScrobblesLog(mountPoint);
			updateList();
		}
		
		private void updateList()
		{
			// Artist, Title, Album, TrackNumber, 
			// Duraion, Mode, TimeStarted, MBID
			treeModel = new ListStore(typeof(string), typeof(string),
			                          typeof(string), typeof(string),
			                          typeof(string), typeof(string),
			                          typeof(string), typeof(string));
			
			TracksTreeView.Model = treeModel;
			
			this.statusLabel.Text = "No device was loaded.";
			this.playedLabel.Text = "Played: 0";
			this.skippedLabel.Text = "Skipped: 0";
			
			if (log == null)
				return;
			
			foreach (PlayedTrack track in log.PlayedTracks)
			{
				string played = "No";
				if (track.Mode == ScrobbleMode.Played)
					played = "Yes";
				
				treeModel.AppendValues(track.Artist, track.Title,
				                       track.Album, track.Number.ToString(),
				                       track.Duration.ToString(), played,
				                       track.TimeStarted.ToString(), track.MBID);
			}
			
			statusLabel.Text = "Device: " + log.DeviceString + " at " + log.MountPoint;
			playedLabel.Text = "Played: " + log.PlayedCount.ToString();
			skippedLabel.Text = "Skipped: " + log.SkippedCount.ToString();
			
			SubmitAction.Sensitive = true;
		}

		protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
		{
			Application.Quit();
		}

		protected virtual void OnLastFmAccountActionActivated (object sender, System.EventArgs e)
		{
			LastfmAccountDialog dialog = new LastfmAccountDialog(this);
			dialog.Run();
		}

		protected virtual void OnSubmitActionActivated (object sender, System.EventArgs e)
		{
			if (SettingsProxy.Username.Length == 0 || SettingsProxy.Password.Length == 0)
			{
				MessageHandler.ShowError(this, "Last.fm credentials unset",
				                         "Please set your Last.fm account username and password first.");
				
				LastFmAccountAction.Activate();
				SubmitAction.Activate();
			}
			
			SubmitDialog dialog = new SubmitDialog(this, this.log);
			
			if ((ResponseType)dialog.Run() == ResponseType.Ok)
			{
				if (SettingsProxy.DeleteLog)
					log.RemoveFromDevice();
				
				treeModel.Clear();
				log = null;
				SubmitAction.Sensitive = false;
				updateList();
			}
		}

		protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
		{
			AboutDialog dialog = new AboutDialog();
			dialog.SkipPagerHint = true;
			dialog.SkipTaskbarHint = true;
			dialog.Authors = new string[] {"Amr Hassan <amr.hassan@gmail.com>"};
			dialog.Comments = "A Last.fm log scrobbler for GNOME";
			dialog.Copyright = "Copyright (c) 2009 Amr Hassan";
			dialog.LogoIconName = "gtk-go-up";
			dialog.ProgramName = "GNOME Log Scrobbler";
			// TODO: figure out how to get a pixbuf from a gtk.Image
			dialog.Run();
			dialog.Destroy();
		}
	}
}