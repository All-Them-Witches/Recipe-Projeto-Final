using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<int, User>
    {
        //find by id , select all , add , isexist , savechange 
        User FindUser(Expression<Func<User, bool>> expression);
        List<User> users(Expression<Func<User, bool>> expression);
        void Update(User user);
        void AddRole(int userId, string role);
    }
}
