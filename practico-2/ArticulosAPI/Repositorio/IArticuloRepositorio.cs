using ArticulosAPI.Dto;

namespace ArticulosAPI.Repositorio
{
    public interface IArticuloRepositorio
    {
        Task<List<ArticuloDto>> GetArticulos();
        Task<ArticuloDto> GetArticulo(int id);
        Task<ArticuloDto> CreateUpdate(ArticuloDto articuloDto);
        Task<bool> DeleteArticulo(int id);
    }
}
