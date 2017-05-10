using Backtester.backend;
using Backtester.backend.interfaces;
using Backtester.backend.model;
using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using Backtester.backend.model.system.estatistica;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UsoComum;
namespace Backtester.controller
{
    public class ConfigController : IController, ICaller
    {
        private FrmPrincipal frmPrincipal;
        public static Config config;
        public FacadeBacktester facade;

        public ConfigController(FrmPrincipal frmPrincipal)
        {
            this.frmPrincipal = frmPrincipal;
            facade = new FacadeBacktester();
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
            frmPrincipal.listPapeis.Items.Clear();
            foreach (string papel in config.papeis.OrderBy(x => x.ToString()).ToList())
            {
                frmPrincipal.listPapeis.Items.Add(papel);
            }
        }

        public virtual void UpdateUI()
        {
            frmPrincipal.chkFlagCompra.Checked = config.flagCompra;
            frmPrincipal.chkFlagVenda.Checked = config.flagVenda;
            frmPrincipal.txtCapitalInicial.Text = config.capitalInicial.ToString();
            frmPrincipal.txtCustoOperacao.Text = config.custoOperacao.ToString();
            frmPrincipal.txtVarsDebug.Text = config.varsDebug;
            frmPrincipal.txtTestesNaTela.Text = config.maxTestes.ToString();
            frmPrincipal.textGPVars.Text = config.gpVars;

            frmPrincipal.radioTPDiario.Checked = config.tipoPeriodo == Consts.PERIODO_ACAO.DIARIO;

            frmPrincipal.radioTPSemanal.Checked = config.tipoPeriodo == Consts.PERIODO_ACAO.SEMANAL;

        }

        internal void Salva()
        {
            UpdateValuesFromUI();
            config.SaveToFile();
        }

        public void UpdateValuesFromUI()
        {
            config.flagCompra = frmPrincipal.chkFlagCompra.Checked;
            config.flagVenda = frmPrincipal.chkFlagVenda.Checked;
            config.varsDebug = frmPrincipal.txtVarsDebug.Text;
            config.gpVars = frmPrincipal.textGPVars.Text;


            config.tipoPeriodo = frmPrincipal.radioTPDiario.Checked ? Consts.PERIODO_ACAO.DIARIO : Consts.PERIODO_ACAO.SEMANAL;

            try
            {
                config.maxTestes = int.Parse(frmPrincipal.txtTestesNaTela.Text);
                config.capitalInicial = int.Parse(frmPrincipal.txtCapitalInicial.Text);
                config.custoOperacao = int.Parse(frmPrincipal.txtCustoOperacao.Text);
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
        internal void Run(TradeSystem ts)
        {
            contaTestes = 0;
            facade.LoadAtivos(config.papeis, config.tipoPeriodo);
            frmPrincipal.dataGridRuns.Rows.Clear();
            //facade.RunSingleTS();
            facade.Run(this, config, ts);
            //facade.RunSingle(this, config, ts);
        }

        internal void RunSingle(TradeSystem ts)
        {
            facade.LoadAtivos(config.papeis, config.tipoPeriodo);
            frmPrincipal.dataGridRuns.Rows.Clear();
            //facade.RunSingleTS();
            facade.RunSingle("Run Single", this, config, ts);
        }

        public void UpdateApplication(Carteira carteira, MonteCarlo mC, int countLoops, int totalLoops)
        {
            Application.DoEvents();
            if (totalLoops <= 0) totalLoops = 1;
            frmPrincipal.labelStatus.Text = countLoops + " / " + totalLoops;
            //frmPrincipal.dataSetBacktest.Tables[0].Rows.Add(mC);
            //DataGridViewRow row = new DataGridViewRow();
            //row.Cells.Add(new DataGridViewCell());
            //frmPrincipal.dataGridRuns.Rows.Add(row);
            int rowLine = frmPrincipal.dataGridRuns.Rows.Count - 1;
            frmPrincipal.dataGridRuns.Rows.Add();
            int colIndex = 0;
            contaTestes++;
            SubSubDado todosTrades = mC.getGlobal().getGeral().getAmbasPontas().todosTrades;
            SubSubDado tradesGanhos = mC.getGlobal().getGeral().getAmbasPontas().tradesGanhos;
            SubSubDado tradesPerdidos = mC.getGlobal().getGeral().getAmbasPontas().tradesPerdidos;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = carteira;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = contaTestes;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.ToString();

            int iterations = (int)mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_ITERATIONS);


            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = iterations;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_TOTAL_PROFIT) == null ? 0 : (float)mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_TOTAL_PROFIT) / iterations;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_TOTAL_LOSS) == null ? 0 : (float)mC.properties.GetPropriedade(UsoComum.ConstsComuns.OBJ_TOTAL_LOSS) / iterations;

            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.CalcFitness();
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.getCapitalFinal();
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.getMaxCapital();
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.getMinCapital();
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.winLossRatio;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.qtdTrades;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = tradesGanhos.getnTrades();
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = tradesPerdidos.getnTrades();
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.totalGanho;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.totalPerdido;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.percAcerto;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = mC.capitalUsePercent;
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = todosTrades.getMaxDias();
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = todosTrades.getMinDias();
            frmPrincipal.dataGridRuns.Rows[rowLine].Cells[colIndex++].Value = todosTrades.getAvgDias();

