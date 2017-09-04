using Backtester.backend.model.system.condicoes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Backtester.backend.model.system.estatistica
{

    [DataContract]
    public class DadoEstatistico
    {
        [DataMember]
        float capital;
        [DataMember]
        SubDado compras, vendas, ambasPontas;


        public DadoEstatistico()
        {
        //    operacoes = new List<Operacao>();
            compras = new SubDado();
            vendas = new SubDado();
            ambasPontas = new SubDado();
        }

        public void print()
        {
            //System.out.println("Capital:"+capital);
            compras.print("Dado da ponta comprada");
            vendas.print("Dado da ponta vendida");
            ambasPontas.print("TODOS TRADES");
        }

        public void addOperacao(Operacao oper)
        {
            //		operacoes.add(oper);

            ambasPontas.addOperacao(oper);
            if (oper.direcao > 0)
                compras.addOperacao(oper);
            else
                vendas.addOperacao(oper);


        }

        public void atualizaDia(float capital)
        {
            this.capital = capital;

        }


        public float getCapital()
        {
            return capital;
        }

        public SubDado getCompras()
        {
            return compras;
        }

        public SubDado getVendas()
        {
            return vendas;
        }

        public SubDado getAmbasPontas()
        {
            return ambasPontas;
        }

    }
}
