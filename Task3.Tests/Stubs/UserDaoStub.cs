using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs
{
    internal class UserDaoStub : IUserDao
    {
        private readonly IDictionary<int, IUser> data = new Dictionary<int, IUser>
        {
            { 1, new UserStab() }
        };

        public IUser GetUser(int id)
        {
            return this.data.ContainsKey(id) ? data[id] : null;
        }
    }
}