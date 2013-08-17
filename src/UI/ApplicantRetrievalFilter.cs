using System.Web.Mvc;
using Core;

namespace UI
{
	public class ApplicantRetrievalFilter : ActionFilterAttribute
	{
		private readonly IApplicantRepository _repository;

		public ApplicantRetrievalFilter(IApplicantRepository repository)
		{
			_repository = repository;
		}

		public ApplicantRetrievalFilter() : this(
			new ApplicantRepositoryFactory().BuildRepository())
		{
		}

		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			Applicant[] applicants = _repository.GetRecentApplicants(10);
			filterContext.Controller.ViewData[Constants.ViewData.APPLICANTS] = applicants;
		}
	}
}