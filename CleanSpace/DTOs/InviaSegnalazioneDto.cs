using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSpace.DTOs
{
    public class InviaSegnalazioneDto
    {
        public int CategoriaId { get; set; }
        public int ComuneId { get; set; }

        public decimal Latitudine { get; set; }
        public decimal Longitudine { get; set; }
        public string? FotoBase64 { get; set; }

        public string? Nota { get; set; }
        public string? OspiteNome { get; set; }

        public string? OspiteCognome { get; set; }

        public string? OspiteEmail { get; set; }
    }
}
