using Domain.Commands;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ViewModel;

namespace Service.Services
{
    public class VeiculoService : IVeiculoService
    {
        //Injeção de dependencia

        private readonly IVeiculoRepository _repository;

        public VeiculoService(IVeiculoRepository repository)
        {
            _repository = repository;
        }
        public void GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> PostAsync(VeiculoCommand command)
        {
            if (command == null)
                return "Todos os Campos são Obrigatórios";

            int anoAtual = DateTime.Now.Year;
            if (anoAtual - command.AnoFabricacao > 5)
                return "O Ano do veículo é menor que o permitido";

            if (command.TipoVeiculo != ETipoVeiculo.SUV
               && command.TipoVeiculo != ETipoVeiculo.Hatch
               && command.TipoVeiculo != ETipoVeiculo.Sedan
            )
                return "O Tipo de Veículo não pe permitido";

            return  await _repository.PostAsync(command);
        }

        public void PostAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<VeiculoCommand>> GetVeiculosDisponiveis()
        {
            return await _repository.GetVeiculosDisponiveis();
        }

        public async Task<SimularVeiculoAluguelViewModel> SimularVeiculoAluguel(int totalDiasSimulado, ETipoVeiculo tipoVeiculo)
        {
            var veiculoPreco = await _repository.GetPrecoDiaria(tipoVeiculo);
            double taxaEstadual = 10.50;
            double taxaFederal = 3.5;
            double taxaMunicipal = 13.5;

            var simulacao = new SimularVeiculoAluguelViewModel();
            simulacao.TotalDiasSimulado = totalDiasSimulado;
            simulacao.Taxas = (decimal)(taxaMunicipal + taxaEstadual + taxaFederal);
            simulacao.TipoVeiculo = tipoVeiculo;
            simulacao.ValorDiaria = veiculoPreco.Preco;
            simulacao.ValorTotal = (totalDiasSimulado * veiculoPreco.Preco) + simulacao.Taxas;
        
          return simulacao;
        }
    }
}
