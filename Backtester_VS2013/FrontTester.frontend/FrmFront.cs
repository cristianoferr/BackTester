using Backtester.backend.DataManager;
using Backtester.backend.model.system;
using Backtester.controller;
using Backtester.interfaces;
using FrontTester.frontend.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontTester.frontend
{
    public partial class FrmFront : Form, IReferView
    {
        ConfigController configController;
        FrontController frontController;
        ReferView referView;
        public FrmFront()
        {
            InitializeComponent();
            RegisterElements();
            configController = new ConfigController(this);
            frontController = new FrontController(this, configController);
            frontController.Init();


        }

        private void RegisterElements()
        {
            referView = new ReferView();
            referView.Register("listPapeis", listPapeis);
            referView.Register("chkFlagCompra", chkFlagCompra);
            referView.Register("chkFlagVenda", chkFlagVenda);
            referView.Register("txtCapitalInicial", txtCapitalInicial);
            referView.Register("txtCustoOperacao", txtCustoOperacao);
            referView.Register("txtTestesNaTela", txtTestesNaTela);
            referView.Register("textGPVars", textGPVars);
            referView.Register("radioTPDiario", radioTPDiario);
            referView.Register("radioTPSemanal", radioTPSemanal);
            referView.Register("cbTradeSystem", cbTradeSystem);
            referView.Register("labelStatus", labelStatus);
            referView.Register("txtVarsDebug", txtVarsDebug);
            referView.Register("txtDescriber", txtDescriber);
            referView.Register("cbTradeSystemEmUso", cbTradeSystemEmUso);
            referView.Register("dataGridCarteira", dataGridCarteira);
            referView.Register("dataGridEntradas", dataGridEntradas);

            /*
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
            referView.Register("listTSVars", listTSVars);*/

            referView.SetStatusComponent(txtStatus);
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
            Text = "Backtester [" + v + "]";
        }
        #endregion IReferView

        private void btnSalvaConfig_Click(object sender, EventArgs e)
        {
            configController.Salva();
        }

        private void btnAtualizaDados_Click(object sender, EventArgs e)
        {
            frontController.AtualizaDados();
        }

        private void btnCarregaAtivoIniciais_Click(object sender, EventArgs e)
        {
           // frontController.AddPapeisDefault();
        }

        private void cbTradeSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            frontController.Describe(cbTradeSystem.SelectedItem as CandidatoData);
        }

        private void cbTradeSystemEmUso_SelectedIndexChanged(object sender, EventArgs e)
        {
            frontController.ChangeTradeSystemEmUso(cbTradeSystemEmUso.SelectedItem as CandidatoData);
        }

        private void txtCapitalAtual_TextChanged(object sender, EventArgs e)
        {
            float val = 0;
            float.TryParse(txtCapitalAtual.Text, out val);
            if (val>0)
                frontController.ChangeCapitalAtual(val);
        }

        private void btnAnalisaEntradas_Click(object sender, EventArgs e)
        {
            frontController.AnalisaEntradas();
        }
    }
}
