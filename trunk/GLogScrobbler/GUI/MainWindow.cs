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
using GLogScrobbler.Core;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Web;

namespace GLogScrobbler.GUI
{
	public partial class MainWindow: Gtk.Window
	{
		ScrobblesLog scrobblesLog;
		ListStore treeModel;
		DoubleMapping<Lastfm.Scrobbling.Entry, Gtk.TreeIter> mapping;
		List<TreeIter> treeIters;
		Thread scrobblerThread;
		bool scrobblerThreadAbortRequested;
		Thread artThread;
		bool artThreadAbortRequested;
		log4net.ILog log;
		
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();
			
			log = log4net.LogManager.GetLogger(this.ToString());
			
			// Set the exception handler
			GLib.ExceptionManager.UnhandledException += this.OnUnhandledException;
			
			initList();
			loadSettings();
			update();
		}
		
		private void OnUnhandledException(GLib.UnhandledExceptionArgs args)
		{
			Gdk.Threads.Enter();
			MessageHandler.ShowException(this, "An unhandled exception has occured.", (Exception)args.ExceptionObject);
			Gdk.Threads.Leave();
			
			args.ExitApplication = false;
		}
		
		private void loadSettings()
		{
			log.Info("Loading settings");
			
			NewestFirstAction.Active = SettingsProxy.NewestFirst;
			ShowArtAction.Active = SettingsProxy.ShowArt;
		}
		
