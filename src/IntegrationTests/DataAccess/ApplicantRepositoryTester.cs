using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Infrastructure;
using NHibernate;
using Should;
using NUnit.Framework;

namespace IntegrationTests.DataAccess
{
    [TestFixture]
    public class ApplicantRepositoryTester
    {
        [Test]
        public void When_saving_should_write_to_database()
        {
            new DatabaseTester().Clean();
            var visitor = CreateApplicant(Convert.ToDateTime("1/2/2345"));

            var repository = new ApplicantRepository();
            repository.Save(visitor);

            Applicant loadedApplicant;
            using (ISession session = DataContext.GetSession())
            {
                loadedApplicant = session.Load<Applicant>(
                    visitor.Id);
            }

            loadedApplicant.ShouldNotBeNull();
            loadedApplicant.VisitDate.ToShortDateString().ShouldEqual("1/2/2345");
        }

        [Test]
        public void Should_get_two_most_recent_applicants()
        {
            new DatabaseTester().Clean();
            Applicant visitor1 =
                CreateApplicant(new DateTime(2000, 1, 1));
            Applicant visitor2 =
                CreateApplicant(new DateTime(2000, 1, 2));
            Applicant visitor3 =
                CreateApplicant(new DateTime(2000, 1, 3));
            using (ISession session1 = DataContext.GetSession())
            {
                session1.BeginTransaction();
                session1.SaveOrUpdate(visitor1);
                session1.SaveOrUpdate(visitor2);
                session1.SaveOrUpdate(visitor3);
                session1.Transaction.Commit();
            }

            var repository = new ApplicantRepository();
            Applicant[] recentApplicants =
                repository.GetRecentApplicants(2);

            recentApplicants.Length.ShouldEqual(2);
            IEnumerable<Guid> idList = recentApplicants.Select(x => x.Id);
            idList.Contains(visitor3.Id).ShouldBeTrue();
            idList.Contains(visitor2.Id).ShouldBeTrue();
            idList.Contains(visitor1.Id).ShouldBeFalse();
        }

        private Applicant CreateApplicant(DateTime visitDate)
        {
            return new Applicant
                       {
                           Browser = "1",
                           IpAddress = "2",
                           LoginName = "3",
                           PathAndQuerystring = "4",
                           VisitDate = visitDate
                       };
        }
    }
}