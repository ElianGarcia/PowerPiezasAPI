using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerPiezasAPI.Models;
using PowerPiezasAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPiezasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IRepositorio _repositorio;

        public MarcasController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet(nameof(Get))]
        public async Task<IEnumerable<Marcas>> Get()
        {
            var result = await Task.FromResult(_repositorio.GetAll<Marcas>($"SELECT * FROM Marcas WHERE Activo = 1", null, commandType: CommandType.Text));
            return result;
        }
    }
}
