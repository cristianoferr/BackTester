
using GeneticProgramming.nodes;
using System.Linq;
using System.Collections.Generic;
using System;
namespace GeneticProgramming.semantica
{
    public class SemanticaList
    {
        public SemanticaList(string listName)
        {
            this.name = listName;
            lista = new List<GPSemantica>();
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

        public GPAbstractNode CreateRandomNode(GPConfig config,GPConsts.GPNODE_TYPE nodeType,bool flagTerminal,GPAbstractNode parent=null)
        {
            int maxNodes = config.maxNodes;
            Random rnd = new Random();

            IList<GPSemantica> subLista = lista.Where(x => (x.nodeType == nodeType || nodeType==GPConsts.GPNODE_TYPE.ANY) && (!flagTerminal || (x.IsTerminal && flagTerminal))).ToList();
            GPSemantica semantica = subLista[rnd.Next(subLista.Count)];
            GPAbstractNode node = semantica.InstantiateEmpty();
            if (parent == null)
            {
                parent = node;
            }
            for (int i = 0; i < semantica.propriedades.Count();i++ )
            {
                GeneticProgramming.GPConsts.GPNODE_TYPE nodeTypeRequired = semantica.NextNodeType(i);
                GPAbstractNode nodeParam = CreateRandomNode(config, nodeTypeRequired, parent.Size() >= config.maxNodes, parent);
                if (!node.CanAddNode(nodeParam))
                {
                    throw new Exception("Node não permite adicionar nodeParam!");
                }
                node.AddNode(nodeParam);
            }
           /* if (parent != null )
            {
                //if (parent.Size() <= config.maxNodes)
                //{
            //        parent.AddNode(node);
                }
                else
                {
                    return null;
                }
            }*/


            return node;
        }
    }
}
