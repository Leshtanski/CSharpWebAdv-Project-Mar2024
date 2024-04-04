namespace TennisShopSystem.Services.Data.Interfaces
{
    using Web.ViewModels;

    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
