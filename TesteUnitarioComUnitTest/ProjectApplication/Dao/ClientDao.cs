using ProjectApplication.EF;
using ProjectApplication.Entities;
using ProjectApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Dao
{
    public class ClientDao : IClient
    {
        private ProjectUnitTestContext dbContext;

        public ClientDao(ProjectUnitTestContext context)
        {
            dbContext = context;
        }

        public IQueryable<Client> Clients
        {
            get { return dbContext.Client.AsQueryable(); }
        }

        public void Insert(Client client)
        {
            var result = Clients.Where(p => p.sEmail.Equals(client.sEmail));

            if (result.Count() > 0)
            {
                throw new InvalidOperationException("Cliente já cadastrado com esse email.");
            }

            if (client.dtBirth > DateTime.Today)
            {
                throw new InvalidOperationException("Data de nascimento não pode ser maior que atual.");
            }

            dbContext.Client.Add(client);
            dbContext.SaveChanges();
        }

        public Client Get(int iClientID)
        {
            var client = Clients.Where(p => p.iClientID.Equals(iClientID)).FirstOrDefault();

            if (client == null)
            {
                throw new InvalidOperationException("Cliente não foi encontrado.");
            }

            return client;
        }

        public void Update(Client client)
        {
            var result = Clients.Where(p => p.sEmail.Equals(client.sEmail) && p.iClientID != client.iClientID);

            if (result.Count() > 0)
            {
                throw new InvalidOperationException("Cliente já cadastrado com esse email.");
            }

            if (client.dtBirth > DateTime.Today)
            {
                throw new InvalidOperationException("Data de nascimento não pode ser maior que atual.");
            }

            dbContext.Entry(client).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Delete(int iClientID)
        {
            var result = Clients.Where(p => p.iClientID.Equals(iClientID));

            if (result.Count() == 0)
            {
                throw new InvalidOperationException("Cliente não localizado na base de dados");
            }
            dbContext.Client.Remove(result.FirstOrDefault());
            dbContext.SaveChanges();
        }
    }
}
