
using GeneticProgramming.nodes;
using System;
using System.Collections.Generic;
using System.Linq;
namespace GeneticProgramming.semantica
{
    public class SemanticaList
    {
        public SemanticaList(string listName, int minLevel, int maxLevel)
        {
            this.name = listName;
            lista = new List<GPSemantica>();
            this.minLevels = minLevel;
            this.maxLevels = maxLevel;

        }
        string name { get; set; }
        IList<GPSemantica> lista { get; set; }


        public bool Contains(GPSemantica semantica)
        {
            return lista.Contains(semantica);
        }

        internal void Add(GPSemantica semantica)
        {
            lista.Add(semantica);
        }


        static Random rnd = new Random();
        public GPAbstractNode CreateRandomNode(GPConfig config, bool flagTerminal, GPAbstractNode parent = null, int countLevel = 0)
        {
            GPAbstractNode ret = null;
            do
            {
                ret = InternalCreateRandomNode(config, flagTerminal, parent, countLevel);
                if (ret.SizeLevel() > maxLevels || ret.SizeLevel() < minLevels)
                {
                    ret = null;
                }

            } while (ret == null);
            return ret;
        }
        public GPAbstractNode InternalCreateRandomNode(GPConfig config, bool flagTerminal, GPAbstractNode parent = null, int countLevel = 0)
        {
            int maxNodes = config.maxLevels;


            GPSemantica semantica = GetRandomSemantica(flagTerminal);
            GPAbstractNode node = semantica.InstantiateEmpty();
            if (parent == null)
            {
                parent = node;
            }

            int rndVal = rnd.Next(semantica.minParams, semantica.maxParams);

            for (int i = 0; i < rndVal; i++)
            {
                GPAbstractNode nodeParam = InternalCreateRandomNode(config, countLevel + 1 > config.maxLevels, parent, countLevel + 1);
                if (!node.CanAddNode(nodeParam))
                {
                    throw new Exception("Node não permite adicionar nodeParam!");
                }
                node.AddNode(nodeParam);
            }


            return node;
        }


        private GPSemantica GetRandomSemantica(bool flagTerminal)
        {
            IList<GPSemantica> subLista = lista.Where(x => (!flagTerminal || (x.IsTerminal && flagTerminal))).ToList();
            if (subLista.Count == 0)
            {
                subLista = lista;
            }
            GPSemantica semantica = subLista[rnd.Next(subLista.Count)];
            return semantica;
        }


        int maxLevels_ = 1;
        public int maxLevels
        {
            get
            {
                return maxLevels_;
            }

            set
            {
                if (value < 1) value = 1;
                maxLevels_ = value;
            }

        }

        int minLevels_ = 1;
        public int minLevels
        {
            get
            {
                return minLevels_;
            }

            set
            {
                if (value < 1) value = 1;
                minLevels_ = value;
            }

        }
    }
}
