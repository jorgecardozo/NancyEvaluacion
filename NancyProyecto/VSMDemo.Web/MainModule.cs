using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace VSMDemo.Web
{
	public class MainModule: NancyModule
	{
        // IClienteMapper clienteMapper;
        

        public MainModule(IClienteMapper clientesBD):base("/cliente")
        {
           
            /*Obtengo todos los Clientes de la Lista en Memoria*/
            Get["/"] = obtener => {          
                //obtengo un JSON del mapeo de la lista de Clientes
                return Response.AsJson(clientesBD.getClientes());
             };

            /*Obtengo los Clientes por ID*/
             Get["/{id}"] = obtenerId =>
             {
                 Cliente cliente = clientesBD.buscar(obtenerId.id);

                 return Response.AsJson(cliente);
             };

            Post["/agregar"] = agregarCliente => {     
               //Obtengo los valores cargados desde la URL, para armar el nuevo Cliente a cargar
                var config = this.Bind<BodyCliente>();

                Cliente c = new Cliente(config.id, config.nombre, config.apellido, config.direccion);
                clientesBD.insertar(c);
        
                return Response.AsJson(config);

            };
            
            Delete["/eliminar"] = eliminar =>{          
                clientesBD.eliminar();
                
                return "Se eliminaron todos los cliente";
            };

            Delete["/eliminar/{id}"] = eliminar => {              
                string salida;
                bool elimino;
               
                elimino = clientesBD.eliminarID(eliminar.id);

                if (elimino)
                    salida = "Se elimino el Cliente correctamente";
                else
                    salida = "El cliente no Existe";

                return salida;
            };

            Put["/actualizar/{id}"] = actualizar =>
            {
               
                //Obtengo los valores cargados desde la URL, para armar el nuevo Cliente a cargar
                var config = this.Bind<BodyCliente>();
                Cliente act;
                act= clientesBD.actualizar(actualizar.id,config.nombre,config.apellido,config.direccion);

                return Response.AsJson(act);
            };


        }
	}
}
