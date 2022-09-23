using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApi.Dto.CheckIn
{
    public class EquipajeDto
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Peso { get; set; }
        public int EsFragil { get; set; }

    }
}
