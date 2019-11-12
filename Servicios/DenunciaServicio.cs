using Data;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    /**
     * Estados de una Denuncia:
     * 0  -> Nueva
     * 1  -> Aceptada (Se bloquea la propuesta)
     * 2 -> Desestimada
     */
    public class DenunciaServicio
    {
        readonly TpEntities ctx = new TpEntities();

        readonly PropuestaServicio Propuestas = new PropuestaServicio();

        public void Desestimar(int id)
        {
            Denuncia denuncia = ctx.Denuncias.Find(id);
            denuncia.Estado = 2;
            ctx.SaveChanges();
        }

        public void Aceptar(int id)
        {
            Denuncia denuncia = ctx.Denuncias.Find(id);
            denuncia.Estado = 1;

            // La propuesta pasa a quedar Inactiva
            Propuestas.Bloquear(denuncia.IdPropuesta);
            ctx.SaveChanges();
        }

        public List<Denuncia> ObtenerNuevasDenuncias()
        {
            return ctx.Denuncias
                .Where(x => x.Estado == 0)
                .OrderByDescending(x => x.FechaCreacion)
                .ToList();
        }

    }
}
