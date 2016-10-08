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
// Strings for test cases 2-5 are taken from the Hebrew version of the unicode BiDirectional algorithm value in wikipedia.
// http://he.wikipedia.org/wiki/%D7%90%D7%9C%D7%92%D7%95%D7%A8%D7%99%D7%AA%D7%9D_%D7%93%D7%95-%D7%9B%D7%99%D7%95%D7%95%D7%A0%D7%99%D7%95%D7%AA_%D7%A9%D7%9C_%D7%99%D7%95%D7%A0%D7%99%D7%A7%D7%95%D7%93

using NUnit.Framework;
using System;

namespace NBidi
{
    [TestFixture]
    public class NBidiTests
    {
        [Test]
        public void Test01()
        {
            string orig = "he said '\u05d6\u05d5\u05d4\u05d9 \u05de\u05db\u05d5\u05e0\u05d9\u05ea'";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("he said '\u05ea\u05d9\u05e0\u05d5\u05db\u05de \u05d9\u05d4\u05d5\u05d6'", res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test02()
        {
            string orig = "\u05d4\u05d1\u05d8\u05d8\u05d5\u05ea \u05d2\u05d3\u05dc\u05d5\u05ea \u05d1\u05de\u05d9\u05e9\u05d5\u05e8\u05d9\u05dd, \u05d4\u05d0\u05d2\u05d5\u05d6\u05d9\u05dd \u05d1\u05d4\u05e8\u05d9\u05dd.";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual(".\u05dd\u05d9\u05e8\u05d4\u05d1 \u05dd\u05d9\u05d6\u05d5\u05d2\u05d0\u05d4 ,\u05dd\u05d9\u05e8\u05d5\u05e9\u05d9\u05de\u05d1 \u05ea\u05d5\u05dc\u05d3\u05d2 \u05ea\u05d5\u05d8\u05d8\u05d1\u05d4", res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test03()
        {
            string orig = "\u05d4\u05d1\u05d8\u05d8\u05d5\u05ea \u05d2\u05d3\u05dc\u05d5\u05ea \u05d1\u05de\u05d9\u05e9\u05d5\u05e8\u05d9\u05dd, The nuts \u05d1\u05d4\u05e8\u05d9\u05dd.";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual(".\u05dd\u05d9\u05e8\u05d4\u05d1 The nuts ,\u05dd\u05d9\u05e8\u05d5\u05e9\u05d9\u05de\u05d1 \u05ea\u05d5\u05dc\u05d3\u05d2 \u05ea\u05d5\u05d8\u05d8\u05d1\u05d4", res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test04()
        {
            string orig = "I am John, \u05d0\u05e0\u05d9 \u05d0\u05d5\u05d4\u05d1 Bananas.";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("I am John, \u05d1\u05d4\u05d5\u05d0 \u05d9\u05e0\u05d0 Bananas.", res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test05()
        {
            string orig = "\u05d1-15 \u05d1\u05e0\u05d5\u05d1\u05de\u05d1\u05e8 \u05d0\u05de\u05e8\u05ea\u05d9 \u05dc-John: \u05dc\u05da \u05de\u05e4\u05d4!";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("!\u05d4\u05e4\u05de \u05da\u05dc :John-\u05dc \u05d9\u05ea\u05e8\u05de\u05d0 \u05e8\u05d1\u05de\u05d1\u05d5\u05e0\u05d1 15-\u05d1", res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test06_Mirroring_Hebrew()
        {
            string orig = "\u05d1\u05d3\u05d9\u05e7\u05ea (\u05e1\u05d5\u05d2\u05e8\u05d9\u05d9\u05dd) \u05d1\u05e2\u05d1\u05e8\u05d9\u05ea";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("\u05ea\u05d9\u05e8\u05d1\u05e2\u05d1 (\u05dd\u05d9\u05d9\u05e8\u05d2\u05d5\u05e1) \u05ea\u05e7\u05d9\u05d3\u05d1", res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test06B_Mirroring_English()
        {
            string orig = "test (mirror) behaviour";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("test (mirror) behaviour", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test07_Single_Paragraph_Separator_Multiple_Lines()
        {
            string orig = "he said '\u05d6\u05d5\u05d4\u05d9 \u05de\u05db\u05d5\u05e0\u05d9\u05ea'";
            orig += '\u000A'; // Add a single paragraph separator
            orig += "\u05d4\u05d1\u05d8\u05d8\u05d5\u05ea \u05d2\u05d3\u05dc\u05d5\u05ea \u05d1\u05de\u05d9\u05e9\u05d5\u05e8\u05d9\u05dd, \u05d4\u05d0\u05d2\u05d5\u05d6\u05d9\u05dd \u05d1\u05d4\u05e8\u05d9\u05dd.";
            // Note the string does not end with a paragraph separator.

            string expected = "he said '\u05ea\u05d9\u05e0\u05d5\u05db\u05de \u05d9\u05d4\u05d5\u05d6'";
            expected += '\u000A';
            expected += ".\u05dd\u05d9\u05e8\u05d4\u05d1 \u05dd\u05d9\u05d6\u05d5\u05d2\u05d0\u05d4 ,\u05dd\u05d9\u05e8\u05d5\u05e9\u05d9\u05de\u05d1 \u05ea\u05d5\u05dc\u05d3\u05d2 \u05ea\u05d5\u05d8\u05d8\u05d1\u05d4";
            string res = NBidi.LogicalToVisual(orig);

            Assert.AreEqual(expected, res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test08_Single_Paragraph_Separator_Multiple_Lines()
        {
            string orig = "he said '\u05d6\u05d5\u05d4\u05d9 \u05de\u05db\u05d5\u05e0\u05d9\u05ea'";
            orig += '\u000D'; // Add a single paragraph separator
            orig += "\u05d4\u05d1\u05d8\u05d8\u05d5\u05ea \u05d2\u05d3\u05dc\u05d5\u05ea \u05d1\u05de\u05d9\u05e9\u05d5\u05e8\u05d9\u05dd, \u05d4\u05d0\u05d2\u05d5\u05d6\u05d9\u05dd \u05d1\u05d4\u05e8\u05d9\u05dd.";
            // Note the string does not end with a paragraph separator.

            string expected = "he said '\u05ea\u05d9\u05e0\u05d5\u05db\u05de \u05d9\u05d4\u05d5\u05d6'";
            expected += '\u000D';
            expected += ".\u05dd\u05d9\u05e8\u05d4\u05d1 \u05dd\u05d9\u05d6\u05d5\u05d2\u05d0\u05d4 ,\u05dd\u05d9\u05e8\u05d5\u05e9\u05d9\u05de\u05d1 \u05ea\u05d5\u05dc\u05d3\u05d2 \u05ea\u05d5\u05d8\u05d8\u05d1\u05d4";
            string res = NBidi.LogicalToVisual(orig);

            Assert.AreEqual(expected, res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test09_Single_Paragraph_Separator()
        {
            string orig = "he said '\u05d6\u05d5\u05d4\u05d9 \u05de\u05db\u05d5\u05e0\u05d9\u05ea'";
            orig += '\u000A'; // Add a single paragraph separator

            string expected = "he said '\u05ea\u05d9\u05e0\u05d5\u05db\u05de \u05d9\u05d4\u05d5\u05d6'";
            expected += '\u000A';
            string res = NBidi.LogicalToVisual(orig);

            Assert.AreEqual(expected, res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test10_Single_Paragraph_Separator_English_Only()
        {
            string orig = "he said 'This is a car'";
            orig += '\u000A'; // Add a single paragraph separator

            string expected = "he said 'This is a car'";
            expected += '\u000A';
            string res = NBidi.LogicalToVisual(orig);

            Assert.AreEqual(expected, res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test11_Single_Paragraph_Separator_Hebrew_Only()
        {
            string orig = "\u05d4\u05d1\u05d8\u05d8\u05d5\u05ea \u05d2\u05d3\u05dc\u05d5\u05ea \u05d1\u05de\u05d9\u05e9\u05d5\u05e8\u05d9\u05dd, \u05d4\u05d0\u05d2\u05d5\u05d6\u05d9\u05dd \u05d1\u05d4\u05e8\u05d9\u05dd.";
            orig += '\u000D'; // Add a single paragraph separator

            string expected = ".\u05dd\u05d9\u05e8\u05d4\u05d1 \u05dd\u05d9\u05d6\u05d5\u05d2\u05d0\u05d4 ,\u05dd\u05d9\u05e8\u05d5\u05e9\u05d9\u05de\u05d1 \u05ea\u05d5\u05dc\u05d3\u05d2 \u05ea\u05d5\u05d8\u05d8\u05d1\u05d4";
            expected += '\u000D';

            string res = NBidi.LogicalToVisual(orig);

            Assert.AreEqual(expected, res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test12_Multiple_Paragraph_Separators()
        {
            string orig = "he said '\u05d6\u05d5\u05d4\u05d9 \u05de\u05db\u05d5\u05e0\u05d9\u05ea'";
            orig += '\u000A'; // Add a single paragraph separator
            orig += "\u05d4\u05d1\u05d8\u05d8\u05d5\u05ea \u05d2\u05d3\u05dc\u05d5\u05ea \u05d1\u05de\u05d9\u05e9\u05d5\u05e8\u05d9\u05dd, \u05d4\u05d0\u05d2\u05d5\u05d6\u05d9\u05dd \u05d1\u05d4\u05e8\u05d9\u05dd.";
            orig += '\u000D'; // Add a different single paragraph separator
            orig += "\u05d4\u05d1\u05d8\u05d8\u05d5\u05ea \u05d2\u05d3\u05dc\u05d5\u05ea \u05d1\u05de\u05d9\u05e9\u05d5\u05e8\u05d9\u05dd, The nuts \u05d1\u05d4\u05e8\u05d9\u05dd.";
            // Note the string does not end with a paragraph separator.

            string expected = "he said '\u05ea\u05d9\u05e0\u05d5\u05db\u05de \u05d9\u05d4\u05d5\u05d6'";
            expected += '\u000A';
            expected += ".\u05dd\u05d9\u05e8\u05d4\u05d1 \u05dd\u05d9\u05d6\u05d5\u05d2\u05d0\u05d4 ,\u05dd\u05d9\u05e8\u05d5\u05e9\u05d9\u05de\u05d1 \u05ea\u05d5\u05dc\u05d3\u05d2 \u05ea\u05d5\u05d8\u05d8\u05d1\u05d4";
            expected += '\u000D';
            expected += ".\u05dd\u05d9\u05e8\u05d4\u05d1 The nuts ,\u05dd\u05d9\u05e8\u05d5\u05e9\u05d9\u05de\u05d1 \u05ea\u05d5\u05dc\u05d3\u05d2 \u05ea\u05d5\u05d8\u05d8\u05d1\u05d4";
            string res = NBidi.LogicalToVisual(orig);

            Assert.AreEqual(expected, res, "original: \"{0}\"", orig);
        }

        [Test]
        public void Test13_Composition()
        {
            string orig = "\u0627\u0653";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("\uFE81", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test14_ArabicShaping()
        {
            string orig = "\u0647\u0647\u0647\u0647 \u0647";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("\uFEE9 \uFEEA\uFEEC\uFEEC\uFEEB", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test15_ArabicShaping_Joiners()
        {
            string orig = "\u0647 \u200D\u0647 \u0647\u200D \u200D\u0647\u200D";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("\uFEEC \uFEEB \uFEEA \uFEE9", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test16_LRO() //TODO - should the control characters remain in place after bidi?
        {
            string orig = "\u202D\u05D0\u05D1\u05D2\u202C";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("\u05D0\u05D1\u05D2", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test17_RLO()
        {
            string orig = "\u202Eabc\u202C";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("cba", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test18_LRE()
        {
            string orig = "\u05E2\u05D1\u05E8\u05D9\u05EA " + BidiChars.LRE + "1 2 3" + BidiChars.PDF + " \u05D1\u05D3\u05D9\u05E7\u05D4";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("\u05D4\u05E7\u05D9\u05D3\u05D1 1 2 3 \u05EA\u05D9\u05E8\u05D1\u05E2", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test19_RLE()
        {
            string orig = "English \u202B1 2 3\u202C Test";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("English 3 2 1 Test", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test20_Newlines_1()
        {
            string orig = BidiChars.RLM + "Hello World \u05E9\u05DC\u05D5\u05DD1\n\u05E9\u05DC\u05D5\u05DD2 \u05E9\u05DC\u05D5\u05DD3 \u05E9\u05DC\u05D5\u05DD4\n\u05E9\u05DC\u05D5\u05DD5 \u05E9\u05DC\u05D5\u05DD6 \u05E9\u05DC\u05D5\u05DD7";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("1\u05DD\u05D5\u05DC\u05E9 Hello World\n4\u05DD\u05D5\u05DC\u05E9 3\u05DD\u05D5\u05DC\u05E9 2\u05DD\u05D5\u05DC\u05E9\n7\u05DD\u05D5\u05DC\u05E9 6\u05DD\u05D5\u05DC\u05E9 5\u05DD\u05D5\u05DC\u05E9", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test21_Newlines_2()
        {
            string orig = "Hello World\n\u05E9\u05DC\u05D5\u05DD1 \u05E9\u05DC\u05D5\u05DD2\n\u05E9\u05DC\u05D5\u05DD3 \u05E9\u05DC\u05D5\u05DD4\n\u05E9\u05DC\u05D5\u05DD5 \u05E9\u05DC\u05D5\u05DD6\n\u05E9\u05DC\u05D5\u05DD7\n";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("Hello World\n2\u05DD\u05D5\u05DC\u05E9 1\u05DD\u05D5\u05DC\u05E9\n4\u05DD\u05D5\u05DC\u05E9 3\u05DD\u05D5\u05DC\u05E9\n6\u05DD\u05D5\u05DC\u05E9 5\u05DD\u05D5\u05DC\u05E9\n7\u05DD\u05D5\u05DC\u05E9\n", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test22_Indexes_1_NoComposition()
        {
            string orig = "Hello World \u05E9\u05DC\u05D5\u05DD \u05E2\u05D5\u05DC\u05DD";
            int[] indexes;
            int[] lengths;
            string res = NBidi.LogicalToVisual(orig, out indexes, out lengths);
            //HELLO WORLD slom olam
            //012345678901234567890
            Assert.AreEqual(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 20, 19, 18, 17, 16, 15, 14, 13, 12 }, indexes);
            Assert.AreEqual(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, lengths);
        }

        [Test]
        public void Test23_Indexes_2_NoComposition_RLM()
        {
            string orig = BidiChars.RLM + "Hello World \u05E9\u05DC\u05D5\u05DD \u05E2\u05D5\u05DC\u05DD";
            int[] indexes;
            int[] lengths;
            string res = NBidi.LogicalToVisual(orig, out indexes, out lengths);
            // HELLO WORLD slom olam
            //0123456789012345678901
            Assert.AreEqual(new int[] { 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, indexes);
            Assert.AreEqual(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, lengths);
        }

        [Test]
        public void Test23_Indexes_3_Composition()
        {
            string orig = "Hello World \u0628\u0644\u0622 \u0644\u0627\u0653";
            //string exp = "Hello World \uFEF5 \uFEF6\uFE91";
            int[] indexes;
            int[] lengths;
            string res = NBidi.LogicalToVisual(orig, out indexes, out lengths);
            //HELLO WORLD abc def
            //0123456789012345678

            // abc -> xy
            // def -> z

            //HELLO WORLD z-- y-x
            //0123456789012--34-5

            Assert.AreEqual(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 16, 15, 13, 12 }, indexes);
            Assert.AreEqual(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 3 }, lengths);
        }

        [Test]
        public void Test24_Decomposition_and_Recomposition()
        {
            //string orig = "‏ﺟﹷﺂﻧﹻﻰ ﻗﹷﻮﹾﻡﹸ ﺍﻟﹿﻤﹷﺪﹺﻳﹿﻨﹷﺔﹺ ﻛﹹﺒﹿﺮﹶﺁﻭﹸﻫﹹﻢﹾ ﻭﹶ ﺿﹹﻌﹷﻔﹷﺂﻭﹸﻫﹹﻢﹾ The people of the city, the great and the small, came to me אֵלֶּה הַדְּבָרִים, אֲשֶׁר דִּבֶּר מֹשֶׁה אֶל-כָּל-יִשְׂרָאֵל, בְּעֵבֶר, הַיַּרְדֵּן";
            //string orig = "\u200F\uFE9F\uFE77\uFE82\uFEE7\uFE7B\uFEF0 \uFED7\uFE77\uFEEE\uFE7E\uFEE1\uFE78 \uFE8D\uFEDF\uFE7F\uFEE4\uFE77\uFEAA\uFE7A\uFEF3\uFE7F\uFEE8\uFE77\uFE94\uFE7A \uFEDB\uFE79\uFE92\uFE7F\uFEAE\uFE76\uFE81\uFEED\uFE78\uFEEB\uFE79\uFEE2\uFE7E \uFEED\uFE76 \uFEBF\uFE79\uFECC\uFE77\uFED4\uFE77\uFE82\uFEED\uFE78\uFEEB\uFE79\uFEE2\uFE7E The people of the city, the great and the small, came to me \u05D0\u05B5\u05DC\u05BC\u05B6\u05D4 \u05D4\u05B7\u05D3\u05BC\u05B0\u05D1\u05B8\u05E8\u05B4\u05D9\u05DD, \u05D0\u05B2\u05E9\u05C1\u05B6\u05E8 \u05D3\u05BC\u05B4\u05D1\u05BC\u05B6\u05E8 \u05DE\u05B9\u05E9\u05C1\u05B6\u05D4 \u05D0\u05B6\u05DC\u002D\u05DB\u05BC\u05B8\u05DC\u002D\u05D9\u05B4\u05E9\u05C2\u05B0\u05E8\u05B8\u05D0\u05B5\u05DC, \u05D1\u05BC\u05B0\u05E2\u05B5\u05D1\u05B6\u05E8, \u05D4\u05B7\u05D9\u05BC\u05B7\u05E8\u05B0\u05D3\u05BC\u05B5\u05DF";
            string orig = "\u200F\uFE9F\uFE77\uFE82\uFEE7\uFE7B\uFEF0";// \uFED7\uFE77\uFEEE\uFE7E\uFEE1\uFE78 \uFE8D\uFEDF\uFE7F\uFEE4\uFE77\uFEAA\uFE7A\uFEF3\uFE7F\uFEE8\uFE77\uFE94\uFE7A \uFEDB\uFE79\uFE92\uFE7F\uFEAE\uFE76\uFE81\uFEED\uFE78\uFEEB\uFE79\uFEE2\uFE7E \uFEED\uFE76 \uFEBF\uFE79\uFECC\uFE77\uFED4\uFE77\uFE82\uFEED\uFE78\uFEEB\uFE79\uFEE2\uFE7E The people of the city, the great and the small, came to me \u05D0\u05B5\u05DC\u05BC\u05B6\u05D4 \u05D4\u05B7\u05D3\u05BC\u05B0\u05D1\u05B8\u05E8\u05B4\u05D9\u05DD, \u05D0\u05B2\u05E9\u05C1\u05B6\u05E8 \u05D3\u05BC\u05B4\u05D1\u05BC\u05B6\u05E8 \u05DE\u05B9\u05E9\u05C1\u05B6\u05D4 \u05D0\u05B6\u05DC\u002D\u05DB\u05BC\u05B8\u05DC\u002D\u05D9\u05B4\u05E9\u05C2\u05B0\u05E8\u05B8\u05D0\u05B5\u05DC, \u05D1\u05BC\u05B0\u05E2\u05B5\u05D1\u05B6\u05E8, \u05D4\u05B7\u05D9\u05BC\u05B7\u05E8\u05B0\u05D3\u05BC\u05B5\u05DF";
            //string orig = "\u200F\u062C\u0640\u064E\u0627\u0653\u0646\u0640\u0650\u0649";// \uFED7\uFE77\uFEEE\uFE7E\uFEE1\uFE78 \uFE8D\uFEDF\uFE7F\uFEE4\uFE77\uFEAA\uFE7A\uFEF3\uFE7F\uFEE8\uFE77\uFE94\uFE7A";// \uFEDB\uFE79\uFE92\uFE7F\uFEAE\uFE76\uFE81\uFEED\uFE78\uFEEB\uFE79\uFEE2\uFE7E \uFEED\uFE76";// \uFEBF\uFE79\uFECC\uFE77\uFED4\uFE77\uFE82\uFEED\uFE78\uFEEB\uFE79\uFEE2\uFE7E";// The people of the city, the great and the small, came to me \u05D0\u05B5\u05DC\u05BC\u05B6\u05D4 \u05D4\u05B7\u05D3\u05BC\u05B0\u05D1\u05B8\u05E8\u05B4\u05D9\u05DD, \u05D0\u05B2\u05E9\u05C1\u05B6\u05E8 \u05D3\u05BC\u05B4\u05D1\u05BC\u05B6\u05E8 \u05DE\u05B9\u05E9\u05C1\u05B6\u05D4 \u05D0\u05B6\u05DC\u002D\u05DB\u05BC\u05B8\u05DC\u002D\u05D9\u05B4\u05E9\u05C2\u05B0\u05E8\u05B8\u05D0\u05B5\u05DC, \u05D1\u05BC\u05B0\u05E2\u05B5\u05D1\u05B6\u05E8, \u05D4\u05B7\u05D9\u05BC\u05B7\u05E8\u05B0\u05D3\u05BC\u05B5\u05DF";
            int[] indexes;
            int[] lengths;
            string res = NBidi.LogicalToVisual(orig, out indexes, out lengths);
            Assert.AreEqual("\uFEF0\uFE7B\uFEE7\uFE82\uFE77\uFE9F", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test25_Numbers_and_Hebrew()
        {
            string orig = "123.45 \u05E9\u05DC\u05D5\u05DD \u05E2\u05D5\u05DC\u05DD 678.90";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("678.90 \u05DD\u05DC\u05D5\u05E2 \u05DD\u05D5\u05DC\u05E9 123.45", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test26_RTL_Numbers()
        {
            string orig = "\u05E2\u05D1\u05E8\u05D9\u05EA 1 2 3 \u05D1\u05D3\u05D9\u05E7\u05D4";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("\u05D4\u05E7\u05D9\u05D3\u05D1 3 2 1 \u05EA\u05D9\u05E8\u05D1\u05E2", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        [Test]
        public void Test27_LRE_Letters_Only()
        {
            string orig = "\u05E2\u05D1\u05E8\u05D9\u05EA " + BidiChars.LRE + "A B C" + BidiChars.PDF + " \u05D1\u05D3\u05D9\u05E7\u05D4";
            string res = NBidi.LogicalToVisual(orig);
            Assert.AreEqual("\u05D4\u05E7\u05D9\u05D3\u05D1 A B C \u05EA\u05D9\u05E8\u05D1\u05E2", res, "original: \"{0}\"\noriginal as chars: {1}\nresult as chars:   {2}", orig, AsCharArray(orig), AsCharArray(res));
        }

        private string AsCharArray(string str)
        {
            string testRes = string.Empty;
            foreach (char c in str)
                testRes += @"\u" + ((int)c).ToString("X4") + "";
            return testRes;
        }
    }
}
