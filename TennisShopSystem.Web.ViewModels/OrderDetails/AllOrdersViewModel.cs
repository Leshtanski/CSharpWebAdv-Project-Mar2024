using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisShopSystem.Web.ViewModels.OrderDetails
{
    public class AllOrdersViewModel
    {
        public List<OrderDetailsViewModel> Orders { get; set; } = new();
    }
}
