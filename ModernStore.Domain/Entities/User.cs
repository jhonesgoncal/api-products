using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidator;
using ModernStore.Shared.Entities;

namespace ModernStore.Domain.Entities
{
    public class User : Entity
    {
        public User(string username, string password, string confirmPassword)
        {
            Password = EncryptPassword(password);
            Username = username;
            Active = false;

            new ValidationContract<User>(this)
                .AreEquals(x => x.Password, EncryptPassword(confirmPassword), "As senhas não coincidem");
        }

        public string Password { get; private set; }
        public string Username { get; private set; }
        public bool Active { get; private set; }

        public void Activate() => Active = true;
        

        public void Deactivate() => Active = false;

        private string EncryptPassword(string pass)
        {
            if (!string.IsNullOrEmpty(Password)) return "";
            
            var password = (pass += "|2d331cca-f6c0-40c0-bb43-6e32989c2881");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(pass));
            var sbString = new StringBuilder();
            foreach (var t in data)
            {
                sbString.Append(t.ToString("x2"));
            }
            return sbString.ToString();

            

            
        }
    }
}
