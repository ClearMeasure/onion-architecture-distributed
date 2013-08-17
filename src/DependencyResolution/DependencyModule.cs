using System;
using System.Web;
using Core;
using CreditEngineHost;
using Infrastructure;

namespace DependencyResolution
{
    public class DependencyModule : IHttpModule, NServiceBus.IWantToRunAtStartup
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        public void Dispose()
        {
        }

        private void ContextBeginRequest(object sender,
            EventArgs e)
        {
            Run();
        }

        public void Run()
        {
            DataContext.EnsureStartup();
            BusContext.EnsureStartup();
            CreditCardApplicationRepositoryFactory.RepositoryBuilder =
                () => new CreditCardApplicationRepository();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}