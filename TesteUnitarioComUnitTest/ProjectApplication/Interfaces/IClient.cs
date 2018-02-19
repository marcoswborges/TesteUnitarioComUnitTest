using ProjectApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Interfaces
{
    public interface IClient
    {
        IQueryable<Client> Clients { get; }

        void Insert(Client client);

        Client Get(int iClientID);

        void Update(Client client);

        void Delete(int iClientID);
    }
}
