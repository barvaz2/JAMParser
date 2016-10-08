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
namespace NBidi {
	public abstract class UnicodeArabicShapingResolver {
		static Hashtable charForms = new Hashtable();
		public static ArabicShapeJoiningType GetArabicShapeJoiningType(char c) {
			if (c >= '\u0600' && c <= '\u0603') return ArabicShapeJoiningType.U;
			if (c == '\u0608') return ArabicShapeJoiningType.U;
			if (c == '\u060B') return ArabicShapeJoiningType.U;
			if (c == '\u0621') return ArabicShapeJoiningType.U;
			if (c >= '\u0622' && c <= '\u0625') return ArabicShapeJoiningType.R;
			if (c == '\u0626') return ArabicShapeJoiningType.D;
			if (c == '\u0627') return ArabicShapeJoiningType.R;
			if (c == '\u0628') return ArabicShapeJoiningType.D;
			if (c == '\u0629') return ArabicShapeJoiningType.R;
			if (c >= '\u062A' && c <= '\u062E') return ArabicShapeJoiningType.D;
			if (c >= '\u062F' && c <= '\u0632') return ArabicShapeJoiningType.R;
			if (c >= '\u0633' && c <= '\u063F') return ArabicShapeJoiningType.D;
			if (c == '\u0640') return ArabicShapeJoiningType.C;
			if (c >= '\u0641' && c <= '\u0647') return ArabicShapeJoiningType.D;
			if (c == '\u0648') return ArabicShapeJoiningType.R;
			if (c >= '\u0649' && c <= '\u064A') return ArabicShapeJoiningType.D;
			if (c >= '\u066E' && c <= '\u066F') return ArabicShapeJoiningType.D;
			if (c >= '\u0671' && c <= '\u0673') return ArabicShapeJoiningType.R;
			if (c == '\u0674') return ArabicShapeJoiningType.U;
			if (c >= '\u0675' && c <= '\u0677') return ArabicShapeJoiningType.R;
			if (c >= '\u0678' && c <= '\u0687') return ArabicShapeJoiningType.D;
			if (c >= '\u0688' && c <= '\u0699') return ArabicShapeJoiningType.R;
			if (c >= '\u069A' && c <= '\u06BF') return ArabicShapeJoiningType.D;
			if (c == '\u06C0') return ArabicShapeJoiningType.R;
			if (c >= '\u06C1' && c <= '\u06C2') return ArabicShapeJoiningType.D;
			if (c >= '\u06C3' && c <= '\u06CB') return ArabicShapeJoiningType.R;
			if (c == '\u06CC') return ArabicShapeJoiningType.D;
			if (c == '\u06CD') return ArabicShapeJoiningType.R;
			if (c == '\u06CE') return ArabicShapeJoiningType.D;
			if (c == '\u06CF') return ArabicShapeJoiningType.R;
			if (c >= '\u06D0' && c <= '\u06D1') return ArabicShapeJoiningType.D;
			if (c >= '\u06D2' && c <= '\u06D3') return ArabicShapeJoiningType.R;
			if (c == '\u06D5') return ArabicShapeJoiningType.R;
			if (c == '\u06DD') return ArabicShapeJoiningType.U;
			if (c >= '\u06EE' && c <= '\u06EF') return ArabicShapeJoiningType.R;
			if (c >= '\u06FA' && c <= '\u06FC') return ArabicShapeJoiningType.D;
			if (c == '\u06FF') return ArabicShapeJoiningType.D;
			if (c == '\u0710') return ArabicShapeJoiningType.R;
			if (c >= '\u0712' && c <= '\u0714') return ArabicShapeJoiningType.D;
			if (c >= '\u0715' && c <= '\u0719') return ArabicShapeJoiningType.R;
			if (c >= '\u071A' && c <= '\u071D') return ArabicShapeJoiningType.D;
			if (c == '\u071E') return ArabicShapeJoiningType.R;
			if (c >= '\u071F' && c <= '\u0727') return ArabicShapeJoiningType.D;
			if (c == '\u0728') return ArabicShapeJoiningType.R;
			if (c == '\u0729') return ArabicShapeJoiningType.D;
			if (c == '\u072A') return ArabicShapeJoiningType.R;
			if (c == '\u072B') return ArabicShapeJoiningType.D;
			if (c == '\u072C') return ArabicShapeJoiningType.R;
			if (c >= '\u072D' && c <= '\u072E') return ArabicShapeJoiningType.D;
			if (c == '\u072F') return ArabicShapeJoiningType.R;
			if (c == '\u074D') return ArabicShapeJoiningType.R;
			if (c >= '\u074E' && c <= '\u0758') return ArabicShapeJoiningType.D;
			if (c >= '\u0759' && c <= '\u075B') return ArabicShapeJoiningType.R;
			if (c >= '\u075C' && c <= '\u076A') return ArabicShapeJoiningType.D;
			if (c >= '\u076B' && c <= '\u076C') return ArabicShapeJoiningType.R;
			if (c >= '\u076D' && c <= '\u0770') return ArabicShapeJoiningType.D;
			if (c == '\u0771') return ArabicShapeJoiningType.R;
			if (c == '\u0772') return ArabicShapeJoiningType.D;
			if (c >= '\u0773' && c <= '\u0774') return ArabicShapeJoiningType.R;
			if (c >= '\u0775' && c <= '\u0777') return ArabicShapeJoiningType.D;
			if (c >= '\u0778' && c <= '\u0779') return ArabicShapeJoiningType.R;
			if (c >= '\u077A' && c <= '\u077F') return ArabicShapeJoiningType.D;
			if (c >= '\u07CA' && c <= '\u07EA') return ArabicShapeJoiningType.D;
			if (c == '\u07FA') return ArabicShapeJoiningType.C;
			if (c == '\u200D') return ArabicShapeJoiningType.C;
			UnicodeGeneralCategory ugc = UnicodeCharacterDataResolver.GetUnicodeGeneralCategory(c);
			if (ugc == UnicodeGeneralCategory.Mn ||
			    ugc == UnicodeGeneralCategory.Me ||
			    ugc == UnicodeGeneralCategory.Cf)
				return ArabicShapeJoiningType.T;
			return ArabicShapeJoiningType.U;
		}

