using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<int, User>, IUserRepository
    {
        private Context _context;

        public UserRepository(Context context)
            : base(context)
        {
            _context = context;
        }

        public void AddRole(int userId, string role)
        {
            var user = FindUser(u => u.Id == userId);
            if (user != null)
            {
                user.AssignRole(new UserRoles(role));
                _context.SaveChanges();
            }
        }

        public User FindUser(Expression<Func<User, bool>> expression)
        {
            return _context.users.Include(t=>t.roles).FirstOrDefault(expression);
        }

        public void Update(User user)
        {
            var User = Get(user.Id);
            User.Edit(user.Username, user.Email);
            User.ChangePassword(user.Password);
            _context.SaveChanges();
        }

        public List<User> users(Expression<Func<User, bool>> expression)
        {
            return _context.users.Where(expression).ToList();
        }
    }
}
