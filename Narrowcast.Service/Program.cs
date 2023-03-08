using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Narrowcast.Service.Hubs;
using Profilan.SharedKernel;
using StructureMap;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace Narrowcast.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //Thread.Sleep(30000);
            IContainer container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.Assembly("EyeBoard.Logic");
                    x.WithDefaultConventions();
                    x.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                });
            });
            DomainEvents.Container = container;
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(container));

            if (Environment.UserInteractive)
            {

                NarrowcastService service = new NarrowcastService();
                service.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new NarrowcastService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
