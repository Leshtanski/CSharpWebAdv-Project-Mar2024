namespace TennisShopSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.ViewModels;
    using TennisShopSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using TennisShopSystem.Data.Models;

    public class UserService : IUserService
    {
        private readonly TennisShopDbContext dbContext;

        public UserService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
            List<UserViewModel> allUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id.ToString(),
                    Email = u.Email
                })
                .ToListAsync();

            foreach (UserViewModel user in allUsers)
            {
                Seller? seller = this.dbContext
                    .Sellers
                    .FirstOrDefault(s => s.UserId.ToString() == user.Id);

                if (seller != null)
                {
                    user.PhoneNumber = seller.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber = string.Empty;
                }
            }

            return allUsers;
        }
    }
}
