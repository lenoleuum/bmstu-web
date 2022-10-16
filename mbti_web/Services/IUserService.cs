using System.Collections.Generic;
using System.Threading.Tasks;
using mbti_web.Models;
using mbti_web.Entities;

namespace mbti_web.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(UserModel userModel);
        IEnumerable<User> GetAll();
        User GetById(int id);
        bool CheckLoginUnique(string Login);
        void UpdateUser(UserModel userModel);
        void DeleteUser(User user);
        void AddUser(UserModel userModel);
    }
}
