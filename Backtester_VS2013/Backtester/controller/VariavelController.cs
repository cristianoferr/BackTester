using Backtester.backend.model.system;
using Backtester.backend.model.system.condicoes;
using Backtester.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtester.controller
{
    public class VariavelController:IController
    {
        private IReferView frmPrincipal;
        private TradeSystemController tsC;
        private int selectedVar=-1;

        public VariavelController(TradeSystemController tradeSystemController, IReferView frmPrincipal)
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
            if (var.uniqueID.ToString() == frmPrincipal.Text("labelVarId"))
            {
                var.name = frmPrincipal.Text("txtVarName");
                var.descricao = frmPrincipal.Text("txtVarDescricao");
                var.vlrInicial = float.Parse(frmPrincipal.Text("txtVarVlrInicial"));
                var.vlrFinal = float.Parse(frmPrincipal.Text("txtVarVlrFinal"));
                var.steps = int.Parse(frmPrincipal.Text("txtVarSteps"));

                frmPrincipal.SetListItem("listTSVars",selectedVar,var);
            }
        }

        private void UpdateUI(int selectedVar)
        {
            Variavel var = currentVar;
            frmPrincipal.SetText("txtVarName", var.name);
            frmPrincipal.SetText("txtVarDescricao.", var.descricao);
            frmPrincipal.SetText("txtVarVlrInicial", var.vlrInicial.ToString());
            frmPrincipal.SetText("txtVarVlrFinal", var.vlrFinal.ToString());
            frmPrincipal.SetText("txtVarSteps", var.steps.ToString());
            frmPrincipal.SetText("labelVarId", var.uniqueID.ToString());
        }

        public void UpdateUI()
        {
            UpdateList();
            ChangeSelectedVariavel(0);
          //  throw new NotImplementedException();
        }

        private void UpdateList()
        {
            frmPrincipal.ClearList("listTSVars");
            TradeSystem ts = tsC.tradeSystem;
            if (ts != null)
            {
                foreach (Variavel v in ts.vm.variaveis)
                {
                    frmPrincipal.AddItem("listTSVars",v);
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
