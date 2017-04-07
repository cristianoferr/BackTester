using Backtester.backend;
using Backtester.backend.model.system;
using GeneticProgramming;
using GeneticProgramming.semantica;
using GeneticProgramming.solution;
using System.Collections.Generic;

namespace Backtester.GeneticProgramming
{
    public class BTGPRunner : GPRunner
    {
        //formulas
        public const string PROP_COND_ENTRADA_C = "COND_ENTRADA_C";
        public const string PROP_COND_ENTRADA_V = "COND_ENTRADA_V";
        public const string PROP_COND_SAIDA_C = "COND_SAIDA_C";
        public const string PROP_COND_SAIDA_V = "COND_SAIDA_V";
        public const string PROP_COND_STOP_MOVEL = "STOP_MOVEL";
        public const string PROP_COND_STOP_INICIAL = "STOP_INICIAL";

        public const string OBJ_TRADESYSTEM = "TRADESYSTEM";

        IList<string> listFormulas = new List<string>();
        IList<string> listVariaveis = new List<string>();

        public BTGPRunner(Config config)
            : base(LoadGPConfig())
        {
            this.config = config;
        }
        public override GPSolutionDefinition CreateSolutionDefinition()
        {
            GPSolutionDefinition def = new BackTesterSemantica(gpConfig);
            return def;
        }

        public override GPTemplate CreateTemplate()
        {
            GPTemplate template = new GPTemplate(gpConfig);

            SemanticaList listaFormula = definitions.GetListByName(GPConsts.LISTA_FORMULA);
            SemanticaList listaNumeros = definitions.GetListByName(GPConsts.LISTA_NUMEROS);
            //   template.AddProperty(PROP_COND_ENTRADA_C,)
            listFormulas.Add(PROP_COND_ENTRADA_C);
            listFormulas.Add(PROP_COND_ENTRADA_V);
            listFormulas.Add(PROP_COND_SAIDA_C);
            listFormulas.Add(PROP_COND_SAIDA_V);
            listFormulas.Add(PROP_COND_STOP_MOVEL);
            listFormulas.Add(PROP_COND_STOP_INICIAL);

            template.AddProperty(PROP_COND_ENTRADA_C, listaFormula);
            template.AddProperty(PROP_COND_ENTRADA_V, listaFormula);
            template.AddProperty(PROP_COND_SAIDA_C, listaFormula);
            template.AddProperty(PROP_COND_SAIDA_V, listaFormula);
            template.AddProperty(PROP_COND_STOP_MOVEL, listaFormula);
            template.AddProperty(PROP_COND_STOP_INICIAL, listaFormula);

            listVariaveis.Add(Consts.VAR_STOP_GAP);
            listVariaveis.Add(Consts.VAR_RISCO_TRADE);
            listVariaveis.Add(Consts.VAR_STOP_MENSAL);
            listVariaveis.Add(Consts.VAR_MAX_CAPITAL_TRADE);
            listVariaveis.Add(Consts.VAR_PERC_TRADE);
            listVariaveis.Add(Consts.VAR_USA_STOP_MOVEL);
            listVariaveis.Add(Consts.VAR_RISCO_GLOBAL);
            listVariaveis.Add(Consts.VAR_MULTIPLAS_ENTRADAS);

            template.AddProperty(Consts.VAR_STOP_GAP, listaNumeros);
            template.AddProperty(Consts.VAR_RISCO_TRADE, listaNumeros);
            template.AddProperty(Consts.VAR_STOP_MENSAL, listaNumeros);
            template.AddProperty(Consts.VAR_MAX_CAPITAL_TRADE, listaNumeros);
            template.AddProperty(Consts.VAR_PERC_TRADE, listaNumeros);
            template.AddProperty(Consts.VAR_USA_STOP_MOVEL, listaNumeros);
            template.AddProperty(Consts.VAR_RISCO_GLOBAL, listaNumeros);
            template.AddProperty(Consts.VAR_MULTIPLAS_ENTRADAS, listaNumeros);

            return template;
        }

        public override void RunSolution(GPSolution solution)
        {

        }

        public override void PrepareSolution(GPSolution solution)
        {
            TradeSystem ts = new TradeSystem(config);
            foreach (string var in listVariaveis)
            {
                ts.vm.SetVariavel(var, solution.GetValueAsNumber(var));
            }
            solution.SetPropriedade(OBJ_TRADESYSTEM, ts);
        }

        private static GPConfig LoadGPConfig()
        {
            return GPConfig.LoadSaved();

        }



        public Config config { get; set; }
    }
}
