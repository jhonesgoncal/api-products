﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.Contexts;

namespace ModernStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ModernStoreDataContext _context;

        public CustomerRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public Customer Get(Guid id)
        {
            return _context
                .Customers
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public GetCustomerCommandResult Get(string username)
        {
            //return _context
            //    .Customers
            //    .Include(x => x.User)
            //    .AsNoTracking()
            //    .Select(x => new GetCustomerCommandResult
            //    {
            //        Name = x.Name.ToString(),
            //        Document = x.Document.Number,
            //        Active = x.User.Active,
            //        Email = x.Email.Address,
            //        Password = x.User.Password,
            //        Username = x.User.Username
            //    })
            //    .FirstOrDefault(x => x.Username == username);

            var query = "SELECT * FROM [GetCustomerInfoView] WHERE [Active]=1 AND [Username]=@username";
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=ModernStore; Integrated Security = true;"))
            {
                conn.Open();
                return conn
                    .Query<GetCustomerCommandResult>(query,
                        new { username = username })
                    .FirstOrDefault();
            }
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }

        public bool DocumentExists(string document)
        {
            return _context.Customers.Any(x => x.Document.Number == document);
        }

        public void Save(Customer customer)
        {
            _context.Customers.Add(customer);
        }
    }
}
