using Ordering.Application.Models;

namespace Ordering.Application.Contracts.Infrstructure
{
    public interface ISendMailService
    {
        Task<bool> SendMail(Email mail);
    }
}
