using CreditBureau.Commands;

namespace CreditEngineHost
{
    public class CreditBureauMessageMapper
    {
        public CreditScoreRequest CreateRequest(ProcessCreditCardApplicationSagaData data)
        {
            var request = new CreditScoreRequest(data.CreditCardApplicationId);
            request.ApplicantFirstName = data.ApplicantFirstName;
            request.ApplicantLastName = data.ApplicantLastName;
            request.ApplicantDateOfBirth = data.ApplicantDateOfBirth;
            request.ApplicantSocialSecurityNumber = data.ApplicantSocialSecurityNumber;
            return request;
        }
    }
}