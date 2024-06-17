using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class AuthTest
    {
        private Auth authorization;

        [TestInitialize]
        public void Setup()
        {
            authorization = new Auth();
        }

        [TestMethod]
        public void Authenticate_Valid_RerurnTrue()
        {
            string login = "User1";
            string password = "12345";

            bool result = authorization.Authentificate(login, password);

            Assert.IsTrue(result); 
        }

        [TestMethod]
        public void Authenticate_Valid_RerurnFalse()
        {
            string login = "User2";
            string password = "12345";

            bool result = authorization.Authentificate(login, password);

            Assert.IsFalse(result);
        }
    }
}
