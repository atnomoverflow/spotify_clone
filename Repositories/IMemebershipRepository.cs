using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;

namespace Spotify_clone2.Repositories
{
	public interface IMembershipRepository
	{
		Task<Memebership> UpdateAsync(Memebership memebership);
		Task<IEnumerable<Memebership>> GetAsync();
		Task<Memebership> GetByIdAsync(string id);
		Task<Memebership> GetByCustomerIdAsync(string id);
		Task<Memebership> CreateAsync(Memebership memebership);
		Task DeleteAsync(Memebership memebership);

	}
}