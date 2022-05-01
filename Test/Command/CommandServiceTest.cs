using Bussiness.Service.Campaign;
using Bussiness.Service.Command;
using Bussiness.Service.Order;
using Bussiness.Service.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace Test.Command
{

    public class CommandServiceTest
    {
        private readonly CommandService _commandService;
        public CommandServiceTest() 
        {
            _commandService = new CommandService( new ProductService(), new CampaignService(new OrderService()), new OrderService());
        }
        [Fact]
        public void Command_Should_Read()
        {
            string scenario_1 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../../Presentation\CommandsFile\campaign_scenario_1.txt");
            string scenario_2 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../../Presentation\CommandsFile\campaign_scenario_2.txt");


            _commandService.ReadCommandFileAndExecute(scenario_1);

        }
    }
}
