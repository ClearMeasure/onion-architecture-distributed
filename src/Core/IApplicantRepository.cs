using System;

namespace Core
{
	public interface IApplicantRepository
	{
		void Save(Applicant applicant);
		Applicant[] GetRecentApplicants(int number);
	    Applicant GetApplicantByCreditCardApplicationId(Guid creditCardApplicationId);
	}
}