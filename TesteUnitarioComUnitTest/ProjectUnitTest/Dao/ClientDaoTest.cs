using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectApplication.EF;
using ProjectApplication.Entities;
using ProjectApplication.Interfaces;
using System.Linq;
using ProjectApplication.Dao;

namespace ProjectUnitTest.Core
{
    [TestClass]
    public class ClientDaoTest
    {
        private ProjectUnitTestContext clientContext = new ProjectUnitTestContext();

        private IClient clientRepository;
        private Client client;

        [TestInitialize]
        public void InicializarTest()
        {
            //LimparCenario();
            clientRepository = new ClientDao(clientContext);

            client = new Client()
            {
                sName = "Inicializar Test",
                sEmail = "test@gmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today
            };

            clientRepository.Insert(client);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotInsertClientWithSameEmail()
        {
            //Environment
            var AddNewClient = new Client()
            {
                sName = "Same Email",
                sEmail = "test@gmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today
            };

            //Action
            clientRepository.Insert(AddNewClient);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotInsertClientWithDateOfBirthGreaterThanCurrent()
        {
            //Environment

            var AddNewClient = new Client()
            {
                sName = "Birth Greater Than Current",
                sEmail = "test1@gmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today.AddDays(1)
            };

            //Action
            clientRepository.Insert(AddNewClient);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotChangeOtherClientWithSameEmail()
        {
            //Environment
            var client1 = new Client()
            {
                sName = "Same Email",
                sEmail = "test2@gmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today
            };
            clientRepository.Insert(client1);

            var client2 = new Client()
            {
                sName = "Same Email",
                sEmail = "test2@hotmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today
            };
            clientRepository.Insert(client2);

            var clients = clientRepository.Clients;
            client2 = clients.OrderByDescending(p => p.iClientID).FirstOrDefault();
            client2.sEmail = client1.sEmail;

            //Action
            clientRepository.Update(client2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotChangeClientWithDateOfBirthGreaterThanCurrent()
        {
            //Environment

            var AddNewClient = new Client()
            {
                sName = "Birth Greater Than Current",
                sEmail = "test4@gmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today.AddDays(1)
            };

            //Action
            clientRepository.Update(AddNewClient);
        }

        [TestMethod]
        public void GetClientByID()
        {
            //Environment
            var AddNewClient = new Client()
            {
                sName = "Get Client by ID",
                sEmail = "test5@gmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today
            };

            clientRepository.Insert(AddNewClient);

            var clients = clientRepository.Clients;
            
            var client1 = clients.OrderByDescending(p => p.iClientID).FirstOrDefault();

            var client2 = clientRepository.Get(client1.iClientID);

            //Action
            Assert.AreEqual(client1, client2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotGetClientThatDoesNotExists()
        {
            //Environment
            var AddNewClient = new Client()
            {
                sName = "Insert Client",
                sEmail = "insert_client@gmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today
            };

            clientRepository.Insert(AddNewClient);

            var clients = clientRepository.Clients;

            var client1 = clients.OrderByDescending(p => p.iClientID).FirstOrDefault();

            int iClientID = client1.iClientID + 1;

            //Action
            clientRepository.Get(iClientID);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotDeleteClientThatDoesNotExisits()
        {
            //Environment
            var AddNewClient = new Client()
            {
                sName = "Client That Does Not Exisits",
                sEmail = "test6@gmail.com",
                sPhone = "51999999999",
                dtBirth = DateTime.Today
            };

            clientRepository.Insert(AddNewClient);

            var clients = clientRepository.Clients;

            var lastClient = clients.OrderByDescending(p => p.iClientID).FirstOrDefault();

            int iClientID = lastClient.iClientID + 1;

            //Action
            clientRepository.Delete(iClientID);
        }

        [TestCleanup]
        public void LimparCenario()
        {
            var clientToRemove = from p in clientContext.Client
                                 select p;

            foreach (var client in clientToRemove)
            {
                clientContext.Client.Remove(client);
            }
            clientContext.SaveChanges();
        }
    }
}
