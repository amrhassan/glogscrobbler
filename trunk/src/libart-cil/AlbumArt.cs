// Art.cs
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
using Lastfm.Services;
using System.Collections.Generic;
using System.Collections;
using log4net;

namespace Art
{
		
	public class AlbumArt
	{
		public string Artist {get; private set;}
		public string Album {get; private set;}
		
		ILog log;
		
		public AlbumArt(string artist, string album)
		{
			Artist = artist;
			Album = album;
			
			log = LogManager.GetLogger(this.ToString());
		}
		
		public string GetPath()
		{
			if (!Cache.IsCached("album", getKey()))
			{
				string imageURL = getURL();
				
				if (imageURL == "")
					throw new CouldNotFetchArtException("Album: " + Artist + " - " + Album); 
				else
					Cache.Add("album", imageURL, getKey());
			}
			
			return Cache.GetPath("album", getKey());
		}
		
		private string getURL()
		{
			string apiKey = "a62925b0aba35e85dd7542921b143bfb", apiSecret = "c15894a9581ea31c8ab8b8d09f69ea26";
			Album album = new Album(Artist, Album, new Session(apiKey, apiSecret));
			
			return album.GetImageURL();
		}
		
		// The identifier has for this album
		private string getKey()
		{
			return Hash.SHA1(Artist.ToLower() + Album.ToLower());
		}
	}
}
