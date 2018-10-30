using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace VSMDemo.Web
{
	public class MainModule : NancyModule
	{   
        //coleccion 
        static List<Cliente> coleccion = new List<Cliente>();
        
        
       

        public MainModule():base("/cliente")
        {        
            /*Obtengo todos los Clientes de la Lista en Memoria*/
            Get["/"] = obtener => {

                // Inicia el contador:
                Stopwatch tiempo = Stopwatch.StartNew();
                //obtengo un JSON del mapeo de la lista de Clientes
                string clientes = JsonConvert.SerializeObject(coleccion);

                //Para el contador e imprime el resultado:
                Console.WriteLine("TIEMPO GET: " + tiempo.Elapsed.Milliseconds);
                return clientes;
             };

            /*Obtengo los Clientes por ID*/
             Get["/{id}"] = obtenerId =>
             {
                 // Inicia el contador:
                 Stopwatch tiempo = Stopwatch.StartNew();
                 //busco al cliente y lo almaceno en un a lista.
                 var buscar = coleccion.Where(elem => elem.id.Equals(obtenerId.id));
                 string salida;
                 //obtengo un JSON del mapeo de la lista de Clientes
                 if (buscar.Count() == 0)
                     salida= "El Id del CLiente no Existe";
                 else
                 {
                     string clienteId = JsonConvert.SerializeObject(buscar);
                     salida= clienteId;
                 }

                 // Para el contador e imprime el resultado:
                Console.WriteLine("TIEMPO GET ID: " + tiempo.Elapsed.Milliseconds);
                 return salida;
             };

            Post["/agregar"] = agregarCliente => {
                // Inicia el contador:
                Stopwatch tiempo = Stopwatch.StartNew();
                //Obtengo los valores cargados desde la URL, para armar el nuevo Cliente a cargar
                var config = this.Bind<ConfigInfo>();

                Cliente c = new Cliente(config.id, config.nombre, config.apellido, config.direccion);
                coleccion.Add(c);

                string agregar = JsonConvert.SerializeObject(coleccion);             

                // Para el contador e imprime el resultado:
                Console.WriteLine("TIEMPO POST: " + tiempo.Elapsed.Milliseconds);           

                return "se agrego: \n"+agregar;

            };
            


            Delete["/eliminar"] = eliminar =>{
                // Inicia el contador:
                Stopwatch tiempo = Stopwatch.StartNew();

                /*Le asigno una nueva referencia a la lista en memoria*/
                coleccion.Clear();

                // Para el contador e imprime el resultado:
                Console.WriteLine("TIEMPO DELETE: " + tiempo.Elapsed.Milliseconds);
                return "Se elimino";
            };

            Delete["/eliminar/{id}"] = eliminar => {

                // Inicia el contador:
                Stopwatch tiempo = Stopwatch.StartNew();

                string salida;
                var buscar = coleccion.Where(elem => elem.id.Equals(eliminar.id));
                //obtengo un JSON del mapeo de la lista de Clientes
                if (buscar.Count() == 0)
                    salida= "El Id del CLiente no Existe";
                else {
                    coleccion.RemoveAll(x => x.id == eliminar.id);
                    salida= "Se elimino";
                }
                // Para el contador e imprime el resultado:
                Console.WriteLine("TIEMPO DELETE ID: " + tiempo.Elapsed.Milliseconds);
                return salida;
            };

            Put["/actualizar/{id}"] = actualizar =>
            {
                // Inicia el contador:
                Stopwatch tiempo = Stopwatch.StartNew();

                var buscar = coleccion.Where(elem => elem.id.Equals(actualizar.id));
                bool encontro = false;
                int i = 0;
                var config = this.Bind<ConfigInfo>();
                String salida;
                if (buscar.Count() == 0)
                {
                    //Obtengo los valores cargados desde la URL, para armar el nuevo Cliente a cargar


                    Cliente c = new Cliente(actualizar.id, config.nombre, config.apellido, config.direccion);
                    coleccion.Add(c);

                    string agregar = JsonConvert.SerializeObject(coleccion);
                    salida = "Se agrego nuevo Cliente: " + agregar;

                }
                else
                {
                    /*Realizo la busqueda*/

                    while (i < coleccion.Count && !encontro)
                    {
                        if (coleccion.ElementAt(i).id.Equals(actualizar.id))
                        {
                            coleccion.ElementAt(i).setNombre(config.nombre);
                            coleccion.ElementAt(i).setApellido(config.apellido);
                            coleccion.ElementAt(i).setDireccion(config.direccion);
                            encontro = true;
                        }
                        i++;
                    }

                    salida = "Se actualizo el Cliente con ID: " + config.id;
                }
                // Para el contador e imprime el resultado:
                Console.WriteLine("TIEMPO PUT ID: " + tiempo.Elapsed.Milliseconds);
                return salida;

            };


        }
	}
}
