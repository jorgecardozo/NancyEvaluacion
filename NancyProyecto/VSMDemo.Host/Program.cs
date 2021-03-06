﻿using System;
using System.Diagnostics;
using VSMDemo.Web;

namespace VSMDemo.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			var nancyHost = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:9664"), new CustomBootstrapper());
			nancyHost.Start();
			Console.WriteLine("Web server running...");

			Process.Start("http://localhost:9664");

			Console.ReadLine();
			nancyHost.Stop();

		}
	}
}
