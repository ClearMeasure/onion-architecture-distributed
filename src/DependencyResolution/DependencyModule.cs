using System;
using System.Web;
using Core;
using CreditEngine;
using Infrastructure;
using NServiceBus;

namespace DependencyResolution
{
    public class DependencyModule : IHttpModule, IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        public void Dispose()
        {
        }

        public void Run()
        {
            DataContext.EnsureStartup();
            BusContext.SetOutsideBus(Bus);
            CreditCardApplicationRepositoryFactory.RepositoryBuilder =
                () => new CreditCardApplicationRepository();
        }

        public void Stop()
        {
        }

        private void ContextBeginRequest(object sender,
            EventArgs e)
        {
            DataContext.EnsureStartup();
            BusContext.EnsureWebStartup();
            CreditCardApplicationRepositoryFactory.RepositoryBuilder =
                () => new CreditCardApplicationRepository();
        }
    }
}