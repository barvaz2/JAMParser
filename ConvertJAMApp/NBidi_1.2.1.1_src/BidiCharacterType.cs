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
    /// Types of BiDi characters (Table 4 in the Unicode BiDi algorithm).
    /// </summary>
	public enum BidiCharacterType {
		#region Strong Types
		/// <summary>
		/// Left-to-Right
		/// </summary>
		/// <example>
		/// LRM, most alphabetic, syllabic, Han ideographs, non-European or non-Arabic digits, ...
		/// </example>
		/// <remarks>Strong Type</remarks>
		L,
		/// <summary>
		/// Left-to-Right Embedding
		/// </summary>
		/// <example>
		/// LRE
		/// </example>
		/// <remarks>Strong Type</remarks>
		LRE,
		/// <summary>
		/// Left-to-Right Override
		/// </summary>
		/// <example>
		/// LRO
		/// </example>
		/// <remarks>Strong Type</remarks>
		LRO,
		/// <summary>
		/// Right-to-Left
		/// </summary>
		/// <example>
		/// RLM, Hebrew alphabet, and related punctuation
		/// </example>
		/// <remarks>Strong Type</remarks>
		R,
		/// <summary>
		/// Right-to-Left Arabic
		/// </summary>
		/// <example>
		/// Arabic, Thaana, and Syriac alphabets, most punctuation specific to those scripts, ...
		/// </example>
		/// <remarks>Strong Type</remarks>
		AL,
		/// <summary>
		/// Right-to-Left Embedding
		/// </summary>
		/// <example>
		/// RLE
		/// </example>
		/// <remarks>Strong Type</remarks>
		RLE,
		/// <summary>
		/// Right-to-Left Override
		/// </summary>
		/// <example>
		/// RLO
		/// </example>
		/// <remarks>Strong Type</remarks>
		RLO,
		#endregion
		#region Weak Types
		/// <summary>
		/// Pop Directional Format
		/// </summary>
		/// <example>
		/// PDF
		/// </example>
		/// <remarks>Weak Type</remarks>
		PDF,
		/// <summary>
		/// European Number
		/// </summary>
		/// <example>
		/// European digits, Eastern Arabic-Indic digits, ...
		/// </example>
		/// <remarks>Weak Type</remarks>
		EN,
		/// <summary>
		/// European Number Separator
		/// </summary>
		/// <example>
		/// Plus sign, minus sign
		/// </example>
		/// <remarks>Weak Type</remarks>
		ES,
		/// <summary>
		/// European Number Terminator
		/// </summary>
		/// <example>
		/// Degree sign, currency symbols, ...
		/// </example>
		/// <remarks>Weak Type</remarks>
		ET,
		/// <summary>
		/// Arabic Number
		/// </summary>
		/// <example>
		/// Arabic-Indic digits, Arabic decimal and thousands separators, ...
		/// </example>
		/// <remarks>Weak Type</remarks>
		AN,
		/// <summary>
		/// Common Number Separator
		/// </summary>
		/// <example>
		/// Colon, comma, full stop (period), No-break space, ...
		/// </example>
		/// <remarks>Weak Type</remarks>
		CS,
		/// <summary>
		/// Nonspacing Mark
		/// </summary>
		/// <example>
		/// Characters marked Mn (Nonspacing_Mark) and Me (Enclosing_Mark) in the Unicode Character Database
		/// </example>
		/// <remarks>Weak Type</remarks>
		NSM,
		/// <summary>
		/// Boundary Neutral
		/// </summary>
		/// <example>
		/// Most formatting and control characters, other than those explicitly given types above
		/// </example>
		/// <remarks>Weak Type</remarks>
		BN,
		#endregion
		#region Neutral Types
		/// <summary>
		/// Paragraph Separator
		/// </summary>
		/// <example>
		/// Paragraph separator, appropriate Newline Functions, higher-level protocol paragraph determination
		/// </example>
		/// <remarks>Neutral Type</remarks>
		B,
		/// <summary>
		/// Segment Separator
		/// </summary>
		/// <example>
		/// Tab
		/// </example>
		/// <remarks>Neutral Type</remarks>
		S,
		/// <summary>
		/// Whitespace
		/// </summary>
		/// <example>
		/// Space, figure space, line separator, form feed, General Punctuation spaces, ...
		/// </example>
		/// <remarks>Neutral Type</remarks>
		WS,
		/// <summary>
		/// Other Neutrals
		/// </summary>
		/// <example>
		/// All other characters, including OBJECT REPLACEMENT CHARACTER
		/// </example>
		/// <remarks>Neutral Type</remarks>
		ON
		#endregion
	}

}
