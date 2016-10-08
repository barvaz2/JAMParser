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
using System.Collections;
using System.Text;
using System.Globalization;

namespace NBidi
{
    /// <summary>
    /// The main class that implements the BIDI algorithm.
    /// </summary>
    /// <remarks>
    /// Note that the BiDi algorithm does not cover reordering a visual string to logical form.
    /// Some times the very same algorithm will do the job, but in more complex situations it'll fail.
    /// The correct way is to always work in logical form in memory, and only run the BiDi algorithm to display the results on the screen.
    /// </remarks>
    /// <example>
    /// <code>string visual = NBidi.NBidi.LogicalToVisual(string logical)</code>
    /// </example>
    public static class NBidi
    {
        /// <summary>
        /// Implementation of the BIDI algorithm, as described in http://www.unicode.org/reports/tr9/tr9-17.html
        /// </summary>
        /// <param name="logicalString">The original logical-ordered string.</param>
        /// <returns>The visual representation of the string.</returns>
        public static string LogicalToVisual(string logicalString)
        {
            Paragraph[] pars = SplitStringToParagraphs(logicalString);
            StringBuilder sb = new StringBuilder();
            foreach (Paragraph p in pars)
                sb.Append(p.BidiText);

            return sb.ToString();
        }

        /// <summary>
        /// Implementation of the BIDI algorithm, as described in http://www.unicode.org/reports/tr9/tr9-17.html
        /// </summary>
        /// <param name="logicalString">The original logical-ordered string.</param>
        /// <param name="indexes">Implies where the original characters are.</param>
        /// <param name="lengths">Implies how many characters each original character occupies.</param>
        /// <returns>The visual representation of the string.</returns>
        public static string LogicalToVisual(string logicalString, out int[] indexes, out int[] lengths)
        {
            //Section 3:
            //1. seperate text into paragraphs
            //2. resulate each paragraph to its embeding levels of text
            //2.1 find the first character of type L, AL, or R.
            //3. reorder text elements

            //Section 3.3: Resolving Embedding Levels:
            //(1) determining the paragraph level.
            //(2) determining explicit embedding levels and directions.
            //(3) resolving weak types.
            //(4) resolving neutral types.
            //(5) resolving implicit embedding levels.

            ArrayList arrIndexes = new ArrayList();
            ArrayList arrLengths = new ArrayList();

            Paragraph[] pars = SplitStringToParagraphs(logicalString);
            StringBuilder sb = new StringBuilder();
            foreach (Paragraph p in pars)
            {
                sb.Append(p.BidiText);
                arrIndexes.AddRange(p.BidiIndexes);
                arrLengths.AddRange(p.BidiIndexLengths);
            }

            indexes = (int[])arrIndexes.ToArray(typeof(int));
            lengths = (int[])arrLengths.ToArray(typeof(int));
            return sb.ToString();
        }

        private struct CharData
        {
            public char _char;
            public byte _el; // 0-62 => 6
            public BidiCharacterType _ct; // 0-18 => 5
            public int _idx;
        }

        // 3.3.1.P1 - Split the text into separate paragraphs.
        // A paragraph separator is kept with the previous paragraph.
        // Within each paragraph, apply all the other rules of this algorithm.
        private static Paragraph[] SplitStringToParagraphs(string logicalString)
        {
            ArrayList ret = new ArrayList();
            int i;
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < logicalString.Length; ++i)
            {
                char c = logicalString[i];
                BidiCharacterType cType = UnicodeCharacterDataResolver.GetBidiCharacterType(c);
                if (cType == BidiCharacterType.B)
                {
                    Paragraph p = new Paragraph(sb.ToString());
                    p.ParagraphSeparator = c;
                    ret.Add(p);
                    sb.Length = 0;
                }
                else
                    sb.Append(c);
            }
            if (sb.Length > 0) // string ended without a paragraph separator
            {
                ret.Add(new Paragraph(sb.ToString()));
            }
            return (Paragraph[])ret.ToArray(typeof(Paragraph));
        }

        private class Paragraph
        {
            string _original_text;
            string _text;
            string _bidi_text;
            char _paragraph_separator = BidiChars.NotAChar;

            byte embedding_level;
            CharData[] _text_data;
            int[] _char_lengths;
            int[] _bidi_indexes;

            bool _hasArabic;
            bool _hasNSMs;

            public Paragraph(string text)
            {
                Text = text;
            }

            public string Text
            {
                get { return _original_text; }
                set
                {
                    _original_text = value;
                    _text = value;

                    NormalizeText();

                    RecalculateParagraphEmbeddingLevel();
                    RecalculateCharactersEmbeddingLevels();

                    RemoveBidiMarkers();
                }
            }

