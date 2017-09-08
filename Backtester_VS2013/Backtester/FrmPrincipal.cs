using Backtester.backend.model.system;
using Backtester.controller;
using Backtester.interfaces;
using System.Windows.Forms;
using System;
using Backtester.backend.model.ativos;

namespace Backtester
{
    public partial class FrmPrincipal : Form, IReferView
    {
        ConfigController configController;
        TradeSystemController tsController;
        GeneticProgrammingController gpController;

        ReferView referView;

        public FrmPrincipal()
        {
            InitializeComponent();
            RegisterElements();
            configController = new ConfigController(this);
            tsController = new TradeSystemController(this, configController);
            gpController = new GeneticProgrammingController(this, configController);
            
        }

        private void RegisterElements()
        {
            referView=new ReferView();
            referView.Register("listPapeis", listPapeis);
            referView.Register("chkFlagCompra", chkFlagCompra);
            referView.Register("chkFlagVenda", chkFlagVenda);
            referView.Register("txtCapitalInicial", txtCapitalInicial);
            referView.Register("txtCustoOperacao", txtCustoOperacao);
            referView.Register("txtVarsDebug", txtVarsDebug);
            referView.Register("txtTestesNaTela", txtTestesNaTela);
            referView.Register("textGPVars", textGPVars);
            referView.Register("radioTPDiario", radioTPDiario);
            referView.Register("radioTPSemanal", radioTPSemanal);
            referView.Register("dataGridRuns", dataGridRuns);
            referView.Register("dataGridOperacoes", dataGridOperacoes);
            referView.Register("panelTradeSystem", panelTradeSystem);
            referView.Register("txtNameTs", txtNameTs);
            referView.Register("txtCondEntrC", txtCondEntrC);
            referView.Register("txtCondSaidaC", txtCondSaidaC);
            referView.Register("txtCondEntrV", txtCondEntrV);
            referView.Register("txtCondSaidaV", txtCondSaidaV);
            referView.Register("txtStopMovelCompra", txtStopMovelCompra);
            referView.Register("txtStopMovelVenda", txtStopMovelVenda);
            referView.Register("txtStopInicialCompra", txtStopInicialCompra);
            referView.Register("txtStopInicialVenda", txtStopInicialVenda);
            referView.Register("listTradeSystems", listTradeSystems);
            referView.Register("cbTradeSystem", cbTradeSystem);
            referView.Register("btnRun", btnRun);
            referView.Register("txtCondEntrCDesc", txtCondEntrCDesc);
            referView.Register("txtCondSaidaCDesc", txtCondSaidaCDesc);
            referView.Register("txtCondEntrVDesc", txtCondEntrVDesc);
            referView.Register("txtCondSaidaVDesc", txtCondSaidaVDesc);
            referView.Register("labelVarId", labelVarId);
            referView.Register("txtVarName", txtVarName);
            referView.Register("txtVarDescricao", txtVarDescricao);
            referView.Register("txtVarVlrInicial", txtVarVlrInicial);
            referView.Register("txtVarVlrFinal", txtVarVlrFinal);
            referView.Register("txtVarSteps", txtVarSteps);
            referView.Register("listTSVars", listTSVars);
            referView.Register("labelStatus", labelStatus);
            referView.Register("labelAvgDif", labelAvgDif);
            


            referView.SetStatusComponent(txtStatus);
        }


        private void btnSalvaConfig_Click(object sender, System.EventArgs e)
        {
            configController.Salva();
        }

        private void mainTab_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            previousSelectedVar = -1;
            configController.UpdateValuesFromUI();
            tsController.UpdateValuesFromUI();
        }

        private void btnSalvaTradeSystems_Click(object sender, System.EventArgs e)
        {
            tsController.UpdateValuesFromUI();
            tsController.Salva();
        }

        private void btnAdicionaTS_Click(object sender, System.EventArgs e)
        {
            tsController.AdicionaTS();
        }

        private void btnRemoveTS_Click(object sender, System.EventArgs e)
        {
            if (listTradeSystems.SelectedIndex >= 0)
            {
                tsController.RemoveTradeSystem(listTradeSystems.SelectedIndex);
            }
        }

