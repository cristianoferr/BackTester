using Backtester.backend;
using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using Backtester.backend.model.system.estatistica;
using Backtester.interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UsoComum;
namespace Backtester.controller
{
    public class ConfigController : IController, ICaller
    {
        private IReferView frmPrincipal;
        public Config config { get; private set; }
        public FacadeBacktester facade;
        public FacadeBacktester facadeValidation;

        public ConfigController(IReferView frmPrincipal)
        {
            this.frmPrincipal = frmPrincipal;
            facade = new FacadeBacktester();
            facadeValidation = new FacadeBacktester();
            config = Config.LoadSaved();

            List<string> papeis = new List<string>();
            foreach (string papel in config.papeis)
            {
                papeis.Add(papel);
            }
            config.papeis = papeis;
            UpdateUI();
            ReloadPapeis();
        }

        private void ReloadPapeis()
        {
            frmPrincipal.ClearList("listPapeis");
            foreach (string papel in config.papeis.OrderBy(x => x.ToString()).ToList())
            {
                frmPrincipal.AddList("listPapeis",papel);
            }
        }

        public virtual void UpdateUI()
        {
            frmPrincipal.SetChecked("chkFlagCompra",config.flagCompra);
            frmPrincipal.SetChecked("chkFlagVenda",config.flagVenda);
            frmPrincipal.SetText("txtCapitalInicial",config.capitalInicial.ToString());
            frmPrincipal.SetText("txtCustoOperacao",config.custoOperacao.ToString());
            frmPrincipal.SetText("txtVarsDebug", config.varsDebug);
            frmPrincipal.SetText("txtTestesNaTela", config.maxTestes.ToString());
            frmPrincipal.SetText("textGPVars", config.gpVars);
            ReloadPapeis();

            frmPrincipal.SetChecked("radioTPDiario",config.tipoPeriodo == Consts.PERIODO_ACAO.DIARIO);

            frmPrincipal.SetChecked("radioTPSemanal",config.tipoPeriodo == Consts.PERIODO_ACAO.SEMANAL);

        }

        public void Salva()
        {
            UpdateValuesFromUI();
            config.SaveToFile();
        }

        public void UpdateValuesFromUI()
        {
            config.flagCompra = frmPrincipal.IsChecked("chkFlagCompra");
            config.flagVenda = frmPrincipal.IsChecked("chkFlagVenda");
            config.varsDebug = frmPrincipal.Text("txtVarsDebug");
            config.gpVars = frmPrincipal.Text("textGPVars");
            

            config.tipoPeriodo = frmPrincipal.IsChecked("radioTPDiario")? Consts.PERIODO_ACAO.DIARIO : Consts.PERIODO_ACAO.SEMANAL;

            try
            {
                config.maxTestes = int.Parse(frmPrincipal.Text("txtTestesNaTela"));
                config.capitalInicial = int.Parse(frmPrincipal.Text("txtCapitalInicial"));
                config.custoOperacao = int.Parse(frmPrincipal.Text("txtCustoOperacao"));
            }
            catch (System.Exception e)
            {
                Utils.Error(e.Message);
            }

        }

        internal void AdicionaPapel(string papel)
        {
            config.AddPapel(papel);
            ReloadPapeis();
            Salva();
        }

        internal void RemovePapel(string papel)
        {
            config.RemovePapel(papel);
            ReloadPapeis();
            Salva();
        }

        public virtual Carteira RunBackTester(TradeSystem ts, string name)
        {
            return null;
        }

        int contaTestes = 0;
        internal bool Run(TradeSystem ts)
        {
            contaTestes = 0;
            facade.LoadAtivos(config.papeis, config.tipoPeriodo,Consts.TIPO_CARGA_ATIVOS.GERA_CANDIDATOS);
            frmPrincipal.ClearRows("dataGridRuns");
            //facade.RunSingleTS();
            return facade.Run(this, config, ts)!=null;
        }