            public char ParagraphSeparator
            {
                get { return _paragraph_separator; }
                internal set { _paragraph_separator = value; }
            }

            public string BidiText
            {
                get
                {
                    string ret = _bidi_text;
                    if (_paragraph_separator != BidiChars.NotAChar)
                        ret += _paragraph_separator;
                    return ret;
                }
            }

            public int[] BidiIndexes
            {
                get { return _bidi_indexes; }
            }

            public int[] BidiIndexLengths
            {
                get { return _char_lengths; }
            }

            public byte EmbeddingLevel
            {
                get { return embedding_level; }
                set { embedding_level = value; }
            }

            private void RemoveBidiMarkers()
            {
                string controlChars = "\u200F\u202B\u202E\u200E\u202A\u202D\u202C";

                StringBuilder sb = new StringBuilder(_bidi_text);
                ArrayList idxArr = new ArrayList(_bidi_indexes);
                ArrayList lenArr = new ArrayList(_char_lengths);

                int i = 0;
                while (i < sb.Length)
                {
                    if (controlChars.Contains(sb[i].ToString()))
                    {
                        sb.Remove(i, 1);
                        idxArr.RemoveAt(i);
                        lenArr.RemoveAt(i);
                    }
                    else
                        ++i;
                }

                _bidi_text = sb.ToString();
                _bidi_indexes = (int[])idxArr.ToArray(typeof(int));
                _char_lengths = (int[])lenArr.ToArray(typeof(int));
            }

            // 3.3.1 The Paragraph Level
            // P2 - In each paragraph, find the first character of type L, AL, or R.
            // P3 - If a character is found in P2 and it is of type AL or R, then
            // set the paragraph embedding level to one; otherwise, set it to zero.
            public void RecalculateParagraphEmbeddingLevel()
            {
                foreach (char c in _text)
                {
                    BidiCharacterType cType = UnicodeCharacterDataResolver.GetBidiCharacterType(c);
                    if (cType == BidiCharacterType.R || cType == BidiCharacterType.AL)
                    {
                        embedding_level = 1;
                        break;
                    }
                    else if (cType == BidiCharacterType.L)
                        break;
                }
            }

            public void NormalizeText()
            {
                ArrayList char_lengths = new ArrayList();

                StringBuilder sb = InternalDecompose(char_lengths);
                InternalCompose(sb, char_lengths);

                _char_lengths = (int[])char_lengths.ToArray(typeof(int));

                _text = sb.ToString();
            }

