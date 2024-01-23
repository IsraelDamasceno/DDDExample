using CreditCardValidator;
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

            return await _repository.PostAsync(command);
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

        public async Task AlugarVeiculo(AlugarVeiculoViewModelInput input)
        {

            //var veiculoAlugado = await VeiculoEstaAlugado(input.PlacaVeiculo);
            if (await VeiculoEstaAlugado(input.PlacaVeiculo))
            {
                // "Este Veículo não está mais disponível para alugar";
            }
            //Todo
            //chamar método para datas
            if (await ValidarDatas(input.DataDevolucao, input.DataRetirada))
            {
                //Erro
            }
            //Todo

            CreditCardDetector detector = new CreditCardDetector(Convert.ToString(input.Cartao.Numero));
            var bandeira = detector.Brand; // => 4012888888881881
            if (!detector.IsValid()) // => True
            {
                //"Cartão Invalido";
            }

            //Todo
            //chamar método para validar habilitação

        }

        private async Task<bool> ValidarDatas(DateTime inicio, DateTime fim)
        {
            if (fim < inicio)
                return false;
            else if (fim == inicio)
                return false;
            else if (fim < DateTime.Now)
                return false;
            else if (inicio < DateTime.Now)
                return false;
            else
                return true;
        }

        private async Task<bool> VeiculoEstaAlugado(string placaVeiculo)
        {
            return await _repository.VeiculoEstaAlugado(placaVeiculo);
        }
    }
}
