using Backtester.backend.model;
using Backtester.backend.model.system;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtester.backend.interfaces
{
    public interface ICaller
    {

        void SimpleUpdate();

        void UpdateApplication(Carteira carteira, MonteCarlo mc, int countLoops, int totalLoops);
    }
}
