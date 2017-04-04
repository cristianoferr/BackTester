
using GeneticProgramming.semantica;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace GeneticProgramming.nodes
{
    [DataContract]
    public abstract class GPAbstractNode
    {



        public GPAbstractNode(semantica.GPSemantica semantica, GPConsts.GPNODE_TYPE nodeType)
        {
            if (semantica == null)
            {
                throw new Exception("Semantica não definida!");
            }
            this.semantica = semantica;
            this.nodeType = nodeType;
            children = new List<GPAbstractNode>();

        }
        [DataMember]
        public GPAbstractNode nodePai { get; set; }
        [DataMember]
        public IList<GPAbstractNode> children { get; private set; }

        [DataMember]
        public GPSemantica semantica { get; private set; }

        [DataMember]
        public GPConsts.GPNODE_TYPE nodeType { get; private set; }

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

        public override string ToString()
        {

            if (children.Count == 0)
            {
                if (semantica.propriedades.Count > 0)
                {
                    return "ERROR:" + semantica.name;
                }
                return semantica.name;
            }
            string str = "";
            for (int i = 0; i < children.Count; i++)
            {
                str += children[i].ToString();
                if (i < children.Count - 1)
                {
                    str += ",";
                }
            }
            string ret = string.Format("{0}({1})", semantica.name, str);
            return ret;
        }




        public bool TransferNode(GPAbstractNode oldNode, GPAbstractNode newNode, bool mirror = true)
        {
            if (oldNode.nodeType == newNode.nodeType)
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
            return false;
        }
    }
}
