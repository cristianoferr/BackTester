using Backtester.backend.DataManager;
using Backtester.backend.model.ativos;
using Backtester.backend.model.system.condicoes;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.PowerPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backtester.backend.model.system;
using System.Drawing;

namespace Backtester.controller
{
    public class GraphController
    {
        private FrmPrincipal frmPrincipal;
        FrmGraph frmGraph=null;
        Panel panelDraw;
        ShapeContainer canvas;

        IList<Shape> shapes;

        public GraphController(FrmPrincipal frmPrincipal)
        {
            this.frmPrincipal = frmPrincipal;
            shapes = new List<Shape>();


        }

        internal void ShowGraph(Operacao oper)
        {
            if (frmGraph == null || frmGraph.IsDisposed)
            {
                frmGraph = new FrmGraph();
                panelDraw = frmGraph.panelDraw;
                canvas = new ShapeContainer();
                canvas.Parent = panelDraw;
                shapes.Clear();
            }
            if (!frmGraph.Visible)
                frmGraph.Show(frmPrincipal);
            
            Clear(canvas);

            

            Candle candle = oper.candleInicial;
            maxValue =candle.GetValor(FormulaManager.HIGH);
            minValue = candle.GetValor(FormulaManager.LOW);
            maxPeriodos = 0;
            while (candle.candleAnterior != oper.candleFinal && candle!=candle.proximoCandle)
            {
                maxPeriodos++;
                if (candle.GetValor(FormulaManager.HIGH) > maxValue) maxValue = candle.GetValor(FormulaManager.HIGH);
                if (candle.GetValor(FormulaManager.LOW) < minValue) minValue = candle.GetValor(FormulaManager.LOW);
                candle = candle.proximoCandle;
            }
            maxValue *= 1.1f;
            minValue *= 0.9f;


            contaPers = 0;
            candle = oper.candleInicial;
            oper.stop.stopAtual = oper.stop.stopInicial;
            while (candle.candleAnterior != oper.candleFinal && candle != candle.proximoCandle)
            {
                contaPers++;
                DrawCandle(oper,oper.carteira.tradeSystem,candle);
                candle = candle.proximoCandle;
            }

            
        }
        int maxPeriodos = 0;
        float maxValue = 0;
        float minValue=0;
        int contaPers = 0;
        int border = 20;
        //int candleWidth

        private void DrawCandle(Operacao oper, TradeSystem tradeSystem, Candle candle)
        {
            float cw = candleWidth() * 0.8f;
            float x = GetWidthForPeriodo(contaPers)-cw/2;
            float dif = Math.Abs(candle.GetValor(FormulaManager.OPEN) - candle.GetValor(FormulaManager.CLOSE));
            float y = GetYPosition(candle.GetValor(FormulaManager.OPEN));
            float w =  cw;
            float h= GetHeightValueAbs(dif);
            Color cor=Color.Green;
            if (candle.GetValor(FormulaManager.OPEN) < candle.GetValor(FormulaManager.CLOSE))
            {
                cor = Color.Red;
                y -= h;
            }
            float vlrStop = oper.stop.CalcStop(candle);
            DrawLine(x, GetYPosition(vlrStop), x + cw, GetYPosition(vlrStop), Color.Red);



            RectangleShape corpo = new RectangleShape((int)x, (int)y, (int)w, (int)h);
            corpo.FillColor= cor;
            corpo.FillStyle = FillStyle.Solid;
            corpo.BorderColor = Color.Black;
            shapes.Add(corpo);
            corpo.Parent = canvas;

            DrawLine(x+cw/2, GetYPosition(candle.GetValor(FormulaManager.LOW)), x + cw / 2, GetYPosition(candle.GetValor(FormulaManager.HIGH)), Color.Black);
            //DrawLine(x-cw/2, GetYPosition(candle.GetValor(FormulaManager.LOW)), x + cw/2, GetYPosition(candle.GetValor(FormulaManager.LOW)), Color.Blue);
            //DrawLine(x - cw / 2, GetYPosition(candle.GetValor(FormulaManager.HIGH)), x + cw / 2, GetYPosition(candle.GetValor(FormulaManager.HIGH)), Color.Blue);

            



        }

        private void DrawLine(float x1, float y1, float x2, float y2,Color cor)
        {
            LineShape theLine = new LineShape();
            shapes.Add(theLine);
            theLine.Parent = canvas;
            theLine.BorderColor = cor;
            theLine.StartPoint = new System.Drawing.Point((int)x1, (int)y1);
            theLine.EndPoint = new System.Drawing.Point((int)x2, (int)y2);
        }

        float candleWidth()
        {
            return 1f / maxPeriodos * (panelDraw.Width - 2 * border);
        }


        float GetWidthForPeriodo(int p)
        {
            float w = border + p * candleWidth();
            return w;
        }

        float GetYPosition(float val)
        {
            
            return panelDraw.Height - GetHeightValue(val);
        }

        float GetHeightValue(float val)
        {
            float h = (val - minValue) / (maxValue - minValue);
            h = h * panelDraw.Height;
            return h;
        }

        float GetHeightValueAbs(float val)
        {
            float h = (val) / (maxValue - minValue);
            h = h * panelDraw.Height;
            return h;
        }

        private void Clear(ShapeContainer canvas)
        {
            foreach (Shape shape in shapes)
            {
                shape.Parent = null;
            }
            shapes.Clear();
        }
    }
}
