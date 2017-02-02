﻿using System.Collections.Generic;

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
    public class ObjectDTO
    {
        string id;
        string tool;
        string panel;
        string x0;
        string y0;
        string x;
        string y;
        string color;
        string text;
    }
    public class CargaDTO
    {
        string descr;
        bool favorite;
        string feed;
        int precision;
        string ds;
        List<ObjectDTO> objects;
        public List<DataDTO> data;
    }
}