            // 3.3.2 Explicit Levels and Directions
            public void RecalculateCharactersEmbeddingLevels()
            {
                // This method is implemented in such a way it handles the string in logical order,
                // rather than visual order, so it is easier to handle complex layouts. That is why
                // it is placed BEFORE ReorderString rather than AFTER it, as its number suggests.
                if (_hasArabic)
                    _text = PerformArabicShaping(_text);

                _text_data = new CharData[_text.Length];

                #region rules X1 - X9
                // X1
                byte embeddingLevel = EmbeddingLevel;
                DirectionalOverrideStatus dos = DirectionalOverrideStatus.Neutral;
                Stack dosStack = new Stack();
                Stack elStack = new Stack();
                int idx = 0;
                for (int i = 0; i < _text.Length; ++i)
                {
                    bool x9Char = false;
                    char c = _text[i];
                    _text_data[i]._ct = UnicodeCharacterDataResolver.GetBidiCharacterType(c);
                    _text_data[i]._char = c;
                    _text_data[i]._idx = idx;
                    idx += _char_lengths[i];

                    #region rules X2 - X5
                    // X2. With each RLE, compute the least greater odd embedding level.
                    // X4. With each RLO, compute the least greater odd embedding level.
                    if (c == BidiChars.RLE || c == BidiChars.RLO)
                    {
                        x9Char = true;
                        if (embeddingLevel < 60)
                        {
                            elStack.Push(embeddingLevel);
                            dosStack.Push(dos);

                            ++embeddingLevel;
                            embeddingLevel |= 1;

                            if (c == BidiChars.RLE)
                                dos = DirectionalOverrideStatus.Neutral;
                            else
                                dos = DirectionalOverrideStatus.RTL;
                        }
                    }
                    // X3. With each LRE, compute the least greater even embedding level.
                    // X5. With each LRO, compute the least greater even embedding level.
                    else if (c == BidiChars.LRE || c == BidiChars.LRO)
                    {
                        x9Char = true;
                        if (embeddingLevel < 59)
                        {
                            elStack.Push(embeddingLevel);
                            dosStack.Push(dos);

                            embeddingLevel |= 1;
                            ++embeddingLevel;

                            if (c == BidiChars.LRE)
                                dos = DirectionalOverrideStatus.Neutral;
                            else
                                dos = DirectionalOverrideStatus.LTR;
                        }
                    }
                    #endregion

                    #region rule X6
                    // X6. For all types besides RLE, LRE, RLO, LRO, and PDF: (...)
                    else if (c != BidiChars.PDF)
                    {
                        // a. Set the level of the current character to the current embedding level.
                        _text_data[i]._el = embeddingLevel;

                        //b. Whenever the directional override status is not neutral,
                        //reset the current character type to the directional override status.
                        if (dos == DirectionalOverrideStatus.LTR)
                            _text_data[i]._ct = BidiCharacterType.L;
                        else if (dos == DirectionalOverrideStatus.RTL)
                            _text_data[i]._ct = BidiCharacterType.R;
                    }
                    #endregion

                    #region rule X7
                    //Terminating Embeddings and Overrides
                    // X7. With each PDF, determine the matching embedding or override code.
                    // If there was a valid matching code, restore (pop) the last remembered (pushed)
                    // embedding level and directional override.
                    else if (c == BidiChars.PDF)
                    {
                        x9Char = true;
                        if (elStack.Count > 0)
                        {
                            embeddingLevel = (byte)(elStack.Pop());
                            dos = (DirectionalOverrideStatus)(dosStack.Pop());
                        }
                    }
                    #endregion

                    // X8. All explicit directional embeddings and overrides are completely
                    // terminated at the end of each paragraph. Paragraph separators are not
                    // included in the embedding.

                    if (x9Char || _text_data[i]._ct == BidiCharacterType.BN)
                    {
                        _text_data[i]._el = embeddingLevel;
                    }
                }
                #endregion

                // X10. The remaining rules are applied to each run of characters at the same level.
                int prevLevel = EmbeddingLevel;
                int start = 0;
                while (start < _text.Length)
                {
                    #region rule X10 - run level setup
                    byte level = _text_data[start]._el;
                    BidiCharacterType sor = TypeForLevel(Math.Max(prevLevel, level));

                    int limit = start + 1;
                    while (limit < _text.Length && _text_data[limit]._el == level)
                        ++limit;

                    byte nextLevel = limit < _text.Length ? _text_data[limit]._el : EmbeddingLevel;
                    BidiCharacterType eor = TypeForLevel(Math.Max(nextLevel, level));
                    #endregion

                    ResolveWeakTypes(start, limit, sor, eor);
                    ResolveNeutralTypes(start, limit, sor, eor, level);
                    ResolveImplicitTypes(start, limit, level);

                    prevLevel = level;
                    start = limit;
                }

                ReorderString();
                FixMirroredCharacters();

                ArrayList indexes = new ArrayList();
                ArrayList lengths = new ArrayList();

                StringBuilder sb = new StringBuilder();
                foreach (CharData cd in _text_data)
                {
                    sb.Append(cd._char);
                    indexes.Add(cd._idx);
                    lengths.Add(1);
                }

                _bidi_text = sb.ToString();
                _bidi_indexes = (int[])indexes.ToArray(typeof(int));
            }

