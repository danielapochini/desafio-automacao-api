using DesafioAutomacaoAPI.Utils.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI
{
    public class NodeJsHelper
    {
        public static string RetornaDataAleatoriaEmTrintaDias()
        {   
            string node = @"C:\Program Files\nodejs\node.exe";  
            string arquivoScript = "scriptDataAleatoria.js";  

            string saida = ProcessNode.ExecutarProcesso(node, null, arquivoScript, DiretorioNode());

            string resultado = saida.Trim('\n');

            return resultado;
        }

        public static string InstalarNodeModules()
        {  
            return ProcessNode.ExecutarProcesso("cmd", "npm install", null, DiretorioNode());
        }

        public static void LimparNodeModules()
        {
            DirectoryInfo di = new DirectoryInfo(DiretorioNode());
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }
                dir.Delete(true);
            }
        }

        private static string DiretorioNode()
        {
            string subpasta = @"Utils\Resources\NodeJS";  
            return Path.Combine(Environment.CurrentDirectory, subpasta);
        } 

    }
}
