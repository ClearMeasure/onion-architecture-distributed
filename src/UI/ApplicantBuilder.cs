using System;
using System.Web;
using Core;

namespace UI
{
	public class ApplicantBuilder
	{
		private readonly HttpRequestBase _httpRequest;
		private readonly DateTime _currentDate;

		public ApplicantBuilder(HttpRequestBase httpRequest, DateTime currentDate)
		{
			_httpRequest = httpRequest;
			_currentDate = currentDate;
		}

		public ApplicantBuilder() : this(
			new HttpContextWrapper(HttpContext.Current).Request, DateTime.Now)
		{
		}

		public Applicant BuildApplicant()
		{
			var visitor = new Applicant
    		{
    			PathAndQuerystring = _httpRequest.Url.PathAndQuery,
    			Browser = _httpRequest.UserAgent,
    			IpAddress = _httpRequest.UserHostAddress,
    			LoginName = _httpRequest.LogonUserIdentity.Name,
    			VisitDate = _currentDate,
                CreditCardApplicationId = Guid.NewGuid()
    		};
			return visitor;
		}
	}
}