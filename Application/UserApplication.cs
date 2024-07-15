using Application.Contracts;
using Application.Contracts.RecipeIngredientContracts;
using Application.Contracts.UserContracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using System.Linq.Expressions;

namespace Application
{
    public class UserApplication : IUserApplication
    {
        private IUserRepository _userRepository;

        public UserApplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ActivateUser(int userId)
        {
            var user = _userRepository.Get(userId);
            if (user != null)
            {
                user.Activate();
                _userRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public void AddRoleToUser(int userId, string role)
        {
            _userRepository.AddRole(userId, role);
        }

        public bool AddUser(CreateUserCommand usercmd,out int? userId)
        {
            var result = new OperationResult();
            if (_userRepository.Exists(u => u.Email == usercmd.Email || u.Username == usercmd.Username))
            {
                result.Failed("there is an other user with this email or username, please choose another");
                userId = null;
                return result.IsSucceeded;
            }
            var user = new User(usercmd.Username, usercmd.Email, usercmd.Password);
            user.AssignRole(new UserRoles(SampleRoles.user));
            _userRepository.Create(user);
            _userRepository.SaveChanges();
            result.Success("operation successed");
            userId = user.Id;
            return result.IsSucceeded;
        }

        public bool DeActiveUser(int userId)
        {
            var user = _userRepository.Get(userId);
            if (user != null)
            {
                user.DeActive();
                _userRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public UserViewModel FindUser(Expression<Func<User, bool>> expression)
        {
            var user = _userRepository.FindUser(expression);
            var uservm = new UserViewModel();
            if (user != null)
            {
                uservm.Username = user.Username;
                uservm.Email = user.Email;
                uservm.Password = user.Password;
                uservm.IsActive = user.IsActive;
                uservm.Id = user.Id;
                uservm.Roles = user.roles.ToList();
            }
            return uservm;
        }

        public List<UserViewModel> SelectAllUsers()
        {
            return _userRepository.Get().Select(t => new UserViewModel
            {
                Id = t.Id,
                IsActive = t.IsActive,
                Email = t.Email,
                Password = t.Password,
                Username = t.Username,
            }).OrderByDescending(t => t.Id).ToList();
        }

        public bool Update(UpdateUserCommand usercmd)
        {
            var user = _userRepository.FindUser(r => r.Id == usercmd.Id);
            if (user != null)
            {
                var updateobj = new User(usercmd.Username, usercmd.Email, usercmd.Password, usercmd.Id);
                _userRepository.Update(updateobj);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
