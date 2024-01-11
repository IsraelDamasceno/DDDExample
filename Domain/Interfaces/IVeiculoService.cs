using Domain.Commands;

namespace Domain.Interfaces
{
    public interface IVeiculoService
    {
        //A interface é um contrato, apenas usamos para adicionar
        //metodos,
        //não é feita implementação de nada

        Task<string> PostAsync(VeiculoCommand command);
        void PostAsync();
        void GetAsync();

    }
}
