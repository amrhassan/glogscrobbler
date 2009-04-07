// RelativeTime.cs
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

namespace GLogScrobbler.Core
{
	
	
	public class RelativeTime
	{
		
		public static string GetTimeString(DateTime dateTime)
		{
			DateTime now = DateTime.Now;
			
			// Less than an hour
			if (now.Subtract(dateTime).TotalMinutes < 60)
				return Convert.ToInt32(now.Subtract(dateTime).TotalMinutes) + " minutes ago";
			
			// An hour ago
			else if (Convert.ToInt32(now.Subtract(dateTime).TotalHours) == 1)
				return "1 hour ago";
			
			// Sometime in the last 12 hours
			else if (now.Subtract(dateTime).TotalHours < 12)
				return Convert.ToInt32(now.Subtract(dateTime).TotalHours).ToString() + " hours ago";
			
			// Sometime today
			else if (now.Subtract(dateTime).TotalDays == 0)
				return "Today at " + dateTime.ToShortTimeString();
			
			// Sometime Yesterday
			else if (Convert.ToInt32(now.Subtract(dateTime).TotalDays) == 1)
				return "Yesterday at " + dateTime.ToShortTimeString();
			
			// Sometime this week
			else if (now.Subtract(dateTime).TotalDays < 7)
				return Convert.ToInt32(now.Subtract(dateTime).TotalDays).ToString() + 
					" days ago at " + dateTime.ToShortTimeString();
			
			// Just the date then
			else
				return dateTime.ToString("MMMM dd") + " at " + dateTime.ToShortTimeString();
		}
	}
}
