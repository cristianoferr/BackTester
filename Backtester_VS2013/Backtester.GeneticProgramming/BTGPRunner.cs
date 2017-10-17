using Backtester.backend;
using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.system;
using GeneticProgramming;
using GeneticProgramming.semantica;
using GeneticProgramming.solution;
using System;
using System.Collections.Generic;
using UsoComum;

namespace Backtester.GeneticProgramming
{
    public class BTGPRunner : GPRunner
    {
        //formulas
        
            public const string PROP_SIZING_C = "PROP_SIZING_C";
        public const string PROP_SIZING_V = "PROP_SIZING_V";
        public const string PROP_COND_ENTRADA_C = "COND_ENTRADA_C";
        public const string PROP_COND_ENTRADA_V = "COND_ENTRADA_V";
        public const string PROP_COND_SAIDA_C = "COND_SAIDA_C";
        public const string PROP_COND_SAIDA_V = "COND_SAIDA_V";
        public const string PROP_COND_STOP_MOVEL_C = "STOP_MOVEL_C";
        public const string PROP_COND_STOP_MOVEL_V = "STOP_MOVEL_V";
        public const string PROP_COND_STOP_INICIAL_C = "STOP_INICIAL_C";
        public const string PROP_COND_STOP_INICIAL_V = "STOP_INICIAL_V";



        IList<string> listFormulas = new List<string>();
        IList<string> listVariaveis = new List<string>();

        public BTGPRunner(Config config, ICaller gpController)
            : base(LoadGPConfig(),config.tipoPeriodo.ToString())
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
            SemanticaList listaBooleanos = definitions.GetListByName(GPConsts.LISTA_NUMEROS_BOOLEANOS);
            SemanticaList listaNumerosGrandes = definitions.GetListByName(GPConsts.LISTA_NUMEROS_GRANDES);
            SemanticaList listaNumerosPercentuais = definitions.GetListByName(GPConsts.LISTA_NUMEROS_PERCENTUAIS);
            //   template.AddProperty(PROP_COND_ENTRADA_C,)

            if (config.flagCompra)
            {
                listFormulas.Add(PROP_COND_ENTRADA_C);
                listFormulas.Add(PROP_SIZING_C);

                listFormulas.Add(PROP_COND_SAIDA_C);
                listFormulas.Add(PROP_COND_STOP_MOVEL_C);
                listFormulas.Add(PROP_COND_STOP_INICIAL_C);
                template.AddProperty(PROP_COND_ENTRADA_C, listaFormula);
                template.AddProperty(PROP_SIZING_C, listaFormula);
                template.AddProperty(PROP_COND_SAIDA_C, listaFormula);
                template.AddProperty(PROP_COND_STOP_MOVEL_C, listaFormula);
                template.AddProperty(PROP_COND_STOP_INICIAL_C, listaFormula);
            }

            if (config.flagVenda)
            {
                listFormulas.Add(PROP_SIZING_V);

                listFormulas.Add(PROP_COND_ENTRADA_V);
                listFormulas.Add(PROP_COND_SAIDA_V);
                listFormulas.Add(PROP_COND_STOP_MOVEL_V);
                listFormulas.Add(PROP_COND_STOP_INICIAL_V);
                template.AddProperty(PROP_COND_ENTRADA_V, listaFormula);
                template.AddProperty(PROP_SIZING_V, listaFormula);
                template.AddProperty(PROP_COND_SAIDA_V, listaFormula);
                template.AddProperty(PROP_COND_STOP_MOVEL_V, listaFormula);
                template.AddProperty(PROP_COND_STOP_INICIAL_V, listaFormula);
            }


            listVariaveis.Add(Consts.VAR_STOP_GAP);
            listVariaveis.Add(Consts.VAR_RISCO_TRADE);
            listVariaveis.Add(Consts.VAR_STOP_MENSAL);
            listVariaveis.Add(Consts.VAR_MAX_CAPITAL_TRADE);
            listVariaveis.Add(Consts.VAR_PERC_TRADE);
            listVariaveis.Add(Consts.VAR_USA_STOP_MOVEL);
            listVariaveis.Add(Consts.VAR_RISCO_GLOBAL);
            listVariaveis.Add(Consts.VAR_MULTIPLAS_ENTRADAS);
         //   listVariaveis.Add(Consts.VAR_MULTIPLAS_SAIDAS);

            template.AddProperty(Consts.VAR_STOP_GAP, listaNumerosPercentuais);
            template.AddProperty(Consts.VAR_RISCO_TRADE, listaNumerosPercentuais);
            template.AddProperty(Consts.VAR_STOP_MENSAL, listaNumerosPercentuais);
            template.AddProperty(Consts.VAR_MAX_CAPITAL_TRADE, listaNumerosGrandes);
            template.AddProperty(Consts.VAR_PERC_TRADE, listaNumerosPercentuais);
            template.AddProperty(Consts.VAR_USA_STOP_MOVEL, listaBooleanos);
            template.AddProperty(Consts.VAR_RISCO_GLOBAL, listaNumerosPercentuais);
            template.AddProperty(Consts.VAR_MULTIPLAS_ENTRADAS, listaBooleanos);
            template.AddProperty(Consts.VAR_MULTIPLAS_SAIDAS, listaBooleanos);

