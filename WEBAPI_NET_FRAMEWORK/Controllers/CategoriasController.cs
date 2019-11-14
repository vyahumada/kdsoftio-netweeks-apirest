using System.Linq;
using System.Web.Http;
using System.Web;
using System.Web.Security;

using WEBAPI_NET_FRAMEWORK.Models;
using WEBAPI_NET_FRAMEWORK.ViewsModels;


namespace WEBAPI_NET_FRAMEWORK.Controllers
{
    [RoutePrefix("api/Categorias")]
    public class CategoriasController : ApiController
    {
        [HttpGet]
        [Route("ObtenerCategorias")]
        public IHttpActionResult ObtenerCategorias()
        {
            var db = new Test_WEBAPIEntities();

            var res = db.Categorias.Select(p => new CategoriaVM
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToList();

            return Ok(res);

        }

        [HttpPost]
        [Route("CrearCategoria")]
        public IHttpActionResult CrearCategoria(CategoriaVM cat)
        {
            var db = new Test_WEBAPIEntities();

            var catNueva = new Categorias
            {
                Id = cat.Id,
                Nombre = cat.Nombre,
                Habilitada = true,
            };

            db.Categorias.Add(catNueva);

            var res = db.SaveChanges();
            if (res > 0)
            {
                return Ok(catNueva);
            }
            return BadRequest("No se pudo guardar");
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult ModificarCate(int id, [FromBody] CategoriaVM cat)
        {
            var header = Request.Headers.FirstOrDefault(p => p.Key == "mandatario");


            var db = new Test_WEBAPIEntities();
            var obj = db.Categorias.FirstOrDefault(p => p.Id == id);

            if (obj == null)
            {
                return BadRequest("No se encontro el dato");
            }

            obj.Nombre = cat.Nombre;

            var res = db.SaveChanges();
            if (res > 0)
            {
                return Ok(obj);
            }
           
            return BadRequest("No se pudo guardar");
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult EliminarCate(int id)
        {
            var db = new Test_WEBAPIEntities();
            var obj = db.Categorias.FirstOrDefault(p => p.Id == id);

            if (obj == null)
            {
                return BadRequest("No se encontro el dato");
            }
            db.Categorias.Remove(obj);

            var res = db.SaveChanges();
            if (res > 0)
            {
                return Ok(obj);
            }
            return BadRequest("No se pudo guardar");
        }

    }
}
