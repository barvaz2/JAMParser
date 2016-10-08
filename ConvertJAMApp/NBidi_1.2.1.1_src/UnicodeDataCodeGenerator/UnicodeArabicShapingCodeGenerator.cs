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
using System.Text;
using System.Globalization;
using System.IO;

namespace NBidi
{
	public class UnicodeArabicShapingCodeGenerator
	{
		public static void GenerateCode(StreamWriter sw)
		{
            CodeGenerator.WriteLicenseTerms(sw);
			sw.WriteLine("using System;");
			sw.WriteLine("using System.Collections;");
			sw.WriteLine("namespace NBidi {");
			sw.WriteLine("\tpublic abstract class UnicodeArabicShapingResolver {");
			sw.WriteLine("\t\tstatic Hashtable charForms = new Hashtable();");
			sw.WriteLine("\t\tpublic static ArabicShapeJoiningType GetArabicShapeJoiningType(char c) {");
			int start_val = -1;
			int last_val = -1;
			string last_result = string.Empty;
			using (StreamReader sr = File.OpenText("ArabicShaping.txt"))
			{
				while (sr.Peek() >= 0)
				{
					string line = sr.ReadLine();
					int comment = line.IndexOf('#');
					if (comment == 0) continue;
					if (comment > 0)
						line = line.Substring(0, comment - 1);
					line = line.Trim();
					if (line == null || line == string.Empty) continue;
					
					string[] fields = line.Split(';');
					int char_val = int.Parse(fields[0], NumberStyles.HexNumber);
					if (char_val != last_val + 1 || last_result != fields[2])
					{
						if (start_val != -1)
						{
							if (last_val > start_val)
								sw.WriteLine("\t\t\tif (c >= '\\u{0:X4}' && c <= '\\u{1:X4}') return ArabicShapeJoiningType.{2};",
								             start_val, last_val, last_result.Trim());
							else if (last_val == start_val)
								sw.WriteLine("\t\t\tif (c == '\\u{0:X4}') return ArabicShapeJoiningType.{1};",
								             last_val, last_result.Trim());
						}
						start_val = char_val;
					}
					
					last_result = fields[2];
					last_val = char_val;
				}
			}
			sw.WriteLine("\t\t\tUnicodeGeneralCategory ugc = UnicodeCharacterDataResolver.GetUnicodeGeneralCategory(c);");
			sw.WriteLine("\t\t\tif (ugc == UnicodeGeneralCategory.Mn ||");
			sw.WriteLine("\t\t\t    ugc == UnicodeGeneralCategory.Me ||");
			sw.WriteLine("\t\t\t    ugc == UnicodeGeneralCategory.Cf)");
			sw.WriteLine("\t\t\t\treturn ArabicShapeJoiningType.T;");
			sw.WriteLine("\t\t\treturn ArabicShapeJoiningType.U;");
			sw.WriteLine("\t\t}"); // method
			sw.WriteLine();
			sw.WriteLine("\t\tpublic static char GetArabicCharacterByLetterForm(char ch, LetterForm form) {");
			sw.WriteLine("\t\t\tint key = ((int)ch) | ((int)form) << 16;");
			sw.WriteLine("\t\t\tif (charForms.ContainsKey(key))");
			sw.WriteLine("\t\t\t\treturn (char)charForms[key];");
			sw.WriteLine("\t\t\treturn ch;");
			sw.WriteLine("\t\t}"); // method
			sw.WriteLine();
			sw.WriteLine("\t\tstatic UnicodeArabicShapingResolver() {");
			using (StreamReader sr = File.OpenText("UnicodeData.txt"))
			{
				while (sr.Peek() >= 0)
				{
					string line = sr.ReadLine();
					int comment = line.IndexOf('#');
					if (comment > 0)
						line = line.Substring(0, comment - 1);
					line = line.Trim();
					if (line == null || line == string.Empty) continue;
					
					if (comment != 0)
					{
						string[] fields = line.Split(';');
						string charStr = fields[0];
						string[] decomposition = fields[5].Split(' ');
						//string key = string.Empty;
						int key;
						if (decomposition.Length == 2)
						{
							int charNum = int.Parse(decomposition[1], NumberStyles.HexNumber);
							key = charNum;
							switch (decomposition[0])
							{
								case "<isolated>": key |= (int)(LetterForm.Isolated) << 16; break;
								case "<final>": key |= (int)(LetterForm.Final) << 16; break;
								case "<initial>": key |= (int)(LetterForm.Initial) << 16; break;
								case "<medial>": key |= (int)(LetterForm.Medial) << 16; break;
								default: key = -1; break;
							}
							if (key != -1)
								sw.WriteLine("\t\t\tcharForms[0x{0:X5}] = '\\u{1}';", key, charStr);
						}
					}
				}
			}		
			sw.WriteLine("\t\t}"); // cctor
			sw.WriteLine("\t}"); // class
			sw.WriteLine("}"); // namespace
			
		}
	}
}