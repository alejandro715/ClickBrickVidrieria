using ClickBrickVidrieria.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio
{
    public interface iProductoBodegaRepositorio : iRepositorio<BodegaProducto>
    {

        void Actualizar(BodegaProducto bodegaProducto);


    }
}
