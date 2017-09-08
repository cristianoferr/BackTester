using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backtester.interfaces
{
    public class MockReferView : IReferView
    {
        private string title;

        public void AddItem(string v, object tradeSystem)
        {
        }

        public void AddList(string v, string papel)
        {
        }

        public void ClearList(string v)
        {
        }

        public void ClearRows(string v)
        {
        }

        public Control GetControl(string v)
        {
            return null;
        }

        public DataGridViewRowCollection GetRows(string v)
        {
            //return new DataGridViewRowCollection(null);
            return null;
        }

        public bool IsChecked(string v)
        {
            return true;
        }

        public void SetChecked(string v, bool flagCompra)
        {
        }

        public void SetEnabled(string v1, bool v2)
        {
        }

        public void SetListItem(string v, int index, object var)
        {
        }

        public void SetStatus(string v)
        {
        }

        public void SetText(string v1, string v2)
        {
        }

        public void SetTitle(string v)
        {
            this.title = v;
        }

        public void SetVisible(string v1, bool v2)
        {
        }

        public string Text(string v)
        {
            return v;
        }
    }
}
