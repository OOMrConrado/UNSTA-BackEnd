using AutoMapper;
using ArticulosAPI.Data;
using ArticulosAPI.Dto;
using ArticulosAPI.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ArticulosAPI.Repositorio
{
    public class ArticuloRepositorio : IArticuloRepositorio
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ArticuloRepositorio(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ArticuloDto> CreateUpdate(ArticuloDto articuloDto)
        {
            Articulo articulo = _mapper.Map<ArticuloDto, Articulo>(articuloDto);
            if (articulo.Id > 0)
            {
                _db.Articulos.Update(articulo);
            }
            else
            {
                _db.Articulos.Add(articulo);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Articulo, ArticuloDto>(articulo);
        }

        public async Task<bool> DeleteArticulo(int id)
        {
            try
            {
                Articulo articulo = await _db.Articulos.FindAsync(id);
                if (articulo == null)
                {
                    return false;
                }
                _db.Articulos.Remove(articulo);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ArticuloDto> GetArticulo(int id)
        {
            Articulo articulo = await _db.Articulos.FindAsync(id);
            return _mapper.Map<ArticuloDto>(articulo);
        }

        public async Task<List<ArticuloDto>> GetArticulos()
        {
            List<Articulo> lista = await _db.Articulos.ToListAsync();
            return _mapper.Map<List<ArticuloDto>>(lista);
        }
    }
}
