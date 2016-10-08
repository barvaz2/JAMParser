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
    /// The type of Unicode character decomposition.
    /// </summary>
	public enum UnicodeDecompositionType {
		///<summary>A base form or no special variant.</summary>
		None,
		
		///<summary>A font variant (e.g. a blackletter form).</summary>
		Font,
		
		///<summary>A no-break version of a space or hyphen.</summary>
		NoBreak,
		
		///<summary>An initial presentation form (Arabic).</summary>
		Initial,
		
		///<summary>A medial presentation form (Arabic).</summary>
		Medial,
		
		///<summary>A final presentation form (Arabic).</summary>
		Final,
		
		///<summary>An isolated presentation form (Arabic).</summary>
		Isolated,
		
		///<summary>An encircled form.</summary>
		Circle,
		
		///<summary>A superscript form.</summary>
		Super,
		
		///<summary>A subscript form.</summary>
		Sub,
		
		///<summary>A vertical layout presentation form.</summary>
		Vertical,
		
		///<summary>A wide (or zenkaku) compatibility character.</summary>
		Wide,
		
		///<summary>A narrow (or hankaku) compatibility character.></summary>
		Narrow,
		
		///<summary>A small variant form (CNS compatibility).</summary>
		Small,
		
		///<summary>A CJK squared font variant.</summary>
		Square,
		
		///<summary>A vulgar fraction form.</summary>
		Fraction,
		
		///<summary>Otherwise unspecified compatibility character.</summary>
		Compat
	}
}
