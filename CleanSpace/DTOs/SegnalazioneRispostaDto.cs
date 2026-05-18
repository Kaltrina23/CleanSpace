using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CleanSpace.DTOs
{
    public partial class SegnalazioneRispostaDto: ObservableObject
    {
        public int Id { get; set; }
        public decimal Latitudine { get; set; }
        public decimal Longitudine { get; set; }
        public string? FotoBase64 { get; set; }
        public string Nota { get; set; }
        private string priorita;

        public string Priorita
        {
            get => priorita;
            set
            {
                priorita = value;
                OnPropertyChanged();
            }
        }

        public string Stato { get; set; }
        public DateTime Data { get; set; }
        public string Comune { get; set; }
        public string Categoria { get; set; }
        public string? OspiteNome { get; set; }
        public string? OspiteCognome { get; set; }
        public string? OspiteEmail { get; set; }
    }
}