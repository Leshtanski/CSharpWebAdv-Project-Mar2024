namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.DataTransferObjects.User;

    public interface IUserService
    {
        Task<IEnumerable<UserDto>> AllAsync();
    }
}
