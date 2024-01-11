using Domain.Commands;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;
        public VeiculoController(IVeiculoService veiculoService)
        {
           _veiculoService = veiculoService;
        }
        #region Post
        [HttpPost]
        [Route("CadastrarVeiculo")]
        public async Task<IActionResult> PostAsync([FromBody] VeiculoCommand command)
        {
            return Ok(await _veiculoService.PostAsync(command));
        }
        [HttpPost]
        [Route("Alugar")]
        public IActionResult PostAsync()
        {
            return Ok();
        }
        #endregion

        [HttpGet]
        [Route("SimularAluguel")]
        public IActionResult GetAsync() 
        {
            return Ok();
        }
        
    }
}
