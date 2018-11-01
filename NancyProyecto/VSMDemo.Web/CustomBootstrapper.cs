using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Responses;
using Nancy.Session;
using Nancy.ViewEngines;
using TinyIoC;

namespace VSMDemo.Web
{
	public class CustomBootstrapper : DefaultNancyBootstrapper
	{
        Stopwatch timer;
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {    /*Inyeccion de Dependecia*/
            ///container.Register(new IClienteMapper());

            base.ConfigureApplicationContainer(container);
			ResourceViewLocationProvider.RootNamespaces.Add(Assembly.GetAssembly(typeof(MainModule)), "VSMDemo.Web");

            //container.Register<IDataMapper<Cliente>,IClienteMapper>();
            // container.Register<IDataMapper<Cliente>>(new IClienteMapper()).AsSingleto();

            //container.Register<IClienteMapper>(new IClienteMapper()).AsSingleton();
           // container.Register<IDataMapper<Cliente>>(new IClienteMapper());
          

            // This is a concrete type registration.
            // When I ask the container for one of these, it will build me one each time.
            container.Register<IClienteMapper>().AsSingleton();

        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            // Get our session manager - this will "bubble up" to the parent container
            // and get our application scope singleton
           var session = container.Resolve<IClienteMapper>();

            // We can put this in context.items and it will be disposed when the request ends
            // assuming it implements IDisposable.
            context.Items["RavenSession"] = session;

            // Just guessing what this type is called
            container.Register<IDataMapper<Cliente>>(session);

            container.Register<IDataMapper<Cliente>,IClienteMapper>();


            //otra opcion
            TinyIoCContainer.Current.Resolve<IClienteMapper>();

        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);
			CookieBasedSessions.Enable(pipelines);

            pipelines.BeforeRequest += (ctx) => {

                //activo el timer
                Console.WriteLine("Activo el timer");
                timer = Stopwatch.StartNew();
                return null;
            };

        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            pipelines.AfterRequest += (ctx) => {
                // Tomo el valor del timer
                Console.WriteLine("Tiempo de la solicitud: " + timer.Elapsed.Milliseconds);
            };
        }
        protected override NancyInternalConfiguration InternalConfiguration
		{
			get
			{
				return NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder);
			}
		}

		void OnConfigurationBuilder(NancyInternalConfiguration x)
		{
			x.ViewLocationProvider = typeof(ResourceViewLocationProvider);
		}

		public static Func<NancyContext, string, Response> AddStaticResourcePath(string requestedPath, Assembly assembly, string namespacePrefix)
		{
			return (context, s) =>
			       	{
			       		var path = context.Request.Path;
						if (!path.StartsWith(requestedPath))
						{
							return null;
						}

						string resourcePath;
						string name;

						var adjustedPath = path.Substring(requestedPath.Length + 1);
						if (adjustedPath.IndexOf('/') >= 0)
						{
							name = Path.GetFileName(adjustedPath);
							resourcePath = namespacePrefix + "." + adjustedPath.Substring(0, adjustedPath.Length - name.Length - 1).Replace('/', '.');
						}
						else
						{
							name = adjustedPath;
							resourcePath = namespacePrefix;
						}
						return new EmbeddedFileResponse(assembly, resourcePath, name);
			       	};
		}
	}
}