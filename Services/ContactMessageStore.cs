using System.Text;
using System.Text.Json;
using PaulAlarba.Models;

namespace PaulAlarba.Services
{
    public class ContactMessageStore
    {
        private readonly string _storePath;
        private readonly SemaphoreSlim _writeLock = new(1, 1);

        public ContactMessageStore(IWebHostEnvironment environment)
        {
            var dataDirectory = Path.Combine(environment.ContentRootPath, "App_Data");
            _storePath = Path.Combine(dataDirectory, "contact-messages.jsonl");
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

            var line = JsonSerializer.Serialize(message);

            await _writeLock.WaitAsync(cancellationToken);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_storePath)!);
                await File.AppendAllTextAsync(_storePath, line + Environment.NewLine, Encoding.UTF8, cancellationToken);
            }
            finally
            {
                _writeLock.Release();
            }
        }
    }
}
