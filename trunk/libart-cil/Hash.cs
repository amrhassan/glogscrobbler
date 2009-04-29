// Hash.cs
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
using System.Security.Cryptography;
using System.Text;

namespace Art
{
	
	
	internal class Hash
	{
		
		public static string MD5(string text)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(text);
			
			MD5CryptoServiceProvider c = new MD5CryptoServiceProvider();
			buffer = c.ComputeHash(buffer);
			
			StringBuilder builder = new StringBuilder();
			foreach(byte b in buffer)
				builder.Append(b.ToString("x2").ToLower());
			
			return builder.ToString();
		}
		
		public static string SHA1(string text)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(text);
			
			SHA1CryptoServiceProvider c = new SHA1CryptoServiceProvider();
			buffer = c.ComputeHash(buffer);
			
			StringBuilder builder = new StringBuilder();
			foreach(byte b in buffer)
				builder.Append(b.ToString("x2").ToLower());
			
			return builder.ToString();
		}
	}
}
