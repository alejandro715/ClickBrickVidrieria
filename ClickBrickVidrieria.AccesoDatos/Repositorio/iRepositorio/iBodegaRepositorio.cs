using ClickBrickVidrieria.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio
{
    public interface iBodegaRepositorio : iRepositorio<Bodega>
    {

        void Actualizar(Bodega bodega);

    }
}
