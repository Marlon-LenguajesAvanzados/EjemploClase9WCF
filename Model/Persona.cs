using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjemploClase9WCF.Model
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CorreoElectronico { get; set; }
        public int Edad { get; set; }
    }
}