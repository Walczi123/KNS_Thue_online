using System;
using System.Collections.Generic;
using System.Text;
using ThueOnline.Game;

namespace GameTest
{
    public static class ExtensionMethods
    {
        public static bool CheckAlphabet(this String str, Alphabet alph)
        {
            foreach(var ch in str)
            {
                if (!alph.Letters.Contains(ch))
                    return false;
            }
            return true;
        }
    }
}
