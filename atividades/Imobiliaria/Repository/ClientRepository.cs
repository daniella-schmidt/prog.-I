using Model;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ClientRepository
    {
        public List<Client> _clients => ClientData.Clients;

        public Client Retrieve(int id)
        {
            return _clients.FirstOrDefault(c => c.Id == id)!;
        }

        public List<Client> RetrieveByName(string name)
        {
            return _clients
                .Where(c => c.Name?.ToLower().Contains(name.ToLower()) ?? false)
                .ToList();
        }

        public List<Client> RetrieveByEmail(string email)
        {
            return _clients
                .Where(c => c.Email?.ToLower().Contains(email.ToLower()) ?? false)
                .ToList();
        }

        public List<Client> RetrieveAll()
        {
            return _clients;
        }

        public void Save(Client client)
        {
            client.Id = _clients.Count + 1;
            _clients.Add(client);
        }

        public bool Delete(Client client)
        {
            return _clients.Remove(client);
        }

        public bool DeleteById(int id)
        {
            var client = Retrieve(id);
            return client != null && Delete(client);
        }

        public void Update(Client updatedClient)
        {
            var existingClient = Retrieve(updatedClient.Id);
            if (existingClient != null)
            {
                existingClient.Name = updatedClient.Name;
                existingClient.Email = updatedClient.Email;
                existingClient.Phone = updatedClient.Phone;
                existingClient.CPF = updatedClient.CPF;
                existingClient.InterestedProperties = updatedClient.InterestedProperties;
            }
        }

        public void AddInterest(int clientId, int propertyId)
        {
            var client = Retrieve(clientId);
            var property = new PropertyRepository().Retrieve(propertyId);

            if (client != null && property != null)
            {
                client.InterestedProperties ??= new List<Property>();
                client.InterestedProperties.Add(property);
            }
        }
    }
}
