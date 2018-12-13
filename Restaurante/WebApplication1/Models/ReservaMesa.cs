using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ReservaMesa
    {
        public int Comensales { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.TimeSpan> Horainicio { get; set; }
        public enum CantComensales
        {
            Dos = 2,
            Tres = 3,
            Cuatro = 4,
            Cinco = 5,
            Seis = 6,
            Siete = 7,
            Ocho = 8
        }

    }
    
}