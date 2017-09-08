using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backtester.controller;
using Backtester.backend.DataManager;
using Backtester.interfaces;
using Backtester.backend.model.system;
using System.Windows.Forms;
using System.Drawing;
using UsoComum;
using Backtester.backend.model.system.estatistica;
using FrontTester.frontend.model;
using Backtester.backend;

namespace FrontTester.frontend.controller
{
    public class FrontController
    {
        private IReferView frmFront;
        private ConfigController configController;
        CandidatoManager candidatoManager;
        FrontData frontData;
        FacadeBacktester facade;

        public FrontController(IReferView frmFront, ConfigController configController)
        {
            this.frmFront = frmFront;
            this.configController = configController;
            frontData = FrontData.LoadSaved();
            //precisa ser mesmo dia... o preço será sugerido para comprar
            configController.config.flagCompraMesmoDia = true;
            configController.config.flagVendaMesmoDia = true;
            
        }

        public void Init()
        {
            LoadCandidatos();
            ComboBox cbTradeSystemEmUso = frmFront.GetControl("cbTradeSystemEmUso") as ComboBox;
            if (frontData.tradeSystemEmUso != null)
            {
                cbTradeSystemEmUso.Items.Add(frontData.tradeSystemEmUso);
                cbTradeSystemEmUso.SelectedItem = frontData.tradeSystemEmUso;
            }

            facade = new FacadeBacktester();
            facade.LoadAtivos(configController.config.papeis, configController.config.tipoPeriodo, Consts.TIPO_CARGA_ATIVOS.DADOS_ATUAIS);
        }

        private void LoadCandidatos()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("FrontTester.frontend", "Backtester");
            candidatoManager = CandidatoManager.LoadSaved(path);
            for (int i = 0; i < candidatoManager.ranking.Count; i++)
            {
                CandidatoData data = candidatoManager.ranking[i];
                frmFront.AddItem("cbTradeSystem", data);
                frmFront.AddItem("cbTradeSystemEmUso", data);
            }
        }

        internal void AtualizaDados()
        {
            throw new NotImplementedException();
        }

        internal void AddPapeisDefault()
        {
            configController.config.papeis.Clear();
            configController.config.AddPapel("BOV", "ABEV3");
            configController.config.AddPapel("BOV", "BBAS3");
            configController.config.AddPapel("BOV", "BBDC4");
            configController.config.AddPapel("BOV", "BRAP4");
            configController.config.AddPapel("BOV", "BRFS3");
            configController.config.AddPapel("BOV", "BVMF3");
            configController.config.AddPapel("BOV", "CMIG4");
            configController.config.AddPapel("BOV", "CSNA3");
            configController.config.AddPapel("BOV", "GGBR4");
            configController.config.AddPapel("BOV", "ITUB4");
            configController.config.AddPapel("BOV", "EMBR3");
            configController.config.AddPapel("BOV", "VALE5");
            configController.config.AddPapel("BOV", "PETR4");
            configController.config.AddPapel("BOV", "CMIG4");
            configController.config.AddPapel("BOV", "PETR4");
            configController.config.AddPapel("BOV", "SUZB5");
            configController.config.AddPapel("BOV", "NATU3");
            configController.config.AddPapel("BOV", "WEGE3");
            configController.config.AddPapel("BOV", "CCRO3");
            configController.UpdateUI();
        }

        internal void Describe(CandidatoData candidatoData)
        {
            RichTextBox txt = frmFront.GetControl("txtDescriber") as RichTextBox;
            txt.Text = "";
            TradeSystem ts = candidatoData.tradeSystem;
            AddTitle(txt, ts.name);
            AddQuebraLinha(txt);
            Clarify clarify = new Clarify();

            DescreveCaracteristicas(txt, ts);
            AddQuebraLinha(txt);
            DescreveFormulas(txt, ts, clarify);
            AddQuebraLinha(txt);
            AddQuebraLinha(txt);

            AddTitle(txt, "ESTATÍSTICAS");
            Estatistica stat = candidatoData.estatistica;
            AddText(txt, "Max.Capital", Utils.FormatCurrency(stat.maxCapital));
            AddText(txt, "Min.Capital", Utils.FormatCurrency(stat.minCapital));
            AddQuebraLinha(txt);
            txt.SelectionIndent += 10;
            if (configController.config.flagCompra)
            {
                AddTitle(txt, "Ponta Comprada");
                SubDado dado = stat.geral.getCompras();
                DescreveDado(txt, dado);
            }
            if (configController.config.flagVenda)
            {
                AddTitle(txt, "Ponta Vendida");
                SubDado dado = stat.geral.getVendas();
                DescreveDado(txt, dado);
            }
            txt.SelectionIndent -= 10;

        }

        private void DescreveDado(RichTextBox txt, SubDado dado)
        {
            
            txt.SelectionBullet = true;
            AddText(txt, "% Acerto", Utils.FormatCurrency(dado.percAcerto) + "%");
            AddText(txt, "$Win/$Loss", Utils.FormatCurrency(dado.winLossRatio));
            AddText(txt, "Total Ganho", Utils.FormatCurrency(dado.totalGanho));
            AddText(txt, "Total Perdido", Utils.FormatCurrency(dado.totalPerdido));
            AddText(txt, "Trades Stopados", "" + dado.getnTradesStopados());
            
            txt.SelectionIndent += 10;
            SubSubDado ssd = dado.getTodosTrades();
            DescreveSubDado(txt, "TODOS TRADES:",ssd);
            ssd = dado.getTradesGanhos();
            DescreveSubDado(txt, "TRADES GANHOS:", ssd);
            ssd = dado.getTradesPerdidos();
            DescreveSubDado(txt, "TRADES PERDIDOS:", ssd);
            txt.SelectionIndent -= 10;

        }

