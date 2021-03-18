using System.Collections.Generic;

namespace Model
{
    public static class Db
    {
        public static Dictionary<string, string> Accounts = new Dictionary<string, string>
        {
            //{"wuu","1234" },
        };

        public static string GetPasswordBy(string username)
        {
            return Accounts[username];
        }
    }
}