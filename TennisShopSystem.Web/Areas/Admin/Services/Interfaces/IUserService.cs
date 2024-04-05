namespace TennisShopSystem.Web.Areas.Admin.Services.Interfaces
{
    using TennisShopSystem.Web.Areas.Admin.ViewModels.User;

    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
