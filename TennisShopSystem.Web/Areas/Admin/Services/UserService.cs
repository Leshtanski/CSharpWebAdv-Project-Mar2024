﻿namespace TennisShopSystem.Web.Areas.Admin.Services
{
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Models;
    using ViewModels.User;
    using Interfaces;

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