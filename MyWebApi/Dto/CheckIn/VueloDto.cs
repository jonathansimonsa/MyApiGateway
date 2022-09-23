using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApi.Dto.CheckIn
{
    public class VueloDto
    {
        public Guid Id { get; set; }
        public int NroVuelo { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public int CantidadCheckIn { get; set; }

        public ICollection<CheckInDto> Lista = new List<CheckInDto>();
    }
}
