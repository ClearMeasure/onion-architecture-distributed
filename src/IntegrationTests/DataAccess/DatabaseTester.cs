using Infrastructure;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace IntegrationTests.DataAccess
{
    [TestFixture]
    public class DatabaseTester
    {
        [Test, Explicit, Category("DataSchema")]
        public void CreateDatabaseSchema()
        {
            var export = new SchemaExport(
                DataContext.BuildConfiguration());
            export.Execute(true, true, false);
        }

        public void Clean()
        {
            using (var session = DataContext.GetSession())
            {
                session.BeginTransaction();
                session.CreateQuery("delete from Applicant").ExecuteUpdate();
                session.Transaction.Commit();
            }
        }
    }
}