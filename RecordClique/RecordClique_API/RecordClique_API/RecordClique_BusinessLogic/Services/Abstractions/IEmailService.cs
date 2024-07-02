
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IEmailService
    {
        void SendEmail(EmailDto email);   

    }
}
