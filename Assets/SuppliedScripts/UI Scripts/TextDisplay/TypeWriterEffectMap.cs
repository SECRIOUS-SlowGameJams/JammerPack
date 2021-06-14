using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECRIOUS.Effects
{
    public static class TypeWriterEffectMap
    {
        public static int charsPerSecond = 15;
        public static int fastCharsPerSecond
        {
            get { return charsPerSecond * 3; }
            set { fastCharsPerSecond = value; }
        }

    }
}
