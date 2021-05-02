using Allure.Commons;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Base
{
    public class TestBase : IDisposable
    { 
        public TestBase()
        { 
            OneTimeSetUp(); 
        }

        public static void OneTimeSetUp()
        {
            DatabaseHelper.ResetMantisDatabase();
            AllureHelper.SetupAllure();
        }

        public void Dispose()
        {
          
        }
    }
}
