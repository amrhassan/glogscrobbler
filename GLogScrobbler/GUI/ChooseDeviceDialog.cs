// ChooseDeviceDialog.cs
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
using System.Collections.Generic;
using GLogScrobbler.Core;

namespace GLogScrobbler
{
	public partial class ChooseDeviceDialog : Gtk.Dialog
	{
		private List<string> mountPoints;
		private ListStore model;
		
		public ChooseDeviceDialog(Window parent, string[] detectedDevices)
			:base("", parent, DialogFlags.DestroyWithParent)
		{
			this.Build();
			
			mountPoints = new List<string>(detectedDevices);
			initCombo();
			updateCombo();
		}
		
		public ChooseDeviceDialog(Window parent)
			:base("", parent, DialogFlags.DestroyWithParent)
		{
			Build();
			
			mountPoints = new List<string>();
			initCombo();
			updateCombo();
		}
		
		private void initCombo()
		{			
			CellRendererPixbuf pixbufCell = new CellRendererPixbuf();
			CellRendererText pathCell = new CellRendererText();
			
			devicesBox.PackStart(pixbufCell, false);
			devicesBox.PackStart(pathCell, true);

			devicesBox.AddAttribute(pixbufCell, "stock-id", 0);
			devicesBox.AddAttribute(pathCell, "text", 1);
		}
		
		private void updateCombo()
		{
			model = new ListStore(typeof(string), typeof(string));
			devicesBox.Model = model; 
			
			foreach(string path in mountPoints)
				model.AppendValues("gtk-directory", path);
			
			if (mountPoints.Count == 0)
				expander.Expanded = true;
			else
				devicesBox.Active = mountPoints.Count - 1;
		}
		
		public string GetSelectedPath()
		{
			TreeIter iter;
			devicesBox.GetActiveIter(out iter);
			
			return (string)model.GetValue(iter, 1);
		}

		protected virtual void OnResponse (object o, Gtk.ResponseArgs args)
		{
			this.Destroy();
		}

		protected virtual void OnDevicesBoxChanged (object sender, System.EventArgs e)
		{			
			if (devicesBox.Active >= 0)
				buttonOk.Sensitive = true;
			else
				buttonOk.Sensitive = false;
		}

		protected virtual void OnFileChooserCurrentFolderChanged (object sender, System.EventArgs e)
		{
			if (DeviceDetector.PathHasLog(fileChooser.Filename))
			{
				if (!mountPoints.Contains(fileChooser.Filename))
				{
					SettingsProxy.AddRecentMountpoint(fileChooser.Filename);
					mountPoints.Add(fileChooser.Filename);
					updateCombo();
				}
			}
		}
	}
}
