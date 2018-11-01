using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSMDemo.Web
{
    public interface IDataMapper<TObject> where TObject : class
    {
        void insertar(TObject obj);
        TObject actualizar(string id, string nombre, string apellido, string direccion);
        void eliminar();
        bool eliminarID(string id);
        TObject buscar(string id);
        
        List<TObject> getClientes();
    }
}
