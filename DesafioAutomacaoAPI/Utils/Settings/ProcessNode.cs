using System;
using System.Diagnostics;

namespace DesafioAutomacaoAPI.Utils.Settings
{
    public static class ProcessNode
    { 
        public static string ExecutarProcesso(string arquivo, string comando, string argumento, string diretorio)
        { 
            var processStartInfo = new ProcessStartInfo()
            {
                FileName = arquivo,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                WorkingDirectory = diretorio,
                Arguments = argumento
            };

            var processo = Process.Start(processStartInfo);

            if (processo == null)
            {
                throw new Exception("Process should not be null.");
            }

            processo.StandardInput.WriteLine($"{comando} & exit");
            processo.WaitForExit();

            var output = processo.StandardOutput.ReadToEnd();
            return output;
        }
    }
}
