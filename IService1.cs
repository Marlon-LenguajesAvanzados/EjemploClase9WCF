using EjemploClase9WCF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EjemploClase9WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        string IngresarPersona(Persona userInfo);

        [OperationContract]
        string IngresarPersonaSP(Persona userInfo);

        [OperationContract]
        string ObtenerNombrePersonaPorId(int idPersona);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    public class Persona
    {
        string nombre = string.Empty;
        string apellido1 = string.Empty;
        string apellido2 = string.Empty;
        DateTime fechaNacimiento = DateTime.MinValue;
        string correoElectronico = string.Empty;
        int edad = 0;

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        [DataMember]
        public string Apellido1
        {
            get { return apellido1; }
            set { apellido1 = value; }
        }

        [DataMember]
        public string Apellido2
        {
            get { return apellido2; }
            set { apellido2 = value; }
        }

        [DataMember]
        public string FechaNacimiento
        {
            get { return fechaNacimiento.ToString("yyyy-MM-dd"); }
            set
            {
                if (DateTime.TryParse(value, out DateTime fecha))
                {
                    fechaNacimiento = fecha;
                }
                else
                {
                    throw new ArgumentException("Fecha de nacimiento no válida");
                }
            }
        }

        [DataMember]
        public string CorreoElectronico
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }

        [DataMember]
        public int Edad
        {
            get { return edad; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La edad no puede ser negativa");
                }
                edad = value;
            }
        }
    }
}