using Dapper;
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
    public class AreasController : ControllerBase
    {
        private readonly IRepositorio _repositorio;
        protected string Campos, Tabla;

        public AreasController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
            Campos = "ID, Nombre, Activo";
            Tabla = "Areas";
        }

        [HttpGet(nameof(Get))]
        public async Task<IEnumerable<Areas>> Get()
        {
            var result = await Task.FromResult(_repositorio.GetAll<Areas>($"SELECT {Campos} FROM Areas WHERE Activo = 1", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPost(nameof(Create))]
        public async Task<Int64> Create(Areas marca)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Nombre", marca.Nombre);
            parameters.Add("Activo", marca.Activo);

            var result = await Task.FromResult(
                _repositorio.Insert<Int64>($"INSERT INTO Areas({Campos.Replace("ID,", "")}) VALUES (@{Campos.Replace("ID,", "").Replace(" ", "").Replace(",", ",@")});" +
                $"SELECT ISNULL(SCOPE_IDENTITY(), 0);",
                parameters,
                commandType: CommandType.Text));
            return result;
        }

        [HttpPut(nameof(Update))]
        public async Task<Int64> Update(Areas marca)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ID", marca.Id);
            parameters.Add("Nombre", marca.Nombre);
            parameters.Add("Activo", marca.Activo);

            var result = await Task.FromResult(
                _repositorio.Insert<Int64>($"UPDATE Areas " +
                $"SET Nombre = @Nombre, " +
                $"Activo = @Activo " +
                $"WHERE ID = @ID;" +
                $"SELECT ISNULL(SCOPE_IDENTITY(), 0);",
                parameters,
                commandType: CommandType.Text));
            return result;
        }
    }
}
