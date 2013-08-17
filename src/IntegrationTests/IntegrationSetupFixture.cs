using System;
using NUnit.Framework;

namespace IntegrationTests
{
    [SetUpFixture]
    public class IntegrationSetUpFixture : IDisposable
    {
        public IntegrationSetUpFixture()
        {
            CleanData();
            LoadEnvirontmentData();
        }

        public void Dispose()
        {
            CleanData();
        }

        private void CleanData()
        {
            //TODO: Code to clean out all test data from the database
        }

        private void LoadEnvirontmentData()
        {
            //TODO: Code to populate all the data needed when integration tests are run.
        }
    }
}