		public static char GetArabicCharacterByLetterForm(char ch, LetterForm form) {
			int key = ((int)ch) | ((int)form) << 16;
			if (charForms.ContainsKey(key))
				return (char)charForms[key];
			return ch;
		}

		static UnicodeArabicShapingResolver() {
			charForms[0x30671] = '\uFB50';
			charForms[0x20671] = '\uFB51';
			charForms[0x3067B] = '\uFB52';
			charForms[0x2067B] = '\uFB53';
			charForms[0x0067B] = '\uFB54';
			charForms[0x1067B] = '\uFB55';
			charForms[0x3067E] = '\uFB56';
			charForms[0x2067E] = '\uFB57';
			charForms[0x0067E] = '\uFB58';
			charForms[0x1067E] = '\uFB59';
			charForms[0x30680] = '\uFB5A';
			charForms[0x20680] = '\uFB5B';
			charForms[0x00680] = '\uFB5C';
			charForms[0x10680] = '\uFB5D';
			charForms[0x3067A] = '\uFB5E';
			charForms[0x2067A] = '\uFB5F';
			charForms[0x0067A] = '\uFB60';
			charForms[0x1067A] = '\uFB61';
			charForms[0x3067F] = '\uFB62';
			charForms[0x2067F] = '\uFB63';
			charForms[0x0067F] = '\uFB64';
			charForms[0x1067F] = '\uFB65';
			charForms[0x30679] = '\uFB66';
			charForms[0x20679] = '\uFB67';
			charForms[0x00679] = '\uFB68';
			charForms[0x10679] = '\uFB69';
			charForms[0x306A4] = '\uFB6A';
			charForms[0x206A4] = '\uFB6B';
			charForms[0x006A4] = '\uFB6C';
			charForms[0x106A4] = '\uFB6D';
			charForms[0x306A6] = '\uFB6E';
			charForms[0x206A6] = '\uFB6F';
			charForms[0x006A6] = '\uFB70';
			charForms[0x106A6] = '\uFB71';
			charForms[0x30684] = '\uFB72';
			charForms[0x20684] = '\uFB73';
			charForms[0x00684] = '\uFB74';
			charForms[0x10684] = '\uFB75';
			charForms[0x30683] = '\uFB76';
			charForms[0x20683] = '\uFB77';
			charForms[0x00683] = '\uFB78';
			charForms[0x10683] = '\uFB79';
			charForms[0x30686] = '\uFB7A';
			charForms[0x20686] = '\uFB7B';
			charForms[0x00686] = '\uFB7C';
			charForms[0x10686] = '\uFB7D';
			charForms[0x30687] = '\uFB7E';
			charForms[0x20687] = '\uFB7F';
			charForms[0x00687] = '\uFB80';
			charForms[0x10687] = '\uFB81';
			charForms[0x3068D] = '\uFB82';
			charForms[0x2068D] = '\uFB83';
			charForms[0x3068C] = '\uFB84';
			charForms[0x2068C] = '\uFB85';
			charForms[0x3068E] = '\uFB86';
			charForms[0x2068E] = '\uFB87';
			charForms[0x30688] = '\uFB88';
			charForms[0x20688] = '\uFB89';
			charForms[0x30698] = '\uFB8A';
			charForms[0x20698] = '\uFB8B';
			charForms[0x30691] = '\uFB8C';
			charForms[0x20691] = '\uFB8D';
			charForms[0x306A9] = '\uFB8E';
			charForms[0x206A9] = '\uFB8F';
			charForms[0x006A9] = '\uFB90';
			charForms[0x106A9] = '\uFB91';
			charForms[0x306AF] = '\uFB92';
			charForms[0x206AF] = '\uFB93';
			charForms[0x006AF] = '\uFB94';
			charForms[0x106AF] = '\uFB95';
			charForms[0x306B3] = '\uFB96';
			charForms[0x206B3] = '\uFB97';
			charForms[0x006B3] = '\uFB98';
			charForms[0x106B3] = '\uFB99';
			charForms[0x306B1] = '\uFB9A';
			charForms[0x206B1] = '\uFB9B';
			charForms[0x006B1] = '\uFB9C';
			charForms[0x106B1] = '\uFB9D';
			charForms[0x306BA] = '\uFB9E';
			charForms[0x206BA] = '\uFB9F';
			charForms[0x306BB] = '\uFBA0';
			charForms[0x206BB] = '\uFBA1';
			charForms[0x006BB] = '\uFBA2';
			charForms[0x106BB] = '\uFBA3';
			charForms[0x306C0] = '\uFBA4';
			charForms[0x206C0] = '\uFBA5';
			charForms[0x306C1] = '\uFBA6';
			charForms[0x206C1] = '\uFBA7';
			charForms[0x006C1] = '\uFBA8';
			charForms[0x106C1] = '\uFBA9';
			charForms[0x306BE] = '\uFBAA';
			charForms[0x206BE] = '\uFBAB';
			charForms[0x006BE] = '\uFBAC';
			charForms[0x106BE] = '\uFBAD';
			charForms[0x306D2] = '\uFBAE';
			charForms[0x206D2] = '\uFBAF';
			charForms[0x306D3] = '\uFBB0';
			charForms[0x206D3] = '\uFBB1';
			charForms[0x306AD] = '\uFBD3';
			charForms[0x206AD] = '\uFBD4';
			charForms[0x006AD] = '\uFBD5';
			charForms[0x106AD] = '\uFBD6';
			charForms[0x306C7] = '\uFBD7';
			charForms[0x206C7] = '\uFBD8';
			charForms[0x306C6] = '\uFBD9';
			charForms[0x206C6] = '\uFBDA';
			charForms[0x306C8] = '\uFBDB';
			charForms[0x206C8] = '\uFBDC';
			charForms[0x30677] = '\uFBDD';
			charForms[0x306CB] = '\uFBDE';
			charForms[0x206CB] = '\uFBDF';
			charForms[0x306C5] = '\uFBE0';
			charForms[0x206C5] = '\uFBE1';
			charForms[0x306C9] = '\uFBE2';
			charForms[0x206C9] = '\uFBE3';
			charForms[0x306D0] = '\uFBE4';
			charForms[0x206D0] = '\uFBE5';
			charForms[0x006D0] = '\uFBE6';
			charForms[0x106D0] = '\uFBE7';
			charForms[0x00649] = '\uFBE8';
			charForms[0x10649] = '\uFBE9';
			charForms[0x306CC] = '\uFBFC';
			charForms[0x206CC] = '\uFBFD';
			charForms[0x006CC] = '\uFBFE';
			charForms[0x106CC] = '\uFBFF';
			charForms[0x30621] = '\uFE80';
			charForms[0x30622] = '\uFE81';
			charForms[0x20622] = '\uFE82';
			charForms[0x30623] = '\uFE83';
			charForms[0x20623] = '\uFE84';
			charForms[0x30624] = '\uFE85';
			charForms[0x20624] = '\uFE86';
			charForms[0x30625] = '\uFE87';
			charForms[0x20625] = '\uFE88';
			charForms[0x30626] = '\uFE89';
			charForms[0x20626] = '\uFE8A';
			charForms[0x00626] = '\uFE8B';
			charForms[0x10626] = '\uFE8C';
			charForms[0x30627] = '\uFE8D';
			charForms[0x20627] = '\uFE8E';
			charForms[0x30628] = '\uFE8F';
			charForms[0x20628] = '\uFE90';
			charForms[0x00628] = '\uFE91';
			charForms[0x10628] = '\uFE92';
			charForms[0x30629] = '\uFE93';
			charForms[0x20629] = '\uFE94';
			charForms[0x3062A] = '\uFE95';
			charForms[0x2062A] = '\uFE96';
			charForms[0x0062A] = '\uFE97';
			charForms[0x1062A] = '\uFE98';
			charForms[0x3062B] = '\uFE99';
			charForms[0x2062B] = '\uFE9A';
			charForms[0x0062B] = '\uFE9B';
			charForms[0x1062B] = '\uFE9C';
			charForms[0x3062C] = '\uFE9D';
			charForms[0x2062C] = '\uFE9E';
			charForms[0x0062C] = '\uFE9F';
			charForms[0x1062C] = '\uFEA0';
			charForms[0x3062D] = '\uFEA1';
			charForms[0x2062D] = '\uFEA2';
			charForms[0x0062D] = '\uFEA3';
			charForms[0x1062D] = '\uFEA4';
			charForms[0x3062E] = '\uFEA5';
			charForms[0x2062E] = '\uFEA6';
			charForms[0x0062E] = '\uFEA7';
			charForms[0x1062E] = '\uFEA8';
			charForms[0x3062F] = '\uFEA9';
			charForms[0x2062F] = '\uFEAA';
			charForms[0x30630] = '\uFEAB';
			charForms[0x20630] = '\uFEAC';
			charForms[0x30631] = '\uFEAD';
			charForms[0x20631] = '\uFEAE';
			charForms[0x30632] = '\uFEAF';
			charForms[0x20632] = '\uFEB0';
			charForms[0x30633] = '\uFEB1';
			charForms[0x20633] = '\uFEB2';
			charForms[0x00633] = '\uFEB3';
			charForms[0x10633] = '\uFEB4';
			charForms[0x30634] = '\uFEB5';
			charForms[0x20634] = '\uFEB6';
			charForms[0x00634] = '\uFEB7';
			charForms[0x10634] = '\uFEB8';
			charForms[0x30635] = '\uFEB9';
			charForms[0x20635] = '\uFEBA';
			charForms[0x00635] = '\uFEBB';
			charForms[0x10635] = '\uFEBC';
			charForms[0x30636] = '\uFEBD';
			charForms[0x20636] = '\uFEBE';
			charForms[0x00636] = '\uFEBF';
			charForms[0x10636] = '\uFEC0';
			charForms[0x30637] = '\uFEC1';
			charForms[0x20637] = '\uFEC2';
			charForms[0x00637] = '\uFEC3';
			charForms[0x10637] = '\uFEC4';
			charForms[0x30638] = '\uFEC5';
			charForms[0x20638] = '\uFEC6';
			charForms[0x00638] = '\uFEC7';
			charForms[0x10638] = '\uFEC8';
			charForms[0x30639] = '\uFEC9';
			charForms[0x20639] = '\uFECA';
			charForms[0x00639] = '\uFECB';
			charForms[0x10639] = '\uFECC';
			charForms[0x3063A] = '\uFECD';
			charForms[0x2063A] = '\uFECE';
			charForms[0x0063A] = '\uFECF';
			charForms[0x1063A] = '\uFED0';
			charForms[0x30641] = '\uFED1';
			charForms[0x20641] = '\uFED2';
			charForms[0x00641] = '\uFED3';
			charForms[0x10641] = '\uFED4';
			charForms[0x30642] = '\uFED5';
			charForms[0x20642] = '\uFED6';
			charForms[0x00642] = '\uFED7';
			charForms[0x10642] = '\uFED8';
			charForms[0x30643] = '\uFED9';
			charForms[0x20643] = '\uFEDA';
			charForms[0x00643] = '\uFEDB';
			charForms[0x10643] = '\uFEDC';
			charForms[0x30644] = '\uFEDD';
			charForms[0x20644] = '\uFEDE';
			charForms[0x00644] = '\uFEDF';
			charForms[0x10644] = '\uFEE0';
			charForms[0x30645] = '\uFEE1';
			charForms[0x20645] = '\uFEE2';
			charForms[0x00645] = '\uFEE3';
			charForms[0x10645] = '\uFEE4';
			charForms[0x30646] = '\uFEE5';
			charForms[0x20646] = '\uFEE6';
			charForms[0x00646] = '\uFEE7';
			charForms[0x10646] = '\uFEE8';
			charForms[0x30647] = '\uFEE9';
			charForms[0x20647] = '\uFEEA';
			charForms[0x00647] = '\uFEEB';
			charForms[0x10647] = '\uFEEC';
			charForms[0x30648] = '\uFEED';
			charForms[0x20648] = '\uFEEE';
			charForms[0x30649] = '\uFEEF';
			charForms[0x20649] = '\uFEF0';
			charForms[0x3064A] = '\uFEF1';
			charForms[0x2064A] = '\uFEF2';
			charForms[0x0064A] = '\uFEF3';
			charForms[0x1064A] = '\uFEF4';
		}
	}
}