            /// <summary>
            /// 3.3.3 Resolving Weak Types
            /// </summary>
            private void ResolveWeakTypes(int start, int limit, BidiCharacterType sor, BidiCharacterType eor)
            {
                // TODO - all these repeating runs seems somewhat unefficient...
                // TODO - rules 2 and 7 are the same, except for minor parameter changes...

                #region rule W1
                // W1. Examine each nonspacing mark (NSM) in the level run, and change the type of the NSM to the type of the previous character. If the NSM is at the start of the level run, it will get the type of sor.
                if (_hasNSMs)
                {
                    BidiCharacterType preceedingCharacterType = sor;
                    for (int i = start; i < limit; ++i)
                    {
                        BidiCharacterType t = _text_data[i]._ct;
                        if (t == BidiCharacterType.NSM)
                            _text_data[i]._ct = preceedingCharacterType;
                        else
                            preceedingCharacterType = t;
                    }
                }
                #endregion

                #region rule W2
                // W2. Search backward from each instance of a European number until the first strong type (R, L, AL, or sor) is found. If an AL is found, change the type of the European number to Arabic number.

                BidiCharacterType t_w2 = BidiCharacterType.EN;
                for (int i = start; i < limit; ++i)
                {
                    if (_text_data[i]._ct == BidiCharacterType.L || _text_data[i]._ct == BidiCharacterType.R)
                        t_w2 = BidiCharacterType.EN;
                    else if (_text_data[i]._ct == BidiCharacterType.AL)
                        t_w2 = BidiCharacterType.AN;
                    else if (_text_data[i]._ct == BidiCharacterType.EN)
                        _text_data[i]._ct = t_w2;
                }
                #endregion

                #region rule #3
                // W3. Change all ALs to R.
                if (_hasArabic)
                {
                    for (int i = start; i < limit; ++i)
                    {
                        if (_text_data[i]._ct == BidiCharacterType.AL)
                            _text_data[i]._ct = BidiCharacterType.R;
                    }
                }
                #endregion

                #region rule W4
                // W4. A single European separator between two European numbers changes to a European number. A single common separator between two numbers of the same type changes to that type.

                // Since there must be values on both sides for this rule to have an
                // effect, the scan skips the first and last value.
                //
                // Although the scan proceeds left to right, and changes the type values
                // in a way that would appear to affect the computations later in the scan,
                // there is actually no problem.  A change in the current value can only
                // affect the value to its immediate right, and only affect it if it is
                // ES or CS.  But the current value can only change if the value to its
                // right is not ES or CS.  Thus either the current value will not change,
                // or its change will have no effect on the remainder of the analysis.

                for (int i = start + 1; i < limit - 1; ++i)
                {
                    if (_text_data[i]._ct == BidiCharacterType.ES || _text_data[i]._ct == BidiCharacterType.CS)
                    {
                        BidiCharacterType prevSepType = _text_data[i - 1]._ct;
                        BidiCharacterType succSepType = _text_data[i + 1]._ct;
                        if (prevSepType == BidiCharacterType.EN && succSepType == BidiCharacterType.EN)
                        {
                            _text_data[i]._ct = BidiCharacterType.EN;
                        }
                        else if (_text_data[i]._ct == BidiCharacterType.CS && prevSepType == BidiCharacterType.AN && succSepType == BidiCharacterType.AN)
                        {
                            _text_data[i]._ct = BidiCharacterType.AN;
                        }
                    }
                }
                #endregion

                #region rule W5
                // W5. A sequence of European terminators adjacent to European numbers changes to all European numbers.
                for (int i = start; i < limit; ++i)
                {
                    if (_text_data[i]._ct == BidiCharacterType.ET)
                    {
                        // locate end of sequence
                        int runstart = i;
                        int runlimit = FindRunLimit(runstart, limit, new BidiCharacterType[] { BidiCharacterType.ET });

                        // check values at ends of sequence
                        BidiCharacterType t = runstart == start ? sor : _text_data[runstart - 1]._ct;

                        if (t != BidiCharacterType.EN)
                            t = runlimit == limit ? eor : _text_data[runlimit]._ct;

                        if (t == BidiCharacterType.EN)
                            SetTypes(runstart, runlimit, BidiCharacterType.EN);

                        // continue at end of sequence
                        i = runlimit;
                    }
                }
                #endregion

                #region rule W6
                // W6. Otherwise, separators and terminators change to Other Neutral.
                for (int i = start; i < limit; ++i)
                {
                    BidiCharacterType t = _text_data[i]._ct;
                    if (t == BidiCharacterType.ES || t == BidiCharacterType.ET || t == BidiCharacterType.CS)
                    {
                        _text_data[i]._ct = BidiCharacterType.ON;
                    }
                }
                #endregion

                #region rule W7
                // W7. Search backward from each instance of a European number until the first strong type (R, L, or sor) is found.
                //     If an L is found, then change the type of the European number to L.

                BidiCharacterType t_w7 = sor == BidiCharacterType.L ? BidiCharacterType.L : BidiCharacterType.EN;
                for (int i = start; i < limit; ++i)
                {
                    if (_text_data[i]._ct == BidiCharacterType.R)
                        t_w7 = BidiCharacterType.EN;
                    else if (_text_data[i]._ct == BidiCharacterType.L)
                        t_w7 = BidiCharacterType.L;
                    else if (_text_data[i]._ct == BidiCharacterType.EN)
                        _text_data[i]._ct = t_w7;
                }
                #endregion
            }

