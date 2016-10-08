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
using System.IO;

namespace NBidi
{
	/// <summary>
	/// Description of Main.
	/// </summary>
	public class CodeGenerator
	{
		public static void Main()
		{
			using (StreamWriter sw = File.CreateText("BidiCharacterTypeResolver.generated.cs"))
			{
				UnicodeDataCodeGenerator.GenerateCode(sw);
			}
			
			using (StreamWriter sw = File.CreateText("BidiCharacterMirrorResolver.generated.cs"))
			{
				UnicodeMirroringCodeGenerator.GenerateCode(sw);
			}
			
			using (StreamWriter sw = File.CreateText("UnicodeArabicShapingResolver.generated.cs"))
			{
				UnicodeArabicShapingCodeGenerator.GenerateCode(sw);
			}			
		}

        internal static void WriteLicenseTerms(StreamWriter sw)
        {
            sw.WriteLine("// NBidi - a .Net implementation of the BIDI (Bi-Directional Text) algorithm.");
            sw.WriteLine("// Copyright (C) 2007-2009 Itai Bar-Haim");
            sw.WriteLine("// ");
            sw.WriteLine("// This library is free software; you can redistribute it and/or");
            sw.WriteLine("// modify it under the terms of the GNU Lesser General Public");
            sw.WriteLine("// License as published by the Free Software Foundation; either");
            sw.WriteLine("// version 2.1 of the License, or (at your option) any later version.");
            sw.WriteLine("// ");
            sw.WriteLine("// This library is distributed in the hope that it will be useful,");
            sw.WriteLine("// but WITHOUT ANY WARRANTY; without even the implied warranty of");
            sw.WriteLine("// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU");
            sw.WriteLine("// Lesser General Public License for more details.");
            sw.WriteLine("// ");
            sw.WriteLine("// You should have received a copy of the GNU Lesser General Public");
            sw.WriteLine("// License along with this library; if not, write to the Free Software");
            sw.WriteLine("// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA");
            sw.WriteLine("//");
            sw.WriteLine("// ---");
            sw.WriteLine("//");
            sw.WriteLine("// Unicode Data Copyright:");
            sw.WriteLine("// Copyright © 1991-2006 Unicode, Inc. All rights reserved. Distributed under the Terms of Use in http://www.unicode.org/copyright.html.");
            sw.WriteLine();
        }
	}
}
