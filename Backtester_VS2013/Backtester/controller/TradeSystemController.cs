using Backtester.backend.DataManager;
using Backtester.backend.model.system;
using Backtester.interfaces;

namespace Backtester.controller
{
    public class TradeSystemController : IController
    {
        private IReferView frmPrincipal;
        TradeSystemHolder holder;
        VariavelController vc;


        public TradeSystemController(IReferView frmPrincipal,ConfigController configController)
        {
            this.frmPrincipal = frmPrincipal;
            this.configController = configController;
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
            TradeSystem ts = new TradeSystem(configController.config);
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
                frmPrincipal.SetVisible("panelTradeSystem",value >= 0);
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
            ts.name = frmPrincipal.Text("txtNameTs");
            ts.condicaoEntradaC=frmPrincipal.Text("txtCondEntrC");
            //ts.condicaoEntradaC.descricao = frmPrincipal.txtCondEntrCDesc.Text;

            ts.condicaoSaidaC=frmPrincipal.Text("txtCondSaidaC.Text");
            //ts.condicaoSaidaC.descricao = frmPrincipal.txtCondSaidaCDesc.Text;


            ts.condicaoEntradaV=frmPrincipal.Text("txtCondEntrV.Text");
           // ts.condicaoEntradaV.descricao = frmPrincipal.txtCondEntrVDesc.Text;

            ts.condicaoSaidaV=frmPrincipal.Text("txtCondSaidaV.Text");
            //ts.condicaoSaidaV.descricao = frmPrincipal.txtCondSaidaVDesc.Text;

            ts.stopMovelC = frmPrincipal.Text("txtStopMovelCompra.Text");
            ts.stopMovelV = frmPrincipal.Text("txtStopMovelVenda.Text");
            ts.stopInicialC= frmPrincipal.Text("txtStopInicialCompra.Text");
            ts.stopInicialV = frmPrincipal.Text("txtStopInicialVenda.Text");


            vc.ChangeSelectedVariavel(0);

        }


        public virtual void UpdateUI()
        {
            UpdateUI(selectedTS);
            frmPrincipal.ClearList("listTradeSystems");
            frmPrincipal.ClearList("cbTradeSystem");
            for (int i = 0; i < holder.Count; i++)
            {
                frmPrincipal.AddItem("listTradeSystems", holder.GetTS(i));
                frmPrincipal.AddItem("cbTradeSystem", holder.GetTS(i));

            }
            frmPrincipal.SetEnabled("btnRun",holder.Count > 0);
        }


        private void UpdateUI(int index)
        {
            frmPrincipal.SetVisible("panelTradeSystem",index >= 0);
            if (index < 0)
            {
                return;
            }
            TradeSystem ts = holder.GetTS(index);
            frmPrincipal.SetText("txtNameTs",ts.name);


            UpdateUI(ts.condicaoEntradaC, "txtCondEntrC", "txtCondEntrCDesc");
            UpdateUI(ts.condicaoSaidaC, "txtCondSaidaC", "txtCondSaidaCDesc");
            UpdateUI(ts.condicaoEntradaV, "txtCondEntrV", "txtCondEntrVDesc");
            UpdateUI(ts.condicaoSaidaV, "txtCondSaidaV", "txtCondSaidaVDesc");

            frmPrincipal.SetText("txtStopMovelCompra",ts.stopMovelC);
            frmPrincipal.SetText("txtStopMovelVenda", ts.stopMovelV);
            frmPrincipal.SetText("txtStopInicialCompra", ts.stopInicialC);
            frmPrincipal.SetText("txtStopInicialVenda", ts.stopInicialV);

            vc.UpdateUI();
        }

        private void UpdateUI(string condicao, string txtFormula, string txtDesc)
        {
            frmPrincipal.SetText(txtFormula, condicao);
            //txtDesc.Text = condicao.descricao;
        }



        public TradeSystem tradeSystem { get{
            return holder.GetTS(selectedTS);
        }  }

        public ConfigController configController { get; private set; }

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
