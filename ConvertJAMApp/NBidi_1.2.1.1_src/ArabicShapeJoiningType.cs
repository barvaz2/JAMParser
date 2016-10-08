// NBidi - a .Net implementation of the BIDI (Bi-Directional Text) algorithm.
// Copyright (C) 2007-2009 Itai Bar-Haim
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// ---
//
// Unicode Data Copyright:
// Copyright © 1991-2006 Unicode, Inc. All rights reserved. Distributed under the Terms of Use in http://www.unicode.org/copyright.html.

namespace NBidi
{
    /// <summary>
    /// Letter joining types of Arabic letters.
    /// </summary>
	public enum ArabicShapeJoiningType {
		///<summary>Right Joining</summary>
		R,
		
		///<summary>Left Joining</summary>
		L,
		
		///<summary>Dual Joining</summary>
		D,
		
		///<summary>Join Causing</summary>
		C,
		
		///<summary>Non Joining</summary>
		U,
		
		///<summary>Transparent</summary>
		T 
	}
}
