using Backtester.backend;
using Backtester.backend.interfaces;
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
        public const string PROP_COND_STOP_MOVEL_C = "STOP_MOVEL_C";
        public const string PROP_COND_STOP_MOVEL_V = "STOP_MOVEL_V";
        public const string PROP_COND_STOP_INICIAL_C = "STOP_INICIAL_C";
        public const string PROP_COND_STOP_INICIAL_V = "STOP_INICIAL_V";

        public const string OBJ_TRADESYSTEM = "TRADESYSTEM";

        IList<string> listFormulas = new List<string>();
        IList<string> listVariaveis = new List<string>();
        private Config config1;

        public BTGPRunner(Config config, ICaller gpController)
            : base(LoadGPConfig())
        {
            this.config = config;
            this.gpController = gpController;
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
            listFormulas.Add(PROP_COND_STOP_MOVEL_C);
            listFormulas.Add(PROP_COND_STOP_MOVEL_V);
            listFormulas.Add(PROP_COND_STOP_INICIAL_C);
            listFormulas.Add(PROP_COND_STOP_INICIAL_V);

            template.AddProperty(PROP_COND_ENTRADA_C, listaFormula);
            template.AddProperty(PROP_COND_ENTRADA_V, listaFormula);
            template.AddProperty(PROP_COND_SAIDA_C, listaFormula);
            template.AddProperty(PROP_COND_SAIDA_V, listaFormula);
            template.AddProperty(PROP_COND_STOP_MOVEL_C, listaFormula);
            template.AddProperty(PROP_COND_STOP_MOVEL_V, listaFormula);
            template.AddProperty(PROP_COND_STOP_INICIAL_C, listaFormula);
            template.AddProperty(PROP_COND_STOP_INICIAL_V, listaFormula);

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
            TradeSystem ts = solution.GetPropriedade(OBJ_TRADESYSTEM) as TradeSystem;
            gpController.RunBackTester(ts);
        }

        public override void PrepareSolution(GPSolution solution)
        {
            TradeSystem ts = new TradeSystem(config);
            foreach (string var in listVariaveis)
            {
                ts.vm.SetVariavel(var, solution.GetValueAsNumber(var));
            }
            ts.condicaoEntradaC = solution.GetValueAsString(PROP_COND_ENTRADA_C);
            ts.condicaoEntradaV = solution.GetValueAsString(PROP_COND_ENTRADA_V);
            ts.condicaoSaidaC = solution.GetValueAsString(PROP_COND_SAIDA_C);
            ts.condicaoSaidaV = solution.GetValueAsString(PROP_COND_SAIDA_V);
            ts.stopMovelC = solution.GetValueAsString(PROP_COND_STOP_MOVEL_C);
            ts.stopMovelV = solution.GetValueAsString(PROP_COND_STOP_MOVEL_V);
            ts.stopInicialC = solution.GetValueAsString(PROP_COND_STOP_INICIAL_C);
            ts.stopInicialV = solution.GetValueAsString(PROP_COND_STOP_INICIAL_V);

            solution.SetPropriedade(OBJ_TRADESYSTEM, ts);
        }

        private static GPConfig LoadGPConfig()
        {
            return GPConfig.LoadSaved();

        }



        public Config config { get; set; }



        public ICaller gpController { get; set; }
    }
}
