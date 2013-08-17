using System;
using CreditEngine.Commands;
using NServiceBus;

namespace TestHarness
{
    public class Startup : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            while(true)
            {
                Console.Write("Press any key to send a credit card application");
                Console.ReadLine();
                Bus.Send(CreateCommand());  
            }
        }

        private static ApplyForCreditCommand CreateCommand()
        {
            var command = new ApplyForCreditCommand(Guid.NewGuid());
            command.ApplicantFirstName = "ApplicantFirstName";
            command.ApplicantLastName = "ApplicantLastName";
            command.ApplicantDateOfBirth = DateTime.Now;
            command.ApplicantSocialSecurityNumber = "ApplicantSocialSecurityNumber";

            return command;
        }

        public void Stop()
        {
        }
    }
}