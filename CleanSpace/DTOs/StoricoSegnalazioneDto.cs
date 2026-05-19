using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSpace.DTOs;

public class StoricoSegnalazioneDto
{
    public int Id { get; set; }

    public string Categoria { get; set; }

    public string Nota { get; set; }

    public string Stato { get; set; }

    public string Priorita { get; set; }

    public DateTime DataCreazione { get; set; }
}