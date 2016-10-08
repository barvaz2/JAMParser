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

using System;

namespace NBidi
{
    /// <summary>
    /// A set of common Unicode characters, including BiDi control characters, and some Arabic letters used mainly for testing purposes.
    /// </summary>
	public static class BidiChars
	{
		#region Bidi Control Characters
		/// <summary>
		/// Right-to-Left Mark
		/// </summary>
		public const char RLM = '\u200F';
		
		/// <summary>
		/// Start of Right-to-Left Embedding
		/// </summary>
		public const char RLE = '\u202B';
		
		/// <summary>
		/// Start of Right-to-Left Override
		/// </summary>
		public const char RLO = '\u202E';
	
		/// <summary>
		/// Left-to-Right Mark
		/// </summary>
		public const char LRM = '\u200E';
		
		/// <summary>
		/// Start of Left-to-Right Embedding
		/// </summary>
		public const char LRE = '\u202A';
		
		/// <summary>
		/// Start of Left-to-Right Override
		/// </summary>
		public const char LRO = '\u202D';
		
		/// <summary>
		/// Pop Directional Formatting
		/// </summary>
		public const char PDF = '\u202C';
		#endregion
		
		/// <summary>
		/// A Dummy Character Indicating None.
		/// </summary>
		public const char NotAChar = '\uFFFF';
		
		#region Arabic Characters
		/// <summary>
		/// Arabic Letter Lam
		/// </summary>
		public const char ARABIC_LAM = '\u0644';
		
		/// <summary>
		/// Arabic Letter Alef With Madda Above
		/// </summary>
		public const char ARABIC_ALEF_MADDA_ABOVE = '\u0622';
		
		/// <summary>
		/// Arabic Letter Alef With Hamza Above
		/// </summary>
		public const char ARABIC_ALEF_HAMZA_ABOVE = '\u0623';
		
		/// <summary>
		/// Arabic Letter Alef With Hamza Below
		/// </summary>
		public const char ARABIC_ALEF_HAMZA_BELOW = '\u0625';
		
		/// <summary>
		/// Arabic Letter Alef
		/// </summary>
		public const char ARABIC_ALEF = '\u0627';

		/// <summary>
		/// Arabic Ligature Lam With Alef With Madda Above Isolated Form
		/// </summary>
		public const char ARABIC_LAM_ALEF_MADDA_ABOVE_ISOLATED = '\uFEF5';
		
		/// <summary>
		/// Arabic Ligature Lam With Alef With Madda Above Final Form
		/// </summary>
		public const char ARABIC_LAM_ALEF_MADDA_ABOVE_FINAL = '\uFEF6';
	
		/// <summary>
		/// Arabic Ligature Lam With Alef With Hamza Above Isolated Form
		/// </summary>
		public const char ARABIC_LAM_ALEF_HAMZA_ABOVE_ISOLATED = '\uFEF7';
		
		/// <summary>
		/// Arabic Ligature Lam With Alef With Hamza Above Final Form
		/// </summary>
		public const char ARABIC_LAM_ALEF_HAMZA_ABOVE_FINAL = '\uFEF8';
	
		/// <summary>
		/// Arabic Ligature Lam With Alef With Hamza Below Isolated Form
		/// </summary>
		public const char ARABIC_LAM_ALEF_HAMZA_BELOW_ISOLATED = '\uFEF9';
		
		/// <summary>
		/// Arabic Ligature Lam With Alef With Hamza Below Final Form
		/// </summary>
		public const char ARABIC_LAM_ALEF_HAMZA_BELOW_FINAL = '\uFEFA';
		
		/// <summary>
		/// Arabic Ligature Lam With Alef Isolated Form
		/// </summary>
		public const char ARABIC_LAM_ALEF_ISOLATED = '\uFEFB';
		
		/// <summary>
		/// Arabic Ligature Lam With Alef Final Form
		/// </summary>
		public const char ARABIC_LAM_ALEF_FINAL = '\uFEFC';
		#endregion
	}
}
