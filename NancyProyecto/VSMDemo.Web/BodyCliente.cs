using System;

namespace VSMDemo.Web
{
	[Serializable]
	public class BodyCliente
	{
        public string id { get; set; }
        public string nombre { get; set; }
		public string apellido { get; set; }
        public string direccion { get; set; }
    }
}
