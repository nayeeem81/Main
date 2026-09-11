using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Main.Model.Identity;

namespace Main.Repository;

public class EmailOutboxRepository: IEmailOutboxRepository
{
    private readonly LogDbContext _context;

    public EmailOutboxRepository (LogDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync (string email,string subject,string htmlMessage)
    {
        EmailOutboxMessage emailOutboxMessage = new ()
        {
            ReceiverEmail = email,
            Subject = subject,
            Body = htmlMessage,
            CreatedOnUtc = DateTime.UtcNow
        };

        _ = _context.EmailOutboxMessages.Add (emailOutboxMessage);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

}