            /// <summary>
            /// 3.3.4 Resolving Neutral Types
            /// </summary>
            private void ResolveNeutralTypes(int start, int limit, BidiCharacterType sor, BidiCharacterType eor, int level)
            {
                // N1. A sequence of neutrals takes the direction of the surrounding strong text if the text on both sides has the same direction. 
                //     European and Arabic numbers act as if they were R in terms of their influence on neutrals.
                //     Start-of-level-run (sor) and end-of-level-run (eor) are used at level run boundaries.
                // N2. Any remaining neutrals take the embedding direction.

                for (int i = start; i < limit; ++i)
                {
                    BidiCharacterType t = _text_data[i]._ct;
                    if (t == BidiCharacterType.WS || t == BidiCharacterType.ON || t == BidiCharacterType.B || t == BidiCharacterType.S)
                    {
                        // find bounds of run of neutrals
                        int runstart = i;
                        int runlimit = FindRunLimit(runstart, limit, new BidiCharacterType[] { BidiCharacterType.B, BidiCharacterType.S, BidiCharacterType.WS, BidiCharacterType.ON });

                        // determine effective types at ends of run
                        BidiCharacterType leadingType;
                        BidiCharacterType trailingType;

                        if (runstart == start)
                            leadingType = sor;
                        else
                        {
                            leadingType = _text_data[runstart - 1]._ct;
                            if (leadingType == BidiCharacterType.AN || leadingType == BidiCharacterType.EN)
                                leadingType = BidiCharacterType.R;
                        }

                        if (runlimit == limit)
                            trailingType = eor;
                        else
                        {
                            trailingType = _text_data[runlimit]._ct;
                            if (trailingType == BidiCharacterType.AN || trailingType == BidiCharacterType.EN)
                                trailingType = BidiCharacterType.R;
                        }

                        BidiCharacterType resolvedType;
                        if (leadingType == trailingType)
                        {
                            // Rule N1.
                            resolvedType = leadingType;
                        }
                        else
                        {
                            // Rule N2.
                            // Notice the embedding level of the run is used, not
                            // the paragraph embedding level.
                            resolvedType = TypeForLevel(level);
                        }

                        SetTypes(runstart, runlimit, resolvedType);

                        // skip over run of (former) neutrals
                        i = runlimit;
                    }
                }
            }

            /// <summary>
            /// 3.3.5 Resolving Implicit Levels
            /// </summary>
            private void ResolveImplicitTypes(int start, int limit, int level)
            {
                // I1. For all characters with an even (left-to-right) embedding direction, those of type R go up one level and those of type AN or EN go up two levels.
                // I2. For all characters with an odd (right-to-left) embedding direction, those of type L, EN or AN go up one level.

                if ((level & 1) == 0) // even level
                {
                    for (int i = start; i < limit; ++i)
                    {
                        BidiCharacterType t = _text_data[i]._ct;
                        // Rule I1.
                        if (t == BidiCharacterType.R)
                            _text_data[i]._el += 1;
                        else if (t == BidiCharacterType.AN || t == BidiCharacterType.EN)
                            _text_data[i]._el += 2;
                    }
                }
                else // odd level
                {
                    for (int i = start; i < limit; ++i)
                    {
                        BidiCharacterType t = _text_data[i]._ct;
                        // Rule I2.
                        if (t == BidiCharacterType.L || t == BidiCharacterType.AN || t == BidiCharacterType.EN)
                            _text_data[i]._el += 1;
                    }
                }
            }

            /// <summary>
            /// 3.4 Reordering Resolved Levels
            /// </summary>
            private void ReorderString()
            {
                //L1. On each line, reset the embedding level of the following characters to the paragraph embedding level:
                //    1. Segment separators,
                //    2. Paragraph separators,
                //    3. Any sequence of whitespace characters preceding a segment separator or paragraph separator, and
                //    4. Any sequence of white space characters at the end of the line.

                int l1_start = 0;
                for (int i = 0; i < _text_data.Length; ++i)
                {
                    if (_text_data[i]._ct == BidiCharacterType.S || _text_data[i]._ct == BidiCharacterType.B)
                    {
                        for (int j = l1_start; j <= i; ++j)
                            _text_data[j]._el = EmbeddingLevel;
                    }

                    if (_text_data[i]._ct != BidiCharacterType.WS)
                    {
                        l1_start = i + 1;
                    }
                }
                for (int j = l1_start; j < _text_data.Length; ++j)
                    _text_data[j]._el = EmbeddingLevel;

                // L2. From the highest level found in the text to the lowest odd level on each
                //     line, including intermediate levels not actually present in the text,
                //     reverse any contiguous sequence of characters that are at that level or
                //     higher.
                byte highest = 0;
                byte lowest_odd = 63;
                foreach (CharData cd in _text_data)
                {
                    if (cd._el > highest) highest = cd._el;
                    if ((cd._el & 1) == 1 && cd._el < lowest_odd) lowest_odd = cd._el;
                }

                for (byte el = highest; el >= lowest_odd; --el)
                {
                    for (int i = 0; i < _text_data.Length; ++i)
                    {
                        if (_text_data[i]._el >= el)
                        {
                            // find range of text at or above this level
                            int l2_start = i;
                            int limit = i + 1;
                            while (limit < _text_data.Length && _text_data[limit]._el >= el)
                            {
                                ++limit;
                            }

                            // reverse run
                            for (int j = l2_start, k = limit - 1; j < k; ++j, --k)
                            {
                                CharData temp_cd = _text_data[j];
                                _text_data[j] = _text_data[k];
                                _text_data[k] = temp_cd;
                            }

                            // skip to end of level run
                            i = limit;
                        }
                    }
                }

                // TODO - L3. Combining marks applied to a right-to-left base character will at this point precede their base 
                // character. If the rendering engine expects them to follow the base characters in the final display process,
                // then the ordering of the marks and the base character must be reversed.
            }

