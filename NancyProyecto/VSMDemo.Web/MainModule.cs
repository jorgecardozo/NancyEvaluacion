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
                string clientes = JsonConvert.SerializeObject(clientesBD.getClientes());
               
                return clientes;
             };

            /*Obtengo los Clientes por ID*/
             Get["/{id}"] = obtenerId =>
             {
                 
                 //busco al cliente y lo almaceno en un a lista.
                 var buscar = clientesBD.getClientes().Where(elem => elem.id.Equals(obtenerId.id));
                 string salida;
                 //obtengo un JSON del mapeo de la lista de Clientes
                 if (buscar.Count() == 0)
                     salida= "El Id del CLiente no Existe";
                 else
                 {   string clienteId = JsonConvert.SerializeObject(buscar);
                     salida= clienteId;
                 }               
                 return salida;
             };

            Post["/agregar"] = agregarCliente => {     
               //Obtengo los valores cargados desde la URL, para armar el nuevo Cliente a cargar
                var config = this.Bind<ConfigInfo>();

                Cliente c = new Cliente(config.id, config.nombre, config.apellido, config.direccion);
                clientesBD.insertar(c);
                string agregar = JsonConvert.SerializeObject(clientesBD.getClientes());  
                
                return "se agrego: \n"+agregar;

            };
            


            Delete["/eliminar"] = eliminar =>{          
                clientesBD.eliminar();
                
                return "Se elimino";
            };

            Delete["/eliminar/{id}"] = eliminar => {              
                string salida;
                var buscar = clientesBD.getClientes().Where(elem => elem.id.Equals(eliminar.id));
                //obtengo un JSON del mapeo de la lista de Clientes
                if (buscar.Count() == 0)
                    salida= "El Id del CLiente no Existe";
                else {
                    clientesBD.eliminarID(eliminar.id);
                    salida= "Se elimino";
                }
               
                return salida;
            };

            Put["/actualizar/{id}"] = actualizar =>
            {
                var buscar = clientesBD.getClientes().Where(elem => elem.id.Equals(actualizar.id));
                //Obtengo los valores cargados desde la URL, para armar el nuevo Cliente a cargar
                var config = this.Bind<ConfigInfo>();
                String salida;
                if (buscar.Count() == 0) {                   
                    Cliente c = new Cliente(actualizar.id, config.nombre, config.apellido, config.direccion);
                    clientesBD.insertar(c);

                    string agregar = JsonConvert.SerializeObject(clientesBD.getClientes());
                    salida = "Se agrego nuevo Cliente: " + agregar;
                }
                else { /*actualizo*/
                    clientesBD.actualizar(actualizar.id, config.nombre, config.apellido, config.direccion);
                    salida = "Se actualizo el Cliente con ID: " + config.id;
                }
                
                return salida;

            };


        }
	}
}
