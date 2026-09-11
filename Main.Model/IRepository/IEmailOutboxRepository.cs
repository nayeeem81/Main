
namespace Main.IRepository;

public interface IEmailOutboxRepository
{
    Task<bool> SaveChangesAsync (string email,string subject,string htmlMessage);
}
