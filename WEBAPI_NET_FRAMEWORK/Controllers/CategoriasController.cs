using System.Linq;
using System.Web.Http;

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

            var res = db.Categoria.Select(p => new CategoriaVM
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToList();


            return Ok(res);

        }

        [HttpGet]
        [Route("ObtenerCategoriaxId/{id}")]
        public IHttpActionResult ObtenerCategoriaxId(int id)
        {
            var db = new Test_WEBAPIEntities();

            var obj = db.Categoria.FirstOrDefault(p => p.Id == id);

            if (obj == null)
            {
                return BadRequest("No se encontro el dato");
            }

            var res = new CategoriaVM
            {
                Id = obj.Id,
                Nombre = obj.Nombre
            };

            return Ok(res);
        }

        [HttpPost]
        public IHttpActionResult CrearCate(CategoriaVM cat)
        {
            var db = new Test_WEBAPIEntities();

            var catNueva = new Categoria {
               Id =cat.Id,
               Nombre = cat.Nombre
            };

            db.Categoria.Add(catNueva);

            var res = db.SaveChanges();
            if (res > 0)
            {
                return Ok(catNueva);
            }
            return BadRequest("No se pudo guardar");
        }

        [HttpPut]
        public IHttpActionResult ModificarCate([FromBody] CategoriaVM cat)
        {
            var db = new Test_WEBAPIEntities();
            var obj = db.Categoria.FirstOrDefault(p => p.Id == cat.Id);

            if (obj == null)
            {
                return BadRequest("No se encontro el dato");
            }

            obj.Id = cat.Id;
            obj.Nombre = cat.Nombre;           

            var res = db.SaveChanges();
            if (res > 0)
            {
                return Ok(obj);
            }
            return BadRequest("No se pudo guardar");
        }

        [HttpDelete]
        [Route("EliminarCate/{id}")]
        public IHttpActionResult EliminarCate(int id)
        {
            var db = new Test_WEBAPIEntities();
            var obj = db.Categoria.FirstOrDefault(p => p.Id == id);

            if (obj == null)
            {
                return BadRequest("No se encontro el dato");
            }
            db.Categoria.Remove(obj);

            var res = db.SaveChanges();
            if (res > 0)
            {
                return Ok(obj);
            }
            return BadRequest("No se pudo guardar");
        }



    }
}
