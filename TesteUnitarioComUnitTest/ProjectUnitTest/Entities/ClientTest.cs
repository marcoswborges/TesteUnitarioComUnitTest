using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectApplication.Entities;

namespace ProjectUnitTest.Entities
{
    /// <summary>
    /// Summary description for ClientTest
    /// </summary>
    [TestClass]
    public class ClientTest
    {
        public Client client1, client2;

        [TestInitialize]
        public void StartTest()
        {
            client1 = new Client()
            {
                iClientID = 1,
                sName = "Marcos"
            };
        }

        [TestMethod]
        public void EnsureThatTwoClientsAreEqualWhenTheyHaveSameID()
        {
            client2 = new Client()
            {
                iClientID = 1,
                sName = "Borges"
            };

            Assert.AreEqual(client1, client2);
        }
    }
}