		private void initList()
		{
			log.Info("Initializing list");
			
			CellRendererPixbuf image = new CellRendererPixbuf();
			CellRendererText text = new CellRendererText();
		
			TreeViewColumn column = new TreeViewColumn();
			TracksTreeView.AppendColumn(column);
			
			column.PackStart(image, false);
			column.PackStart(text, true);
			
			column.AddAttribute(image, "pixbuf", 0);
			column.AddAttribute(text, "markup", 1);
			
			scrobblesLog = null;
			mapping = null;
			treeIters = null;
		}
				
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			QuitAction.Activate();
			a.RetVal = true;
		}

		protected virtual void OnOpenActionActivated (object sender, System.EventArgs e)
		{
			log.Debug("OpenAction activated");
			
			string[] mountPoints = DeviceDetector.GetDevicesWithLogs();
			string mountPoint = "";
			
			if (mountPoints.Length == 0)
			{
				ChooseDeviceDialog dialog = new ChooseDeviceDialog(this);
				ResponseType response = (ResponseType)dialog.Run();
				dialog.Destroy();
				
				if (response == ResponseType.Ok)
					mountPoint = dialog.GetSelectedPath();
				else
					return;
			}
			else if (mountPoints.Length == 1)
			{
				mountPoint = mountPoints[0];
			}
			else if(mountPoints.Length > 1)
			{
				ChooseDeviceDialog dialog = new 
					ChooseDeviceDialog(this, mountPoints);
				if ((ResponseType)dialog.Run() == ResponseType.Ok)
					mountPoint = dialog.GetSelectedPath();
				else
					return;
			}
						
			// log	
			log.Info("Loading device at " + mountPoint);
			
			// Add to history
			SettingsProxy.AddRecentMountpoint(mountPoint);
			
			scrobblesLog = new ScrobblesLog(mountPoint);
			log.Debug("NEW LOG");
			update();
		}
		
		private void update()
		{
			log.Info("Updating UI");
			
			statusLabel.Text = "No device was loaded.";
			playedLabel.Text = "";
			
			if (scrobblesLog == null)
			{
				SubmitAction.Sensitive = false;
				clearAction.Sensitive = false;
				OpenAction.Sensitive = true;
				
				return;
			}
			
			fillList();
			
			statusLabel.Text = scrobblesLog.DeviceString + " at " + scrobblesLog.MountPoint;
			playedLabel.Text = scrobblesLog.PlayedTracks.Count + " tracks";
			
			SubmitAction.Sensitive = true;
			clearAction.Sensitive = true;
			OpenAction.Sensitive = true;
		}
		
		private void fillList()
		{
			log.Info("Filling list");
			
			treeModel = new ListStore(typeof(Gdk.Pixbuf), typeof(string));
			mapping = new DoubleMapping<Lastfm.Scrobbling.Entry,TreeIter>();
			treeIters = new List<TreeIter>();
			
			// temp cover
			// Gdk.Pixbuf noCover = new Gdk.Pixbuf("/home/amr/Dropbox/projects/GLogScrobbler/trunk/GLogScrobbler/resources");
			Gdk.Pixbuf noCover = Gdk.Pixbuf.LoadFromResource("album.png");
			noCover = PixbufUtils.resize(noCover, 50);
			
			// The tracks from the log
			List<Lastfm.Scrobbling.Entry> tracks = scrobblesLog.PlayedTracks;
			
			// Reverse the list if it's NewestFirst
			if (SettingsProxy.NewestFirst)
				tracks.Reverse();

			// Work the magic on each track
			foreach(Lastfm.Scrobbling.Entry track in tracks)
			{
				string markup = "";
				markup += "<span weight=\"bold\">" + HttpUtility.HtmlEncode(track.Title) + "</span>\n";
				markup += "<span>by <span>" + HttpUtility.HtmlEncode(track.Artist) + "</span></span>";
				if (track.Album != "")
					markup += "\n<span size=\"small\">from " + HttpUtility.HtmlEncode(track.Album) + "</span>";
				markup += "\n<span size=\"small\" color=\"#6F6F6F\">" + 
					RelativeTime.GetTimeString(track.TimeStarted) + "</span>";
				
				TreeIter iter = treeModel.Append();
				
				// Show default art if enabled
				if (SettingsProxy.ShowArt)
					treeModel.SetValue(iter, 0, noCover);

				// Map the Entry to a TreeIter
				mapping.AddMapping(iter, track);
				treeIters.Add(iter);
				
				// set the markup
				treeModel.SetValue(iter, 1, markup);
			}

			TracksTreeView.Model = treeModel;
			
			TracksTreeView.Columns[0].CellRenderers[0].Visible = SettingsProxy.ShowArt;
			
			// Fetch art
			if (SettingsProxy.ShowArt)
			{								
				artThread = new Thread(this.fetchArt);
				artThreadAbortRequested = false;
				artThread.Start();
			}
		}
		
		private void fetchArt()
		{
			foreach (Lastfm.Scrobbling.Entry track in scrobblesLog.PlayedTracks)
			{
				if (artThreadAbortRequested)
					return;
				
				log.Info("Fetching art for " + track);
				
				Art.AlbumArt art = new Art.AlbumArt(track.Artist, track.Album);
				
				string filePath;
				
				try {
					filePath = art.GetPath();
				}
				catch {
					log.Info("No art for " + track);
					continue;
				}
				
				Gdk.Threads.Enter();
				Gdk.Pixbuf p = PixbufUtils.resize(new Gdk.Pixbuf(filePath), 65);
				
				// Check that the track is still mapped to a TreeIter first
				if (mapping.ContainsKey(track))
					treeModel.SetValue(mapping.Map(track), 0, p);
				
				Gdk.Threads.Leave();
			}
		}

		protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
		{
			log.Debug("QuitAction activated");
			
			if (artThread != null && artThread.IsAlive)
			{
				artThreadAbortRequested = true;
				artThread.Join();
			}
			
			if (scrobblerThread != null && scrobblerThread.IsAlive)
			{
				scrobblerThreadAbortRequested = true;
				scrobblerThread.Join();
			}
			
			Application.Quit ();
		}

		protected virtual void OnLastFmAccountActionActivated (object sender, System.EventArgs e)
		{
			LastfmAccountDialog dialog = new LastfmAccountDialog(this);
			dialog.Run();
		}

		protected virtual void OnSubmitActionActivated (object sender, System.EventArgs e)
		{
			log.Debug("SubmitAction activated");
			
			if (SettingsProxy.Username.Length == 0 || SettingsProxy.Password.Length == 0)
			{
				MessageHandler.ShowError(this, "Last.fm credentials unset",
				                         "Please set your Last.fm account username " +
				                         "and password first.");
				
				LastFmAccountAction.Activate();
				return;
			}
			
			OpenAction.Sensitive = false;
			SubmitAction.Sensitive = false;
			clearAction.Sensitive = false;
			ViewAction.Sensitive = false;
			LastFmAccountAction.Sensitive = false;

			log.Info("Starting scrobbling");
			scrobblerThread = new Thread(new ThreadStart(this.scrobble));
			scrobblerThreadAbortRequested = false;
			scrobblerThread.Start();
		}
		
		void updateScrobblerProgressBar(int progress, int max, string message)
		{
			double fraction = Convert.ToDouble(progress)/Convert.ToDouble(max);
			if (fraction >=0 && fraction <= 1.0)
				scrobblingProgressBar.Fraction = fraction;
			scrobblingProgressBar.Text = message + "...";
		}
		
		void scrobble()
		{
			// Create the Connection object
			Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Connection connection = new Connection("glo", v.ToString(), SettingsProxy.Username,
			                            SettingsProxy.Password);

			// Update the progress bar
			Gdk.Threads.Enter();
			progressBox.Visible = true;
			updateScrobblerProgressBar(0, 0, "Initializing");
			Gdk.Threads.Leave();
			
			// Try to intialize the connection
			try {
				connection.Initialize();
			}
			catch (Exception e) {
				Gdk.Threads.Enter();
				MessageHandler.ShowException(this, "An unexpected error has occured.\nPlease try again in a few minutes.", e);
				Gdk.Threads.Leave();
				
				progressBox.Visible = false;
				update();
				
				return;
			}
			
			// Start submitting
			int progress = 0;
			int max = scrobblesLog.PlayedTracks.Count;
			foreach(Lastfm.Scrobbling.Entry track in scrobblesLog.PlayedTracks)
			{
				if (scrobblerThreadAbortRequested)
					return;
				
				Gdk.Threads.Enter();
				updateScrobblerProgressBar(++progress, max, "Scrobbling " + track.Artist + " - " + track.Title);
				Gdk.Threads.Leave();
			
				try
				{
					log.Info("Scrobbling " + track.Artist + " - " + track.TimeStarted);
					connection.Scrobble(track);
				} catch (Exception e) {
					Gdk.Threads.Enter();
					MessageHandler.ShowException(this, "An unexpected error has occured.\nPlease try again in a few minutes.", e);
					Gdk.Threads.Leave();
					progressBox.Visible = false;
					update();
				
					return;
				}		
			}
			
			if (SettingsProxy.DeleteLog)
				removeLog();
		
			Gdk.Threads.Enter();
			progressBox.Visible = false;
			ViewAction.Sensitive = true;
			LastFmAccountAction.Sensitive = true;
			treeModel.Clear();
			scrobblesLog = null;
			update();
			Gdk.Threads.Leave();
		}
		
		protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
		{
			Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			
			AboutDialog dialog = new AboutDialog();
			dialog.SkipPagerHint = true;
			dialog.SkipTaskbarHint = true;
			dialog.Authors = new string[] {"Amr Hassan <amr.hassan@gmail.com>"};
			dialog.Comments = "A Last.fm log scrobbler for GNOME";
			dialog.Copyright = "Copyright © 2009 Amr Hassan";
			dialog.LogoIconName = "gtk-go-up";
			dialog.ProgramName = "GNOME Log Scrobbler " + 
				v.Major + "." + v.Minor + "." + v.Build;
			dialog.Title = "About GNOME Log Scrobbler";
			dialog.Run();
			dialog.Destroy();
		}

		protected virtual void OnNewestFirstActionToggled (object sender, System.EventArgs e)
		{
			SettingsProxy.NewestFirst = NewestFirstAction.Active;
			update();
		}

		protected virtual void OnShowArtActionToggled (object sender, System.EventArgs e)
		{
			SettingsProxy.ShowArt = ShowArtAction.Active;
			update();
		}

		protected virtual void OnCancelSubmitActionActivated (object sender, System.EventArgs e)
		{
			log.Info("Aborting scrobbling");
			
			scrobblerThreadAbortRequested = true;
			scrobblerThread.Join();
			
			progressBox.Visible = false;
			OpenAction.Sensitive = true;
			SubmitAction.Sensitive = true;
			clearAction.Sensitive = true;
			ViewAction.Sensitive = true;
			LastFmAccountAction.Sensitive = true;
		}

		protected virtual void OnClearActionActivated (object sender, System.EventArgs e)
		{
			MessageDialog msg = new MessageDialog(this, DialogFlags.DestroyWithParent | DialogFlags.Modal, MessageType.Warning,
			                                      ButtonsType.YesNo,
			                                      "This will clear the log from your device without submitting " +
			                                      "any of it to Last.fm, causing you to lose preciouse scrobbles.\n\n" +
			                                      "<b>Are you sure you wan to do this?</b>");
			
			ResponseType response = (ResponseType)msg.Run();
			msg.Destroy();
			
			if (response != ResponseType.Yes)
				return;
			
			removeLog();
			
			treeModel.Clear();
			scrobblesLog = null;
			mapping = null;
			treeIters = null;
			OpenAction.Sensitive = true;
			clearAction.Sensitive = false;
			update();
		}
		
		private void removeLog()
		{
			try {
					log.Info("Removing log from device");
					scrobblesLog.RemoveFromDevice();
				}
				catch {
					string message = "For some reason, the log file from " + scrobblesLog.MountPoint + 
						@" could not be removed.\nPlease remove it manually.
							\nHint: Try unmounting then mounting your device.";
					
				  Gdk.Threads.Enter();
					MessageHandler.ShowError(null, "Error Removing Log.", message);
				  Gdk.Threads.Leave();
				}
		}

		protected virtual void OnCancelButtonClicked (object sender, System.EventArgs e)
		{
			CancelSubmitAction.Activate();
		}

		protected virtual void OnTracksTreeViewRowActivated (object o, Gtk.RowActivatedArgs args)
		{
			int i = args.Path.Indices[0];
			
			TreeIter iter;
			
			if (i > treeIters.Count)
				return;
			
			iter = treeIters[i];
			
			if (mapping.ContainsKey(iter))
			{
				ViewTrackDialog dialog = new ViewTrackDialog(this, mapping.Map(iter));
				dialog.ShowAll();
			}
		}
	}
}