using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArticulosAPI.Dto;
using ArticulosAPI.Repositorio;

namespace ArticulosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloRepositorio _articuloRepositorio;
        protected ResponseDto _response;

        public ArticulosController(IArticuloRepositorio articuloRepositorio)
        {
            _articuloRepositorio = articuloRepositorio;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetArticulos()
        {
            try
            {
                var lista = await _articuloRepositorio.GetArticulos();
                _response.Result = lista;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetArticulo(int id)
        {
            try
            {
                var articulo = await _articuloRepositorio.GetArticulo(id);
                if (articulo == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Artículo no encontrado";
                }
                else
                {
                    _response.Result = articulo;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> PutArticulo(int id, ArticuloDto articuloDto)
        {
            try
            {
                if (id != articuloDto.Id)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "El Id no coincide";
                    return BadRequest(_response);
                }

                var articulo = await _articuloRepositorio.CreateUpdate(articuloDto);
                _response.Result = articulo;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> PostArticulo(ArticuloDto articuloDto)
        {
            try
            {
                var articulo = await _articuloRepositorio.CreateUpdate(articuloDto);
                _response.Result = articulo;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteArticulo(int id)
        {
            try
            {
                bool isSuccess = await _articuloRepositorio.DeleteArticulo(id);
                if (isSuccess)
                {
                    _response.Result = true;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Artículo no encontrado";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
