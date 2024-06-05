using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Contexts
{
    public partial class OrderContext : DbContext
    {
        public void EnsureSeedDataForContext()
        {
            Database.EnsureDeleted();
            Database.Migrate();
            SaveChanges();

            List<Order> orders = new List<Order>()
            {
                new Order()
                {
                    OrderId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    Client = "Jaime Casino",
                    DateofOrder = new DateTimeOffset(new DateTime(2024, 5, 12)),
                    AdressClient = "Calle A Nro 12 los arrabales",
                    District = "Lince",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            ItemId = new Guid("c7ba6add-09c4-45f8-8dd0-eaca221e5d93"),
                            DescriptionProduct = "Impresora Matricial 359 LX",
                            Cant =1
                        },
                  
                        new Item()
                        {
                            ItemId = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6"),
                            DescriptionProduct = "Sevidor HP 500 TB ",
                            Cant =2
                        },
                         
                    }
                },
                new Order()
                {
                    OrderId = new Guid("76053df4-6687-4353-8937-b45556748abe"),
                    Client = "George Martins",
                    DateofOrder = new DateTimeOffset(new DateTime(2024, 5, 11)),
                    AdressClient = "Los nogales 234 ",
                    District = "San Isidro",
                   Items = new List<Item>()
                    {
                        new Item()
                        {
                            ItemId = new Guid("a1da1d8e-1988-4634-b538-a01709477b77"),
                            DescriptionProduct = "Impresora Epson Stylus color 2344",
                            Cant =1
                        },
                        new Item()
                        {
                            ItemId = new Guid("60188a2b-2784-4fc4-8df8-8919ff838b0b"),
                            DescriptionProduct = "CPU HP core I7 ",
                            Cant =1
                        },
                         new Item()
                        {
                            ItemId = new Guid("70a1f9b9-0a37-4c1a-99b1-c7709fc64167"),
                            DescriptionProduct = "CPU HP core I7 ",
                            Cant =1
                        }
                   }
                } ,

                new Order()
                {
                    OrderId = new Guid("412c3012-d891-4f5e-9613-ff7aa63e6bb3"),
                    Client = "Ana Melendez",
                    DateofOrder = new DateTimeOffset(new DateTime(2024, 5, 13)),
                    AdressClient = "Calle Jacaranda 334",
                    District = "Comas",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            ItemId = new Guid("9edf91ee-ab77-4521-a402-5f188bc0c577"),
                            DescriptionProduct = "Monitor LG 54 Pulgadas",
                            Cant =1
                        },
                        new Item()
                        {
                            ItemId = new Guid("578359b7-1967-41d6-8b87-64ab7605587e"),
                            DescriptionProduct = "Procesador Intel 7 Generacion 10",
                            Cant =2
                        },
                         new Item()
                        {
                            ItemId = new Guid("f74d6899-9ed2-4137-9876-66b070553f8f"),
                            DescriptionProduct = "Movil Apple X  ",
                            Cant =3
                        }
                    }
                },


                new Order()
                {
                        OrderId = new Guid("bfa37631-9f3b-4c43-9989-7f1c48682534"),
                        Client = "Juan Valdiviezo",
                        DateofOrder = new DateTimeOffset(new DateTime(2024, 5, 5)),
                        AdressClient = "Los almacenes 123",
                        District = "San Isidro",
                        Items = new List<Item>()
                        {
                        new Item()
                        {
                        ItemId = new Guid("570e0ef5-d6d4-43de-bddd-f3187e00ed59"),
                        DescriptionProduct = "Mesa cuadrada 20 milimetros",
                        Cant =2
                        },
                        new Item()
                        {
                        ItemId = new Guid("5fa813a2-bc35-45ab-be50-92839ac16b53"),
                        DescriptionProduct = "CPU compac core I7 ",
                        Cant =1
                        },
                        new Item()
                        {
                        ItemId = new Guid("66923c39-b2c6-4d30-a62b-0964957c82cd"),
                        DescriptionProduct = "CPU HP core I7 ",
                        Cant =1
                        }
                    }
                } ,


                new Order()
                {
                        OrderId = new Guid("79cf9c2c-bb82-4867-85ff-2c6fcae2f5bf"),
                        Client = "Maria Perez",
                        DateofOrder = new DateTimeOffset(new DateTime(2024, 5, 12)),
                        AdressClient = "Los almacenes 4435",
                        District = "San Isidro",
                        Items = new List<Item>()
                        {
                        new Item()
                        {
                            ItemId = new Guid("c02069be-b099-4236-8495-77d9f46d3db2"),
                            DescriptionProduct = "Mesa cuadrada 20 milimetros",
                            Cant =2
                        },
                        new Item()
                        {
                            ItemId = new Guid("533f7788-48df-415a-ada0-1410dc5bef62"),
                            DescriptionProduct = "Laptop Hp  I7 ",
                            Cant =1
                        }
                    }
                } ,


                new Order()
                {
                        OrderId = new Guid("eaa6fa19-395c-4148-bfb5-e908fcd43e27"),
                        Client = "Juliana Lopez ",
                        DateofOrder = new DateTimeOffset(new DateTime(2024, 5, 21)),
                        AdressClient = "Calle C Mz C lt 3",
                        District = "Comas",
                        Items = new List<Item>()
                        {
                        new Item()
                        {
                            ItemId = new Guid("e4fcb0e5-f6ed-45fc-8791-f4789e3b1363"),
                            DescriptionProduct = "Impresora Multifuncional HP modelo 3443",
                            Cant =1
                        },
                        new Item()
                        {
                            ItemId = new Guid("2a9fb765-b58d-4b8b-82e6-daf341311345"),
                            DescriptionProduct = "Laptop Lenovo 453 intel 7 ",
                            Cant =1
                        },
                        new Item()
                        {
                            ItemId = new Guid("0d3ed719-a83f-497c-b765-203a9db633ce"),
                            DescriptionProduct = "Computadora Toshiba 5 nucleos ",
                            Cant =1
                        }
                    }
                } ,




        };

            Orders.AddRange(orders);
            SaveChanges();
        }
    }
}
