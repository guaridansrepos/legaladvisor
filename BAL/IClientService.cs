using Advocate_Invoceing.Models.Entity;

namespace Advocate_Invoceing.BAL
{
    public interface IClientService
    {
        Task<List<ClientEntity>> GetAllClientsAsync();
        Task<ClientEntity> GetClientByIdAsync(int id);
        Task<ClientEntity> AddClientAsync(ClientEntity client);
        Task<ClientEntity> UpdateClientAsync(ClientEntity client);
        Task<bool> DeleteClientAsync(int id);
        Task<int> GetActiveClientsAsync();
    }
}
