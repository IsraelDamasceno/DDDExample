using Domain.Commands;

namespace Domain.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<string> PostAsync(VeiculoCommand command);
        void PostAsync();
        void GetAsync();
    }
}
