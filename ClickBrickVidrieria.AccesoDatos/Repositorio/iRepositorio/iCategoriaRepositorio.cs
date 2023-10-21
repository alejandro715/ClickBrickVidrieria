using ClickBrickVidrieria.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio
{
    public interface iCategoriaRepositorio : iRepositorio<Categoria>
    {

        void Actualizar(Categoria categoria);

    }
}
