using Microsoft.EntityFrameworkCore;
using PaulAlarba.Models;
using PaulAlarba.Models.Data;

namespace PaulAlarba.Services
{
    public class ContactMessageStore
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactMessageStore(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync(ContactFormViewModel form, CancellationToken cancellationToken = default)
        {
            var message = new ContactMessage
            {
                Id = Guid.NewGuid(),
                Name = form.Name.Trim(),
                Email = form.Email.Trim(),
                Subject = form.Subject.Trim(),
                Message = form.Message.Trim(),
                CreatedAtUtc = DateTimeOffset.UtcNow
            };

            _dbContext.ContactMessages.Add(message);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