            /// <summary>
            /// L4. A character is depicted by a mirrored glyph if and only if (a) the resolved directionality of that character is R, and (b) the Bidi_Mirrored property value of that character is true.
            /// </summary>
            private void FixMirroredCharacters()
            {
                for (int i = 0; i < _text_data.Length; ++i)
                {
                    if ((_text_data[i]._el & 1) == 1)
                        _text_data[i]._char = BidiCharacterMirrorResolver.GetBidiCharacterMirror(_text_data[i]._char);
                }
            }

            /// <summary>
            /// 3.5 Shaping
            /// Implements rules R1-R7 and rules L1-L3 of section 8.2 (Arabic) of the Unicode standard.
            /// </summary>
            // TODO - this code is very special-cased.
            private string PerformArabicShaping(string text)
            {
                ArabicShapeJoiningType last_jt = ArabicShapeJoiningType.U;
                LetterForm last_form = LetterForm.Isolated;
                int last_pos = 0;
                char last_char = BidiChars.NotAChar;
                LetterForm[] letterForms = new LetterForm[text.Length];

                for (int curr_pos = 0; curr_pos < text.Length; ++curr_pos)
                {
                    char ch = text[curr_pos];
                    //string chStr = ((int)ch).ToString("X4");

                    ArabicShapeJoiningType jt = UnicodeArabicShapingResolver.GetArabicShapeJoiningType(ch);
                    if ((jt == ArabicShapeJoiningType.R ||
                         jt == ArabicShapeJoiningType.D ||
                         jt == ArabicShapeJoiningType.C) &&
                        (last_jt == ArabicShapeJoiningType.L ||
                         last_jt == ArabicShapeJoiningType.D ||
                         last_jt == ArabicShapeJoiningType.C))
                    {
                        if (last_form == LetterForm.Isolated && (last_jt == ArabicShapeJoiningType.D ||
                                                                       last_jt == ArabicShapeJoiningType.L))
                        {
                            letterForms[last_pos] = LetterForm.Initial;
                        }
                        else if (last_form == LetterForm.Final && last_jt == ArabicShapeJoiningType.D)
                        {
                            letterForms[last_pos] = LetterForm.Medial;
                        }
                        letterForms[curr_pos] = LetterForm.Final;
                        last_form = LetterForm.Final;
                        last_jt = jt;
                        last_pos = curr_pos;
                        last_char = ch;
                    }
                    else if (jt != ArabicShapeJoiningType.T)
                    {
                        letterForms[curr_pos] = LetterForm.Isolated;
                        last_form = LetterForm.Isolated;
                        last_jt = jt;
                        last_pos = curr_pos;
                        last_char = ch;
                    }
                    else
                        letterForms[curr_pos] = LetterForm.Isolated;
                }

                last_char = BidiChars.NotAChar;
                last_pos = 0;
                int insert_pos = 0;

                StringBuilder sb = new StringBuilder();

                ArrayList lenArr = new ArrayList(_char_lengths);

                for (int curr_pos = 0; curr_pos < text.Length; ++curr_pos)
                {
                    char ch = text[curr_pos];
                    //string chStr = ((int)ch).ToString("X4");
                    ArabicShapeJoiningType jt = UnicodeArabicShapingResolver.GetArabicShapeJoiningType(ch);

                    if (last_char == BidiChars.ARABIC_LAM &&
                        ch != BidiChars.ARABIC_ALEF &&
                        ch != BidiChars.ARABIC_ALEF_MADDA_ABOVE &&
                        ch != BidiChars.ARABIC_ALEF_HAMZA_ABOVE &&
                        ch != BidiChars.ARABIC_ALEF_HAMZA_BELOW &&
                        jt != ArabicShapeJoiningType.T)
                    {
                        last_char = BidiChars.NotAChar;
                    }
                    else if (ch == BidiChars.ARABIC_LAM)
                    {
                        last_char = ch;
                        last_pos = curr_pos;
                        insert_pos = sb.Length;
                    }

                    if (last_char == BidiChars.ARABIC_LAM)
                    {
                        if (letterForms[last_pos] == LetterForm.Medial)
                        {
                            switch (ch)
                            {
                                case BidiChars.ARABIC_ALEF:
                                    sb[insert_pos] = BidiChars.ARABIC_LAM_ALEF_FINAL;
                                    lenArr.RemoveAt(insert_pos);
                                    continue;

                                case BidiChars.ARABIC_ALEF_MADDA_ABOVE:
                                    sb[insert_pos] = BidiChars.ARABIC_LAM_ALEF_MADDA_ABOVE_FINAL;
                                    lenArr.RemoveAt(insert_pos);
                                    lenArr[insert_pos] = (int)(lenArr[insert_pos]) + 1;
                                    continue;

                                case BidiChars.ARABIC_ALEF_HAMZA_ABOVE:
                                    sb[insert_pos] = BidiChars.ARABIC_LAM_ALEF_HAMZA_ABOVE_FINAL;
                                    lenArr.RemoveAt(insert_pos);
                                    continue;

                                case BidiChars.ARABIC_ALEF_HAMZA_BELOW:
                                    sb[insert_pos] = BidiChars.ARABIC_LAM_ALEF_HAMZA_BELOW_FINAL;
                                    lenArr.RemoveAt(insert_pos);
                                    continue;
                            }
                        }
                        else if (letterForms[last_pos] == LetterForm.Initial)
                        {
                            switch (ch)
                            {
                                case BidiChars.ARABIC_ALEF:
                                    sb[insert_pos] = BidiChars.ARABIC_LAM_ALEF_ISOLATED;
                                    lenArr.RemoveAt(insert_pos);
                                    continue;

                                case BidiChars.ARABIC_ALEF_MADDA_ABOVE:
                                    sb[insert_pos] = BidiChars.ARABIC_LAM_ALEF_MADDA_ABOVE_ISOLATED;
                                    lenArr.RemoveAt(insert_pos);
                                    lenArr[insert_pos] = (int)(lenArr[insert_pos]) + 1;
                                    continue;

                                case BidiChars.ARABIC_ALEF_HAMZA_ABOVE:
                                    sb[insert_pos] = BidiChars.ARABIC_LAM_ALEF_HAMZA_ABOVE_ISOLATED;
                                    lenArr.RemoveAt(insert_pos);
                                    continue;

                                case BidiChars.ARABIC_ALEF_HAMZA_BELOW:
                                    sb[insert_pos] = BidiChars.ARABIC_LAM_ALEF_HAMZA_BELOW_ISOLATED;
                                    lenArr.RemoveAt(insert_pos);
                                    continue;
                            }
                        }
                    }

                    sb.Append(UnicodeArabicShapingResolver.GetArabicCharacterByLetterForm(ch, letterForms[curr_pos]));
                }

                _char_lengths = (int[])lenArr.ToArray(typeof(int));

                return sb.ToString();
            }

