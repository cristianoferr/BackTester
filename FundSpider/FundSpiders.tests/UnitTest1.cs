using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Fundspider.Backend;

namespace FundSpiders.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAnalisaSaida()
        {
            string url=BuscaPapeis.URL_LISTA + "A";
            BuscaPapeis busca = new BuscaPapeis();
            IList<string> ret = new List<string>();
            string saidaLista=busca.LoadFromWeb(url);
            busca.AnalisaSaida(saidaLista, ret);
            Assert.IsTrue(ret.Count > 0);

        }
    }
}