        internal Carteira RunSingle(TradeSystem ts,string name, Consts.TIPO_CARGA_ATIVOS tipoCarga)
        {
            
            if (tipoCarga==Consts.TIPO_CARGA_ATIVOS.VALIDA_CANDIDATO)
            {
                //valido com todos os papeis disponíveis
                config.qtdPercPapeis = 100;
                facadeValidation.LoadAtivos(config.papeisValidation, config.tipoPeriodo, tipoCarga);
                return facadeValidation.RunValidation( this, config, ts, name);
            } else
            {
                facade.LoadAtivos(config.papeis, config.tipoPeriodo, tipoCarga);
                return facade.RunSingle(name, this, config, ts);
            }
        }

        public void UpdateApplication(Carteira carteira, MonteCarlo mC, int countLoops, int totalLoops)
        {
            Application.DoEvents();
            if (totalLoops <= 0) totalLoops = 1;
            frmPrincipal.SetText("labelStatus",countLoops + " / " + totalLoops);
            //frmPrincipal.dataSetBacktest.Tables[0].Rows.Add(mC);
            //DataGridViewRow row = new DataGridViewRow();
            //row.Cells.Add(new DataGridViewCell());
            //frmPrincipal.dataGridRuns.Rows.Add(row);
            DataGridViewRowCollection Rows = frmPrincipal.GetRows("dataGridRuns");
            if (Rows == null) return;
            int rowLine = Rows.Count - 1;
            Rows.Add();
            int colIndex = 0;
            contaTestes++;
            SubSubDado todosTrades = mC.getGlobal().getGeral().getAmbasPontas().todosTrades;
            SubSubDado tradesGanhos = mC.getGlobal().getGeral().getAmbasPontas().tradesGanhos;
            SubSubDado tradesPerdidos = mC.getGlobal().getGeral().getAmbasPontas().tradesPerdidos;
            Rows[rowLine].Cells[colIndex++].Value = mC;
            Rows[rowLine].Cells[colIndex++].Value = carteira;
            Rows[rowLine].Cells[colIndex++].Value = contaTestes;
            Rows[rowLine].Cells[colIndex++].Value = mC.ToString();

            object objIters = mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_ITERATIONS);
            int iterations = (objIters == null ? 1 : (int)objIters);


