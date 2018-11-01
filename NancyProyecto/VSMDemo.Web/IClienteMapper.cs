using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSMDemo.Web
{
    public class IClienteMapper : IDataMapper<Cliente>
    {
        private List<Cliente> listaCliente;

        public IClienteMapper() { listaCliente = new List<Cliente>(); }

        public void actualizar(string id,string nombre, string apellido, string direccion)
        {
            int i = 0;
            bool encontro = false;
            Cliente resul = null;

            while (i < listaCliente.Count && !encontro)
            {
                if (listaCliente.ElementAt(i).id.Equals(id))
                {
                    listaCliente.ElementAt(i).setNombre(nombre);
                    listaCliente.ElementAt(i).setApellido(apellido);
                    listaCliente.ElementAt(i).setDireccion(direccion);
                    encontro = true;
                }
                i++;
            }
        }

        public void eliminar()
        {
            listaCliente.Clear();
        }

        public void eliminarID(string id){

            listaCliente.RemoveAll(x => x.id == id);
        }

        public List<Cliente> getClientes()
        {
            return listaCliente;
        }

        public void insertar(Cliente obj)
        {
            listaCliente.Add(obj);
        }

        public Cliente getClienteId(string id) {

            int i = 0;
            bool encontro = false;
            Cliente resul = null;

            while (i < listaCliente.Count && !encontro)
            {
                if (listaCliente.ElementAt(i).id.Equals(id))
                {
                    resul = listaCliente.ElementAt(i);
                    encontro = true;
                }
                i++;
            }

            return resul;
        }



        
    }
}
