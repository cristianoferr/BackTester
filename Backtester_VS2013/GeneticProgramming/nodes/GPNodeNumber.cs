﻿
using GeneticProgramming.semantica;
using System.Globalization;
using System.Runtime.Serialization;
using UsoComum;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public class GPNodeNumber : GPAbstractNode
    {

        public GPNodeNumber(GPSemantica semanticaNumber, float vlrInicial)
            : base(semanticaNumber)
        {
            this.valor = vlrInicial;
        }


        public override GPAbstractNode Clone()
        {
            GPNodeNumber node = base.Clone() as GPNodeNumber;
            node.valor = valor;
            return node;
        }
        public override void Mutate(SemanticaList semanticaList)
        {
            valor += Utils.Random(- 2, + 2);
        }

        float valor_;
        [DataMember]
        public float valor { get{
            return valor_;
        }
            set{
                if (semanticaNumber != null)
                {
                    if (value > semanticaNumber.maxValue)
                    {
                        value = semanticaNumber.maxValue;
                    }
                    if (value < semanticaNumber.minValue)
                    {
                        value = semanticaNumber.minValue;
                    }
                }

                valor_ = value;
            }
        }

        public override string ToString()
        {
            return valor.ToString("0.###", CultureInfo.CreateSpecificCulture("en-US"));
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
