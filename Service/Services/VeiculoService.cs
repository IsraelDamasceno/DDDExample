using Domain.Commands;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class VeiculoService : IVeiculoService
    {
        public void GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task PostAsync(VeiculoCommand command)
        {
            if (command == null)
                throw new ArgumentNullException();
            //Todo

            // Incluir validação, só podem cadastrar veículos com
            //até 5 anos de uso

            //To do
            //Incluir Somente carros do tipo SUV, Sedan e Hatch 
            if (command.TipoVeiculo != ETipoVeiculo.SUV
               && command.TipoVeiculo != ETipoVeiculo.Hatch
               && command.TipoVeiculo != ETipoVeiculo.Sedan
            )
            {
                Console.WriteLine("Não cadastrou o veiculo");
                throw new ArgumentNullException();
            }
            else
            {
                Console.WriteLine("Cadastrou Veiculo");
            }
           


            throw new NotImplementedException();
        }

        public void PostAsync()
        {
            throw new NotImplementedException();
        }
    }
}
