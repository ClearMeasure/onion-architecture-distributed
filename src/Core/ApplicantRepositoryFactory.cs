using System;

namespace Core
{
    public class ApplicantRepositoryFactory
    {
        //meant to be set on application start
        public static Func<IApplicantRepository>
            RepositoryBuilder =
                CreateDefaultRepositoryBuilder;

        private static IApplicantRepository CreateDefaultRepositoryBuilder()
        {
            //throw if factory not initialized
            throw new Exception(
                "No repository builder specified.");
        }

        public IApplicantRepository BuildRepository()
        {
            //Uses the Func<IApplicantRepository> to build the instance
            IApplicantRepository repository =
                RepositoryBuilder();
            return repository;
        }
    }
}