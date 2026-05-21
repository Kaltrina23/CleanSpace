using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.Google.Crypto.Tink.Jwt;

namespace CleanSpace.ViewModels
{
    public class Segnalazione
    {
        public int CategoriaID { get; set; }
        public string CategoriaNome { get; set; }
        public string FotobBase64 { get; set; }
        public string Nota { get; set; }
        public string OspiteNome { get; set; }
        public string OspiteCognome { get; set; }
        public string OspiteEmail { get; set; }
        public Posizione Posizione { get; set; } = new Posizione();
    }

    public class Posizione
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public int IDComune { get; set; }
    }
}
