namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using Interfaces;
    using TennisShopSystem.DataTransferObjects.Order;

    public class OrderService : IOrderService
    {
        private readonly TennisShopDbContext dbContext;

        public OrderService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string[]> CreateAndSaveOrderAsync(OrderDetailsDto orderDto, string userId)
        {
            string[] result = new string[2];

            OrderDetails orderDetails = new()
            {
                FirstName = orderDto.FirstName,
                LastName = orderDto.LastName,
                Address = orderDto.Address,
                PhoneNumber = orderDto.PhoneNumber,
                EmailAddress = orderDto.EmailAddress,
                Comment = orderDto.Comment,
                TotalPrice = orderDto.TotalPrice,
                OrderedOn = DateTime.UtcNow
            };

            Order order = new()
            {
                UserId = Guid.Parse(userId),
                OrderDetailsId = orderDetails.Id
            };

            List<OrderedItem> orderedItems = new();

            foreach (var item in orderDto.Items)
            {
                OrderedItem orderedItem = new()
                {
                    ProductId = item.Product.Id.ToString(),
                    OrderedQuantity = item.ItemQuantity,
                    OrderDetailsId = orderDetails.Id
                };

                orderedItems.Add(orderedItem);
            }
            

            foreach (var item in orderDto.Items)
            {
                Product productToDecreaseQuantity = await this.dbContext
                    .Products
                    .FirstAsync(p => p.Id == item.Product.Id);

                if (productToDecreaseQuantity.AvailableQuantity - item.ItemQuantity < 0)
                {
                    result[0] = "Quantity";
                    result[1] = productToDecreaseQuantity.Title;

                    return result;
                }

                try
                {
                    productToDecreaseQuantity.AvailableQuantity -= item.ItemQuantity;

                    if (productToDecreaseQuantity.AvailableQuantity == 0)
                    {
                        productToDecreaseQuantity.IsAvailable = false;
                    }
                }
                catch (Exception)
                {
                    result[0] = "Error";
                    result[1] = string.Empty;

                    return result;
                }
            }

            try
            {
                await this.dbContext.OrderedItems.AddRangeAsync(orderedItems);
                await this.dbContext.OrdersDetails.AddAsync(orderDetails);
                await this.dbContext.Orders.AddAsync(order);
                await this.dbContext.SaveChangesAsync();
                result[0] = "Success";
                result[1] = order.Id.ToString();

                return result;
            }
            catch (Exception)
            {
                result[0] = "Error";
                result[1] = string.Empty;

                return result;
            }
        }

        public async Task<AllOrdersDto> GetAllOrdersAsync()
        {
            List<Order> orders = await this.dbContext.Orders.ToListAsync();

            List<OrderDetails> ordersDetails = await this.dbContext.OrdersDetails.ToListAsync();

            List<OrderedItem> orderedItems = await this.dbContext.OrderedItems.ToListAsync();

            AllOrdersDto allOrdersDto = new();

            foreach (var order in orders)
            {
                foreach (var orderDetails in ordersDetails.Where(od => od.Id == order.OrderDetailsId))
                {
                    OrderDetailsDto model = new()
                    {
                        Id = order.Id,
                        FirstName = orderDetails.FirstName,
                        LastName = orderDetails.LastName,
                        Address = orderDetails.Address,
                        PhoneNumber = orderDetails.PhoneNumber,
                        EmailAddress = orderDetails.EmailAddress,
                        TotalPrice = orderDetails.TotalPrice,
                        Items = new List<ShoppingCartItem>(),
                        OrderRegisteredOn = orderDetails.OrderedOn.ToString()
                    };

                    foreach (var orderedItem in orderedItems.Where(oi => oi.OrderDetailsId == orderDetails.Id))
                    {
                        var productToShoppingCartItem = await this.dbContext
                            .Products
                            .FirstOrDefaultAsync(pts => pts.Id.ToString() == orderedItem.ProductId);

                        ShoppingCartItem shoppingCartItem = new()
                        {
                            Product = productToShoppingCartItem!,
                            ItemQuantity = orderedItem.OrderedQuantity
                        };


                        model.Items.Add(shoppingCartItem);
                    }

                    allOrdersDto.Orders.Add(model);

                }

            }

            return allOrdersDto;
        }

        public async Task<AllOrdersDto> GetAllOrdersByUserIdAsync(string userId)
        {
            List<Order> orders = await this.dbContext
                .Orders
                .Where(o => o.UserId.ToString() == userId)
                .ToListAsync();

            List<OrderDetails> ordersDetails = new();

            foreach (var order in orders)
            {
                OrderDetails orderDetails = await this.dbContext
                    .OrdersDetails
                    .FirstAsync(od => od.Id == order.OrderDetailsId);

                ordersDetails.Add(orderDetails);
            }

            List<OrderedItem> allOrderedItems = new();

            foreach (var orderDetail in ordersDetails)
            {
                List<OrderedItem> orderedItems = await this.dbContext
                    .OrderedItems
                    .Where(oi => oi.OrderDetailsId == orderDetail.Id)
                    .ToListAsync();

                allOrderedItems.AddRange(orderedItems);
            }

            AllOrdersDto ordersDto = new();

            foreach (var order in orders)
            {
                foreach (var orderDetails in ordersDetails.Where(od => od.Id == order.OrderDetailsId))
                {
                    OrderDetailsDto orderDetailsDto = new()
                    {
                        Id = order.Id,
                        FirstName = orderDetails.FirstName,
                        LastName = orderDetails.LastName,
                        Address = orderDetails.Address,
                        PhoneNumber = orderDetails.PhoneNumber,
                        EmailAddress = orderDetails.EmailAddress,
                        TotalPrice = orderDetails.TotalPrice,
                        Items = new List<ShoppingCartItem>(),
                        OrderRegisteredOn = orderDetails.OrderedOn.ToString()
                    };

                    foreach (var orderedItem in allOrderedItems.Where(oi => oi.OrderDetailsId == orderDetails.Id))
                    {
                        var productToShoppingCartItem = await this.dbContext
                            .Products
                            .FirstOrDefaultAsync(pts => pts.Id.ToString() == orderedItem.ProductId);

                        ShoppingCartItem shoppingCartItem = new()
                        {
                            Product = productToShoppingCartItem!,
                            ItemQuantity = orderedItem.OrderedQuantity
                        };


                        orderDetailsDto.Items.Add(shoppingCartItem);
                    }

                    ordersDto.Orders.Add(orderDetailsDto);

                }

            }

            return ordersDto;
        }
    }
}
