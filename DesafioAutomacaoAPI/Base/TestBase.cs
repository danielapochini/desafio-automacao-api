using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Settings;
using System;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

[assembly: TestFramework(AssemblyFixtureFramework.TypeName, AssemblyFixtureFramework.AssemblyName)]

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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            NodeJsHelper.LimparNodeModules();
        }
    }
}