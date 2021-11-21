using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Models;

namespace Spotify_clone2.Repositories
{

	public interface IClientRepository
    {	
		Task<Client> UpdateAsync(Client client);
		Task<IEnumerable<Client>> GetAsync();
		Task<Client> GetByIdAsync(string id);
		Task<Client> GetByCustomerIdAsync(string id);
		Task<Client> CreateAsync(Client client);
		Task DeleteAsync(Client client);

	}
}
