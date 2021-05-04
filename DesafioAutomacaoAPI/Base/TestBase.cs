using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Settings;
using System;

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
            NodeJsHelper.InstalarNodeModules(); 
            AllureHelper.SetupAllure();
        }

        public void Dispose()
        {
            NodeJsHelper.LimparNodeModules();
        }
    }
}
