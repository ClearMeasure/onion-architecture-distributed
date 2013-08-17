namespace Core
{
    public interface ICreditCardApplicationRepository
    {
        void SaveApplicationFor(Applicant applicant);
    }
}