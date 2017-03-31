namespace Backtester
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.mainTab = new System.Windows.Forms.TabControl();
            this.tabBackTester = new System.Windows.Forms.TabPage();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.txtRiscoMensal = new System.Windows.Forms.MaskedTextBox();
            this.txtRiscoTrade = new System.Windows.Forms.MaskedTextBox();
            this.txtPercentualTrade = new System.Windows.Forms.MaskedTextBox();
            this.txtCustoOperacao = new System.Windows.Forms.MaskedTextBox();
            this.txtCapitalMaximo = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalvaConfig = new System.Windows.Forms.Button();
            this.chkFlagVenda = new System.Windows.Forms.CheckBox();
            this.chkFlagCompra = new System.Windows.Forms.CheckBox();
            this.tabTradeSystem = new System.Windows.Forms.TabPage();
            this.panelTradeSystem = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNameTs = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panelTSLeft = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRemoveTS = new System.Windows.Forms.Button();
            this.btnAdicionaTS = new System.Windows.Forms.Button();
            this.btnSalvaTradeSystems = new System.Windows.Forms.Button();
            this.tabTSOpcoes = new System.Windows.Forms.TabControl();
            this.tabTSVars = new System.Windows.Forms.TabPage();
            this.tabTSCompra = new System.Windows.Forms.TabPage();
            this.tabTSVenda = new System.Windows.Forms.TabPage();
            this.groupCondVenda = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCondSaidaVDesc = new System.Windows.Forms.TextBox();
            this.txtCondSaidaV = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCondEntrVDesc = new System.Windows.Forms.TextBox();
            this.txtCondEntrV = new System.Windows.Forms.TextBox();
            this.panelCompra = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCondSaidaCDesc = new System.Windows.Forms.TextBox();
            this.txtCondSaidaC = new System.Windows.Forms.TextBox();
            this.panelCompraEntrada = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCondEntrCDesc = new System.Windows.Forms.TextBox();
            this.txtCondEntrC = new System.Windows.Forms.TextBox();
            this.listTradeSystems = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listTSVars = new System.Windows.Forms.ListBox();
            this.mainTab.SuspendLayout();
            this.tabConfig.SuspendLayout();
            this.tabTradeSystem.SuspendLayout();
            this.panelTradeSystem.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelTSLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabTSOpcoes.SuspendLayout();
            this.tabTSVars.SuspendLayout();
            this.tabTSCompra.SuspendLayout();
            this.tabTSVenda.SuspendLayout();
            this.groupCondVenda.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.panelCompra.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelCompraEntrada.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.tabBackTester);
            this.mainTab.Controls.Add(this.tabConfig);
            this.mainTab.Controls.Add(this.tabTradeSystem);
            this.mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTab.Location = new System.Drawing.Point(0, 0);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(1252, 644);
            this.mainTab.TabIndex = 0;
            this.mainTab.SelectedIndexChanged += new System.EventHandler(this.mainTab_SelectedIndexChanged);
            // 
            // tabBackTester
            // 
            this.tabBackTester.Location = new System.Drawing.Point(4, 22);
            this.tabBackTester.Name = "tabBackTester";
            this.tabBackTester.Padding = new System.Windows.Forms.Padding(3);
            this.tabBackTester.Size = new System.Drawing.Size(1166, 617);
            this.tabBackTester.TabIndex = 0;
            this.tabBackTester.Text = "Backtester";
            this.tabBackTester.UseVisualStyleBackColor = true;
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.txtRiscoMensal);
            this.tabConfig.Controls.Add(this.txtRiscoTrade);
            this.tabConfig.Controls.Add(this.txtPercentualTrade);
            this.tabConfig.Controls.Add(this.txtCustoOperacao);
            this.tabConfig.Controls.Add(this.txtCapitalMaximo);
            this.tabConfig.Controls.Add(this.label6);
            this.tabConfig.Controls.Add(this.label5);
            this.tabConfig.Controls.Add(this.label4);
            this.tabConfig.Controls.Add(this.label3);
            this.tabConfig.Controls.Add(this.label1);
            this.tabConfig.Controls.Add(this.btnSalvaConfig);
            this.tabConfig.Controls.Add(this.chkFlagVenda);
            this.tabConfig.Controls.Add(this.chkFlagCompra);
            this.tabConfig.Location = new System.Drawing.Point(4, 22);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfig.Size = new System.Drawing.Size(1166, 617);
            this.tabConfig.TabIndex = 1;
            this.tabConfig.Text = "Config";
            this.tabConfig.UseVisualStyleBackColor = true;
            // 
            // txtRiscoMensal
            // 
            this.txtRiscoMensal.Location = new System.Drawing.Point(569, 57);
            this.txtRiscoMensal.Name = "txtRiscoMensal";
            this.txtRiscoMensal.Size = new System.Drawing.Size(59, 20);
            this.txtRiscoMensal.TabIndex = 13;
            // 
            // txtRiscoTrade
            // 
            this.txtRiscoTrade.Location = new System.Drawing.Point(455, 57);
            this.txtRiscoTrade.Name = "txtRiscoTrade";
            this.txtRiscoTrade.Size = new System.Drawing.Size(59, 20);
            this.txtRiscoTrade.TabIndex = 12;
            // 
            // txtPercentualTrade
            // 
            this.txtPercentualTrade.Location = new System.Drawing.Point(333, 57);
            this.txtPercentualTrade.Name = "txtPercentualTrade";
            this.txtPercentualTrade.Size = new System.Drawing.Size(86, 20);
            this.txtPercentualTrade.TabIndex = 11;
            // 
            // txtCustoOperacao
            // 
            this.txtCustoOperacao.Location = new System.Drawing.Point(195, 56);
            this.txtCustoOperacao.Mask = "00000000";
            this.txtCustoOperacao.Name = "txtCustoOperacao";
            this.txtCustoOperacao.Size = new System.Drawing.Size(99, 20);
            this.txtCustoOperacao.TabIndex = 10;
            // 
            // txtCapitalMaximo
            // 
            this.txtCapitalMaximo.Location = new System.Drawing.Point(30, 57);
            this.txtCapitalMaximo.Mask = "00000000";
            this.txtCapitalMaximo.Name = "txtCapitalMaximo";
            this.txtCapitalMaximo.Size = new System.Drawing.Size(106, 20);
            this.txtCapitalMaximo.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Custo por Operação";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Percentual Trade";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(452, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Risco por Trade";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stop Mensal";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Capital Maximo / Trade";
            // 
            // btnSalvaConfig
            // 
            this.btnSalvaConfig.Location = new System.Drawing.Point(29, 418);
            this.btnSalvaConfig.Name = "btnSalvaConfig";
            this.btnSalvaConfig.Size = new System.Drawing.Size(110, 23);
            this.btnSalvaConfig.TabIndex = 2;
            this.btnSalvaConfig.Text = "Salva Configuração";
            this.btnSalvaConfig.UseVisualStyleBackColor = true;
            this.btnSalvaConfig.Click += new System.EventHandler(this.btnSalvaConfig_Click);
            // 
            // chkFlagVenda
            // 
            this.chkFlagVenda.AutoSize = true;
            this.chkFlagVenda.Location = new System.Drawing.Point(150, 15);
            this.chkFlagVenda.Name = "chkFlagVenda";
            this.chkFlagVenda.Size = new System.Drawing.Size(110, 17);
            this.chkFlagVenda.TabIndex = 1;
            this.chkFlagVenda.Text = "Opera na Venda?";
            this.chkFlagVenda.UseVisualStyleBackColor = true;
            // 
            // chkFlagCompra
            // 
            this.chkFlagCompra.AutoSize = true;
            this.chkFlagCompra.Location = new System.Drawing.Point(29, 15);
            this.chkFlagCompra.Name = "chkFlagCompra";
            this.chkFlagCompra.Size = new System.Drawing.Size(115, 17);
            this.chkFlagCompra.TabIndex = 0;
            this.chkFlagCompra.Text = "Opera na Compra?";
            this.chkFlagCompra.UseVisualStyleBackColor = true;
            // 
            // tabTradeSystem
            // 
            this.tabTradeSystem.Controls.Add(this.panelTradeSystem);
            this.tabTradeSystem.Controls.Add(this.panelTSLeft);
            this.tabTradeSystem.Location = new System.Drawing.Point(4, 22);
            this.tabTradeSystem.Name = "tabTradeSystem";
            this.tabTradeSystem.Size = new System.Drawing.Size(1244, 618);
            this.tabTradeSystem.TabIndex = 2;
            this.tabTradeSystem.Text = "TradeSystem";
            this.tabTradeSystem.UseVisualStyleBackColor = true;
            // 
            // panelTradeSystem
            // 
            this.panelTradeSystem.Controls.Add(this.tabTSOpcoes);
            this.panelTradeSystem.Controls.Add(this.panel3);
            this.panelTradeSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTradeSystem.Location = new System.Drawing.Point(176, 0);
            this.panelTradeSystem.Name = "panelTradeSystem";
            this.panelTradeSystem.Size = new System.Drawing.Size(1068, 618);
            this.panelTradeSystem.TabIndex = 7;
            this.panelTradeSystem.TabStop = false;
            this.panelTradeSystem.Text = "TradeSystem";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.txtNameTs);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1062, 36);
            this.panel3.TabIndex = 13;
            // 
            // txtNameTs
            // 
            this.txtNameTs.Location = new System.Drawing.Point(55, 7);
            this.txtNameTs.Name = "txtNameTs";
            this.txtNameTs.Size = new System.Drawing.Size(928, 20);
            this.txtNameTs.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Margin = new System.Windows.Forms.Padding(50, 30, 50, 50);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label14.Size = new System.Drawing.Size(48, 23);
            this.label14.TabIndex = 13;
            this.label14.Text = "Name:";
            // 
            // panelTSLeft
            // 
            this.panelTSLeft.Controls.Add(this.panel1);
            this.panelTSLeft.Controls.Add(this.listTradeSystems);
            this.panelTSLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTSLeft.Location = new System.Drawing.Point(0, 0);
            this.panelTSLeft.Name = "panelTSLeft";
            this.panelTSLeft.Size = new System.Drawing.Size(176, 618);
            this.panelTSLeft.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRemoveTS);
            this.panel1.Controls.Add(this.btnAdicionaTS);
            this.panel1.Controls.Add(this.btnSalvaTradeSystems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 587);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 31);
            this.panel1.TabIndex = 12;
            // 
            // btnRemoveTS
            // 
            this.btnRemoveTS.Location = new System.Drawing.Point(44, 5);
            this.btnRemoveTS.Name = "btnRemoveTS";
            this.btnRemoveTS.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveTS.TabIndex = 13;
            this.btnRemoveTS.Text = "-";
            this.btnRemoveTS.UseVisualStyleBackColor = true;
            // 
            // btnAdicionaTS
            // 
            this.btnAdicionaTS.Location = new System.Drawing.Point(6, 4);
            this.btnAdicionaTS.Name = "btnAdicionaTS";
            this.btnAdicionaTS.Size = new System.Drawing.Size(32, 23);
            this.btnAdicionaTS.TabIndex = 12;
            this.btnAdicionaTS.Text = "+";
            this.btnAdicionaTS.UseVisualStyleBackColor = true;
            // 
            // btnSalvaTradeSystems
            // 
            this.btnSalvaTradeSystems.Location = new System.Drawing.Point(82, 5);
            this.btnSalvaTradeSystems.Name = "btnSalvaTradeSystems";
            this.btnSalvaTradeSystems.Size = new System.Drawing.Size(88, 23);
            this.btnSalvaTradeSystems.TabIndex = 11;
            this.btnSalvaTradeSystems.Text = "Salva";
            this.btnSalvaTradeSystems.UseVisualStyleBackColor = true;
            // 
            // tabTSOpcoes
            // 
            this.tabTSOpcoes.Controls.Add(this.tabTSVars);
            this.tabTSOpcoes.Controls.Add(this.tabTSCompra);
            this.tabTSOpcoes.Controls.Add(this.tabTSVenda);
            this.tabTSOpcoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTSOpcoes.Location = new System.Drawing.Point(3, 52);
            this.tabTSOpcoes.Name = "tabTSOpcoes";
            this.tabTSOpcoes.SelectedIndex = 0;
            this.tabTSOpcoes.Size = new System.Drawing.Size(1062, 563);
            this.tabTSOpcoes.TabIndex = 14;
            // 
            // tabTSVars
            // 
            this.tabTSVars.Controls.Add(this.panel2);
            this.tabTSVars.Location = new System.Drawing.Point(4, 22);
            this.tabTSVars.Name = "tabTSVars";
            this.tabTSVars.Padding = new System.Windows.Forms.Padding(3);
            this.tabTSVars.Size = new System.Drawing.Size(1054, 537);
            this.tabTSVars.TabIndex = 0;
            this.tabTSVars.Text = "Variaveis";
            this.tabTSVars.UseVisualStyleBackColor = true;
            // 
            // tabTSCompra
            // 
            this.tabTSCompra.Controls.Add(this.panelCompra);
            this.tabTSCompra.Location = new System.Drawing.Point(4, 22);
            this.tabTSCompra.Name = "tabTSCompra";
            this.tabTSCompra.Padding = new System.Windows.Forms.Padding(3);
            this.tabTSCompra.Size = new System.Drawing.Size(1054, 537);
            this.tabTSCompra.TabIndex = 1;
            this.tabTSCompra.Text = "Compra";
            this.tabTSCompra.UseVisualStyleBackColor = true;
            // 
            // tabTSVenda
            // 
            this.tabTSVenda.Controls.Add(this.groupCondVenda);
            this.tabTSVenda.Location = new System.Drawing.Point(4, 22);
            this.tabTSVenda.Name = "tabTSVenda";
            this.tabTSVenda.Size = new System.Drawing.Size(901, 300);
            this.tabTSVenda.TabIndex = 2;
            this.tabTSVenda.Text = "Venda";
            this.tabTSVenda.UseVisualStyleBackColor = true;
            // 
            // groupCondVenda
            // 
            this.groupCondVenda.Controls.Add(this.groupBox5);
            this.groupCondVenda.Controls.Add(this.groupBox7);
            this.groupCondVenda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCondVenda.Location = new System.Drawing.Point(0, 0);
            this.groupCondVenda.Name = "groupCondVenda";
            this.groupCondVenda.Size = new System.Drawing.Size(901, 300);
            this.groupCondVenda.TabIndex = 12;
            this.groupCondVenda.TabStop = false;
            this.groupCondVenda.Text = "Condições de Venda";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtCondSaidaVDesc);
            this.groupBox5.Controls.Add(this.txtCondSaidaV);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 131);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(895, 115);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Condição de Saida";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.textBox5);
            this.groupBox6.Controls.Add(this.textBox6);
            this.groupBox6.Location = new System.Drawing.Point(6, 115);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(706, 115);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Condição de Saida";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Descrição:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(6, 71);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(694, 38);
            this.textBox5.TabIndex = 1;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(8, 16);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(692, 38);
            this.textBox6.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Descrição:";
            // 
            // txtCondSaidaVDesc
            // 
            this.txtCondSaidaVDesc.Location = new System.Drawing.Point(6, 71);
            this.txtCondSaidaVDesc.Multiline = true;
            this.txtCondSaidaVDesc.Name = "txtCondSaidaVDesc";
            this.txtCondSaidaVDesc.Size = new System.Drawing.Size(694, 38);
            this.txtCondSaidaVDesc.TabIndex = 1;
            // 
            // txtCondSaidaV
            // 
            this.txtCondSaidaV.Location = new System.Drawing.Point(8, 16);
            this.txtCondSaidaV.Multiline = true;
            this.txtCondSaidaV.Name = "txtCondSaidaV";
            this.txtCondSaidaV.Size = new System.Drawing.Size(692, 38);
            this.txtCondSaidaV.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.txtCondEntrVDesc);
            this.groupBox7.Controls.Add(this.txtCondEntrV);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(3, 16);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(895, 115);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Condição de Entrada";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.textBox9);
            this.groupBox8.Controls.Add(this.textBox10);
            this.groupBox8.Location = new System.Drawing.Point(6, 115);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(706, 115);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Condição de Saida";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Descrição:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(6, 71);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(694, 38);
            this.textBox9.TabIndex = 1;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(8, 16);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(692, 38);
            this.textBox10.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Descrição:";
            // 
            // txtCondEntrVDesc
            // 
            this.txtCondEntrVDesc.Location = new System.Drawing.Point(6, 71);
            this.txtCondEntrVDesc.Multiline = true;
            this.txtCondEntrVDesc.Name = "txtCondEntrVDesc";
            this.txtCondEntrVDesc.Size = new System.Drawing.Size(694, 38);
            this.txtCondEntrVDesc.TabIndex = 1;
            // 
            // txtCondEntrV
            // 
            this.txtCondEntrV.Location = new System.Drawing.Point(8, 16);
            this.txtCondEntrV.Multiline = true;
            this.txtCondEntrV.Name = "txtCondEntrV";
            this.txtCondEntrV.Size = new System.Drawing.Size(692, 38);
            this.txtCondEntrV.TabIndex = 0;
            // 
            // panelCompra
            // 
            this.panelCompra.Controls.Add(this.groupBox3);
            this.panelCompra.Controls.Add(this.panelCompraEntrada);
            this.panelCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCompra.Location = new System.Drawing.Point(3, 3);
            this.panelCompra.Name = "panelCompra";
            this.panelCompra.Size = new System.Drawing.Size(1048, 531);
            this.panelCompra.TabIndex = 9;
            this.panelCompra.TabStop = false;
            this.panelCompra.Text = "Condições de Compra";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtCondSaidaCDesc);
            this.groupBox3.Controls.Add(this.txtCondSaidaC);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 131);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1042, 115);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Condição de Saida";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Location = new System.Drawing.Point(6, 115);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(706, 115);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Condição de Saida";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Descrição:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(6, 71);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(694, 38);
            this.textBox3.TabIndex = 1;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(8, 16);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(692, 38);
            this.textBox4.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Descrição:";
            // 
            // txtCondSaidaCDesc
            // 
            this.txtCondSaidaCDesc.Location = new System.Drawing.Point(6, 71);
            this.txtCondSaidaCDesc.Multiline = true;
            this.txtCondSaidaCDesc.Name = "txtCondSaidaCDesc";
            this.txtCondSaidaCDesc.Size = new System.Drawing.Size(694, 38);
            this.txtCondSaidaCDesc.TabIndex = 1;
            // 
            // txtCondSaidaC
            // 
            this.txtCondSaidaC.Location = new System.Drawing.Point(8, 16);
            this.txtCondSaidaC.Multiline = true;
            this.txtCondSaidaC.Name = "txtCondSaidaC";
            this.txtCondSaidaC.Size = new System.Drawing.Size(692, 38);
            this.txtCondSaidaC.TabIndex = 0;
            // 
            // panelCompraEntrada
            // 
            this.panelCompraEntrada.Controls.Add(this.groupBox1);
            this.panelCompraEntrada.Controls.Add(this.label2);
            this.panelCompraEntrada.Controls.Add(this.txtCondEntrCDesc);
            this.panelCompraEntrada.Controls.Add(this.txtCondEntrC);
            this.panelCompraEntrada.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCompraEntrada.Location = new System.Drawing.Point(3, 16);
            this.panelCompraEntrada.Name = "panelCompraEntrada";
            this.panelCompraEntrada.Size = new System.Drawing.Size(1042, 115);
            this.panelCompraEntrada.TabIndex = 9;
            this.panelCompraEntrada.TabStop = false;
            this.panelCompraEntrada.Text = "Condição de Entrada";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(6, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(706, 115);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Condição de Saida";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Descrição:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 71);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(694, 38);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(8, 16);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(692, 38);
            this.textBox2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Descrição:";
            // 
            // txtCondEntrCDesc
            // 
            this.txtCondEntrCDesc.Location = new System.Drawing.Point(6, 71);
            this.txtCondEntrCDesc.Multiline = true;
            this.txtCondEntrCDesc.Name = "txtCondEntrCDesc";
            this.txtCondEntrCDesc.Size = new System.Drawing.Size(694, 38);
            this.txtCondEntrCDesc.TabIndex = 1;
            // 
            // txtCondEntrC
            // 
            this.txtCondEntrC.Location = new System.Drawing.Point(8, 16);
            this.txtCondEntrC.Multiline = true;
            this.txtCondEntrC.Name = "txtCondEntrC";
            this.txtCondEntrC.Size = new System.Drawing.Size(692, 38);
            this.txtCondEntrC.TabIndex = 0;
            // 
            // listTradeSystems
            // 
            this.listTradeSystems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTradeSystems.FormattingEnabled = true;
            this.listTradeSystems.Location = new System.Drawing.Point(0, 0);
            this.listTradeSystems.Name = "listTradeSystems";
            this.listTradeSystems.Size = new System.Drawing.Size(176, 618);
            this.listTradeSystems.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.listTSVars);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(176, 531);
            this.panel2.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 500);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(176, 31);
            this.panel4.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // listTSVars
            // 
            this.listTSVars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTSVars.FormattingEnabled = true;
            this.listTSVars.Location = new System.Drawing.Point(0, 0);
            this.listTSVars.Name = "listTSVars";
            this.listTSVars.Size = new System.Drawing.Size(176, 531);
            this.listTSVars.TabIndex = 5;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 644);
            this.Controls.Add(this.mainTab);
            this.Name = "FrmPrincipal";
            this.Text = "Form1";
            this.mainTab.ResumeLayout(false);
            this.tabConfig.ResumeLayout(false);
            this.tabConfig.PerformLayout();
            this.tabTradeSystem.ResumeLayout(false);
            this.panelTradeSystem.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelTSLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabTSOpcoes.ResumeLayout(false);
            this.tabTSVars.ResumeLayout(false);
            this.tabTSCompra.ResumeLayout(false);
            this.tabTSVenda.ResumeLayout(false);
            this.groupCondVenda.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.panelCompra.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panelCompraEntrada.ResumeLayout(false);
            this.panelCompraEntrada.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl mainTab;
        public System.Windows.Forms.TabPage tabBackTester;
        public System.Windows.Forms.TabPage tabConfig;
        public System.Windows.Forms.CheckBox chkFlagCompra;
        public System.Windows.Forms.CheckBox chkFlagVenda;
        public System.Windows.Forms.Button btnSalvaConfig;
        public System.Windows.Forms.MaskedTextBox txtRiscoMensal;
        public System.Windows.Forms.MaskedTextBox txtRiscoTrade;
        public System.Windows.Forms.MaskedTextBox txtPercentualTrade;
        public System.Windows.Forms.MaskedTextBox txtCustoOperacao;
        public System.Windows.Forms.MaskedTextBox txtCapitalMaximo;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TabPage tabTradeSystem;
        public System.Windows.Forms.GroupBox panelTradeSystem;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.TextBox txtNameTs;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.Panel panelTSLeft;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnRemoveTS;
        public System.Windows.Forms.Button btnAdicionaTS;
        public System.Windows.Forms.Button btnSalvaTradeSystems;
        private System.Windows.Forms.TabControl tabTSOpcoes;
        private System.Windows.Forms.TabPage tabTSVars;
        private System.Windows.Forms.TabPage tabTSCompra;
        public System.Windows.Forms.GroupBox panelCompra;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtCondSaidaCDesc;
        public System.Windows.Forms.TextBox txtCondSaidaC;
        public System.Windows.Forms.GroupBox panelCompraEntrada;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCondEntrCDesc;
        public System.Windows.Forms.TextBox txtCondEntrC;
        private System.Windows.Forms.TabPage tabTSVenda;
        public System.Windows.Forms.GroupBox groupCondVenda;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.TextBox textBox6;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtCondSaidaVDesc;
        public System.Windows.Forms.TextBox txtCondSaidaV;
        public System.Windows.Forms.GroupBox groupBox7;
        public System.Windows.Forms.GroupBox groupBox8;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox textBox9;
        public System.Windows.Forms.TextBox textBox10;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtCondEntrVDesc;
        public System.Windows.Forms.TextBox txtCondEntrV;
        public System.Windows.Forms.ListBox listTradeSystems;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.ListBox listTSVars;
    }
}

