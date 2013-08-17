namespace Core
{
	public interface IApplicantRepository
	{
		void Save(Applicant applicant);
		Applicant[] GetRecentApplicants(int number);
	}
}