        private void listTradeSystems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            tsController.selectedTS = listTradeSystems.SelectedIndex;
        }

        private void btnAtualizaTradeSystem_Click(object sender, System.EventArgs e)
        {
            tsController.UpdateValuesFromUI();
        }

        private void btnAdicionaVar_Click(object sender, System.EventArgs e)
        {
            tsController.AdicionaVariavel(Microsoft.VisualBasic.Interaction.InputBox("Nome:", "Nova Variável", "VAR_NAME").ToUpper());
        }

        private void btnRemoveVar_Click(object sender, System.EventArgs e)
        {
            tsController.RemoveVariavel();
        }

        private int previousSelectedVar = -1;
        private void listTSVars_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (previousSelectedVar != listTSVars.SelectedIndex)
            {
                previousSelectedVar = listTSVars.SelectedIndex;
                tsController.ChangeSelectedVariavel(listTSVars.SelectedIndex);

            }
        }



        private void btnAdicionaPapel_click(object sender, System.EventArgs e)
        {
            configController.AdicionaPapel(Microsoft.VisualBasic.Interaction.InputBox("Nome:", "Novo Papel").ToUpper());
        }

        private void btnRemovePapel_Click(object sender, System.EventArgs e)
        {
            if (listPapeis.SelectedIndex >= 0)
            {
                configController.RemovePapel(listPapeis.SelectedItem.ToString());
            }
        }

        private void btnRun_Click(object sender, System.EventArgs e)
        {
            if (cbTradeSystem.SelectedIndex >= 0)
            {

                configController.Run(tsController.GetTS(cbTradeSystem.SelectedItem.ToString()));
            }
        }

        private void cbTradeSystem_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void btnRodaSingle_Click(object sender, System.EventArgs e)
        {
            if (cbTradeSystem.SelectedIndex >= 0)
            {
                TradeSystem ts = tsController.GetTS(cbTradeSystem.SelectedItem.ToString());
                if (txtVarsDebug.Text != "")
                {
                    ts.vm.LoadVars(txtVarsDebug.Text);
                }
                configController.RunSingle(ts,"Roda Single",backend.Consts.TIPO_CARGA_ATIVOS.GERA_CANDIDATOS);
            }
        }

        private void txtVarsDebug_TextChanged(object sender, System.EventArgs e)
        {
            if (configController != null)
            {
                configController.VarDebugChanged(txtVarsDebug.Text);
                configController.Salva();
            }
        }

        private void dataGridRuns_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridRuns.SelectedCells.Count > 0)
                configController.SelectMonteCarlo(dataGridRuns.SelectedCells[0].RowIndex);
        }

        private void dataGridRuns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRodaGP_Click(object sender, System.EventArgs e)
        {
            gpController.InitPool();
            gpController.Run();
        }

        private void btnRodaSingleGP_Click(object sender, System.EventArgs e)
        {
            gpController.RunSingle();

        }

        private void darioTPDiario_CheckedChanged(object sender, System.EventArgs e)
        {
            radioTPSemanal.Checked = !radioTPDiario.Checked;
            if (configController == null) return;
            configController.UpdateValuesFromUI();
        }

        private void radioTPSemanal_CheckedChanged(object sender, System.EventArgs e)
        {
            radioTPDiario.Checked = !radioTPSemanal.Checked;
            if (configController == null) return;
            configController.UpdateValuesFromUI();
        }

        private void btnValidaCandidatos_Click(object sender, System.EventArgs e)
        {
            txtStatus.Text = "";
            gpController.ValidaCandidatos(true);
        }

        #region IReferView
        public Control GetControl(string v)
        {
            return ((IReferView)referView).GetControl(v);
        }

        void IReferView.ClearList(string v)
        {
            referView.ClearList(v);
        }

        void IReferView.AddList(string v, string papel)
        {
            referView.AddList(v, papel);
        }

        void IReferView.SetChecked(string v, bool flagCompra)
        {
            referView.SetChecked(v, flagCompra);
        }

        void IReferView.SetText(string v1, string v2)
        {
            referView.SetText(v1, v2);
        }

        bool IReferView.IsChecked(string v)
        {
            return referView.IsChecked(v);
        }

        string IReferView.Text(string v)
        {
            return referView.Text(v);
        }

        void ClearRows(string v)
        {
            referView.ClearRows(v);
        }

        DataGridViewRowCollection IReferView.GetRows(string v)
        {
            return referView.GetRows(v);
        }

        void IReferView.ClearRows(string v)
        {
            referView.ClearRows(v);
        }

        public void SetVisible(string v1, bool v2)
        {
            referView.SetVisible(v1, v2);
        }

        public void SetEnabled(string v1, bool v2)
        {
            referView.SetEnabled(v1, v2);
        }

        public void AddItem(string v, object tradeSystem)
        {
            referView.AddItem(v, tradeSystem);
        }

        public void SetListItem(string v, int index, object var)
        {
            referView.SetListItem(v, index, var);
        }
        public void SetStatus(string v)
        {
            referView.SetStatus(v);
        }
        public void SetTitle(string v)
        {
            Text = "Backtester ["+v+"]";   
        }
        #endregion IReferView

        private void btnGeraMockGrafico_Click(object sender, EventArgs e)
        {
            int seed = int.Parse(txtSeed.Text);
            Ativo ativo = tsController.GenerateMockAtivo(seed);
            ativo.DrawIn(panelGrafico.CreateGraphics());
        }

       
    }
}
