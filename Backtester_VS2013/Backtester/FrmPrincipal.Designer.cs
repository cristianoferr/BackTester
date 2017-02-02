namespace Backtester
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtVendaSaidaDireita = new System.Windows.Forms.TextBox();
            this.txtVendaSaidaOperador = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtVendaSaidaEsquerda = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtVendaEntradaDireita = new System.Windows.Forms.TextBox();
            this.txtVendaEntradaOperador = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtVendaEntradaEsquerda = new System.Windows.Forms.TextBox();
            this.panelCompra = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCompraSaidaDireita = new System.Windows.Forms.TextBox();
            this.txtCompraSaidaOperador = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCompraSaidaEsquerda = new System.Windows.Forms.TextBox();
            this.panelCompraEntrada = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCompraEntradaDireita = new System.Windows.Forms.TextBox();
            this.txtCompraEntradaOperador = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompraEntradaEsquerda = new System.Windows.Forms.TextBox();
            this.btnRemoveTS = new System.Windows.Forms.Button();
            this.btnAdicionaTS = new System.Windows.Forms.Button();
            this.listTradeSystems = new System.Windows.Forms.ListBox();
            this.btnSalvaTradeSystems = new System.Windows.Forms.Button();
            this.btnAtualizaTradeSystem = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNameTs = new System.Windows.Forms.TextBox();
            this.mainTab.SuspendLayout();
            this.tabConfig.SuspendLayout();
            this.tabTradeSystem.SuspendLayout();
            this.panelTradeSystem.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelCompra.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelCompraEntrada.SuspendLayout();
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
            this.mainTab.Size = new System.Drawing.Size(1017, 558);
            this.mainTab.TabIndex = 0;
            this.mainTab.SelectedIndexChanged += new System.EventHandler(this.mainTab_SelectedIndexChanged);
            // 
            // tabBackTester
            // 
            this.tabBackTester.Location = new System.Drawing.Point(4, 22);
            this.tabBackTester.Name = "tabBackTester";
            this.tabBackTester.Padding = new System.Windows.Forms.Padding(3);
            this.tabBackTester.Size = new System.Drawing.Size(1009, 532);
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
            this.tabConfig.Size = new System.Drawing.Size(1009, 532);
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
            this.tabTradeSystem.Controls.Add(this.btnRemoveTS);
            this.tabTradeSystem.Controls.Add(this.btnAdicionaTS);
            this.tabTradeSystem.Controls.Add(this.listTradeSystems);
            this.tabTradeSystem.Controls.Add(this.btnSalvaTradeSystems);
            this.tabTradeSystem.Location = new System.Drawing.Point(4, 22);
            this.tabTradeSystem.Name = "tabTradeSystem";
            this.tabTradeSystem.Size = new System.Drawing.Size(1009, 532);
            this.tabTradeSystem.TabIndex = 2;
            this.tabTradeSystem.Text = "TradeSystem";
            this.tabTradeSystem.UseVisualStyleBackColor = true;
            // 
            // panelTradeSystem
            // 
            this.panelTradeSystem.Controls.Add(this.txtNameTs);
            this.panelTradeSystem.Controls.Add(this.label14);
            this.panelTradeSystem.Controls.Add(this.btnAtualizaTradeSystem);
            this.panelTradeSystem.Controls.Add(this.groupBox2);
            this.panelTradeSystem.Controls.Add(this.panelCompra);
            this.panelTradeSystem.Location = new System.Drawing.Point(218, 23);
            this.panelTradeSystem.Name = "panelTradeSystem";
            this.panelTradeSystem.Size = new System.Drawing.Size(730, 462);
            this.panelTradeSystem.TabIndex = 7;
            this.panelTradeSystem.TabStop = false;
            this.panelTradeSystem.Text = "TradeSystem";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Location = new System.Drawing.Point(6, 253);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 191);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Condições de Venda";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtVendaSaidaDireita);
            this.groupBox3.Controls.Add(this.txtVendaSaidaOperador);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtVendaSaidaEsquerda);
            this.groupBox3.Location = new System.Drawing.Point(6, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(472, 71);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Condição de Saida";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(251, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Formula Direita";
            // 
            // txtVendaSaidaDireita
            // 
            this.txtVendaSaidaDireita.Location = new System.Drawing.Point(251, 45);
            this.txtVendaSaidaDireita.Name = "txtVendaSaidaDireita";
            this.txtVendaSaidaDireita.Size = new System.Drawing.Size(178, 20);
            this.txtVendaSaidaDireita.TabIndex = 4;
            // 
            // txtVendaSaidaOperador
            // 
            this.txtVendaSaidaOperador.FormattingEnabled = true;
            this.txtVendaSaidaOperador.Items.AddRange(new object[] {
            ">",
            ">=",
            "<",
            "<=",
            "=",
            "!="});
            this.txtVendaSaidaOperador.Location = new System.Drawing.Point(190, 45);
            this.txtVendaSaidaOperador.Name = "txtVendaSaidaOperador";
            this.txtVendaSaidaOperador.Size = new System.Drawing.Size(55, 21);
            this.txtVendaSaidaOperador.TabIndex = 3;
            this.txtVendaSaidaOperador.Text = "=";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Formula Esquerda";
            // 
            // txtVendaSaidaEsquerda
            // 
            this.txtVendaSaidaEsquerda.Location = new System.Drawing.Point(6, 45);
            this.txtVendaSaidaEsquerda.Name = "txtVendaSaidaEsquerda";
            this.txtVendaSaidaEsquerda.Size = new System.Drawing.Size(178, 20);
            this.txtVendaSaidaEsquerda.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtVendaEntradaDireita);
            this.groupBox4.Controls.Add(this.txtVendaEntradaOperador);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.txtVendaEntradaEsquerda);
            this.groupBox4.Location = new System.Drawing.Point(6, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(472, 71);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Condição de Entrada";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(251, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Formula Direita";
            // 
            // txtVendaEntradaDireita
            // 
            this.txtVendaEntradaDireita.Location = new System.Drawing.Point(251, 45);
            this.txtVendaEntradaDireita.Name = "txtVendaEntradaDireita";
            this.txtVendaEntradaDireita.Size = new System.Drawing.Size(178, 20);
            this.txtVendaEntradaDireita.TabIndex = 4;
            // 
            // txtVendaEntradaOperador
            // 
            this.txtVendaEntradaOperador.FormattingEnabled = true;
            this.txtVendaEntradaOperador.Items.AddRange(new object[] {
            ">",
            ">=",
            "<",
            "<=",
            "=",
            "!="});
            this.txtVendaEntradaOperador.Location = new System.Drawing.Point(190, 45);
            this.txtVendaEntradaOperador.Name = "txtVendaEntradaOperador";
            this.txtVendaEntradaOperador.Size = new System.Drawing.Size(55, 21);
            this.txtVendaEntradaOperador.TabIndex = 3;
            this.txtVendaEntradaOperador.Text = "=";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Formula Esquerda";
            // 
            // txtVendaEntradaEsquerda
            // 
            this.txtVendaEntradaEsquerda.Location = new System.Drawing.Point(6, 45);
            this.txtVendaEntradaEsquerda.Name = "txtVendaEntradaEsquerda";
            this.txtVendaEntradaEsquerda.Size = new System.Drawing.Size(178, 20);
            this.txtVendaEntradaEsquerda.TabIndex = 0;
            // 
            // panelCompra
            // 
            this.panelCompra.Controls.Add(this.groupBox1);
            this.panelCompra.Controls.Add(this.panelCompraEntrada);
            this.panelCompra.Location = new System.Drawing.Point(6, 56);
            this.panelCompra.Name = "panelCompra";
            this.panelCompra.Size = new System.Drawing.Size(489, 191);
            this.panelCompra.TabIndex = 8;
            this.panelCompra.TabStop = false;
            this.panelCompra.Text = "Condições de Compra";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtCompraSaidaDireita);
            this.groupBox1.Controls.Add(this.txtCompraSaidaOperador);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtCompraSaidaEsquerda);
            this.groupBox1.Location = new System.Drawing.Point(6, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 71);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Condição de Saida";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(251, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Formula Direita";
            // 
            // txtCompraSaidaDireita
            // 
            this.txtCompraSaidaDireita.Location = new System.Drawing.Point(251, 45);
            this.txtCompraSaidaDireita.Name = "txtCompraSaidaDireita";
            this.txtCompraSaidaDireita.Size = new System.Drawing.Size(178, 20);
            this.txtCompraSaidaDireita.TabIndex = 4;
            // 
            // txtCompraSaidaOperador
            // 
            this.txtCompraSaidaOperador.FormattingEnabled = true;
            this.txtCompraSaidaOperador.Items.AddRange(new object[] {
            ">",
            ">=",
            "<",
            "<=",
            "=",
            "!="});
            this.txtCompraSaidaOperador.Location = new System.Drawing.Point(190, 45);
            this.txtCompraSaidaOperador.Name = "txtCompraSaidaOperador";
            this.txtCompraSaidaOperador.Size = new System.Drawing.Size(55, 21);
            this.txtCompraSaidaOperador.TabIndex = 3;
            this.txtCompraSaidaOperador.Text = "=";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Formula Esquerda";
            // 
            // txtCompraSaidaEsquerda
            // 
            this.txtCompraSaidaEsquerda.Location = new System.Drawing.Point(6, 45);
            this.txtCompraSaidaEsquerda.Name = "txtCompraSaidaEsquerda";
            this.txtCompraSaidaEsquerda.Size = new System.Drawing.Size(178, 20);
            this.txtCompraSaidaEsquerda.TabIndex = 0;
            // 
            // panelCompraEntrada
            // 
            this.panelCompraEntrada.Controls.Add(this.label7);
            this.panelCompraEntrada.Controls.Add(this.txtCompraEntradaDireita);
            this.panelCompraEntrada.Controls.Add(this.txtCompraEntradaOperador);
            this.panelCompraEntrada.Controls.Add(this.label2);
            this.panelCompraEntrada.Controls.Add(this.txtCompraEntradaEsquerda);
            this.panelCompraEntrada.Location = new System.Drawing.Point(6, 28);
            this.panelCompraEntrada.Name = "panelCompraEntrada";
            this.panelCompraEntrada.Size = new System.Drawing.Size(472, 71);
            this.panelCompraEntrada.TabIndex = 9;
            this.panelCompraEntrada.TabStop = false;
            this.panelCompraEntrada.Text = "Condição de Entrada";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(251, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Formula Direita";
            // 
            // txtCompraEntradaDireita
            // 
            this.txtCompraEntradaDireita.Location = new System.Drawing.Point(251, 45);
            this.txtCompraEntradaDireita.Name = "txtCompraEntradaDireita";
            this.txtCompraEntradaDireita.Size = new System.Drawing.Size(178, 20);
            this.txtCompraEntradaDireita.TabIndex = 4;
            // 
            // txtCompraEntradaOperador
            // 
            this.txtCompraEntradaOperador.FormattingEnabled = true;
            this.txtCompraEntradaOperador.Items.AddRange(new object[] {
            ">",
            ">=",
            "<",
            "<=",
            "=",
            "!="});
            this.txtCompraEntradaOperador.Location = new System.Drawing.Point(190, 45);
            this.txtCompraEntradaOperador.Name = "txtCompraEntradaOperador";
            this.txtCompraEntradaOperador.Size = new System.Drawing.Size(55, 21);
            this.txtCompraEntradaOperador.TabIndex = 3;
            this.txtCompraEntradaOperador.Text = "=";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Formula Esquerda";
            // 
            // txtCompraEntradaEsquerda
            // 
            this.txtCompraEntradaEsquerda.Location = new System.Drawing.Point(6, 45);
            this.txtCompraEntradaEsquerda.Name = "txtCompraEntradaEsquerda";
            this.txtCompraEntradaEsquerda.Size = new System.Drawing.Size(178, 20);
            this.txtCompraEntradaEsquerda.TabIndex = 0;
            // 
            // btnRemoveTS
            // 
            this.btnRemoveTS.Location = new System.Drawing.Point(156, 32);
            this.btnRemoveTS.Name = "btnRemoveTS";
            this.btnRemoveTS.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveTS.TabIndex = 6;
            this.btnRemoveTS.Text = "-";
            this.btnRemoveTS.UseVisualStyleBackColor = true;
            this.btnRemoveTS.Click += new System.EventHandler(this.btnRemoveTS_Click);
            // 
            // btnAdicionaTS
            // 
            this.btnAdicionaTS.Location = new System.Drawing.Point(156, 3);
            this.btnAdicionaTS.Name = "btnAdicionaTS";
            this.btnAdicionaTS.Size = new System.Drawing.Size(32, 23);
            this.btnAdicionaTS.TabIndex = 5;
            this.btnAdicionaTS.Text = "+";
            this.btnAdicionaTS.UseVisualStyleBackColor = true;
            this.btnAdicionaTS.Click += new System.EventHandler(this.btnAdicionaTS_Click);
            // 
            // listTradeSystems
            // 
            this.listTradeSystems.Dock = System.Windows.Forms.DockStyle.Left;
            this.listTradeSystems.FormattingEnabled = true;
            this.listTradeSystems.Location = new System.Drawing.Point(0, 0);
            this.listTradeSystems.Name = "listTradeSystems";
            this.listTradeSystems.Size = new System.Drawing.Size(150, 532);
            this.listTradeSystems.TabIndex = 4;
            this.listTradeSystems.SelectedIndexChanged += new System.EventHandler(this.listTradeSystems_SelectedIndexChanged);
            // 
            // btnSalvaTradeSystems
            // 
            this.btnSalvaTradeSystems.Location = new System.Drawing.Point(156, 501);
            this.btnSalvaTradeSystems.Name = "btnSalvaTradeSystems";
            this.btnSalvaTradeSystems.Size = new System.Drawing.Size(159, 23);
            this.btnSalvaTradeSystems.TabIndex = 3;
            this.btnSalvaTradeSystems.Text = "Salva Trade Systems";
            this.btnSalvaTradeSystems.UseVisualStyleBackColor = true;
            this.btnSalvaTradeSystems.Click += new System.EventHandler(this.btnSalvaTradeSystems_Click);
            // 
            // btnAtualizaTradeSystem
            // 
            this.btnAtualizaTradeSystem.Location = new System.Drawing.Point(571, 9);
            this.btnAtualizaTradeSystem.Name = "btnAtualizaTradeSystem";
            this.btnAtualizaTradeSystem.Size = new System.Drawing.Size(159, 23);
            this.btnAtualizaTradeSystem.TabIndex = 10;
            this.btnAtualizaTradeSystem.Text = "Atualiza Trade System";
            this.btnAtualizaTradeSystem.UseVisualStyleBackColor = true;
            this.btnAtualizaTradeSystem.Click += new System.EventHandler(this.btnAtualizaTradeSystem_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Name:";
            // 
            // txtNameTs
            // 
            this.txtNameTs.Location = new System.Drawing.Point(65, 24);
            this.txtNameTs.Name = "txtNameTs";
            this.txtNameTs.Size = new System.Drawing.Size(424, 20);
            this.txtNameTs.TabIndex = 12;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 558);
            this.Controls.Add(this.mainTab);
            this.Name = "FrmPrincipal";
            this.Text = "Form1";
            this.mainTab.ResumeLayout(false);
            this.tabConfig.ResumeLayout(false);
            this.tabConfig.PerformLayout();
            this.tabTradeSystem.ResumeLayout(false);
            this.panelTradeSystem.ResumeLayout(false);
            this.panelTradeSystem.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panelCompra.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelCompraEntrada.ResumeLayout(false);
            this.panelCompraEntrada.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage tabBackTester;
        private System.Windows.Forms.TabPage tabConfig;
        public System.Windows.Forms.CheckBox chkFlagCompra;
        public System.Windows.Forms.CheckBox chkFlagVenda;
        private System.Windows.Forms.Button btnSalvaConfig;
        public System.Windows.Forms.MaskedTextBox txtRiscoMensal;
        public System.Windows.Forms.MaskedTextBox txtRiscoTrade;
        public System.Windows.Forms.MaskedTextBox txtPercentualTrade;
        public System.Windows.Forms.MaskedTextBox txtCustoOperacao;
        public System.Windows.Forms.MaskedTextBox txtCapitalMaximo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TabPage tabTradeSystem;
        public System.Windows.Forms.Button btnSalvaTradeSystems;
        public System.Windows.Forms.Button btnRemoveTS;
        public System.Windows.Forms.Button btnAdicionaTS;
        public System.Windows.Forms.ListBox listTradeSystems;
        public System.Windows.Forms.GroupBox panelTradeSystem;
        public System.Windows.Forms.GroupBox panelCompra;
        public System.Windows.Forms.GroupBox panelCompraEntrada;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCompraEntradaEsquerda;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtCompraEntradaDireita;
        public System.Windows.Forms.ComboBox txtCompraEntradaOperador;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtCompraSaidaDireita;
        public System.Windows.Forms.ComboBox txtCompraSaidaOperador;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtCompraSaidaEsquerda;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtVendaSaidaDireita;
        public System.Windows.Forms.ComboBox txtVendaSaidaOperador;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtVendaSaidaEsquerda;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtVendaEntradaDireita;
        public System.Windows.Forms.ComboBox txtVendaEntradaOperador;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtVendaEntradaEsquerda;
        public System.Windows.Forms.Button btnAtualizaTradeSystem;
        public System.Windows.Forms.TextBox txtNameTs;
        private System.Windows.Forms.Label label14;
    }
}