        private void DescreveSubDado(RichTextBox txt, string titulo,SubSubDado ssd)
        {
            txt.SelectionBullet = false;
            AddQuebraLinha(txt);
            AddTitle(txt, titulo);
            txt.SelectionBullet = true;
            AddText(txt, "Qtd Trades", "" + ssd.getnTrades());
            
            AddText(txt, "Duração Média", "" + ssd.getAvgDias());
            AddText(txt, "Duração Máxima", "" + ssd.getMaxDias());
            AddText(txt, "Duração Mínima", "" + ssd.getMinDias());
            AddText(txt, "Maior Variação", "" + Utils.FormatCurrency(ssd.getMaxTrade()));
            AddText(txt, "Menor Variação", "" + Utils.FormatCurrency(ssd.getMinTrade()));
            AddText(txt, "Variação Total", "" + Utils.FormatCurrency(ssd.getTotal()));
            txt.SelectionBullet = false;
        }

        internal void AnalisaEntradas()
        {
            DataGridViewRowCollection RowsEntradas = frmFront.GetRows("dataGridEntradas");
            RowsEntradas.Clear();
            //TODO: continuar daqui

        }

        internal void ChangeCapitalAtual(float val)
        {
            frontData.capitalAtual = val;
            frontData.SaveToFile();
        }

        internal void ChangeTradeSystemEmUso(CandidatoData candidatoData)
        {
            frontData.tradeSystemEmUso = candidatoData;
            frontData.SaveToFile();
        }

        private void DescreveFormulas(RichTextBox txt, TradeSystem ts, Clarify clarify)
        {
            if (configController.config.flagCompra)
            {
                AddTitle(txt, "CONDIÇÕES COMPRA");
                AddText(txt, "Cond.Compra", clarify.ClarificaFormula(ts.condicaoEntradaC));
                AddText(txt, "Sizing Compra", clarify.ClarificaFormula(ts.sizingCompra));
                AddText(txt, "Stop Inicial Compra", clarify.ClarificaFormula(ts.stopInicialC));
                if (ts.usaStopMovel)
                {
                    AddText(txt, "Stop Movel Compra", clarify.ClarificaFormula(ts.stopMovelC));
                }
            }
            if (configController.config.flagVenda)
            {
                AddTitle(txt, "CONDIÇÕES VENDA");
                AddText(txt, "Cond.Venda", clarify.ClarificaFormula(ts.condicaoEntradaC));
                AddText(txt, "Sizing Venda", clarify.ClarificaFormula(ts.sizingCompra));
                AddText(txt, "Stop Inicial Venda", clarify.ClarificaFormula(ts.stopInicialC));
                if (ts.usaStopMovel)
                {
                    AddText(txt, "Stop Movel Venda", clarify.ClarificaFormula(ts.stopMovelC));
                }
            }
        }

        private void DescreveCaracteristicas(RichTextBox txt, TradeSystem ts)
        {
            if (ts.usaMultiplasEntradas)
            {
                AddTitle(txt, "CARACTERÍSTICAS");
                txt.SelectionBullet = true;
                AddText(txt, ts.usaMultiplasEntradas ? "Usa Múltiplas Entradas" : "Não usa Múltiplas Entradas");
                AddText(txt, ts.usaStopMovel ? "Usa Stop Móvel" : "Não Usa Stop Móvel");
                AddText(txt, "Risco por trade", Utils.FormatCurrency(ts.riscoTrade) + "%");
                AddText(txt, "Risco Global", Utils.FormatCurrency(ts.riscoGlobal) + "%");
                AddText(txt, "Gap Stop Inicial", Utils.FormatCurrency(ts.stopGapPerc) + "%");
                AddText(txt, "% capital máximo por trade", Utils.FormatCurrency(ts.percTrade) + "%");
                AddText(txt, "Capital máximo por trade", "$ " + Utils.FormatCurrency(ts.maxCapitalTrade));
                AddText(txt, "Stop Mensal", Utils.FormatCurrency(ts.stopMensal) + "%");
                txt.SelectionBullet = false;
            }
        }

        private void AddText(RichTextBox txt, string v)
        {
            txt.AppendText(v);
            AddQuebraLinha(txt);
        }

        private void AddText(RichTextBox txt, string v1, string v2)
        {
            txt.SelectionFont = new Font(txt.Font, FontStyle.Bold);
            txt.AppendText(v1+": ");
            txt.SelectionFont = new Font(txt.Font, FontStyle.Regular);
            txt.AppendText(v2);
            AddQuebraLinha(txt);
        }

        private void AddQuebraLinha(RichTextBox txt)
        {
            txt.AppendText("\r\n");
        }

        private void AddTitle(RichTextBox txt, string name)
        {
            txt.SelectionFont= new Font(txt.Font, FontStyle.Bold);
            txt.AppendText(name);
            txt.SelectionFont = new Font(txt.Font, FontStyle.Regular);
            AddQuebraLinha(txt);
        }
    }
}
