using Bussiness.Service.Campaign;
using Bussiness.Service.Command;
using Bussiness.Service.Order;
using Bussiness.Service.Product;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Presentation
{
    public class ServiceLoader
    {
        public ServiceProvider Init()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IProductService, ProductService>();
            serviceCollection.AddSingleton<IOrderService, OrderService>();
            serviceCollection.AddSingleton<ICampaignService, CampaignService>();
            serviceCollection.AddSingleton<ICommandService, CommandService>();
            return serviceCollection.BuildServiceProvider();

        }
    }
}
