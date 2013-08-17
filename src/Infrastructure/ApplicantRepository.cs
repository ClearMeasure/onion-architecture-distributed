using System.Linq;
using Core;
using NHibernate;
using NHibernate.Linq;

namespace Infrastructure
{
    public class ApplicantRepository : IApplicantRepository
    {
        public void Save(Applicant applicant)
        {
            using (ISession session = DataContext.GetSession())
            {
                session.BeginTransaction();
                session.SaveOrUpdate(applicant);
                session.Transaction.Commit();
            }
        }

        public Applicant[] GetRecentApplicants(int number)
        {
            using (ISession session = DataContext.GetSession())
            {
                Applicant[] recentApplicants =
                    session.Query<Applicant>()
                        .OrderByDescending(v => v.VisitDate)
                        .Take(number)
                        .ToArray();

                return recentApplicants;
            }
        }
    }
}

//          ***If we were to use HQL***
//            IList<Applicant> applicants = session
//                .CreateQuery("select v from Applicant v order by v.VisitDate desc")
//                .SetMaxResults(number)
//                .List<Applicant>();
//
//            return applicants.ToArray();