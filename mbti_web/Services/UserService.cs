using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using mbti_web.Entities;
using mbti_web.Models;
using mbti_web.Repository;
using mbti_web.Middleware;

namespace mbti_web.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUser _repuser;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        
        public UserService(IRepositoryUser repuser, IConfiguration configuration, IMapper mapper)
        {
            _repuser = repuser;
            _configuration = configuration;
            _mapper = mapper;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _repuser.GetAll().FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public Task<AuthenticateResponse>? Register(UserModel userModel)
        {
            if (CheckLoginUnique(userModel.Login))
            {
                var user = _mapper.Map<User>(userModel);

                //var addedUser = await _repuser.Add(user);

                _repuser.Add(user);

                var response = Authenticate(new AuthenticateRequest
                {
                    Login = user.Login,
                    Password = user.Password
                });

                return Task.FromResult(response);
            }
            else
                return null;
        }
        public void AddUser(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            _repuser.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _repuser.GetAll();
        }

        public User GetById(int id)
        {
            return _repuser.Find(id);
        }
        public bool CheckLoginUnique(string Login)
        {
            if (_repuser.GetAll().FirstOrDefault(u => u.Login == Login) == null)
                return true;
            else
                return false;
        }
        public void UpdateUser(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            _repuser.Update(user);
        }
        public void DeleteUser(User user)
        {
            _repuser.Remove(user);
        }
    }
}
