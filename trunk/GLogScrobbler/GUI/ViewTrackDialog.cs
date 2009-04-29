// ViewTrackDialog.cs
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
using Art;
using Gtk;
using Gdk;
using System.Threading;
using System.Web;

namespace GLogScrobbler
{
	
	
	public partial class ViewTrackDialog : Gtk.Dialog
	{
		
		Thread thread;
		Lastfm.Scrobbling.Entry track;
		
		public ViewTrackDialog(Gtk.Window parent, Lastfm.Scrobbling.Entry track) : 
				base("", parent, Gtk.DialogFlags.DestroyWithParent)
		{
			this.Build();
			
			loadTrack(track);
		}
		
		private void loadTrack(Lastfm.Scrobbling.Entry track)
		{
			// set track field
			this.track = track;
			
			// set the description markup
			string markup = "";
			markup += "<span weight=\"ultrabold\" size=\"xx-large\">" + HttpUtility.HtmlEncode(track.Title) + "</span>\n";
			markup += "<span weight=\"bold\">by <span>" + HttpUtility.HtmlEncode(track.Artist) + "</span></span>";
			if (track.Album != "")
				markup += "\n<span>from " + HttpUtility.HtmlEncode(track.Album) + "</span>";
			markup += "\n\n<span>Played:\n" + 
				track.TimeStarted.ToLongDateString() + "\n" +
					track.TimeStarted.ToLongTimeString() +
					"</span>";
			
			markup += "\n\n";
			
			markup += "<span>Duration:\n" + track.Duration + "</span>";
			
			this.descriptionLabel.Markup = markup;
			
			// default art
			this.artImage.Pixbuf = PixbufUtils.resize(Gdk.Pixbuf.LoadFromResource("album.png"), 150);

			// set window title
			this.Title = track.Artist + " - " + track.Title;
			
			// fetch art on another thread
			thread = new Thread(fetchArt);
			thread.Start();
		}

		private void fetchArt()
		{
			AlbumArt art = new AlbumArt(track.Artist, track.Album);
						
			try
			{
				art.GetPath();
			}
			catch
			{
				return;
			}
			
			Gdk.Threads.Enter();
			artImage.Pixbuf = PixbufUtils.resize(new Gdk.Pixbuf(art.GetPath()), 150);
			Gdk.Threads.Leave();			
		}

		protected virtual void OnResponse (object o, Gtk.ResponseArgs args)
		{
			// open track's Last.fm page
			if ((int)args.ResponseId == 666)
			{
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				string url = 
					new Lastfm.Services.Track(track.Artist, track.Title, new Lastfm.Session("", "")).GetURL(Lastfm.Services.SiteLanguage.English);
				
				process.StartInfo.FileName = url;
				process.Start();
			}
			
			// destroy anyway
			this.Destroy();
		}
	}
}
