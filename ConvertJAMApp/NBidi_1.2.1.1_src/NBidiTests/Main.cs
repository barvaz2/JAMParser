using System;
using System.Collections.Generic;
using System.Text;

namespace NBidiTests
{
    class TestsMain
    {
        static void Main(string[] args)
        {
            NBidi.NBidiTests tests = new NBidi.NBidiTests();
            tests.Test01();
            //tests.Test27_LRE_Letters_Only();
            //tests.Test18_LRE();
            //tests.Test19_RLE();

            //tests.Test22_Indexes_1_NoComposition();
            //tests.Test23_Indexes_2_NoComposition_RLM();
            //tests.Test23_Indexes_3_Composition();
            //tests.Test24_Decomposition_and_Recomposition();
            //tests.Test20_Newlines_1();
        }

    }
}
