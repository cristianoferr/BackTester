using Backtester.backend;
using Backtester.backend.DataManager;
using Backtester.backend.model.system;

namespace Backtester
{
    class TradeSystemController
    {
        private FrmPrincipal frmPrincipal;
        TradeSystemHolder holder;



        public TradeSystemController(FrmPrincipal frmPrincipal)
        {
            this.frmPrincipal = frmPrincipal;
            holder = TradeSystemHolder.LoadSaved();
        }
        internal void UpdateValuesFromUI()
        {
            //throw new NotImplementedException();
        }
        private void UpdateUI()
        {
            throw new System.NotImplementedException();
        }

        internal void Salva()
        {
            holder.SaveToFile();
        }

        internal void RemoveTradeSystem(int p)
        {
            holder.tradeSystems.RemoveAt(p);
            selectedTS = -1;
            UpdateUI();
        }



        internal void AdicionaTS()
        {
            TradeSystem ts = new TradeSystem();
            ts.name = "TradeSystem Novo..." + holder.tradeSystems.Count;
            holder.tradeSystems.Add(ts);
            selectedTS = holder.tradeSystems.Count - 1;
            UpdateUI();
        }

        internal void SelectTS(int p)
        {
            selectedTS = p;
        }

        int selectedTS_ = -1;
        private int selectedTS
        {
            get { return selectedTS_; }
            set
            {
                //panelTradeSystem
                frmPrincipal.panelTradeSystem.Visible = value >= 0;
                if (value >= 0)
                {
                    UpdateUI(value);
                }
                //if (value>=)
                selectedTS_ = value;
            }
        }

        private void UpdateUI(int index)
        {
            TradeSystem ts = holder.GetTS(index);
            frmPrincipal.txtNameTs.Text = ts.name;
            frmPrincipal.txtCompraEntradaEsquerda.Text = ts.condicaoEntradaC.cond1;
            frmPrincipal.txtCompraEntradaOperador.Text = ts.condicaoEntradaC.operador.ToString();
            frmPrincipal.txtCompraEntradaDireita.Text = ts.condicaoEntradaC.cond2;

            frmPrincipal.txtCompraSaidaEsquerda.Text = ts.condicaoSaidaC.cond1;
            frmPrincipal.txtCompraSaidaOperador.Text = ts.condicaoSaidaC.operador.ToString();
            frmPrincipal.txtCompraSaidaDireita.Text = ts.condicaoSaidaC.cond2;

            frmPrincipal.txtVendaEntradaEsquerda.Text = ts.condicaoEntradaV.cond1;
            frmPrincipal.txtVendaEntradaOperador.Text = ts.condicaoEntradaV.operador.ToString();
            frmPrincipal.txtVendaEntradaDireita.Text = ts.condicaoEntradaV.cond2;

            frmPrincipal.txtVendaSaidaEsquerda.Text = ts.condicaoSaidaV.cond1;
            frmPrincipal.txtVendaSaidaOperador.Text = ts.condicaoSaidaV.operador.ToString();
            frmPrincipal.txtVendaSaidaDireita.Text = ts.condicaoSaidaV.cond2;
        }

        internal void AtualizaTradeSystem()
        {
            TradeSystem ts = holder.GetTS(selectedTS);
            ts.name = frmPrincipal.txtNameTs.Text;
            ts.condicaoEntradaC.cond1 = frmPrincipal.txtCompraEntradaEsquerda.Text;
            ts.condicaoEntradaC.operador = Util.ConverteOperador(frmPrincipal.txtCompraEntradaOperador.Text);
            ts.condicaoEntradaC.cond2 = frmPrincipal.txtCompraEntradaDireita.Text;

            ts.condicaoSaidaC.cond1 = frmPrincipal.txtCompraSaidaEsquerda.Text;
            ts.condicaoSaidaC.operador = Util.ConverteOperador(frmPrincipal.txtCompraSaidaOperador.Text);
            ts.condicaoSaidaC.cond2 = frmPrincipal.txtCompraSaidaDireita.Text;

            ts.condicaoEntradaV.cond1 = frmPrincipal.txtVendaEntradaEsquerda.Text;
            ts.condicaoEntradaV.operador = Util.ConverteOperador(frmPrincipal.txtVendaEntradaOperador.Text);
            ts.condicaoEntradaV.cond2 = frmPrincipal.txtVendaEntradaDireita.Text;

            ts.condicaoSaidaV.cond1 = frmPrincipal.txtVendaSaidaEsquerda.Text;
            ts.condicaoSaidaV.operador = Util.ConverteOperador(frmPrincipal.txtVendaSaidaOperador.Text);
            ts.condicaoSaidaV.cond2 = frmPrincipal.txtVendaSaidaDireita.Text;

        }
    }
}
