using Bussiness.Dto;
using Bussiness.ExceptionMessage;
using Bussiness.Log;
using Bussiness.Service.Campaign;
using Bussiness.Service.Command;
using Bussiness.Service.Order;
using Bussiness.Service.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bussiness.Service.Command
{
    public class CommandService : ICommandService
    {
        private static Dictionary<string, Action<string[]>> CommandList;
        private readonly IProductService productService;
        private readonly ICampaignService campaignService;
        private readonly IOrderService orderService;
        private TimeSpan systemTime;

        public CommandService(IProductService productService, ICampaignService campaignService, IOrderService orderService)
        {
            this.productService = productService;
            this.campaignService = campaignService;
            this.orderService = orderService;
            systemTime = new TimeSpan(0, 0, 0);
            Init();
        }
        public void ReadCommandFileAndExecute(string scenario)
        {
            string[] commands = File.ReadAllLines(scenario);
          
            foreach (string command in commands)
            {
                string[] splitString = command.Split();
                string[] splitParams = new string[splitString.Length - 1];

                for (int i =1; i<splitString.Length; i++)
                {
                   splitParams[i - 1] = splitString[i]; 
                }      
                Execute(splitString[0], splitParams);  
            }
        }
        public void Execute(string command, string[] arguments)
        {
            if (CommandList.ContainsKey(command))
            {
                CommandList[command].Invoke(arguments);
            }
            else
            {
                Logger.Log("Command is not found.");
            }
        }
        public void Init()
        {
            if (CommandList == null)
            {
                CommandList = new Dictionary<string, Action<string[]>>();

                CommandList.Add("create_product", CreateProductCommand);
                CommandList.Add("change_product_price", ChangeProductPrice);
                CommandList.Add("get_product_info", GetProductInfoCommand);
                CommandList.Add("create_order", CreateOrderCommand);
                CommandList.Add("create_campaign", CreateCampaignCommand);
                CommandList.Add("get_campaign_info", GetCampaignInfoCommand);
                CommandList.Add("increase_time", IncraseTimeCommand);
                CommandList.Add("clear", ClearCommands);
            }
        }

        private void ChangeProductPrice(string[] arguments)
        {
            string productCode = GetParameter<string>(arguments, 0);
            double price = GetParameter<double>(arguments, 1);
      
        }

        private void ClearCommands(string[] obj) => Console.Clear();

        public void CreateProductCommand(string[] arguments)
        {
            string productCode = GetParameter<string>(arguments, 0);
            double price = GetParameter<double>(arguments, 1);
            int stock = GetParameter<int>(arguments, 2);

            productService.CreateProduct(productCode, price, stock);


        }
        public void GetProductInfoCommand(string[] arguments)
        {
            string productCode = GetParameter<string>(arguments, 0);

            var product = productService.GetProduct(productCode);

            Logger.Log($"Product {product.ProductCode} info; price {product.Price}, stock {product.Stock}");


        }
        public void CreateOrderCommand(string[] arguments)
        {
            string productCode = GetParameter<string>(arguments, 0);
            int quantity = GetParameter<int>(arguments, 1);
            var product = productService.GetProduct(productCode);

            orderService.CreateOrder(product, quantity, systemTime);
        }
        public void CreateCampaignCommand(string[] arguments)
        {
            string campaignName = GetParameter<string>(arguments, 0);
            string productCode = GetParameter<string>(arguments, 1);
            int duration = GetParameter<int>(arguments, 2);
            int priceManipulationLimit = GetParameter<int>(arguments, 3);
            int targetSalesCount = GetParameter<int>(arguments, 4);

            var product = productService.GetProduct(productCode);

            campaignService.CreateCampaign(campaignName, product, duration, priceManipulationLimit, targetSalesCount);
        }
        public void GetCampaignInfoCommand(string[] arguments)
        {
            string campaignName = GetParameter<string>(arguments, 0);

            campaignService.GetCampaignInfo(campaignName);

        }
        public void IncraseTimeCommand(string[] arguments)
        {
            int totalIncrase = GetParameter<int>(arguments, 0);

            systemTime = systemTime.Add(new TimeSpan(totalIncrase, 0, 0));

            Logger.Log($"Time is {systemTime.ToString("hh\\:mm")}");

            productService.IncraseTime(totalIncrase);
        }
        private T GetParameter<T>(string[] values, int index)
        {
            try
            {
                return (T)Convert.ChangeType(values[index], typeof(T));
            }
            catch (Exception ex)
            {
                Logger.Log("Unexcepted paramater value.");
                return Activator.CreateInstance<T>();
            }
        }

        public TimeSpan GetTime()
        {
            return systemTime;
        }
    }
}