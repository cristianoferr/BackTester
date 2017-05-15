using Backtester.backend.model.system;
using Backtester.controller;
using System.Windows.Forms;

namespace Backtester
{
    public partial class FrmPrincipal : Form
    {
        ConfigController configController;
        TradeSystemController tsController;
        GeneticProgrammingController gpController;
        public FrmPrincipal()
        {
            InitializeComponent();
            configController = new ConfigController(this);
            tsController = new TradeSystemController(this);
            gpController = new GeneticProgrammingController(this, configController);

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
                configController.RunSingle(ts);
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
            gpController.ValidaCandidatos();
        }





    }
}
