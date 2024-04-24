namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Interfaces;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.DataTransferObjects.User;

    public class UserService : IUserService
    {
        private readonly TennisShopDbContext dbContext;

        public UserService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserDto>> AllAsync()
        {
            List<UserDto> allUsers = await this.dbContext
                .Users
                .Select(u => new UserDto()
                {
                    Id = u.Id.ToString(),
                    Email = u.Email
                })
                .ToListAsync();

            foreach (UserDto user in allUsers)
            {
                Seller? seller = await this.dbContext
                    .Sellers
                    .FirstOrDefaultAsync(s => s.UserId.ToString() == user.Id);

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
