using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MeetupContestManager.Core.Entities;

namespace MeetupContestManager.Core.DataAccess
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
    }

    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(string connectionStringName) 
            : base(connectionStringName)
        {
        }

        public User GetUser(string username, string password)
        {
            using (var session = GetSession())
            {
                var user =
                    session
                        .Query<User>()
                        .Where(u => u.Username == username)
                        .SingleOrDefault();

                if (user == null)
                    return null;

                using (var sha = SHA256.Create())
                {
                    var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(user.PasswordSalt + password));

                    if (Convert.ToBase64String(computedHash) != user.PasswordHash)
                        return null;
                }

                return user;
            }
        }
    }
}