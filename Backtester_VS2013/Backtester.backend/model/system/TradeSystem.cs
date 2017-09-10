
using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using Backtester.backend.model.system.condicoes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Backtester.backend.model.system
{
    [DataContract]
    [KnownType(typeof(TradeSystem))]
    public class TradeSystem
    {
        

        /*A diferença entre stopInicial e stopMovel: 
a formula stopInicial será usada para determinar o Stop Inicial  e o stopMovel é usado para determinar o stop daquele dia
E caso o valor do stopMovel seja menor que o stopInicial então o valor retornado será o stopInicial
*/

        [DataMember]
        public VariavelManager vm { get; private set; }

        public TradeSystem(Config config)
        {
            mensagens = new List<string>();
            vm = new VariavelManager();
            stopInicialC = "SUBTRACT(L,MULTIPLY(" + FormulaManager.STDDEV + "(C,10),2))";
            stopInicialV = "SUM(H,MULTIPLY(" + FormulaManager.STDDEV + "(C,10),2))";//"HV(H,6)";
            stopMovelC = stopInicialC;
            stopMovelV = stopInicialV;
            GetVariavel(Consts.VAR_STOP_GAP, "Usado para dar algum espaçamento entre a formula e o valor realmente usado", 0, 5, 20); //Usado para dar algum espaçamento entre a formula e o valor realmente usado
            GetVariavel(Consts.VAR_RISCO_TRADE, "Valor percentual máximo que um trade pode ter de risco", 0, 5, 20);
            GetVariavel(Consts.VAR_RISCO_GLOBAL, "Valor percentual máximo que pode ficar exposto", 0, 5, 20);
            GetVariavel(Consts.VAR_STOP_MENSAL, "Valor percentual que caso atingido implica em paralisar as operações para aquele mês", 0f, 5, 30);
            GetVariavel(Consts.VAR_MAX_CAPITAL_TRADE, "Valor absoluto máximo que um trade pode ter", 10000f, 5, 100000);
            GetVariavel(Consts.VAR_PERC_TRADE, "informa quanto do capital eu posso ter por ativo", 10, 5, 100);
            GetVariavel(Consts.VAR_USA_STOP_MOVEL, "Usa stop movel? 0=nao, 1=sim", 0, 1, 1);
            GetVariavel(Consts.VAR_MULTIPLAS_ENTRADAS, "A posição pode ter mais de uma entrada(operacao)? 0=nao, 1=sim", 0, 1, 1);
            GetVariavel(Consts.VAR_MULTIPLAS_SAIDAS, "A posição pode ter mais de uma saida(operacao)? 0=nao, 1=sim", 0, 5, 20);

            /*  condicaoEntradaC = new CondicaoComplexa();
              condicaoEntradaV = new CondicaoComplexa();
              condicaoSaidaC = new CondicaoComplexa();
              condicaoSaidaV = new CondicaoComplexa();*/

            string entrada = "GREATER(MME(C,9),MME(C,6))";
            string saida = "LOWER(MME(C,9),MME(C,6))";
            /*condicaoEntradaC.ChangeCondicao(entrada);
            condicaoSaidaC.ChangeCondicao(saida);
            condicaoEntradaV.ChangeCondicao(saida);
            condicaoSaidaV.ChangeCondicao(entrada);*/
            condicaoEntradaC = entrada;
            condicaoSaidaC = saida;
            condicaoEntradaV = saida;
            condicaoSaidaV = entrada;

        }

        #region propriedades




        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string stopInicialC { get; set; }
        [DataMember]
        public string stopInicialV { get; set; }
        [DataMember]
        public string stopMovelC { get; set; }

        [DataMember]
        public IList<string> mensagens { get; private set; }
        internal void AddError(string v)
        {
            if (!mensagens.Contains(v)) mensagens.Add("ERROR:" + v);
        }
        internal void AddWarning(string v)
        {
            if (!mensagens.Contains(v)) mensagens.Add("WARNING:" + v);
        }

        [DataMember]
        public string stopMovelV { get; set; }


        [DataMember]
        public string sizingCompra { get; set; }
        [DataMember]
        public string sizingVenda { get; set; }

        [DataMember]
        public string condicaoEntradaC { get; set; }

        [DataMember]
        public string condicaoSaidaC { get; set; }
        [DataMember]
        public string condicaoEntradaV { get; set; }

        [DataMember]
        public string condicaoSaidaV { get; set; }
        /*  [DataMember]
          public CondicaoComplexa condicaoEntradaC { get; private set; }

          [DataMember]
          public CondicaoComplexa condicaoSaidaC { get; private set; }
          [DataMember]
          public CondicaoComplexa condicaoEntradaV { get; private set; }

          [DataMember]
          public CondicaoComplexa condicaoSaidaV { get; private set; }
          */
        #endregion
        /*
         * Esse método verifica se a entrada foi ativada para o candle atual
         * O metodo retorna onde deve ficar o stop, e se for positivo é compra e negativo é venda
         */
        public float checaCondicaoEntrada(Candle candle, Config config)
        {
            if ((config.flagCompra && condicaoEntradaC != null))
            {
                float valor = candle.GetValor(vm.ReplaceVariavel(condicaoEntradaC));
                if (valor > 0)
                    return Math.Abs(Stop.CalcValorStop(candle.GetValor(stopInicialC),1, stopGapPerc));
                //return candle.GetValor(stopInicialC) * (1f - stopGapPerc / 100f);
            }
            if ((config.flagVenda && condicaoEntradaV != null))
            {
                float valor = candle.GetValor(vm.ReplaceVariavel(condicaoEntradaV));
                if (valor > 0)
                    return -Math.Abs(Stop.CalcValorStop(candle.GetValor(stopInicialC), -1, stopGapPerc));
                //return -candle.GetValor(stopInicialV) * (1f + stopGapPerc / 100f);
            }
            return 0;
        }

        public float checaCondicaoSaida(Candle candle, int direcao)
        {
            //SE for compra e ativou a condicao de saida
            if (direcao > 0)
            {
                float valor = candle.GetValor(vm.ReplaceVariavel(condicaoSaidaC));
                if (valor >= 1)
                {
                    return 1;
                }
            }
            if (direcao < 0)
            {
                float valor = candle.GetValor(vm.ReplaceVariavel(condicaoSaidaV));
                if (valor >= 1) return 1;
            }
            return 0;
        }


        public Variavel GetVariavel(string name, string descricao, float vlrInicial, int step, float vlrFinal)
        {
            return vm.GetVariavel(name, descricao, vlrInicial, step, vlrFinal);
        }

        #region getters

        public bool usaStopMovel
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_USA_STOP_MOVEL).vlrAtual == 1;
            }
        }

        public bool usaMultiplasEntradas
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_MULTIPLAS_ENTRADAS).vlrAtual == 1;
            }
        }


        public bool usaMultiplasSaidas
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_MULTIPLAS_SAIDAS).vlrAtual == 1;
            }
        }


        

        public float riscoGlobal
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_RISCO_GLOBAL).vlrAtual;
            }
        }
        public float riscoTrade
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_RISCO_TRADE).vlrAtual;
            }
        }
        public float stopMensal
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_STOP_MENSAL).vlrAtual;
            }
        }

        public override string ToString()
        {
            return name;
        }

        public float maxCapitalTrade
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_MAX_CAPITAL_TRADE).vlrAtual;
            }
        }
        public float percTrade
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_PERC_TRADE).vlrAtual;
            }
        }


        public float stopGapPerc
        {
            get
            {
                return vm.GetVariavel(Consts.VAR_STOP_GAP).vlrAtual;
            }
        }


        public string GetStopMovel(int sentido)
        {
            if (!usaStopMovel)
            {
                return null;
            }
            if (sentido > 0) return stopMovelC;
            return stopMovelV;
        }

        public string GetStopInicial(int sentido)
        {
            if (sentido > 0) return stopInicialC;
            return stopInicialV;
        }

        #endregion




    }
}
