using Bussiness.Service.Command;
using System;
using System.IO;
using System.Reflection;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceLoader dependencyLoader = new ServiceLoader();
            var provider = dependencyLoader.Init();

            var commandService = (ICommandService)provider.GetService(typeof(ICommandService));

            string scenario_1 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../../Presentation\CommandsFile\campaign_scenario_1.txt");
            string scenario_2 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../../Presentation\CommandsFile\campaign_scenario_2.txt");

            commandService.ReadCommandFileAndExecute(scenario_2);

        }
    }
}
