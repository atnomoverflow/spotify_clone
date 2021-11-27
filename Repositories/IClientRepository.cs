using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Models;

namespace Spotify_clone2.Repositories
{

	public interface IClientRepository
    {	
		Task<User> UpdateAsync(User client);
		Task<IEnumerable<User>> GetAsync();
		Task<User> GetByIdAsync(string id);
		Task<User> GetByCustomerIdAsync(string id);
		Task<User> CreateAsync(User client);
		Task DeleteAsync(User client);

	}
}
