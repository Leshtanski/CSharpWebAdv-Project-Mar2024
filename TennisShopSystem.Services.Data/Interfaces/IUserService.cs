namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.Web.ViewModels.User;

    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
