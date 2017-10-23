using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernStore.Domain.Commands;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Repositories;

namespace ModernStore
{
    class Program
    {
        static void Main(string[] args)
        {

            var command = new RegisterOrderCommand
            {
                Customer = Guid.NewGuid(),
                DeliveryFee = 9,
                Discount = 30,
                Items = new List<RegisterOrderItemCommand>
                {
                    new RegisterOrderItemCommand
                    {
                        Product =  Guid.NewGuid(),
                        Quantity = 3
                    }
                }

            };

            GenerateOrder(
                new FakeCustomerRepository(), 
                new FakeProductRepository(), 
                new FakeOrderRepository(), 
                command
            );

            Console.ReadKey();
        }

        private static void GenerateOrder(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            RegisterOrderCommand command)
        {

            var handler = new OrderCommandHandler(customerRepository, productRepository, orderRepository);
            handler.Handle(command);

            if (handler.IsValid())
            {
                Console.WriteLine("Pedido efetuado com sucesso!");
            }
        }
    }

    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(Guid id)
        {
            return null;
        }

        public Customer GetByUserId(Guid id)
        {
            return new Customer(
                new Name("Jhones", "Gonçalves"),
                new Email("jhones.goncalves@outlook.com"), 
                new Document("41490944893"), 
                new User("jhonesgoncalves", "41321158")
            );
        }
    }

    public class FakeProductRepository : IProductRepository
    {
        public Product Get(Guid id)
        {
           return new Product("Mouse", 299, "",50);
        }
    }

    public class FakeOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            
        }
    }
}

