using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class Auth
    {
        private Dictionary<string, string> users = new Dictionary<string, string>
        {
            {"User1", "12345"},
            {"User3", "12345"},
        };

        public bool Authentificate(string login, string password)
        {
            return users.ContainsKey(login) && users[login] == password;
        }

    }
}
