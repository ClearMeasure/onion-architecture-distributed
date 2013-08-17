using System;

namespace Core
{
    public class CreditCardApplicationRepositoryFactory
    {
        //meant to be set on application start
        public static Func<ICreditCardApplicationRepository>
            RepositoryBuilder =
                CreateDefaultRepositoryBuilder;

        private static ICreditCardApplicationRepository CreateDefaultRepositoryBuilder()
        {
            //throw if factory not initialized
            throw new Exception(
                "No repository builder specified.");
        }

        public ICreditCardApplicationRepository BuildRepository()
        {
            //Uses the Func<IApplicantRepository> to build the instance
            ICreditCardApplicationRepository repository =
                RepositoryBuilder();
            return repository;
        }
    }
}