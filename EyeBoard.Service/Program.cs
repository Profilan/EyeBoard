using EyeBoard.Service.DependencyResolution;
using EyeBoard.Service.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Profilan.SharedKernel;
using StructureMap;
using System;
using System.ServiceProcess;

namespace EyeBoard.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            IContainer container = IoC.Initialize();
            DomainEvents.Container = container;
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(container));

            if (Environment.UserInteractive)
            {
                EyeBoardService service1 = new EyeBoardService();
                service1.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new EyeBoardService()
                };
                ServiceBase.Run(ServicesToRun);
            }
            
        }
    }
}
