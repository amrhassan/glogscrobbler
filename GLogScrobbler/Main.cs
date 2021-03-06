// Main.cs
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
using System.IO;

namespace GLogScrobbler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Configure logging
			log4net.Config.BasicConfigurator.Configure();
			
			if (args.Length > 0)
			{
				// CLI
				CLI.Main.Init(args);
			}
			else
			{
				// GUI
				Gdk.Threads.Init();
				Application.Init();
				GUI.MainWindow win = new GUI.MainWindow();
				win.Show ();
				Application.Run ();
			}
		}
	}
}