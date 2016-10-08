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
//
// Some tests were written by Yasser Markam.

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace NBidi
{
    [TestFixture]
    public class NBiDiArabicShapingTest
    {
        [Test]
        public void Test01ArabicShapingWithTashkeel()
        {
            //Tashkeel characters are a set of glyphs that controls how a word is pronounced, but not part of the word
            //Tashkeel characters appear above or below a character and should not affect joining (irrelevant to joining)
            //Tashkeel characters are:
            //Fathatan \u064B
            //Dammatan \u064c
            //Kasratan \u064D
            //Fatha \u064E
            //Damma \u064F
            //Kasra \u0650
            //Shadda \u0651
            //Sukun \u0652
            string orig = "\u0639\u0631\u0628\u0650\u064A";
            string res = NBidi.LogicalToVisual(orig);
            string exp = "\uFEF2\u0650\uFE91\uFEAE\uFECB";
            Assert.AreEqual(exp.ToCharArray(), res.ToCharArray(),
                            "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}\nexpected as chars: {3}",
                            orig, AsCharArray(orig), AsCharArray(res), AsCharArray(exp));
        }

        //It is required that squences of Lam and Alef characters, converted to a single presentation character Lamalef
        [Test]
        public void Test02ArabicShapingBasicLamAlef()
        {
            string orig = "\u0628\u0644\u0627 \u0644\u0627";
            string res = NBidi.LogicalToVisual(orig);
            string exp = "\uFEFB \uFEFC\uFE91";
            Assert.AreEqual(exp.ToCharArray(), res.ToCharArray(),
                            "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}\nexpected as chars: {3}",
                            orig, AsCharArray(orig), AsCharArray(res), AsCharArray(exp));
        }

        [Test]
        public void Test03ArabicShapingLamAlefAbove()
        {
            string orig = "\u0628\u0644\u0623 \u0644\u0623";
            string res = NBidi.LogicalToVisual(orig);
            string exp = "\uFEF7 \uFEF8\uFE91";
            Assert.AreEqual(exp.ToCharArray(), res.ToCharArray(),
                            "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}\nexpected as chars: {3}",
                            orig, AsCharArray(orig), AsCharArray(res), AsCharArray(exp));
        }

        [Test]
        public void Test04ArabicShapingWithLamAlefBelow()
        {
            string orig = "\u0628\u0644\u0625 \u0644\u0625";
            string res = NBidi.LogicalToVisual(orig);
            string exp = "\uFEF9 \uFEFA\uFE91";
            Assert.AreEqual(exp.ToCharArray(), res.ToCharArray(),
                            "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}\nexpected as chars: {3}",
                            orig, AsCharArray(orig), AsCharArray(res), AsCharArray(exp));
        }

        [Test]
        public void Test05ArabicShapingWithLamAlefMaddaAbove()
        {
            string orig = "\u0628\u0644\u0622 \u0644\u0622";
            string res = NBidi.LogicalToVisual(orig);
            string exp = "\uFEF5 \uFEF6\uFE91";
            Assert.AreEqual(exp.ToCharArray(), res.ToCharArray(),
                            "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}\nexpected as chars: {3}",
                            orig, AsCharArray(orig), AsCharArray(res), AsCharArray(exp));
        }
        	
        [Test]
        public void Test06ArabicShapingTashkil()
        {
        	//meem-initial madda ha-medial meem-medial dal-final
            string orig = "\u0645\u064F\u062D\u0645\u062F";
            string res = NBidi.LogicalToVisual(orig);
            string exp = "\uFEAA\uFEE4\uFEA4\u064F\uFEE3";
            Assert.AreEqual(exp.ToCharArray(), res.ToCharArray(),
                            "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}\nexpected as chars: {3}",
                            orig, AsCharArray(orig), AsCharArray(res), AsCharArray(exp));
        }
        
        private string AsCharArray(string str)
        {
            string testRes = string.Empty;
            foreach (char c in str)
                testRes += ((int)c).ToString("X4") + " ";
            return testRes;
        }
    }
}
