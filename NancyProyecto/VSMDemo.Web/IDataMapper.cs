using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSMDemo.Web
{
    public interface IDataMapper<TObject> where TObject : class
    {
        void insertar(TObject obj);
        void actualizar(string id, string nombre, string apellido, string direccion);
        void eliminar();
        void eliminarID(string id);
        List<TObject> getClientes();
    }
}
