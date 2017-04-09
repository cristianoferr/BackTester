
using GeneticProgramming.semantica;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public abstract class GPAbstractNode
    {

        public GPAbstractNode(semantica.GPSemantica semantica)
        {
            if (semantica == null)
            {
                throw new Exception("Semantica não definida!");
            }
            this.semantica = semantica;
            children = new List<GPAbstractNode>();

        }
        [DataMember]
        public GPAbstractNode nodePai { get; set; }
        [DataMember]
        public IList<GPAbstractNode> children { get; private set; }

        [DataMember]
        public GPSemantica semantica { get; internal set; }

        public virtual bool CanAddNode(GPAbstractNode nodeFilho)
        {
            if (nodeFilho.nodePai != null)
            {
                return false;
            }
            if (nodeFilho.ContainsNode(this))
            {
                return false;
            }
            if (semantica == null)
            {
                return false;
            }
            int count = children.Count;

            return semantica.CanAddNode(count, nodeFilho);
        }

        public bool ContainsNode(GPAbstractNode nodeToCheck)
        {
            if (this == nodeToCheck) return true;
            foreach (GPAbstractNode node in children)
            {
                if (node.ContainsNode(nodeToCheck))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddNode(GPAbstractNode nodeFilho)
        {
            if (CanAddNode(nodeFilho))
            {
                nodeFilho.nodePai = this;
                children.Add(nodeFilho);
            }
        }

        internal string NotacaoDoMeio()
        {
            if (children.Count == 0) return "Error!";
            if (children.Count == 1) return semantica.name + children[0];
            string ret = "(" + children[0].ToString() + ")";
            for (int i = 1; i < children.Count; i++)
            {
                ret += semantica.name + "(" + children[1] + ")";
            }
            return ret;
        }

        public override string ToString()
        {

            if (children.Count < semantica.minParams || children.Count > semantica.maxParams)
            {
                return "ERROR:" + semantica.name;
            }
            if (children.Count == 0)
            {
                return semantica.name;
            }
            string str = "";
            for (int i = 0; i < children.Count; i++)
            {
                str += children[i].ToString();
                str += ",";
            }
            str = str.Substring(0, str.LastIndexOf(","));
            string ret = string.Format("{0}({1})", semantica.name, str);
            return ret;
        }




        public bool TransferNode(GPAbstractNode oldNode, GPAbstractNode newNode, bool mirror = true)
        {
                if (newNode.ContainsNode(this))
                {
                    return false;
                }
                if (!children.Contains(oldNode))
                {
                    return false;
                }
                for (int i = 0; i < children.Count; i++)
                {
                    if (children[i] == oldNode)
                    {
                        children[i] = newNode;
                        if (mirror && newNode.nodePai != null)
                        {
                            newNode.nodePai.TransferNode(newNode, oldNode, false);

                        }
                        else
                        {
                            oldNode.nodePai = newNode.nodePai;
                            newNode.nodePai = this;
                        }
                    }
                }
                return true;
        }

        public int Size()
        {
            int size = 1;
            foreach (GPAbstractNode node in children)
            {
                size += node.Size();
            }
            return size;
        }

        public int SizeLevel(int sizeLevel = 1)
        {
            int maxSize = sizeLevel;
            foreach (GPAbstractNode node in children)
            {
                int size = node.SizeLevel(sizeLevel + 1);
                if (size > maxSize) maxSize = size;
            }
            return maxSize;
        }

        internal GPAbstractNode GetNthChild(int nth, ref int count)
        {
            count++;
            if (count == nth) { return this; }

            foreach (GPAbstractNode node in children)
            {
                if (node.GetNthChild(nth, ref count) != null)
                {
                    return node;
                }
            }
            return null;
        }

        public abstract void Mutate(SemanticaList semanticaList);

        public virtual GPAbstractNode Clone()
        {
            
            GPAbstractNode node = semantica.InstantiateEmpty();
            for (int i = 0; i < children.Count; i++)
            {
                node.AddNode(children[i].Clone());
            }
            return node;
        }
    }
}
