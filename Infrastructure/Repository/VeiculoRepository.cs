using Dapper;
using Domain.Commands;
using Domain.Interfaces;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        string conexao = @"Server=(localdb)\mssqllocaldb;Database=AluguelVeiculos;Trusted_Connection=True;MultipleActiveResultSets=true";
        public async Task<string> PostAsync(VeiculoCommand command)
        {
            string queryInsert = @"
            INSERT INTO Veiculo(Placa, AnoFabricacao, TipoVeiculoId, Estado, MontadoraId)
            VALUES(@Placa,@AnoFabricacao , @TipoVeiculoId, @Estado, @MontadoraId)";
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Execute(queryInsert, new
                {
                    Placa = command.Placa,
                    AnoFabricacao = command.AnoFabricacao,
                    TipoVeiculoId = (int)command.TipoVeiculo,
                    Estado = command.Estado,
                    MontadoraId = (int)command.Montadora
                });

                return "Veiculo Cadastrado com sucesso";
            }
        }

        public void PostAsync()
        {

        }
        public void GetAsync()
        {

        }

        public async Task<IEnumerable<VeiculoCommand>> GetVeiculosDisponiveis()
        {
            string queryBuscarVeiculosDisponiveis = @"
               SELECT * FROM Veiculo WHERE ALUGADO = 0";
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                return conn.QueryAsync<VeiculoCommand>(
                    queryBuscarVeiculosDisponiveis).Result.ToList();
            }
        }
    }
}

