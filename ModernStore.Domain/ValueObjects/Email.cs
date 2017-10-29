using FluentValidator;

namespace ModernStore.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email() { }
        public Email(string address)
        {
            Address = address;

            //validations
            new ValidationContract<Email>(this)
               .IsEmail(x => x.Address, "Email invalido!");
        }

        public string Address { get; private set; }

    }
}
