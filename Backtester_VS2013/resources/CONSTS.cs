﻿using System;
using System.Collections.Generic;
using System.Text;

namespace netAvida.backend
{
    public class CONSTS
    {
        public static float MAX_MEMORY_CHILD = 1.5f;
        public static float MIN_MEMORY_CHILD = 0.5f;
        public static string GENEBANK_PATH="genebank/";

	    public static int IP_REG = 3;

        public static int GRAPH_WIDTH = 900;
        public static int GRAPH_SIZE = 1;

        public static int TEMPLATE_LIMIT = 1024;

        public static int REGISTRADORES = 10;
        public static int STACKS = 2;

        internal static float random()
        {
            throw new NotImplementedException();
        }

        public static int MAX_STACK = 32;//qtd maxima por pilha
        public static int MAX_BUFFER = 16;

        //limite superior e inferior do erro 
        public static int ERROR_UPPER_LIMIT = 10000;

        public static int AUTO_SAVE_PROGRAM_WITH_CHILD_COUNT = 10;
        public static int MAX_NEIGHBOURS = 8;
        public static int MAX_SAVED_PROGRAMS = 100;

        public static float FITNESS_TO_ENERGY_RATIO = 10;

        public static string numberFormat(int v)
        {
            return v + "[" + toHexString(v % REGISTRADORES) + "]";
        }

        public static String toHexString(int v)
        {
            String hexString = v.ToString("X");
            if (hexString.Length < 2)
            {
                hexString = "0" + hexString;
            }
            return hexString.ToUpper();
        }

        public static int MUTTYPE_OCCUPATION_RATIO = 1;
        public static int MUTTYPE_POSITIONAL_CENTER = 2;


        public static int IMAGE_WIDTH = 1248;
        public static int IMAGE_HEIGTH = 1248;

        //preenchido automaticamente
        public static int NOP0 = 0;
        public static int NOP1 = 0;

        internal static string getLetter(int i)
        {
            return Char.ToString((char)(65 + i % REGISTRADORES));
        }
    }
}
