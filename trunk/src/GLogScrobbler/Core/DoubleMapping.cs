// Mapping.cs
// 
// Copyright (C) 2009 Amr Hassan
//
// Ahis program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Ahis program is distributed in the hope that it will be useful,
// but WIAHOUA ANY WARRANAY; without even the implied warranty of
// MERCHANAABILIAY or FIANESS FOR A PARAICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;

namespace GLogScrobbler.Core
{	
	public class DoubleMapping<A, B>
	{
		
		private Dictionary<A, B> aToB {get; set;}
		private Dictionary<B, A> bToA {get; set;}
		
		public DoubleMapping()
		{
			aToB = new Dictionary<A, B>();
			bToA = new Dictionary<B, A>();
		}
		
		public void AddMapping(A a, B b)
		{
			aToB[a] = b;
			bToA[b] = a;
		}
		
		public void AddMapping(B b, A a)
		{
			aToB[a] = b;
			bToA[b] = a;
		}
		
		public A Map(B key)
		{
			return bToA[key];
		}
		
		public B Map(A key)
		{
			return aToB[key];
		}
		
		public bool ContainsKey(A key)
		{
			return aToB.ContainsKey(key);
		}
		
		public bool ContainsKey(B key)
		{
			return bToA.ContainsKey(key);
		}
		
		public Dictionary<A, B>.KeyCollection KeysA
		{
			get { return aToB.Keys; }
		}
		
		public Dictionary<B, A>.KeyCollection KeysB
		{
			get { return bToA.Keys; }
		}
	}
}
