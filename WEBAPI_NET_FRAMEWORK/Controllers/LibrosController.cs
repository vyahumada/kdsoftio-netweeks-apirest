using System.Linq;
using System.Web.Http;
using WEBAPI_NET_FRAMEWORK.Models;
using WEBAPI_NET_FRAMEWORK.ViewsModels;

namespace WEBAPI_NET_FRAMEWORK.Controllers
{
    [RoutePrefix("api/Libros")]
    public class LibrosController : ApiController
    {
        [HttpGet]
        [Route("ObtenerLibros")]
        public IHttpActionResult ObtenerLibros()
        {
            var db = new Models.Test_WEBAPIEntities();

            var res = db.Libros.Select(p => new LibroVM
            {
                Id = p.Id,
                Nombre = p.Nombre,
                FechaEdicion = p.FechaEdicion,
                Edicion = p.Edicion,
                EnStock = p.EnStock,
                Id_Categoria = p.Id_Categoria,
                Categoria = new CategoriaVM
                {
                    Id = p.Categoria.Id,
                    Nombre = p.Categoria.Nombre,
                }
            }).ToList();
            
            return Ok(res);
        }

        [HttpGet]
        [Route("ObtenerLibroxId")]
        public IHttpActionResult ObtenerLibroxId(int id)
        {
            var db = new Models.Test_WEBAPIEntities();

            var libro = db.Libros.FirstOrDefault(q => q.Id == id);

            if (libro == null)
                return BadRequest("No se encontro libro con el id indicado");
           

            var res = new LibroVM
            {
                Id = libro.Id,
                Nombre = libro.Nombre,
                FechaEdicion = libro.FechaEdicion,
                Edicion = libro.Edicion,
                EnStock = libro.EnStock,
                Id_Categoria = libro.Id_Categoria,
                Categoria = new CategoriaVM
                {
                    Id = libro.Categoria.Id,
                    Nombre = libro.Categoria.Nombre,
                }
            };

            return Ok(res);
        }

        [HttpPost]
        public IHttpActionResult CrearLibro(LibroVM libro)
        {
            var db = new Models.Test_WEBAPIEntities();

            var libroNuevo = new Libros
            {
                Id = libro.Id,
                Nombre = libro.Nombre,
                FechaEdicion = libro.FechaEdicion,
                Edicion = libro.Edicion,
                EnStock = libro.EnStock,
                Id_Categoria = libro.Id_Categoria,
            };

            db.Libros.Add(libroNuevo);

            var res = db.SaveChanges();

            if (res > 0)
                return Ok(libroNuevo);

            return BadRequest("No se pudo crear el elemento");
        }


        [HttpPut]
        public IHttpActionResult ModificarLibro(int id, [FromBody]LibroVM libro)
        {          

            var db = new Models.Test_WEBAPIEntities();

            var libroModificado = db.Libros.FirstOrDefault(p => p.Id == id);


            if (libro == null)
                return BadRequest("No se encontro libro con el id indicado");

            libroModificado.Id = libro.Id;
            libroModificado.Nombre = libro.Nombre;
            libroModificado.FechaEdicion = libro.FechaEdicion;
            libroModificado.Edicion = libro.Edicion;
            libroModificado.EnStock = libro.EnStock;
            libroModificado.Id_Categoria = libro.Id_Categoria;

            var res = db.SaveChanges();

            if (res > 0)
                return Ok(libro);
            return BadRequest("No se pudo crear el elemento");
        }

        [HttpDelete]
        public IHttpActionResult EliminarLibro(int id)
        {
            var db = new Models.Test_WEBAPIEntities();

            var libro = db.Libros.FirstOrDefault(q => q.Id == id);

            if (libro == null)
                return BadRequest("No se encontro libro con el id indicado");

            db.Libros.Remove(libro);

            var res = db.SaveChanges();

            if (res > 0)
                return Ok("Se elimino correctamente");

            return BadRequest("No se pudo eliminar el elemento");
        }



    }
}
