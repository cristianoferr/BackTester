using System;
using System.Collections.Generic;

namespace Backtester.backend.DataManager
{
    public class DataDTO
    {
        public string date;
        public float a, b, c, d, e, f, g, h, ab, ac, ad, ae, af, an, ap, aq, av, aw;

        public float open { get { return a; } }

        public float close { get { return d; } }

        public float high { get { return b; } }

        public float low { get { return c; } }

        public float vol { get { return e; } }
    }
   

    public class CargaADVFN
    {
        public List<Int64> t;//time
        public List<float> o;//open
        public List<float> h;//high
        public List<float> l;//low
        public List<float> c;//close
        public List<Int64> v;//volume
        public string s;
    }
}