            if (frmPrincipal.dataGridRuns.Rows.Count > config.maxTestes)
            {
                RemovePiorTeste();
            }
        }

        private void RemovePiorTeste()
        {
            MonteCarlo mc = frmPrincipal.dataGridRuns.Rows[0].Cells[0].Value as MonteCarlo;
            int piorIndex = 0;
            float piorResultado = mc.CalcFitness();
            for (int i = 1; i < frmPrincipal.dataGridRuns.Rows.Count; i++)
            {
                mc = frmPrincipal.dataGridRuns.Rows[i].Cells[0].Value as MonteCarlo;
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
            frmPrincipal.dataGridRuns.Rows.RemoveAt(piorIndex);
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
            MonteCarlo mc = frmPrincipal.dataGridRuns.Rows[p].Cells[0].Value as MonteCarlo;
            if (mc == null)
            {
                return;
            }
            Carteira carteira = frmPrincipal.dataGridRuns.Rows[p].Cells[1].Value as Carteira;
            Estatistica global = mc.getGlobal();
            // global.getGeral().getAmbasPontas().todosTrades.o
            frmPrincipal.dataGridOperacoes.Rows.Clear();
            IList<Posicao> posicoes = carteira.posicoesFechadas;
            float capital = config.capitalInicial;
            int contaOperacao = 0;



            for (int i = 0; i < mc.operacoes.Count; i++)
            {
                Operacao oper = mc.operacoes[i];
                Posicao posicao = oper.posicao;
                contaOperacao++;
                int rowLine = frmPrincipal.dataGridOperacoes.Rows.Count - 1;
                frmPrincipal.dataGridOperacoes.Rows.Add();
                int colIndex = 0;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = contaOperacao;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = posicao.idPosicao;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = posicao.ativo.name;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = oper.candleInicial.periodo;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = oper.candleFinal.periodo;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = posicao.direcao > 0 ? "Compra" : "Venda";
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = oper.qtd;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = Utils.FormatCurrency(oper.vlrEntrada);
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = Utils.FormatCurrency(oper.vlrSaida);
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = Utils.FormatCurrency(oper.vlrStopInicial);
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = oper.stopado ? "Sim" : "Não";
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = oper.qtd * oper.vlrEntrada;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = oper.GetDif();
                frmPrincipal.dataGridOperacoes.Rows[rowLine].DefaultCellStyle.ForeColor = Color.Black;
                if (oper.GetDif() > 0)
                {
                    frmPrincipal.dataGridOperacoes.Rows[rowLine].DefaultCellStyle.BackColor = Color.Blue;
                    frmPrincipal.dataGridOperacoes.Rows[rowLine].DefaultCellStyle.ForeColor = Color.White;
                }
                if (oper.GetDif() < 0)
                {
                    frmPrincipal.dataGridOperacoes.Rows[rowLine].DefaultCellStyle.BackColor = Color.Red;
                    frmPrincipal.dataGridOperacoes.Rows[rowLine].DefaultCellStyle.ForeColor = Color.White;
                }
                capital += oper.GetDif() - 2 * config.custoOperacao;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = capital;
                frmPrincipal.dataGridOperacoes.Rows[rowLine].Cells[colIndex++].Value = oper.capitalOnClose;
                //  oper.
            }
        }
    }
}
