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
    public class ProductRepository : IProductRepository
    {
        private readonly ModernStoreDataContext _context;
        public ProductRepository(ModernStoreDataContext context)
        {
            _context = context;
        }
        public Product Get(Guid id)
        {
            return _context
                .Products
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GetProductListCommandResult> Get()
        {
            var query = "SELECT [Id], [Title], [Price], [Image] FROM [Product]";
            using (SqlConnection conn =
                new SqlConnection(@"Data Source=.;Initial Catalog=ModernStore; Integrated Security = true;"))
            {
                conn.Open();
                return conn.Query<GetProductListCommandResult>(query);
            }
        }
    }
}
