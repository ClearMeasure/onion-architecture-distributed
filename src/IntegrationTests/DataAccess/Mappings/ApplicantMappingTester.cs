using System;
using Core;
using Infrastructure;
using NHibernate;
using NUnit.Framework;
using Should;

namespace IntegrationTests.DataAccess.Mappings
{
    [TestFixture]
    public class ApplicantMappingTester
    {
        [Test]
        public void Should_map()
        {
            new DatabaseTester().Clean();
            var visitor = new Applicant
            {
                Browser = "1",
                IpAddress = "2",
                LoginName = "3",
                PathAndQuerystring = "4",
                VisitDate =
                    new DateTime(2000, 1, 1),
                FirstName = "Jones"
            };

            var repository = new ApplicantRepository();
            repository.Save(visitor);

            Applicant loadedApplicant;
            using (ISession session = DataContext.GetSession())
            {
                loadedApplicant = session.Load<Applicant>(visitor.Id);
            }

            loadedApplicant.ShouldNotBeNull();
            loadedApplicant.Browser.ShouldEqual("1");
            loadedApplicant.IpAddress.ShouldEqual("2");
            loadedApplicant.LoginName.ShouldEqual("3");
            loadedApplicant.PathAndQuerystring.ShouldEqual("4");
            loadedApplicant.VisitDate.ShouldEqual(new DateTime(2000, 1, 1));
            loadedApplicant.FirstName.ShouldEqual("Jones");
        }
    }
}
