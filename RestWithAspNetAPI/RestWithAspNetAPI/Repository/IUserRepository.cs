using RestWithAspNetAPI.Data.VO;
using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Repository
{
    public interface IUserRepository
    {
        User? ValidateCredentials(UserVO user);

        User? ValidateCredentials(string username);

        bool RevokeToken(string username);

        User? RefreshUserInfo(User user);
    }
}
