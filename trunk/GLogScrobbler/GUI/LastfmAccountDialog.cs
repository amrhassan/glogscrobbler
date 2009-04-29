// LastfmAccountDialog.cs
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

namespace GLogScrobbler
{
	
	
	public partial class LastfmAccountDialog : Gtk.Dialog
	{
		
		public LastfmAccountDialog(Gtk.Window parent)
			:base("", parent, DialogFlags.DestroyWithParent)
		{
			this.Build();
			
			usernameEntry.Text = SettingsProxy.Username;
		}
		
		public string GetUsername()
		{
			return usernameEntry.Text;
		}
		
		public string GetPasswordHash()
		{
			return Lastfm.Utilities.MD5(passwordEntry.Text);
		}

		protected virtual void OnResponse (object o, Gtk.ResponseArgs args)
		{
			if (args.ResponseId == ResponseType.Ok)
			{
				SettingsProxy.Username = GetUsername();
				SettingsProxy.Password = GetPasswordHash();
				
				MessageHandler.ShowInfo(this, "Credentials Updated",
				                        "Last.fm credentials were updated successfully.");
			}
			
			this.Destroy();
		}
	}
}
