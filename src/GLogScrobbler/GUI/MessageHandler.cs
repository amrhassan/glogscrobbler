// ExceptionHandler.cs
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
	
	
	public static class MessageHandler
	{
		public static void ShowError(Window parent, string title, string description)
		{
			MessageDialog dialog = new MessageDialog(parent, DialogFlags.DestroyWithParent,
			                                         MessageType.Error, ButtonsType.Ok, "");
			dialog.Markup = description;
			dialog.Title = title;
			
			dialog.Run();
			dialog.Destroy();
		}
		
		public static void ShowInfo(Window parent, string title, string description)
		{
			MessageDialog dialog = new MessageDialog(parent, DialogFlags.DestroyWithParent,
			                                         MessageType.Info, ButtonsType.Ok, "");
			dialog.Text = description;
			dialog.Title = title;
						
			dialog.Run();
			dialog.Destroy();
		}
		
		public static void ShowException(Window parent, string description, Exception exception)
		{
			MessageHandler.ShowError(parent, exception.Message, description + "\n\n" + exception.ToString());
		}
	}
}
