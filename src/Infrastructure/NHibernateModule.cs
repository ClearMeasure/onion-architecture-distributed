using System;
using System.Web;

namespace Infrastructure
{
    public class NHibernateModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        private void ContextBeginRequest(object sender,
                                         EventArgs e)
        {
            DataContext.EnsureStartup();
        }

        public void Dispose()
        {
        }
    }
}