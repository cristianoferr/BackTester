using System.Windows.Forms;

namespace Backtester
{
    public partial class FrmPrincipal : Form
    {
        ConfigController configController;
        TradeSystemController tsController;
        public FrmPrincipal()
        {
            InitializeComponent();
            configController = new ConfigController(this);
            tsController = new TradeSystemController(this);

        }

        private void btnSalvaConfig_Click(object sender, System.EventArgs e)
        {
            configController.Salva();
        }

        private void mainTab_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            configController.UpdateValuesFromUI();
            tsController.UpdateValuesFromUI();
        }

        private void btnSalvaTradeSystems_Click(object sender, System.EventArgs e)
        {
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
            tsController.SelectTS(listTradeSystems.SelectedIndex);
        }

        private void btnAtualizaTradeSystem_Click(object sender, System.EventArgs e)
        {
            tsController.AtualizaTradeSystem();
        }

    }
}
