﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backtester.controller;
using Backtester.backend.DataManager;
using Backtester.interfaces;
using Backtester.backend.model.system;
using System.Windows.Forms;
using System.Drawing;
using UsoComum;
using Backtester.backend.model.system.estatistica;
using FrontTester.frontend.model;
using Backtester.backend;

namespace FrontTester.frontend.controller
{
    public class FrontController
    {
        private IReferView frmFront;
        private ConfigController configController;
        CandidatoManager candidatoManager;
        FrontData frontData;
        FacadeBacktester facade;

        public FrontController(IReferView frmFront, ConfigController configController)
        {
            this.frmFront = frmFront;
            this.configController = configController;
            frontData = FrontData.LoadSaved();
            //precisa ser mesmo dia... o preço será sugerido para comprar
            configController.config.flagCompraMesmoDia = true;
            configController.config.flagVendaMesmoDia = true;
            
        }

        public void Init()
        {
            LoadCandidatos();
            ComboBox cbTradeSystemEmUso = frmFront.GetControl("cbTradeSystemEmUso") as ComboBox;
            if (frontData.tradeSystemEmUso != null)
            {
                cbTradeSystemEmUso.Items.Add(frontData.tradeSystemEmUso);
                cbTradeSystemEmUso.SelectedItem = frontData.tradeSystemEmUso;
            }

            facade = new FacadeBacktester();
            facade.LoadAtivos(configController.config.papeis, configController.config.tipoPeriodo, Consts.TIPO_CARGA_ATIVOS.DADOS_ATUAIS);
        }

        private void LoadCandidatos()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("FrontTester.frontend", "Backtester");
            candidatoManager = CandidatoManager.LoadSaved(path);
            for (int i = 0; i < candidatoManager.ranking.Count; i++)
            {
                CandidatoData data = candidatoManager.ranking[i];
                frmFront.AddItem("cbTradeSystem", data);
                frmFront.AddItem("cbTradeSystemEmUso", data);
            }
        }

        internal void AtualizaDados()
        {
            throw new NotImplementedException();
        }

        internal void AddPapeisDefault()
        {
            configController.config.papeis.Clear();
            configController.config.AddPapel("BOV", "ABEV3");
            configController.config.AddPapel("BOV", "BBAS3");
            configController.config.AddPapel("BOV", "BBDC4");
            configController.config.AddPapel("BOV", "BRAP4");
            configController.config.AddPapel("BOV", "BRFS3");
            configController.config.AddPapel("BOV", "BVMF3");
            configController.config.AddPapel("BOV", "CMIG4");
            configController.config.AddPapel("BOV", "CSNA3");
            configController.config.AddPapel("BOV", "GGBR4");
            configController.config.AddPapel("BOV", "ITUB4");
            configController.config.AddPapel("BOV", "EMBR3");
            configController.config.AddPapel("BOV", "VALE5");
            configController.config.AddPapel("BOV", "PETR4");
            configController.config.AddPapel("BOV", "CMIG4");
            configController.config.AddPapel("BOV", "PETR4");
            configController.config.AddPapel("BOV", "SUZB5");
            configController.config.AddPapel("BOV", "NATU3");
            configController.config.AddPapel("BOV", "WEGE3");
            configController.config.AddPapel("BOV", "CCRO3");
            configController.UpdateUI();
        }

        internal void Describe(CandidatoData candidatoData)
        {
            configController.Describe(candidatoData);
        }

        internal void AnalisaEntradas()
        {
            DataGridViewRowCollection RowsEntradas = frmFront.GetRows("dataGridEntradas");
            RowsEntradas.Clear();
            //TODO: continuar daqui

        }

        internal void ChangeCapitalAtual(float val)
        {
            frontData.capitalAtual = val;
            frontData.SaveToFile();
        }

        internal void ChangeTradeSystemEmUso(CandidatoData candidatoData)
        {
            frontData.tradeSystemEmUso = candidatoData;
            frontData.SaveToFile();
        }

        
    }
}
