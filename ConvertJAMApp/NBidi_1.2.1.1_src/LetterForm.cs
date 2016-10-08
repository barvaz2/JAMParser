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
    /// The four different available letter presentation forms.
    /// </summary>
	public enum LetterForm
	{
        /// <summary>
        /// A presentation form of a letter that begins a sequence of connected letters.
        /// </summary>
		Initial,
        /// <summary>
        /// A presentation form of a letter that is connected to other letters on both sides.
        /// </summary>
		Medial,
        /// <summary>
        /// A presentation form of a letter that ends a sequence of connected letters.
        /// </summary>
        Final,
        /// <summary>
        /// A presentation form of a letter that is not connected to other letters on either sides.
        /// </summary>
        Isolated
	}
}
