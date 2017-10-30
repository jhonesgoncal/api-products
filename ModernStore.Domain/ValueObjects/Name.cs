using FluentValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernStore.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        protected Name() { }
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            //validations
            new ValidationContract<Name>(this)
               .IsRequired(x => x.FirstName, "Nome e obrigatorio!")
               .HasMaxLenght(x => x.FirstName, 60)
               .HasMinLenght(x => x.FirstName, 3)
               .IsRequired(x => x.LastName, "Sobrenome e obrigatorio!")
               .HasMaxLenght(x => x.LastName, 60)
               .HasMinLenght(x => x.LastName, 3);
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
