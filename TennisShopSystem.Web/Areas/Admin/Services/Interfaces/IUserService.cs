namespace TennisShopSystem.Web.Areas.Admin.Services.Interfaces
{
    using ViewModels.User;

    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
