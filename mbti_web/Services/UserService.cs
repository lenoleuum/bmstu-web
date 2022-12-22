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

        /*public Task<AuthenticateResponse>? Register(AuthenticateRequest userRequest)
        {
            if (CheckLoginUnique(userRequest.Login))
            {
                var user = _mapper.Map<User>(userRequest);

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
        }*/
        public void Register(AuthenticateRequest userModel)
        {
            if (CheckLoginUnique(userModel.Login))
            {
                User user = _mapper.Map<User>(userModel);

                user.Telagram = "";
                user.Email = "";
                user.Dateofbirth = new DateTime();
                user.Nickname = "";

                _repuser.Add(user);
            }
            else
            {
                // todo: add logger
            }
        }
        public void AddUser(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            _repuser.Add(user);
        }

        public List<UserModel> GetAll()
        {
            List<UserModel> res = new List<UserModel>();

            foreach (User u in _repuser.GetAll())
                res.Add(_mapper.Map<UserModel>(u));

            return res;
        }

        public UserModel GetById(int id)
        {
            return _mapper.Map<UserModel>(_repuser.Find(id));
        }
        public UserModel GetByLogin(string login)
        {
            return _mapper.Map<UserModel>(_repuser.GetAll().Where(u => u.Login == login).FirstOrDefault());
        }
        public User GetByIdUser(int id)
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
        public void DeleteUser(UserModel user)
        {
            _repuser.Remove(_mapper.Map<User>(user));
        }
    }
}
