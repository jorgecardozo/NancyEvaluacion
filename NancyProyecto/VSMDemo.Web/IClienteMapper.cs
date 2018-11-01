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

        public Cliente actualizar(string id,string nombre, string apellido, string direccion)
        {
            Cliente act = buscar(id);

            if (act != null)
            {
                act.setNombre(nombre);
                act.setApellido(apellido);
                act.setDireccion(direccion);
            }
            else {
                Cliente nuevo = new Cliente(id, nombre, apellido, direccion);
                insertar(nuevo);
                act = nuevo;
            }
            return act;                            
        }

        public void eliminar()
        {
            listaCliente.Clear();
        }

        public bool eliminarID(string id){

            bool busqueda=false;

            if (!(buscar(id) == null)) {
                listaCliente.RemoveAll(x => x.id == id);
                busqueda = true;
            }
            return busqueda;
        }

        public List<Cliente> getClientes()
        {
            return listaCliente;
        }

        public void insertar(Cliente obj)
        {
            listaCliente.Add(obj);
        }

        public Cliente buscar(string id) {

            int i = 0;
            bool encontro = false;
            Cliente cliente = null;

            while (i < listaCliente.Count && !encontro)
            {
                if (listaCliente.ElementAt(i).id.Equals(id))
                {
                    cliente = listaCliente.ElementAt(i);
                    encontro = true;
                }
                i++;
            }    
            return cliente;
        }

    }
}
