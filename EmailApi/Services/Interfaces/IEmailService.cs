using CatalogShared;
using System.Threading.Tasks;

namespace EmailApi.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(CatalogEventMessage message);
    }
}
