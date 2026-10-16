using AutoMapper;
using Catalogo.Api.Data;
using Catalogo.Api.Dtos.Cosechas;
using Catalogo.Api.Interfaces.Cosechas;
using Catalogo.Api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IEnumerable<CosechaReadDto>> CatalogoDeCosechas(string? producto, decimal? precioMin, decimal? precioMax)
        {
            //esta consulta base me traera las cosechas disponibles, en crecimiento y con una cantidad disponible mayor a 0;
            var Query = _DBcontext.Cosechas
                .Include(c => c.Producto)
                .Include(c => c.EstadoCosecha)
                .Where(c => (c.IdEstadoCosecha == 1 || c.IdEstadoCosecha == 2) && c.Cantidad > 0)
                .AsQueryable(); // Use .AsQueryable() para no ir directamenta a la base de datos

            //Verifico si el usuario lleno los filtros
            if (!string.IsNullOrWhiteSpace(producto))
                Query = Query.Where(c => c.Producto.Nombre.Contains(producto));   //.Contains() es el equivalente a usar "LIKE '%palabra%'" en SQL


            if (precioMin.HasValue)
            {
                Query = Query.Where(c => c.PrecioUnidad >= precioMin.Value);
            }

            if (precioMax.HasValue)
            {
                Query = Query.Where(c => c.PrecioUnidad <= precioMax.Value);
            }

            //Despues de verificar si el usuario lleno los filtros, ejecutamos la consulta
            var cosechasFiltradas = await Query.ToListAsync();


            return _Mapper.Map<IEnumerable<CosechaReadDto>>(cosechasFiltradas);
        }

        public async Task<CosechaReadDto> ActualizarCosecha(int usuarioId, int cosechaId, CosechaUpdateDto cosechaUpdateDto)
        {
            await ValidacionProducto(cosechaUpdateDto.IdProducto);
            await ValidacionEstadoCosecha(cosechaUpdateDto.IdEstadoCosecha);

            var cosechaExistente = await _DBcontext.Cosechas
               .Where(c => c.IdUsuario == usuarioId && c.Id == cosechaId)
               .FirstOrDefaultAsync();

            if (cosechaExistente == null) throw new KeyNotFoundException($"No existe la cosecha");

            _Mapper.Map(cosechaUpdateDto, cosechaExistente);
            await _DBcontext.SaveChangesAsync();

            //Incluyo referencias de mapeo
            await _DBcontext.Entry(cosechaExistente).Reference(c => c.Producto).LoadAsync();
            await _DBcontext.Entry(cosechaExistente).Reference(c => c.EstadoCosecha).LoadAsync();

            return _Mapper.Map<CosechaReadDto>(cosechaExistente);
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
                throw new KeyNotFoundException($"El estado con el ID {IdEstado} No Existe");
        }
    }
}