            Rows[rowLine].Cells[colIndex++].Value = iterations;
            Rows[rowLine].Cells[colIndex++].Value = mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_TOTAL_PROFIT) == null ? 0 : (float)mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_TOTAL_PROFIT) / iterations;
            Rows[rowLine].Cells[colIndex++].Value = mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_TOTAL_LOSS) == null ? 0 : (float)mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_TOTAL_LOSS) / iterations;


            Rows[rowLine].Cells[colIndex++].Value = mC.CalcFitness();
            Rows[rowLine].Cells[colIndex++].Value = mC.getCapitalFinal();
            Rows[rowLine].Cells[colIndex++].Value = mC.getMaxCapital();
            Rows[rowLine].Cells[colIndex++].Value = mC.getMinCapital();
            Rows[rowLine].Cells[colIndex++].Value = mC.winLossRatio;
            Rows[rowLine].Cells[colIndex++].Value = mC.qtdTrades;
            Rows[rowLine].Cells[colIndex++].Value = tradesGanhos.getnTrades();
            Rows[rowLine].Cells[colIndex++].Value = tradesPerdidos.getnTrades();
            Rows[rowLine].Cells[colIndex++].Value = mC.totalGanho;
            Rows[rowLine].Cells[colIndex++].Value = mC.totalPerdido;
            Rows[rowLine].Cells[colIndex++].Value = mC.percAcerto;
            Rows[rowLine].Cells[colIndex++].Value = mC.capitalUsePercent;
            Rows[rowLine].Cells[colIndex++].Value = todosTrades.getMaxDias();
            Rows[rowLine].Cells[colIndex++].Value = todosTrades.getMinDias();
            Rows[rowLine].Cells[colIndex++].Value = todosTrades.getAvgDias();

            if (Rows.Count > config.maxTestes)
            {
                RemovePiorTeste();
            }
        }

        private void RemovePiorTeste()
        {
            DataGridViewRowCollection Rows = frmPrincipal.GetRows("dataGridRuns");
            MonteCarlo mc = Rows[0].Cells[0].Value as MonteCarlo;
            int piorIndex = 0;
            float piorResultado = mc.CalcFitness();
            for (int i = 1; i < Rows.Count; i++)
            {
                mc = Rows[i].Cells[0].Value as MonteCarlo;
                if (mc != null)
                {
                    float resultado = mc.CalcFitness();
                    if (resultado < piorResultado)
                    {
                        piorResultado = resultado;
                        piorIndex = i;
                    }
                }
            }
            Rows.RemoveAt(piorIndex);
        }

        public void SimpleUpdate()
        {
            Application.DoEvents();
        }

        internal void VarDebugChanged(string p)
        {
            config.varsDebug = p;
        }

        internal void SelectMonteCarlo(int p)
        {
            DataGridViewRowCollection RowsG = frmPrincipal.GetRows("dataGridRuns");
            MonteCarlo mc = RowsG[p].Cells[0].Value as MonteCarlo;
            if (mc == null)
            {
                return;
            }
            Carteira carteira = RowsG[p].Cells[1].Value as Carteira;
            Estatistica global = mc.getGlobal();
            // global.getGeral().getAmbasPontas().todosTrades.o
            frmPrincipal.ClearRows("dataGridOperacoes");
            IList<Posicao> posicoes = carteira.posicoesFechadas;
            float capital = config.capitalInicial;
            int contaOperacao = 0;



                DataGridViewRowCollection Rows = frmPrincipal.GetRows("dataGridOperacoes");
            for (int i = 0; i < mc.operacoes.Count; i++)
            {
                Operacao oper = mc.operacoes[i];
                Posicao posicao = oper.posicao;
                contaOperacao++;
                int rowLine = Rows.Count - 1;
                Rows.Add();
                int colIndex = 0;
                Rows[rowLine].Cells[colIndex++].Value = contaOperacao;
                Rows[rowLine].Cells[colIndex++].Value = posicao.idPosicao;
                string ativoName = posicao.ativo.name;
                if (ativoName.Contains("%5E")) ativoName = ativoName.Substring(ativoName.IndexOf("%5E" )+3);
                Rows[rowLine].Cells[colIndex++].Value = ativoName;
                Rows[rowLine].Cells[colIndex++].Value = oper.candleInicial.periodo;
                Rows[rowLine].Cells[colIndex++].Value = oper.candleFinal.periodo;
                Rows[rowLine].Cells[colIndex++].Value = posicao.direcao > 0 ? "Compra" : "Venda";
                Rows[rowLine].Cells[colIndex++].Value = oper.qtd;
                Rows[rowLine].Cells[colIndex++].Value = Utils.FormatCurrency(oper.vlrEntrada);
                Rows[rowLine].Cells[colIndex++].Value = Utils.FormatCurrency(oper.vlrSaida);
                Rows[rowLine].Cells[colIndex++].Value = Utils.FormatCurrency(oper.vlrStopInicial);
                Rows[rowLine].Cells[colIndex++].Value = oper.stopado ? "Sim" : "Não";
                Rows[rowLine].Cells[colIndex++].Value = oper.qtd * oper.vlrEntrada;
                Rows[rowLine].Cells[colIndex++].Value = oper.GetDif();
                Rows[rowLine].DefaultCellStyle.ForeColor = Color.Black;
                if (oper.GetDif() > 0)
                {
                    Rows[rowLine].DefaultCellStyle.BackColor = Color.Blue;
                    Rows[rowLine].DefaultCellStyle.ForeColor = Color.White;
                }
                if (oper.GetDif() < 0)
                {
                    Rows[rowLine].DefaultCellStyle.BackColor = Color.Red;
                    Rows[rowLine].DefaultCellStyle.ForeColor = Color.White;
                }
                capital += oper.GetDif() - 2 * config.custoOperacao;
                Rows[rowLine].Cells[colIndex++].Value = capital;
                Rows[rowLine].Cells[colIndex++].Value = oper.capitalOnClose;
                //  oper.
            }
        }

  
    }
}
