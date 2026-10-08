using AutoMapper;
using Catalogo.Api.Data;
using Catalogo.Api.Dtos.Cosechas;
using Catalogo.Api.Interfaces.Cosechas;
using Catalogo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Services.Cosechas
{
    public class CosechaService : ICosechaService
    {
        private readonly AppDbContext _DBcontext;
        private readonly IMapper _Mapper;

        public CosechaService(AppDbContext dbcontext, IMapper mapper)
        {
            _DBcontext = dbcontext;
            _Mapper = mapper;
        }

        //Crear
        public async Task<CosechaReadDto> CrearCosecha(int usuarioId , CosechaCreateDto cosechaCreateDto)
        {
            
            //Validaciones necesarias
            await ValidacionProducto(cosechaCreateDto.IdProducto);
            await ValidacionEstadoCosecha(cosechaCreateDto.IdEstadoCosecha);

            //Mapeo
            var nuevaCosecha = _Mapper.Map<Cosecha>(cosechaCreateDto);

            nuevaCosecha.IdUsuario = usuarioId;

            //Guardo en la DB
            _DBcontext.Cosechas.Add(nuevaCosecha);
            await _DBcontext.SaveChangesAsync();

            //Incluyo referencias de mapeo
            await _DBcontext.Entry(nuevaCosecha).Reference(c => c.Producto).LoadAsync();
            await _DBcontext.Entry(nuevaCosecha).Reference(c => c.EstadoCosecha).LoadAsync();

            //Retorno mapeado
            return _Mapper.Map<CosechaReadDto>(nuevaCosecha);
        }

        //Leer
        public async Task<IEnumerable<CosechaReadDto>> ObtenerCosechasDelUsuario(int usuarioId)
        {
            var cosechas = await _DBcontext.Cosechas
                .Include(c => c.Producto)
                .Include(c => c.EstadoCosecha)
                .Where(c => c.IdUsuario == usuarioId)
                .ToListAsync();

            //mapeamos la entidad completa cosechas para convertirla en la entidad DTO cosehasDto
            return _Mapper.Map<IEnumerable<CosechaReadDto>>(cosechas);
        }

        //METODOS PRIVADOS

        private async Task ValidacionProducto(int IdProducto)
        {
            var producto = await _DBcontext.Productos
                .AnyAsync(p  => p.Id == IdProducto);

            if(!producto)
                throw new KeyNotFoundException($"El producto con el ID {IdProducto} No Existe");
        }
        private async Task ValidacionEstadoCosecha(int IdEstado)
        {
            var estado = await _DBcontext.EstadoCosechas
                .AnyAsync(e => e.Id == IdEstado);

            if (!estado)
                throw new KeyNotFoundException($"El producto con el ID {IdEstado} No Existe");
        }
    }
}
