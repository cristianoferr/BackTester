﻿namespace Backtester
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.tabBackTester = new System.Windows.Forms.TabPage();
            this.tabControlBacktester = new System.Windows.Forms.TabControl();
            this.tabRuns = new System.Windows.Forms.TabPage();
            this.dataGridRuns = new System.Windows.Forms.DataGridView();
            this.MCObjeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Carteira = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxCapital = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minCapital = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.winLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ganhos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.perdidos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalGanho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalPerdido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percAcerto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxdias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minDias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Avg_dias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelRunsBottom = new System.Windows.Forms.Panel();
            this.dataGridOperacoes = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posicao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ativo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sentido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vlr_entrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vlr_saida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stopInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stopado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vlr_oper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Capital = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnRodaSingle = new System.Windows.Forms.Button();
            this.cbTradeSystem = new System.Windows.Forms.ComboBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.txtTestesNaTela = new System.Windows.Forms.MaskedTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnRemovePapel = new System.Windows.Forms.Button();
            this.btnAdicionaPapel = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.listPapeis = new System.Windows.Forms.ListBox();
            this.txtCapitalInicial = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCustoOperacao = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSalvaConfig = new System.Windows.Forms.Button();
            this.chkFlagVenda = new System.Windows.Forms.CheckBox();
            this.chkFlagCompra = new System.Windows.Forms.CheckBox();
            this.tabTradeSystem = new System.Windows.Forms.TabPage();
            this.panelTradeSystem = new System.Windows.Forms.GroupBox();
            this.tabTSOpcoes = new System.Windows.Forms.TabControl();
            this.tabDefinicoes = new System.Windows.Forms.TabPage();
            this.tabTSVars = new System.Windows.Forms.TabPage();
            this.labelVarId = new System.Windows.Forms.Label();
            this.txtVarSteps = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtVarVlrFinal = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtVarVlrInicial = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtVarDescricao = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtVarName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnRemoveVar = new System.Windows.Forms.Button();
            this.btnAdicionaVar = new System.Windows.Forms.Button();
            this.listTSVars = new System.Windows.Forms.ListBox();
            this.tabTSCompra = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtStopMovelCompra = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtStopInicialCompra = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.tabTSVenda = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txtStopMovelVenda = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtStopInicialVenda = new System.Windows.Forms.TextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNameTs = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panelTSLeft = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRemoveTS = new System.Windows.Forms.Button();
            this.btnAdicionaTS = new System.Windows.Forms.Button();
            this.btnSalvaTradeSystems = new System.Windows.Forms.Button();
            this.listTradeSystems = new System.Windows.Forms.ListBox();
            this.txtVarsDebug = new System.Windows.Forms.TextBox();
            this.btnRodaGP = new System.Windows.Forms.Button();
            this.mainTab.SuspendLayout();
            this.tabBackTester.SuspendLayout();
            this.tabControlBacktester.SuspendLayout();
            this.tabRuns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRuns)).BeginInit();
            this.panelRunsBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOperacoes)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabConfig.SuspendLayout();
            this.tabTradeSystem.SuspendLayout();
            this.panelTradeSystem.SuspendLayout();
            this.tabTSOpcoes.SuspendLayout();
            this.tabTSVars.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabTSCompra.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelCompraEntrada.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabTSVenda.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelTSLeft.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.mainTab.Size = new System.Drawing.Size(1112, 631);
            this.mainTab.TabIndex = 0;
            this.mainTab.SelectedIndexChanged += new System.EventHandler(this.mainTab_SelectedIndexChanged);
            // 
            // tabBackTester
            // 
            this.tabBackTester.Controls.Add(this.tabControlBacktester);
            this.tabBackTester.Controls.Add(this.panel5);
            this.tabBackTester.Location = new System.Drawing.Point(4, 22);
            this.tabBackTester.Name = "tabBackTester";
            this.tabBackTester.Padding = new System.Windows.Forms.Padding(3);
            this.tabBackTester.Size = new System.Drawing.Size(1104, 605);
            this.tabBackTester.TabIndex = 0;
            this.tabBackTester.Text = "Backtester";
            this.tabBackTester.UseVisualStyleBackColor = true;
            // 
            // tabControlBacktester
            // 
            this.tabControlBacktester.Controls.Add(this.tabRuns);
            this.tabControlBacktester.Controls.Add(this.tabPage1);
            this.tabControlBacktester.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlBacktester.Location = new System.Drawing.Point(3, 46);
            this.tabControlBacktester.Name = "tabControlBacktester";
            this.tabControlBacktester.SelectedIndex = 0;
            this.tabControlBacktester.Size = new System.Drawing.Size(1098, 556);
            this.tabControlBacktester.TabIndex = 0;
            // 
            // tabRuns
            // 
            this.tabRuns.Controls.Add(this.dataGridRuns);
            this.tabRuns.Controls.Add(this.panelRunsBottom);
            this.tabRuns.Controls.Add(this.panel7);
            this.tabRuns.Location = new System.Drawing.Point(4, 22);
            this.tabRuns.Name = "tabRuns";
            this.tabRuns.Padding = new System.Windows.Forms.Padding(3);
            this.tabRuns.Size = new System.Drawing.Size(1090, 530);
            this.tabRuns.TabIndex = 1;
            this.tabRuns.Text = "Runs";
            this.tabRuns.UseVisualStyleBackColor = true;
            // 
            // dataGridRuns
            // 
            this.dataGridRuns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRuns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MCObjeto,
            this.Carteira,
            this.colNr,
            this.colNome,
            this.resultado,
            this.maxCapital,
            this.minCapital,
            this.winLoss,
            this.trades,
            this.ganhos,
            this.perdidos,
            this.totalGanho,
            this.totalPerdido,
            this.percAcerto,
            this.maxdias,
            this.minDias,
            this.Avg_dias});
            this.dataGridRuns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridRuns.Location = new System.Drawing.Point(3, 32);
            this.dataGridRuns.Name = "dataGridRuns";
            this.dataGridRuns.Size = new System.Drawing.Size(1084, 293);
            this.dataGridRuns.TabIndex = 1;
            this.dataGridRuns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridRuns_CellContentClick);
            this.dataGridRuns.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridRuns_RowEnter);
            // 
            // MCObjeto
            // 
            this.MCObjeto.HeaderText = "MCObjeto";
            this.MCObjeto.Name = "MCObjeto";
            this.MCObjeto.Visible = false;
            // 
            // Carteira
            // 
            this.Carteira.HeaderText = "Carteira";
            this.Carteira.Name = "Carteira";
            this.Carteira.Visible = false;
            // 
            // colNr
            // 
            this.colNr.HeaderText = "#";
            this.colNr.Name = "colNr";
            this.colNr.Width = 30;
            // 
            // colNome
            // 
            this.colNome.HeaderText = "Nome";
            this.colNome.Name = "colNome";
            // 
            // resultado
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            this.resultado.DefaultCellStyle = dataGridViewCellStyle14;
            this.resultado.HeaderText = "Resultado";
            this.resultado.Name = "resultado";
            this.resultado.Width = 80;
            // 
            // maxCapital
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N2";
            dataGridViewCellStyle15.NullValue = null;
            this.maxCapital.DefaultCellStyle = dataGridViewCellStyle15;
            this.maxCapital.HeaderText = "Max Capital";
            this.maxCapital.Name = "maxCapital";
            this.maxCapital.Width = 80;
            // 
            // minCapital
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Format = "N2";
            dataGridViewCellStyle16.NullValue = null;
            this.minCapital.DefaultCellStyle = dataGridViewCellStyle16;
            this.minCapital.HeaderText = "Min Capital";
            this.minCapital.Name = "minCapital";
            this.minCapital.Width = 80;
            // 
            // winLoss
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "N2";
            dataGridViewCellStyle17.NullValue = null;
            this.winLoss.DefaultCellStyle = dataGridViewCellStyle17;
            this.winLoss.HeaderText = "$W/$L";
            this.winLoss.Name = "winLoss";
            this.winLoss.Width = 50;
            // 
            // trades
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "N0";
            dataGridViewCellStyle18.NullValue = null;
            this.trades.DefaultCellStyle = dataGridViewCellStyle18;
            this.trades.HeaderText = "Trades";
            this.trades.Name = "trades";
            this.trades.Width = 50;
            // 
            // ganhos
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.Format = "N0";
            dataGridViewCellStyle19.NullValue = null;
            this.ganhos.DefaultCellStyle = dataGridViewCellStyle19;
            this.ganhos.HeaderText = "Ganhos";
            this.ganhos.Name = "ganhos";
            this.ganhos.Width = 50;
            // 
            // perdidos
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.Format = "N0";
            dataGridViewCellStyle20.NullValue = null;
            this.perdidos.DefaultCellStyle = dataGridViewCellStyle20;
            this.perdidos.HeaderText = "Perdidos";
            this.perdidos.Name = "perdidos";
            this.perdidos.Width = 50;
            // 
            // totalGanho
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle21.Format = "N2";
            dataGridViewCellStyle21.NullValue = null;
            this.totalGanho.DefaultCellStyle = dataGridViewCellStyle21;
            this.totalGanho.HeaderText = "Total Ganho";
            this.totalGanho.Name = "totalGanho";
            this.totalGanho.Width = 80;
            // 
            // totalPerdido
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle22.Format = "N2";
            dataGridViewCellStyle22.NullValue = null;
            this.totalPerdido.DefaultCellStyle = dataGridViewCellStyle22;
            this.totalPerdido.HeaderText = "Total Perdido";
            this.totalPerdido.Name = "totalPerdido";
            this.totalPerdido.Width = 80;
            // 
            // percAcerto
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.Format = "N2";
            dataGridViewCellStyle23.NullValue = null;
            this.percAcerto.DefaultCellStyle = dataGridViewCellStyle23;
            this.percAcerto.HeaderText = "%Acerto";
            this.percAcerto.Name = "percAcerto";
            this.percAcerto.Width = 50;
            // 
            // maxdias
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle24.Format = "N0";
            dataGridViewCellStyle24.NullValue = null;
            this.maxdias.DefaultCellStyle = dataGridViewCellStyle24;
            this.maxdias.HeaderText = "Max.Dias";
            this.maxdias.Name = "maxdias";
            this.maxdias.Width = 50;
            // 
            // minDias
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle25.Format = "N0";
            dataGridViewCellStyle25.NullValue = null;
            this.minDias.DefaultCellStyle = dataGridViewCellStyle25;
            this.minDias.HeaderText = "Min.Dias";
            this.minDias.Name = "minDias";
            this.minDias.Width = 50;
            // 
            // Avg_dias
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle26.Format = "N0";
            dataGridViewCellStyle26.NullValue = null;
            this.Avg_dias.DefaultCellStyle = dataGridViewCellStyle26;
            this.Avg_dias.HeaderText = "Avg.Dias";
            this.Avg_dias.Name = "Avg_dias";
            this.Avg_dias.Width = 50;
            // 
            // panelRunsBottom
            // 
            this.panelRunsBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRunsBottom.Controls.Add(this.dataGridOperacoes);
            this.panelRunsBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRunsBottom.Location = new System.Drawing.Point(3, 325);
            this.panelRunsBottom.Name = "panelRunsBottom";
            this.panelRunsBottom.Size = new System.Drawing.Size(1084, 202);
            this.panelRunsBottom.TabIndex = 2;
            // 
            // dataGridOperacoes
            // 
            this.dataGridOperacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridOperacoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.posicao,
            this.ativo,
            this.periodoInicial,
            this.periodoFinal,
            this.sentido,
            this.quantidade,
            this.vlr_entrada,
            this.vlr_saida,
            this.stopInicial,
            this.Stopado,
            this.vlr_oper,
            this.Dif,
            this.Capital});
            this.dataGridOperacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridOperacoes.Location = new System.Drawing.Point(0, 0);
            this.dataGridOperacoes.Name = "dataGridOperacoes";
            this.dataGridOperacoes.Size = new System.Drawing.Size(1082, 200);
            this.dataGridOperacoes.TabIndex = 5;
            // 
            // number
            // 
            this.number.HeaderText = "#";
            this.number.Name = "number";
            this.number.Width = 30;
            // 
            // posicao
            // 
            this.posicao.HeaderText = "Pos#";
            this.posicao.Name = "posicao";
            this.posicao.Width = 30;
            // 
            // ativo
            // 
            this.ativo.HeaderText = "Papel";
            this.ativo.Name = "ativo";
            this.ativo.Width = 60;
            // 
            // periodoInicial
            // 
            this.periodoInicial.HeaderText = "Per.Inicial";
            this.periodoInicial.Name = "periodoInicial";
            this.periodoInicial.Width = 75;
            // 
            // periodoFinal
            // 
            this.periodoFinal.HeaderText = "Per.Final";
            this.periodoFinal.Name = "periodoFinal";
            this.periodoFinal.Width = 75;
            // 
            // sentido
            // 
            this.sentido.HeaderText = "Sentido";
            this.sentido.Name = "sentido";
            this.sentido.Width = 50;
            // 
            // quantidade
            // 
            this.quantidade.HeaderText = "Qtd.";
            this.quantidade.Name = "quantidade";
            this.quantidade.Width = 50;
            // 
            // vlr_entrada
            // 
            this.vlr_entrada.HeaderText = "Vlr.Entrada";
            this.vlr_entrada.Name = "vlr_entrada";
            this.vlr_entrada.Width = 50;
            // 
            // vlr_saida
            // 
            this.vlr_saida.HeaderText = "Vlr.Saida";
            this.vlr_saida.Name = "vlr_saida";
            this.vlr_saida.Width = 50;
            // 
            // stopInicial
            // 
            this.stopInicial.HeaderText = "StopInicial";
            this.stopInicial.Name = "stopInicial";
            this.stopInicial.Width = 50;
            // 
            // Stopado
            // 
            this.Stopado.HeaderText = "Stopado?";
            this.Stopado.Name = "Stopado";
            this.Stopado.Width = 50;
            // 
            // vlr_oper
            // 
            this.vlr_oper.HeaderText = "Vlr.Oper";
            this.vlr_oper.Name = "vlr_oper";
            this.vlr_oper.Width = 70;
            // 
            // Dif
            // 
            this.Dif.HeaderText = "Dif";
            this.Dif.Name = "Dif";
            this.Dif.Width = 65;
            // 
            // Capital
            // 
            this.Capital.HeaderText = "Capital";
            this.Capital.Name = "Capital";
            this.Capital.Width = 80;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtVarsDebug);
            this.panel7.Controls.Add(this.labelStatus);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1084, 29);
            this.panel7.TabIndex = 3;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelStatus.Location = new System.Drawing.Point(1064, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Padding = new System.Windows.Forms.Padding(10);
            this.labelStatus.Size = new System.Drawing.Size(20, 33);
            this.labelStatus.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1090, 530);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnRodaGP);
            this.panel5.Controls.Add(this.btnRodaSingle);
            this.panel5.Controls.Add(this.cbTradeSystem);
            this.panel5.Controls.Add(this.btnRun);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1098, 43);
            this.panel5.TabIndex = 1;
            // 
            // btnRodaSingle
            // 
            this.btnRodaSingle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRodaSingle.Location = new System.Drawing.Point(52, 0);
            this.btnRodaSingle.Name = "btnRodaSingle";
            this.btnRodaSingle.Size = new System.Drawing.Size(74, 43);
            this.btnRodaSingle.TabIndex = 4;
            this.btnRodaSingle.Text = "Roda Single";
            this.btnRodaSingle.UseVisualStyleBackColor = true;
            this.btnRodaSingle.Click += new System.EventHandler(this.btnRodaSingle_Click);
            // 
            // cbTradeSystem
            // 
            this.cbTradeSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTradeSystem.FormattingEnabled = true;
            this.cbTradeSystem.Location = new System.Drawing.Point(227, 12);
            this.cbTradeSystem.Name = "cbTradeSystem";
            this.cbTradeSystem.Size = new System.Drawing.Size(259, 21);
            this.cbTradeSystem.TabIndex = 2;
            this.cbTradeSystem.SelectedIndexChanged += new System.EventHandler(this.cbTradeSystem_SelectedIndexChanged);
            // 
            // btnRun
            // 
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRun.Location = new System.Drawing.Point(0, 0);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(52, 43);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Roda";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.txtTestesNaTela);
            this.tabConfig.Controls.Add(this.label24);
            this.tabConfig.Controls.Add(this.btnRemovePapel);
            this.tabConfig.Controls.Add(this.btnAdicionaPapel);
            this.tabConfig.Controls.Add(this.label23);
            this.tabConfig.Controls.Add(this.listPapeis);
            this.tabConfig.Controls.Add(this.txtCapitalInicial);
            this.tabConfig.Controls.Add(this.label1);
            this.tabConfig.Controls.Add(this.txtCustoOperacao);
            this.tabConfig.Controls.Add(this.label6);
            this.tabConfig.Controls.Add(this.btnSalvaConfig);
            this.tabConfig.Controls.Add(this.chkFlagVenda);
            this.tabConfig.Controls.Add(this.chkFlagCompra);
            this.tabConfig.Location = new System.Drawing.Point(4, 22);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfig.Size = new System.Drawing.Size(1104, 605);
            this.tabConfig.TabIndex = 1;
            this.tabConfig.Text = "Config";
            this.tabConfig.UseVisualStyleBackColor = true;
            // 
            // txtTestesNaTela
            // 
            this.txtTestesNaTela.Location = new System.Drawing.Point(255, 62);
            this.txtTestesNaTela.Mask = "00000000";
            this.txtTestesNaTela.Name = "txtTestesNaTela";
            this.txtTestesNaTela.Size = new System.Drawing.Size(99, 20);
            this.txtTestesNaTela.TabIndex = 21;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(252, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 13);
            this.label24.TabIndex = 20;
            this.label24.Text = "Testes Na Tela";
            // 
            // btnRemovePapel
            // 
            this.btnRemovePapel.Location = new System.Drawing.Point(67, 437);
            this.btnRemovePapel.Name = "btnRemovePapel";
            this.btnRemovePapel.Size = new System.Drawing.Size(32, 23);
            this.btnRemovePapel.TabIndex = 19;
            this.btnRemovePapel.Text = "-";
            this.btnRemovePapel.UseVisualStyleBackColor = true;
            this.btnRemovePapel.Click += new System.EventHandler(this.btnRemovePapel_Click);
            // 
            // btnAdicionaPapel
            // 
            this.btnAdicionaPapel.Location = new System.Drawing.Point(29, 436);
            this.btnAdicionaPapel.Name = "btnAdicionaPapel";
            this.btnAdicionaPapel.Size = new System.Drawing.Size(32, 23);
            this.btnAdicionaPapel.TabIndex = 18;
            this.btnAdicionaPapel.Text = "+";
            this.btnAdicionaPapel.UseVisualStyleBackColor = true;
            this.btnAdicionaPapel.Click += new System.EventHandler(this.btnAdicionaPapel_click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(27, 150);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 13);
            this.label23.TabIndex = 16;
            this.label23.Text = "Papéis";
            // 
            // listPapeis
            // 
            this.listPapeis.FormattingEnabled = true;
            this.listPapeis.Location = new System.Drawing.Point(29, 166);
            this.listPapeis.Name = "listPapeis";
            this.listPapeis.Size = new System.Drawing.Size(158, 264);
            this.listPapeis.TabIndex = 13;
            // 
            // txtCapitalInicial
            // 
            this.txtCapitalInicial.Location = new System.Drawing.Point(150, 62);
            this.txtCapitalInicial.Mask = "00000000";
            this.txtCapitalInicial.Name = "txtCapitalInicial";
            this.txtCapitalInicial.Size = new System.Drawing.Size(99, 20);
            this.txtCapitalInicial.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Capital Inicial";
            // 
            // txtCustoOperacao
            // 
            this.txtCustoOperacao.Location = new System.Drawing.Point(29, 62);
            this.txtCustoOperacao.Mask = "00000000";
            this.txtCustoOperacao.Name = "txtCustoOperacao";
            this.txtCustoOperacao.Size = new System.Drawing.Size(99, 20);
            this.txtCustoOperacao.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Custo por Operação";
            // 
            // btnSalvaConfig
            // 
            this.btnSalvaConfig.Location = new System.Drawing.Point(328, 496);
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
            this.tabTradeSystem.Size = new System.Drawing.Size(1104, 605);
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
            this.panelTradeSystem.Size = new System.Drawing.Size(928, 605);
            this.panelTradeSystem.TabIndex = 7;
            this.panelTradeSystem.TabStop = false;
            this.panelTradeSystem.Text = "TradeSystem";
            // 
            // tabTSOpcoes
            // 
            this.tabTSOpcoes.Controls.Add(this.tabDefinicoes);
            this.tabTSOpcoes.Controls.Add(this.tabTSVars);
            this.tabTSOpcoes.Controls.Add(this.tabTSCompra);
            this.tabTSOpcoes.Controls.Add(this.tabTSVenda);
            this.tabTSOpcoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTSOpcoes.Location = new System.Drawing.Point(3, 52);
            this.tabTSOpcoes.Name = "tabTSOpcoes";
            this.tabTSOpcoes.SelectedIndex = 0;
            this.tabTSOpcoes.Size = new System.Drawing.Size(922, 550);
            this.tabTSOpcoes.TabIndex = 14;
            // 
            // tabDefinicoes
            // 
            this.tabDefinicoes.Location = new System.Drawing.Point(4, 22);
            this.tabDefinicoes.Name = "tabDefinicoes";
            this.tabDefinicoes.Size = new System.Drawing.Size(914, 524);
            this.tabDefinicoes.TabIndex = 3;
            this.tabDefinicoes.Text = "Definições";
            this.tabDefinicoes.UseVisualStyleBackColor = true;
            // 
            // tabTSVars
            // 
            this.tabTSVars.Controls.Add(this.labelVarId);
            this.tabTSVars.Controls.Add(this.txtVarSteps);
            this.tabTSVars.Controls.Add(this.label19);
            this.tabTSVars.Controls.Add(this.txtVarVlrFinal);
            this.tabTSVars.Controls.Add(this.label18);
            this.tabTSVars.Controls.Add(this.txtVarVlrInicial);
            this.tabTSVars.Controls.Add(this.label17);
            this.tabTSVars.Controls.Add(this.txtVarDescricao);
            this.tabTSVars.Controls.Add(this.label16);
            this.tabTSVars.Controls.Add(this.txtVarName);
            this.tabTSVars.Controls.Add(this.label15);
            this.tabTSVars.Controls.Add(this.panel2);
            this.tabTSVars.Location = new System.Drawing.Point(4, 22);
            this.tabTSVars.Name = "tabTSVars";
            this.tabTSVars.Padding = new System.Windows.Forms.Padding(3);
            this.tabTSVars.Size = new System.Drawing.Size(767, 446);
            this.tabTSVars.TabIndex = 0;
            this.tabTSVars.Text = "Variaveis";
            this.tabTSVars.UseVisualStyleBackColor = true;
            // 
            // labelVarId
            // 
            this.labelVarId.AutoSize = true;
            this.labelVarId.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelVarId.Location = new System.Drawing.Point(764, 3);
            this.labelVarId.Margin = new System.Windows.Forms.Padding(0);
            this.labelVarId.Name = "labelVarId";
            this.labelVarId.Size = new System.Drawing.Size(0, 13);
            this.labelVarId.TabIndex = 27;
            // 
            // txtVarSteps
            // 
            this.txtVarSteps.Location = new System.Drawing.Point(260, 126);
            this.txtVarSteps.Name = "txtVarSteps";
            this.txtVarSteps.Size = new System.Drawing.Size(93, 20);
            this.txtVarSteps.TabIndex = 26;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(182, 126);
            this.label19.Margin = new System.Windows.Forms.Padding(0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 25;
            this.label19.Text = "Steps";
            // 
            // txtVarVlrFinal
            // 
            this.txtVarVlrFinal.Location = new System.Drawing.Point(260, 100);
            this.txtVarVlrFinal.Name = "txtVarVlrFinal";
            this.txtVarVlrFinal.Size = new System.Drawing.Size(93, 20);
            this.txtVarVlrFinal.TabIndex = 24;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(182, 100);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 13);
            this.label18.TabIndex = 23;
            this.label18.Text = "Vlr. Final";
            // 
            // txtVarVlrInicial
            // 
            this.txtVarVlrInicial.Location = new System.Drawing.Point(260, 73);
            this.txtVarVlrInicial.Name = "txtVarVlrInicial";
            this.txtVarVlrInicial.Size = new System.Drawing.Size(93, 20);
            this.txtVarVlrInicial.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(182, 73);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 21;
            this.label17.Text = "Vlr. Inicial";
            // 
            // txtVarDescricao
            // 
            this.txtVarDescricao.Location = new System.Drawing.Point(260, 44);
            this.txtVarDescricao.Name = "txtVarDescricao";
            this.txtVarDescricao.Size = new System.Drawing.Size(393, 20);
            this.txtVarDescricao.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(182, 44);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 13);
            this.label16.TabIndex = 19;
            this.label16.Text = "Descrição";
            // 
            // txtVarName
            // 
            this.txtVarName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVarName.Location = new System.Drawing.Point(260, 10);
            this.txtVarName.Name = "txtVarName";
            this.txtVarName.Size = new System.Drawing.Size(164, 20);
            this.txtVarName.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(182, 17);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Nome";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.listTSVars);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(176, 440);
            this.panel2.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnRemoveVar);
            this.panel4.Controls.Add(this.btnAdicionaVar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 409);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(176, 31);
            this.panel4.TabIndex = 12;
            // 
            // btnRemoveVar
            // 
            this.btnRemoveVar.Location = new System.Drawing.Point(44, 5);
            this.btnRemoveVar.Name = "btnRemoveVar";
            this.btnRemoveVar.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveVar.TabIndex = 13;
            this.btnRemoveVar.Text = "-";
            this.btnRemoveVar.UseVisualStyleBackColor = true;
            this.btnRemoveVar.Click += new System.EventHandler(this.btnRemoveVar_Click);
            // 
            // btnAdicionaVar
            // 
            this.btnAdicionaVar.Location = new System.Drawing.Point(6, 4);
            this.btnAdicionaVar.Name = "btnAdicionaVar";
            this.btnAdicionaVar.Size = new System.Drawing.Size(32, 23);
            this.btnAdicionaVar.TabIndex = 12;
            this.btnAdicionaVar.Text = "+";
            this.btnAdicionaVar.UseVisualStyleBackColor = true;
            this.btnAdicionaVar.Click += new System.EventHandler(this.btnAdicionaVar_Click);
            // 
            // listTSVars
            // 
            this.listTSVars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTSVars.FormattingEnabled = true;
            this.listTSVars.Location = new System.Drawing.Point(0, 0);
            this.listTSVars.Name = "listTSVars";
            this.listTSVars.Size = new System.Drawing.Size(176, 440);
            this.listTSVars.TabIndex = 5;
            this.listTSVars.SelectedIndexChanged += new System.EventHandler(this.listTSVars_SelectedIndexChanged);
            // 
            // tabTSCompra
            // 
            this.tabTSCompra.Controls.Add(this.groupBox2);
            this.tabTSCompra.Controls.Add(this.groupBox3);
            this.tabTSCompra.Controls.Add(this.panelCompraEntrada);
            this.tabTSCompra.Location = new System.Drawing.Point(4, 22);
            this.tabTSCompra.Name = "tabTSCompra";
            this.tabTSCompra.Padding = new System.Windows.Forms.Padding(3);
            this.tabTSCompra.Size = new System.Drawing.Size(767, 446);
            this.tabTSCompra.TabIndex = 1;
            this.tabTSCompra.Text = "Compra";
            this.tabTSCompra.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtStopMovelCompra);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.txtStopInicialCompra);
            this.groupBox2.Controls.Add(this.groupBox9);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(761, 115);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stop";
            // 
            // txtStopMovelCompra
            // 
            this.txtStopMovelCompra.Location = new System.Drawing.Point(71, 42);
            this.txtStopMovelCompra.Name = "txtStopMovelCompra";
            this.txtStopMovelCompra.Size = new System.Drawing.Size(628, 20);
            this.txtStopMovelCompra.TabIndex = 19;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(5, 45);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 13);
            this.label22.TabIndex = 18;
            this.label22.Text = "Stop Movel";
            // 
            // txtStopInicialCompra
            // 
            this.txtStopInicialCompra.Location = new System.Drawing.Point(72, 16);
            this.txtStopInicialCompra.Name = "txtStopInicialCompra";
            this.txtStopInicialCompra.Size = new System.Drawing.Size(628, 20);
            this.txtStopInicialCompra.TabIndex = 15;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label3);
            this.groupBox9.Controls.Add(this.textBox7);
            this.groupBox9.Controls.Add(this.textBox8);
            this.groupBox9.Location = new System.Drawing.Point(6, 115);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(706, 115);
            this.groupBox9.TabIndex = 10;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Condição de Saida";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Descrição:";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(6, 71);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(694, 38);
            this.textBox7.TabIndex = 1;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(8, 16);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(692, 38);
            this.textBox8.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Stop Inicial";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtCondSaidaCDesc);
            this.groupBox3.Controls.Add(this.txtCondSaidaC);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(761, 115);
            this.groupBox3.TabIndex = 12;
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
            this.txtCondSaidaC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
            this.panelCompraEntrada.Location = new System.Drawing.Point(3, 3);
            this.panelCompraEntrada.Name = "panelCompraEntrada";
            this.panelCompraEntrada.Size = new System.Drawing.Size(761, 115);
            this.panelCompraEntrada.TabIndex = 11;
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
            this.txtCondEntrC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCondEntrC.Location = new System.Drawing.Point(8, 16);
            this.txtCondEntrC.Multiline = true;
            this.txtCondEntrC.Name = "txtCondEntrC";
            this.txtCondEntrC.Size = new System.Drawing.Size(692, 38);
            this.txtCondEntrC.TabIndex = 0;
            // 
            // tabTSVenda
            // 
            this.tabTSVenda.Controls.Add(this.groupBox10);
            this.tabTSVenda.Controls.Add(this.groupBox5);
            this.tabTSVenda.Controls.Add(this.groupBox7);
            this.tabTSVenda.Location = new System.Drawing.Point(4, 22);
            this.tabTSVenda.Name = "tabTSVenda";
            this.tabTSVenda.Size = new System.Drawing.Size(767, 446);
            this.tabTSVenda.TabIndex = 2;
            this.tabTSVenda.Text = "Venda";
            this.tabTSVenda.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txtStopMovelVenda);
            this.groupBox10.Controls.Add(this.label21);
            this.groupBox10.Controls.Add(this.txtStopInicialVenda);
            this.groupBox10.Controls.Add(this.groupBox11);
            this.groupBox10.Controls.Add(this.label20);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox10.Location = new System.Drawing.Point(0, 230);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(767, 115);
            this.groupBox10.TabIndex = 15;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Stop";
            // 
            // txtStopMovelVenda
            // 
            this.txtStopMovelVenda.Location = new System.Drawing.Point(71, 42);
            this.txtStopMovelVenda.Name = "txtStopMovelVenda";
            this.txtStopMovelVenda.Size = new System.Drawing.Size(628, 20);
            this.txtStopMovelVenda.TabIndex = 17;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(5, 45);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 16;
            this.label21.Text = "Stop Movel";
            // 
            // txtStopInicialVenda
            // 
            this.txtStopInicialVenda.Location = new System.Drawing.Point(72, 16);
            this.txtStopInicialVenda.Name = "txtStopInicialVenda";
            this.txtStopInicialVenda.Size = new System.Drawing.Size(628, 20);
            this.txtStopInicialVenda.TabIndex = 15;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label5);
            this.groupBox11.Controls.Add(this.textBox12);
            this.groupBox11.Controls.Add(this.textBox13);
            this.groupBox11.Location = new System.Drawing.Point(6, 115);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(706, 115);
            this.groupBox11.TabIndex = 10;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Condição de Saida";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Descrição:";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(6, 71);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(694, 38);
            this.textBox12.TabIndex = 1;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(8, 16);
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(692, 38);
            this.textBox13.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 19);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(59, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "Stop Inicial";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtCondSaidaVDesc);
            this.groupBox5.Controls.Add(this.txtCondSaidaV);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 115);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(767, 115);
            this.groupBox5.TabIndex = 14;
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
            this.txtCondSaidaV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(767, 115);
            this.groupBox7.TabIndex = 13;
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
            this.txtCondEntrV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCondEntrV.Location = new System.Drawing.Point(8, 16);
            this.txtCondEntrV.Multiline = true;
            this.txtCondEntrV.Name = "txtCondEntrV";
            this.txtCondEntrV.Size = new System.Drawing.Size(692, 38);
            this.txtCondEntrV.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.txtNameTs);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(922, 36);
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
            this.panelTSLeft.Size = new System.Drawing.Size(176, 605);
            this.panelTSLeft.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRemoveTS);
            this.panel1.Controls.Add(this.btnAdicionaTS);
            this.panel1.Controls.Add(this.btnSalvaTradeSystems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 574);
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
            this.btnRemoveTS.Click += new System.EventHandler(this.btnRemoveTS_Click);
            // 
            // btnAdicionaTS
            // 
            this.btnAdicionaTS.Location = new System.Drawing.Point(6, 4);
            this.btnAdicionaTS.Name = "btnAdicionaTS";
            this.btnAdicionaTS.Size = new System.Drawing.Size(32, 23);
            this.btnAdicionaTS.TabIndex = 12;
            this.btnAdicionaTS.Text = "+";
            this.btnAdicionaTS.UseVisualStyleBackColor = true;
            this.btnAdicionaTS.Click += new System.EventHandler(this.btnAdicionaTS_Click);
            // 
            // btnSalvaTradeSystems
            // 
            this.btnSalvaTradeSystems.Location = new System.Drawing.Point(82, 5);
            this.btnSalvaTradeSystems.Name = "btnSalvaTradeSystems";
            this.btnSalvaTradeSystems.Size = new System.Drawing.Size(88, 23);
            this.btnSalvaTradeSystems.TabIndex = 11;
            this.btnSalvaTradeSystems.Text = "Salva";
            this.btnSalvaTradeSystems.UseVisualStyleBackColor = true;
            this.btnSalvaTradeSystems.Click += new System.EventHandler(this.btnSalvaTradeSystems_Click);
            // 
            // listTradeSystems
            // 
            this.listTradeSystems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTradeSystems.FormattingEnabled = true;
            this.listTradeSystems.Location = new System.Drawing.Point(0, 0);
            this.listTradeSystems.Name = "listTradeSystems";
            this.listTradeSystems.Size = new System.Drawing.Size(176, 605);
            this.listTradeSystems.TabIndex = 5;
            this.listTradeSystems.SelectedIndexChanged += new System.EventHandler(this.listTradeSystems_SelectedIndexChanged);
            // 
            // txtVarsDebug
            // 
            this.txtVarsDebug.Location = new System.Drawing.Point(3, 3);
            this.txtVarsDebug.Name = "txtVarsDebug";
            this.txtVarsDebug.Size = new System.Drawing.Size(942, 20);
            this.txtVarsDebug.TabIndex = 6;
            this.txtVarsDebug.TextChanged += new System.EventHandler(this.txtVarsDebug_TextChanged);
            // 
            // btnRodaGP
            // 
            this.btnRodaGP.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRodaGP.Location = new System.Drawing.Point(126, 0);
            this.btnRodaGP.Name = "btnRodaGP";
            this.btnRodaGP.Size = new System.Drawing.Size(74, 43);
            this.btnRodaGP.TabIndex = 5;
            this.btnRodaGP.Text = "Roda GP";
            this.btnRodaGP.UseVisualStyleBackColor = true;
            this.btnRodaGP.Click += new System.EventHandler(this.btnRodaGP_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 631);
            this.Controls.Add(this.mainTab);
            this.Name = "FrmPrincipal";
            this.Text = "BackTester";
            this.mainTab.ResumeLayout(false);
            this.tabBackTester.ResumeLayout(false);
            this.tabControlBacktester.ResumeLayout(false);
            this.tabRuns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRuns)).EndInit();
            this.panelRunsBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOperacoes)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tabConfig.ResumeLayout(false);
            this.tabConfig.PerformLayout();
            this.tabTradeSystem.ResumeLayout(false);
            this.panelTradeSystem.ResumeLayout(false);
            this.tabTSOpcoes.ResumeLayout(false);
            this.tabTSVars.ResumeLayout(false);
            this.tabTSVars.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabTSCompra.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panelCompraEntrada.ResumeLayout(false);
            this.panelCompraEntrada.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabTSVenda.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelTSLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl mainTab;
        public System.Windows.Forms.TabPage tabBackTester;
        public System.Windows.Forms.TabPage tabConfig;
        public System.Windows.Forms.CheckBox chkFlagCompra;
        public System.Windows.Forms.CheckBox chkFlagVenda;
        public System.Windows.Forms.Button btnSalvaConfig;
        public System.Windows.Forms.MaskedTextBox txtCustoOperacao;
        public System.Windows.Forms.Label label6;
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
        public System.Windows.Forms.TabControl tabTSOpcoes;
        public System.Windows.Forms.TabPage tabTSVars;
        public System.Windows.Forms.TabPage tabTSCompra;
        public System.Windows.Forms.TabPage tabTSVenda;
        public System.Windows.Forms.ListBox listTradeSystems;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Button btnRemoveVar;
        public System.Windows.Forms.Button btnAdicionaVar;
        public System.Windows.Forms.ListBox listTSVars;
        public System.Windows.Forms.TextBox txtVarSteps;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox txtVarVlrFinal;
        public System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtVarVlrInicial;
        public System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox txtVarDescricao;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtVarName;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label labelVarId;
        public System.Windows.Forms.MaskedTextBox txtCapitalInicial;
        public System.Windows.Forms.Label label1;
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
        public System.Windows.Forms.TabPage tabDefinicoes;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txtStopInicialCompra;
        public System.Windows.Forms.GroupBox groupBox9;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox7;
        public System.Windows.Forms.TextBox textBox8;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.GroupBox groupBox10;
        public System.Windows.Forms.TextBox txtStopInicialVenda;
        public System.Windows.Forms.GroupBox groupBox11;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBox12;
        public System.Windows.Forms.TextBox textBox13;
        public System.Windows.Forms.Label label20;
        public System.Windows.Forms.TextBox txtStopMovelVenda;
        public System.Windows.Forms.Label label21;
        public System.Windows.Forms.TextBox txtStopMovelCompra;
        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.TabControl tabControlBacktester;
        public System.Windows.Forms.TabPage tabRuns;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Button btnRun;
        public System.Windows.Forms.Button btnRemovePapel;
        public System.Windows.Forms.Button btnAdicionaPapel;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.ListBox listPapeis;
        public System.Windows.Forms.ComboBox cbTradeSystem;
        public System.Windows.Forms.DataGridView dataGridRuns;
        public System.Windows.Forms.Panel panelRunsBottom;
        public System.Windows.Forms.Button btnRodaSingle;
        public System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.Label labelStatus;
        public System.Windows.Forms.DataGridView dataGridOperacoes;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn posicao;
        private System.Windows.Forms.DataGridViewTextBoxColumn ativo;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn sentido;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn vlr_entrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn vlr_saida;
        private System.Windows.Forms.DataGridViewTextBoxColumn stopInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stopado;
        private System.Windows.Forms.DataGridViewTextBoxColumn vlr_oper;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dif;
        private System.Windows.Forms.DataGridViewTextBoxColumn Capital;
        public System.Windows.Forms.MaskedTextBox txtTestesNaTela;
        public System.Windows.Forms.Label label24;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCObjeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Carteira;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxCapital;
        private System.Windows.Forms.DataGridViewTextBoxColumn minCapital;
        private System.Windows.Forms.DataGridViewTextBoxColumn winLoss;
        private System.Windows.Forms.DataGridViewTextBoxColumn trades;
        private System.Windows.Forms.DataGridViewTextBoxColumn ganhos;
        private System.Windows.Forms.DataGridViewTextBoxColumn perdidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalGanho;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalPerdido;
        private System.Windows.Forms.DataGridViewTextBoxColumn percAcerto;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxdias;
        private System.Windows.Forms.DataGridViewTextBoxColumn minDias;
        private System.Windows.Forms.DataGridViewTextBoxColumn Avg_dias;
        public System.Windows.Forms.TextBox txtVarsDebug;
        public System.Windows.Forms.Button btnRodaGP;
    }
}