            private char GetPairwiseComposition(char first, char second)
            {
                if (first < 0 || first > 0xFFFF || second < 0 || second > 0xFFFF) return BidiChars.NotAChar;
                return UnicodeCharacterDataResolver.Compose(first.ToString() + second.ToString());
            }

            private void InternalCompose(StringBuilder target, ArrayList char_lengths)
            {
                if (target.Length == 0) return;
                int starterPos = 0;
                int compPos = 1;
                int text_idx = 0;
                char starterCh = target[0];

                char_lengths[starterPos] = (int)char_lengths[starterPos] + 1;

                UnicodeCanonicalClass lastClass = UnicodeCharacterDataResolver.GetUnicodeCanonicalClass(starterCh);

                if (lastClass != UnicodeCanonicalClass.NR)
                    lastClass = (UnicodeCanonicalClass)256; // fix for strings staring with a combining mark

                int oldLen = target.Length;

                // Loop on the decomposed characters, combining where possible
                char ch;
                for (int decompPos = compPos; decompPos < target.Length; ++decompPos)
                {
                    ch = target[decompPos];
                    UnicodeCanonicalClass chClass = UnicodeCharacterDataResolver.GetUnicodeCanonicalClass(ch);
                    char composite = GetPairwiseComposition(starterCh, ch);
                    UnicodeDecompositionType composeType = UnicodeCharacterDataResolver.GetUnicodeDecompositionType(composite);

                    if (composeType == UnicodeDecompositionType.None &&
                        composite != BidiChars.NotAChar &&
                        (lastClass < chClass || lastClass == UnicodeCanonicalClass.NR))
                    {
                        target[starterPos] = composite;
                        char_lengths[starterPos] = (int)char_lengths[starterPos] + 1;
                        // we know that we will only be replacing non-supplementaries by non-supplementaries
                        // so we don't have to adjust the decompPos
                        starterCh = composite;
                    }
                    else
                    {
                        if (chClass == UnicodeCanonicalClass.NR)
                        {
                            starterPos = compPos;
                            starterCh = ch;
                            text_idx++;
                        }
                        lastClass = chClass;
                        target[compPos] = ch;
                        //char_lengths[compPos] = (int)char_lengths[compPos] + 1;
                        if (compPos >= char_lengths.Count)
                        {
                            while (char_lengths.Count < target.ToString().Length)
                            {
                                char_lengths.Add(0);
                            }
                        }
                        int chkPos = compPos;
                        if ((int)char_lengths[chkPos] < 0)
                        {
                            while ((int)char_lengths[chkPos] < 0)
                            {
                                char_lengths[chkPos] = (int)char_lengths[chkPos] + 1;
                                char_lengths.Insert(compPos, 0);
                                chkPos++;
                            }
                        }
                        else
                            char_lengths[chkPos] = (int)char_lengths[chkPos] + 1;

                        if (target.Length != oldLen) // MAY HAVE TO ADJUST!
                        {
                            decompPos += target.Length - oldLen;
                            oldLen = target.Length;
                        }
                        ++compPos;
                    }
                }
                target.Length = compPos;
                char_lengths.RemoveRange(compPos, char_lengths.Count - compPos);
            }

