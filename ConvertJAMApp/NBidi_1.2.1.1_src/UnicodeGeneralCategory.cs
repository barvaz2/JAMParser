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
    /// Unicode character categories.
    /// </summary>
	public enum UnicodeGeneralCategory {
		///<summary>Letter, Uppercase</summary>
		Lu,
		///<summary>Letter, Lowercase</summary>
		Ll,
		///<summary>Letter, Titlecase</summary>
		Lt,
		///<summary>Letter, Modifier</summary>
		Lm,
		///<summary>Letter, Other</summary>
		Lo,
		///<summary>Mark, Nonspacing</summary>
		Mn,
		///<summary>Mark, Spacing Combining</summary>
		Mc,
		///<summary>Mark, Enclosing</summary>
		Me,
		///<summary>Number, Decimal Digit</summary>
		Nd,
		///<summary>Number, Letter</summary>
		Nl,
		///<summary>Number, Other</summary>
		No,
		///<summary>Punctuation, Connector</summary>
		Pc,
		///<summary>Punctuation, Dash</summary>
		Pd,
		///<summary>Punctuation, Open</summary>
		Ps,
		///<summary>Punctuation, Close</summary>
		Pe,
		///<summary>Punctuation, Initial quote (may behave like Ps or Pe depending on usage)</summary>
		Pi,
		///<summary>Punctuation, Final quote (may behave like Ps or Pe depending on usage)</summary>
		Pf,
		///<summary>Punctuation, Other</summary>
		Po,
		///<summary>Symbol, Math</summary>
		Sm,
		///<summary>Symbol, Currency</summary>
		Sc,
		///<summary>Symbol, Modifier</summary>
		Sk,
		///<summary>Symbol, Other</summary>
		So,
		///<summary>Separator, Space</summary>
		Zs,
		///<summary>Separator, Line</summary>
		Zl,
		///<summary>Separator, Paragraph</summary>
		Zp,
		///<summary>Other, Control</summary>
		Cc,
		///<summary>Other, Format</summary>
		Cf,
		///<summary>Other, Surrogate</summary>
		Cs,
		///<summary>Other, Private Use</summary>
		Co,
		///<summary>Other, Not Assigned (no characters in the file have this property)</summary>
		Cn
	}
}
