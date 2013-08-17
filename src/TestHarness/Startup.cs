using System;
using Core;
using NServiceBus;

namespace TestHarness
{
    public class Startup : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            while (true)
            {
                Console.Write("Press any key to send a credit card application");
                Console.ReadLine();
                SendCommand();
            }
        }

        public void Stop()
        {
        }

        private static void SendCommand()
        {
            var applicant = new Applicant
            {
                CreditCardApplicationId = Guid.NewGuid(),
                FirstName = "Jeffrey"
                ,
                PathAndQuerystring = ""
                ,
                LoginName = ""
                ,
                Browser = ""
                ,
                VisitDate = DateTime.Now
                ,
                IpAddress = ""
            };
            new ApplicantRepositoryFactory().BuildRepository().Save(applicant);
            new CreditCardApplicationRepositoryFactory().BuildRepository()
                .SaveApplicationFor(applicant);
        }
    }
}