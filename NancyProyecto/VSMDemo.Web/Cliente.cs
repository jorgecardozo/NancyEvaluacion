using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSMDemo.Web
{
    public class Cliente
    {
        public Cliente(String i,String n,String a,String d) {
            nombre = n;
            id = i;
            apellido = a;
            direccion = d;
        }

        public String id { get; set; }
        public String nombre { get; set; }
        public String apellido { get; set; }
        public String direccion { get; set; }

        /*Metodos get y set de los atributos*/
        public String getId() { return id; }
        public String getNombre() { return id; }
        public String getApellido() { return id; }
        public String getDireccion() { return id; }

        public void setId(string elem) { id = elem; }
        public void setNombre(string elem) { nombre = elem; }
        public void setApellido(string elem) { apellido = elem; }
        public void setDireccion(string elem) { direccion = elem; }
    }
}
