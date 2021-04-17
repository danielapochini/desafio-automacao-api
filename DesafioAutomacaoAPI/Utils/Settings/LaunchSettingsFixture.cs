using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Settings
{
    public class LaunchSettingsFixture
    {
        public LaunchSettingsFixture()
        {
            LaunchSettings();
        }

        public static void LaunchSettings()
        {
            using (var file = File.OpenText("Properties\\launchSettings.json"))
            {
            var reader = new JsonTextReader(file);
            var jObject = JObject.Load(reader);
            var variables = jObject.GetValue("profiles")
                                   .SelectMany(profiles => profiles.Children())
                                   .SelectMany(profile => profile.Children<JProperty>())
                                   .ToList();

#if QA
                var variaveisQa = variables.Where(prop => prop.Path == "profiles.DesafioAutomacaoAPI:QA.environmentVariables")
                                    .SelectMany(prop => prop.Value.Children<JProperty>())
                                    .ToList();
                foreach (var variable in variaveisQa)
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
#endif
#if HML

                var variaveisHml = variables.Where(prop => prop.Path == "profiles.DesafioAutomacaoAPI:HML.environmentVariables")
                                        .SelectMany(prop => prop.Value.Children<JProperty>())
                                        .ToList();
            foreach (var variable in variaveisHml)
                Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
#endif 
            }
        }

        public static string GetEnvironment()
        {
            string environmentName = Environment.GetEnvironmentVariable("AMBIENTE_API");

            return environmentName;
        }
    }
}
