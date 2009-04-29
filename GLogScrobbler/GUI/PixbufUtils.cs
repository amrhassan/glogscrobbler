// PixbufUtils.cs
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
using Gdk;

namespace GLogScrobbler
{
		
	public class PixbufUtils
	{
		
		public static Pixbuf resize(Pixbuf original, int size)
		{
			double width = Convert.ToDouble(original.Width);
			double height = Convert.ToDouble(original.Height);
			
			if (width >= height)
				return original.ScaleSimple(Convert.ToInt32(size),
				                            Convert.ToInt32((height/width)*size), InterpType.Bilinear);
			else
				return original.ScaleSimple(Convert.ToInt32((width/height)*size), 
				                            Convert.ToInt32(size), InterpType.Bilinear);
		}
	}
}
