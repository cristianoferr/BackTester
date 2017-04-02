using Backtester.backend.DataManager;
using Backtester.backend.model.system;

namespace Backtester.controller
{
    public class TradeSystemController : IController
    {
        private FrmPrincipal frmPrincipal;
        TradeSystemHolder holder;
        VariavelController vc;


        public TradeSystemController(FrmPrincipal frmPrincipal)
        {
            this.frmPrincipal = frmPrincipal;
            holder = TradeSystemHolder.LoadSaved();
            vc = new VariavelController(this,frmPrincipal);
            UpdateUI();
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
                selectedTS_ = value;
                if (selectedTS_ >= 0)
                {
                    UpdateUI(selectedTS_);
                }

            }
        }

        public TradeSystem GetTS(string name)
        {
            return holder.GetTS(name);
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

            ts.stopMovelC = frmPrincipal.txtStopMovelCompra.Text;
            ts.stopMovelV = frmPrincipal.txtStopMovelVenda.Text;
            ts.stopInicialC= frmPrincipal.txtStopInicialCompra.Text;
            ts.stopInicialV = frmPrincipal.txtStopInicialVenda.Text;


            vc.ChangeSelectedVariavel(0);

        }


        public virtual void UpdateUI()
        {
            UpdateUI(selectedTS);
            frmPrincipal.listTradeSystems.Items.Clear();
            frmPrincipal.cbTradeSystem.Items.Clear();
            for (int i = 0; i < holder.Count; i++)
            {
                frmPrincipal.listTradeSystems.Items.Add(holder.GetTS(i));
                frmPrincipal.cbTradeSystem.Items.Add(holder.GetTS(i));
            }
            frmPrincipal.btnRun.Enabled = frmPrincipal.cbTradeSystem.Items.Count > 0;
        }


        private void UpdateUI(int index)
        {
            frmPrincipal.panelTradeSystem.Visible = index >= 0;
            if (index < 0)
            {
                return;
            }
            TradeSystem ts = holder.GetTS(index);
            frmPrincipal.txtNameTs.Text = ts.name;


            UpdateUI(ts.condicaoEntradaC, frmPrincipal.txtCondEntrC, frmPrincipal.txtCondEntrCDesc);
            UpdateUI(ts.condicaoSaidaC, frmPrincipal.txtCondSaidaC, frmPrincipal.txtCondSaidaCDesc);
            UpdateUI(ts.condicaoEntradaV, frmPrincipal.txtCondEntrV, frmPrincipal.txtCondEntrVDesc);
            UpdateUI(ts.condicaoSaidaV, frmPrincipal.txtCondSaidaV, frmPrincipal.txtCondSaidaVDesc);

            frmPrincipal.txtStopMovelCompra.Text=ts.stopMovelC;
            frmPrincipal.txtStopMovelVenda.Text=ts.stopMovelV;
            frmPrincipal.txtStopInicialCompra.Text = ts.stopInicialC;
            frmPrincipal.txtStopInicialVenda.Text = ts.stopInicialV;

            vc.UpdateUI();
        }

        private void UpdateUI(backend.model.system.condicoes.CondicaoComplexa condicao, System.Windows.Forms.TextBox txtFormula, System.Windows.Forms.TextBox txtDesc)
        {
            txtFormula.Text = condicao.formula;
            txtDesc.Text = condicao.descricao;
        }



        public TradeSystem tradeSystem { get{
            return holder.GetTS(selectedTS);
        }  }

        internal void AdicionaVariavel(string name)
        {
            vc.AdicionaVariavel(name);
        }

        internal void RemoveVariavel()
        {
            vc.RemoveVariavel();
        }

        internal void ChangeSelectedVariavel(int index)
        {
            vc.ChangeSelectedVariavel(index);
        }
    }
}