            private void GetRecursiveDecomposition(bool canonical, char ch, StringBuilder builder)
            {
                string decomp = UnicodeCharacterDataResolver.GetUnicodeDecompositionMapping(ch);
                if (decomp != null && !(canonical && UnicodeCharacterDataResolver.GetUnicodeDecompositionType(ch) != UnicodeDecompositionType.None))
                {
                    for (int i = 0; i < decomp.Length; ++i)
                    {
                        GetRecursiveDecomposition(canonical, decomp[i], builder);
                    }
                }
                else // if no decomp, append
                {
                    builder.Append(ch);
                }
            }

            private StringBuilder InternalDecompose(ArrayList char_lengths)
            {
                StringBuilder target = new StringBuilder();
                StringBuilder buffer = new StringBuilder();
                
                _hasArabic =false;
                _hasNSMs = false;

                for (int i = 0; i < _text.Length; ++i)
                {
                    BidiCharacterType ct = UnicodeCharacterDataResolver.GetBidiCharacterType(_text[i]);
                    _hasArabic |= ((ct == BidiCharacterType.AL) || (ct == BidiCharacterType.AN));
                    _hasNSMs |= (ct == BidiCharacterType.NSM);

                    buffer.Length = 0;
                    GetRecursiveDecomposition(false, _text[i], buffer);
                    char_lengths.Add(1 - buffer.Length);
                    // add all of the characters in the decomposition.
                    // (may be just the original character, if there was
                    // no decomposition mapping)

                    char ch;
                    for (int j = 0; j < buffer.Length; ++j)
                    {
                        ch = buffer[j];
                        UnicodeCanonicalClass chClass = UnicodeCharacterDataResolver.GetUnicodeCanonicalClass(ch);
                        int k = target.Length; // insertion point
                        if (chClass != UnicodeCanonicalClass.NR)
                        {
                            // bubble-sort combining marks as necessary
                            char ch2;
                            for (; k > 0; --k)
                            {
                                ch2 = target[k - 1];
                                if (UnicodeCharacterDataResolver.GetUnicodeCanonicalClass(ch2) <= chClass) break;
                            }
                        }
                        target.Insert(k, ch);
                    }
                }
                return target;
            }

            /// <summary>
            /// Return the strong type (L or R) corresponding to the embedding level.
            /// </summary>
            /// <param name="level">The embedding level to check.</param>
            /// <returns></returns>
            private static BidiCharacterType TypeForLevel(int level)
            {
                return ((level & 1) == 0) ? BidiCharacterType.L : BidiCharacterType.R;
            }

            /// <summary>
            /// Return the limit of the run, starting at index, that includes only resultTypes in validSet.
            /// This checks the value at index, and will return index if that value is not in validSet.
            /// </summary>
            /// <param name="index"></param>
            /// <param name="limit"></param>
            /// <param name="validSet"></param>
            /// <returns></returns>
            private int FindRunLimit(int index, int limit, BidiCharacterType[] validSet)
            {
                --index;
                bool found = false;
                while (++index < limit)
                {
                    BidiCharacterType t = _text_data[index]._ct;
                    found = false;
                    for (int i = 0; i < validSet.Length && !found; ++i)
                    {
                        if (t == validSet[i])
                            found = true;
                    }

                    if (!found)
                        return index; // didn't find a match in validSet
                }
                return limit;
            }

            /// <summary>
            /// Set resultTypes from start up to (but not including) limit to newType.
            /// </summary>
            /// <param name="start"></param>
            /// <param name="limit"></param>
            /// <param name="newType"></param>
            private void SetTypes(int start, int limit, BidiCharacterType newType)
            {
                for (int i = start; i < limit; ++i)
                {
                    _text_data[i]._ct = newType;
                }
            }
        }
    }
}
