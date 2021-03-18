using System.Data;
using Model.CustomException;
using Model.Interface;
using Model.Repository;

namespace Model.Service
{
    public class AuthService : IAuthService
    {
        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new InvalidParameterException("Username/Password cannot be empty.");
            }

            var dt = GetDbTable(username, password);
            var dbErrorCode = dt.Rows[0].Field<int>("ErrorCode");
            if (dbErrorCode.Equals(0))
            {
                return true;
            }

            DebugHelper.Error($"{username} login failed. DbErrorCode:{dbErrorCode}");
            return false;
        }

        public virtual DataTable GetDbTable(string username, string password)
        {
            return AccountDb.Login(username, password);
        }
    }

}
