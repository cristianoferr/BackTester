﻿
using GeneticProgramming.semantica;
using System.Globalization;
using System.Runtime.Serialization;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public class GPNodeNumber : GPAbstractNode
    {

        public GPNodeNumber(GPSemantica semanticaNumber, float vlrInicial)
            : base(semanticaNumber, GPConsts.GPNODE_TYPE.NODE_NUMBER)
        {
            this.valor = vlrInicial;
        }

        [DataMember]
        public float valor { get; set; }

        public override string ToString()
        {
            return valor.ToString("#.###", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public GPSemanticaNumber semanticaNumber
        {
            get
            {
                return semantica as GPSemanticaNumber;
            }
        }
    }
}