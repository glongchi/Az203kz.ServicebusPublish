using Az203kz.ServicebusPublish.ViewModels;
using System;
using System.Threading.Tasks;

namespace Az203kz.ServicebusPublish.Services
{
    public interface IPublisherService
    {
        Task Send(RestaurantOrderViewModel viewModel);
    }
}