            return template;
        }

        public override void RunSolution(GPSolution solution)
        {
            TradeSystem ts = solution.GetPropriedade(ConstsComuns.OBJ_TRADESYSTEM) as TradeSystem;
            ts.name = solution.name;
            Carteira carteira = gpController.RunBackTester(ts, solution.name);

            carteira.monteCarlo.properties = solution;

            float dif = carteira.GetCapital() - carteira.capitalInicial;
            if (dif > 0)
            {
                float? vExist = solution.GetPropriedade(ConstsComuns.OBJ_TOTAL_PROFIT) as float?;
                float v = vExist == null ? 0 : (float)vExist;
                v += dif;
                solution.SetPropriedade(ConstsComuns.OBJ_TOTAL_PROFIT, v);
            }
            else
            {
                float? vExist = solution.GetPropriedade(ConstsComuns.OBJ_TOTAL_LOSS) as float?;
                float v = vExist == null ? 0 : (float)vExist;
                v += dif;
                solution.SetPropriedade(ConstsComuns.OBJ_TOTAL_LOSS, v);
            }

            solution.iterations++;
            solution.SetPropriedade(ConstsComuns.OBJ_ITERATIONS, solution.iterations);
            solution.fitnessResult = carteira.monteCarlo.CalcFitness();
        }

        public override void EndSolution(GPSolution solution)
        {
            solution.RemovePropriedade(UsoComum.ConstsComuns.OBJ_MONTECARLO);

            if (solution.iterations >= config.saveMinIterations)
            {

                float totalProfit = solution.GetPropriedadeAsFloat(UsoComum.ConstsComuns.OBJ_TOTAL_PROFIT);
                float totalLoss = Math.Abs(solution.GetPropriedadeAsFloat(UsoComum.ConstsComuns.OBJ_TOTAL_LOSS));
                float profitRatio = totalProfit / (totalProfit + totalLoss + 1);
                if (profitRatio > config.saveMinProfitRatio)
                {
                    GPSolutionProxy proxy = new GPSolutionProxy();
                    proxy.solution = solution;
                    proxy.tradeSystem = solution.GetPropriedade(UsoComum.ConstsComuns.OBJ_TRADESYSTEM) as TradeSystem;
                    solution.RemovePropriedade(UsoComum.ConstsComuns.OBJ_TRADESYSTEM);
                    SaveSolutionToCheck(proxy);
                }
            }

        }

        private void SaveSolutionToCheck(GPSolutionProxy proxy)
        {
            Utils.CreateFolder(GPConsts.DIRECTORY_TO_CHECK);
            Utils.CreateFolder(GPConsts.DIRECTORY_TO_CHECK + config.tipoPeriodo.ToString());
            proxy.SaveToFile(GPConsts.DIRECTORY_TO_CHECK + config.tipoPeriodo.ToString() + "/" + proxy.solution.name + ".json");
        }

        public override void PrepareSolution(GPSolution solution)
        {
            TradeSystem ts = new TradeSystem(config);
            ts.name = solution.name;
            foreach (string var in listVariaveis)
            {
                if (config.IsGPVarDefined(var))
                {
                    ts.vm.SetVariavel(var, config.GetGPVarValue(var));
                }
                else
                {
                    ts.vm.SetVariavel(var, solution.GetValueAsNumber(var));
                }
            }
            if (config.flagCompra)
            {
                ts.condicaoEntradaC = solution.GetValueAsString(PROP_COND_ENTRADA_C);
                ts.condicaoSaidaC = solution.GetValueAsString(PROP_COND_SAIDA_C);
                ts.stopMovelC = solution.GetValueAsString(PROP_COND_STOP_MOVEL_C);
                ts.stopInicialC = solution.GetValueAsString(PROP_COND_STOP_INICIAL_C);
                ts.sizingCompra = solution.GetValueAsString(PROP_SIZING_C);
            }
            if (config.flagVenda)
            {
                ts.condicaoEntradaV = solution.GetValueAsString(PROP_COND_ENTRADA_V);
                ts.condicaoSaidaV = solution.GetValueAsString(PROP_COND_SAIDA_V);
                ts.stopMovelV = solution.GetValueAsString(PROP_COND_STOP_MOVEL_V);
                ts.stopInicialV = solution.GetValueAsString(PROP_COND_STOP_INICIAL_V);
                ts.sizingVenda= solution.GetValueAsString(PROP_SIZING_V);
            }
            solution.SetPropriedade(ConstsComuns.OBJ_TRADESYSTEM, ts);
        }

        private static GPConfig LoadGPConfig()
        {
            return GPConfig.LoadSaved();

        }



        public Config config { get; set; }



        public ICaller gpController { get; set; }
    }
}
