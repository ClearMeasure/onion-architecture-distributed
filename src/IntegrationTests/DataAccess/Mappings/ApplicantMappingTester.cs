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
            Guid creditCardApplicationId = Guid.NewGuid();
            var applicant = new Applicant
            {
                Browser = "1",
                IpAddress = "2",
                LoginName = "3",
                PathAndQuerystring = "4",
                VisitDate =
                    new DateTime(2000, 1, 1),
                FirstName = "Jones",
                LastName = "Palermo",
                CreditCardApplicationId = creditCardApplicationId,
                CardNumberIssued = "1234"
            };

            var repository = new ApplicantRepository();
            repository.Save(applicant);

            Applicant loadedApplicant;
            using (ISession session = DataContext.GetSession())
            {
                loadedApplicant = session.Load<Applicant>(applicant.Id);
            }

            loadedApplicant.ShouldNotBeNull();
            loadedApplicant.Browser.ShouldEqual("1");
            loadedApplicant.IpAddress.ShouldEqual("2");
            loadedApplicant.LoginName.ShouldEqual("3");
            loadedApplicant.PathAndQuerystring.ShouldEqual("4");
            loadedApplicant.VisitDate.ShouldEqual(new DateTime(2000, 1, 1));
            loadedApplicant.FirstName.ShouldEqual("Jones");
            loadedApplicant.LastName.ShouldEqual("Palermo");
            loadedApplicant.CreditCardApplicationId.ShouldEqual(creditCardApplicationId);
            loadedApplicant.CardNumberIssued.ShouldEqual("1234");
        }
    }
}
