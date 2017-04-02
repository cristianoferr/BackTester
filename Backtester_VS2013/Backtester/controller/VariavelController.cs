using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtester.controller
{
    public class VariavelController:IController
    {
        private FrmPrincipal frmPrincipal;
        private TradeSystemController tsC;
        private int selectedVar=-1;

        public VariavelController(TradeSystemController tradeSystemController, FrmPrincipal frmPrincipal)
        {
            this.tsC = tradeSystemController;
            this.frmPrincipal = frmPrincipal;
        }

        private Variavel currentVar { get {
            if (selectedVar < 0) return null;
            TradeSystem ts = tsC.tradeSystem;
            if (ts.vm.Count <= selectedVar) return null;
            return ts.vm.variaveis[selectedVar];
            }
        }

        public void UpdateValuesFromUI()
        {
            Variavel var = currentVar;
            if (var == null) return;
            if (var.uniqueID.ToString() == frmPrincipal.labelVarId.Text)
            {
                var.name = frmPrincipal.txtVarName.Text;
                var.descricao = frmPrincipal.txtVarDescricao.Text;
                var.vlrInicial = float.Parse(frmPrincipal.txtVarVlrInicial.Text);
                var.vlrFinal = float.Parse(frmPrincipal.txtVarVlrFinal.Text);
                var.steps = int.Parse(frmPrincipal.txtVarSteps.Text);

                frmPrincipal.listTSVars.Items[selectedVar] = var;
            }
        }

        private void UpdateUI(int selectedVar)
        {
            Variavel var = currentVar;
            frmPrincipal.txtVarName.Text = var.name;
            frmPrincipal.txtVarDescricao.Text = var.descricao;
            frmPrincipal.txtVarVlrInicial.Text = var.vlrInicial.ToString();
            frmPrincipal.txtVarVlrFinal.Text = var.vlrFinal.ToString();
            frmPrincipal.txtVarSteps.Text = var.steps.ToString();
            frmPrincipal.labelVarId.Text = var.uniqueID.ToString();
        }

        public void UpdateUI()
        {
            UpdateList();
            ChangeSelectedVariavel(0);
          //  throw new NotImplementedException();
        }

        private void UpdateList()
        {
            frmPrincipal.listTSVars.Items.Clear();
            TradeSystem ts = tsC.tradeSystem;
            if (ts != null)
            {
                foreach (Variavel v in ts.vm.variaveis)
                {
                    frmPrincipal.listTSVars.Items.Add(v);
                }
            }
        }

        internal void AdicionaVariavel(string name)
        {
            TradeSystem ts = tsC.tradeSystem;
            ts.vm.GetVariavel(name,"", 0, 0, 0);
            UpdateUI();
        }

        internal void RemoveVariavel()
        {
            throw new NotImplementedException();
        }

        internal void ChangeSelectedVariavel(int index)
        {
            if (selectedVar >= 0)
            {
                UpdateValuesFromUI();
            }
            selectedVar = index;
            if (selectedVar >= 0)
            {
                UpdateUI(selectedVar);
            }

        }

       
    }
}
