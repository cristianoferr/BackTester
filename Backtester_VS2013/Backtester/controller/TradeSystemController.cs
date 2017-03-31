using Backtester.backend.DataManager;
using Backtester.backend.model.system;

namespace Backtester.controller
{
    class TradeSystemController : IController
    {
        private FrmPrincipal frmPrincipal;
        TradeSystemHolder holder;



        public TradeSystemController(FrmPrincipal frmPrincipal)
        {
            this.frmPrincipal = frmPrincipal;
            holder = TradeSystemHolder.LoadSaved();
        }

        internal void Salva()
        {
            holder.SaveToFile();
        }

        internal void RemoveTradeSystem(int p)
        {
            holder.RemoveTradeSystem(p);
            selectedTS = -1;
            UpdateUI();
        }



        internal void AdicionaTS()
        {
            TradeSystem ts = new TradeSystem(ConfigController.config);
            ts.name = "TradeSystem Novo..." + holder.Count;
            holder.AddTS(ts);
            selectedTS = holder.Count - 1;
            UpdateUI();
        }


        int selectedTS_ = -1;
        public int selectedTS
        {
            get { return selectedTS_; }
            set
            {
                //panelTradeSystem
                frmPrincipal.panelTradeSystem.Visible = value >= 0;
                UpdateValuesFromUI();
                if (value >= 0)
                {
                    UpdateUI(value);
                }

                selectedTS_ = value;
            }
        }

        public virtual void UpdateValuesFromUI()
        {
            if (selectedTS < 0) return;
            TradeSystem ts = holder.GetTS(selectedTS);
            ts.name = frmPrincipal.txtNameTs.Text;
            ts.condicaoEntradaC.formula=frmPrincipal.txtCondEntrC.Text;
            ts.condicaoEntradaC.descricao = frmPrincipal.txtCondEntrCDesc.Text;

            ts.condicaoSaidaC.formula=frmPrincipal.txtCondSaidaC.Text;
            ts.condicaoSaidaC.descricao = frmPrincipal.txtCondSaidaCDesc.Text;


            ts.condicaoEntradaV.formula=frmPrincipal.txtCondEntrV.Text;
            ts.condicaoEntradaV.descricao = frmPrincipal.txtCondEntrVDesc.Text;

            ts.condicaoSaidaV.formula=frmPrincipal.txtCondSaidaV.Text;
            ts.condicaoSaidaV.descricao = frmPrincipal.txtCondSaidaVDesc.Text;


        }


        public virtual void UpdateUI()
        {
            UpdateUI(selectedTS);
        }


        private void UpdateUI(int index)
        {
            if (selectedTS < 0) return;
            TradeSystem ts = holder.GetTS(index);
            frmPrincipal.txtNameTs.Text = ts.name;


            UpdateUI(ts.condicaoEntradaC, frmPrincipal.txtCondEntrC, frmPrincipal.txtCondEntrCDesc);
            UpdateUI(ts.condicaoSaidaC, frmPrincipal.txtCondSaidaC, frmPrincipal.txtCondSaidaCDesc);
            UpdateUI(ts.condicaoEntradaV, frmPrincipal.txtCondEntrV, frmPrincipal.txtCondEntrVDesc);
            UpdateUI(ts.condicaoSaidaV, frmPrincipal.txtCondSaidaV, frmPrincipal.txtCondSaidaVDesc);
        }

        private void UpdateUI(backend.model.system.condicoes.CondicaoComplexa condicao, System.Windows.Forms.TextBox txtFormula, System.Windows.Forms.TextBox txtDesc)
        {
            txtFormula.Text = condicao.formula;
            txtDesc.Text = condicao.descricao;
        }


    }
}
