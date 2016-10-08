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
    public class UnicodeMirroringCodeGenerator
    {
        public static void GenerateCode(StreamWriter sw)
        {
            CodeGenerator.WriteLicenseTerms(sw);
            sw.WriteLine("namespace NBidi {");
            sw.WriteLine("\t/// <summary>");
            sw.WriteLine("\t/// An helper class that provides the mirrored alternatives for characters.");
            sw.WriteLine("\t/// </summary>");
            sw.WriteLine("\tpublic abstract class BidiCharacterMirrorResolver {");
            sw.WriteLine("\t\t/// <summary>");
            sw.WriteLine("\t\t/// Returns the corresponding mirrored character for the given character, if any. If no mirroring available, returns the given character.");
            sw.WriteLine("\t\t/// </summary>");
            sw.WriteLine("\t\t/// <param name=\"c\">A character to mirror.</param>");
            sw.WriteLine("\t\t/// <returns>The mirrored character, or the given character if no mirroring available.</returns>");
            sw.WriteLine("\t\tpublic static char GetBidiCharacterMirror(char c) {");
            sw.WriteLine("\t\t\tswitch (c) {");

            using (StreamReader sr = File.OpenText("BidiMirroring.txt"))
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
                        sw.WriteLine("\t\t\t\tcase '\\u{0}': return '\\u{1}';", fields[0].Trim(), fields[1].Trim());
                    }
                }
            }
            sw.WriteLine("\t\t\t\tdefault: return c;");
            sw.WriteLine("\t\t\t}"); // switch
            sw.WriteLine("\t\t}"); // method
            sw.WriteLine("\t}"); // class
            sw.WriteLine("}"); // namespace
        }
    }
}
