using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPI_NET_FRAMEWORK.ViewsModels
{
    public class LibroVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public string Edicion { get; set; }
        public bool? EnStock { get; set; }
        public int? Id_Categoria { get; set; }
        public CategoriaVM Categoria { get;  set; }
    }
}