using Backtester.backend;
using Backtester.backend.model.system;
namespace Backtester.controller
{
    public class ConfigController : IController
    {
        private FrmPrincipal frmPrincipal;
        public static Config config;

        public ConfigController(FrmPrincipal frmPrincipal)
        {
            this.frmPrincipal = frmPrincipal;
            config = Config.LoadSaved();
            UpdateUI();
        }

        public virtual void UpdateUI()
        {
            frmPrincipal.chkFlagCompra.Checked = config.flagCompra;
            frmPrincipal.chkFlagVenda.Checked = config.flagVenda;
            frmPrincipal.txtCapitalMaximo.Text = config.maxCapitalTrade.ToString();
            frmPrincipal.txtCustoOperacao.Text = config.custoOperacao.ToString();
            frmPrincipal.txtPercentualTrade.Text = config.percTrade.ToString();
            frmPrincipal.txtRiscoMensal.Text = config.stopMensal.ToString();
            frmPrincipal.txtRiscoTrade.Text = config.riscoTrade.ToString();
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
            try
            {
                config.maxCapitalTrade = int.Parse(frmPrincipal.txtCapitalMaximo.Text);
                config.custoOperacao = int.Parse(frmPrincipal.txtCustoOperacao.Text);
                config.percTrade = float.Parse(frmPrincipal.txtPercentualTrade.Text);
                config.stopMensal = float.Parse(frmPrincipal.txtRiscoMensal.Text);
                config.riscoTrade = float.Parse(frmPrincipal.txtRiscoTrade.Text);

            }
            catch (System.Exception e)
            {
                Util.Error(e.Message);
            }

        }
    }
}
