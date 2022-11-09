using InvoicingWebCore.Data;
using InvoicingWebCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicingWebCore
{
    public class Seeder
    {
        private readonly ModelBuilder _modelBuilder;

        public Seeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            _modelBuilder.Entity<TaxType>().HasData(
                new TaxType()
                {
                    Id = 1,
                    Tax = 5
                },
                new TaxType()
                {
                    Id = 2,
                    Tax = 8
                },
                new TaxType()
                {
                    Id = 3,
                    Tax = 23
                }
            );

            _modelBuilder.Entity<InvoiceType>().HasData(
                new InvoiceType()
                {
                    Id = 1,
                    Name = "Faktura sprzedaży",
                },
                new InvoiceType()
                {
                    Id = 2,
                    Name = "Faktura zaliczkowa",
                },
                new InvoiceType()
                {
                    Id = 3,
                    Name = "Faktura końcowa",
                },
                new InvoiceType()
                {
                    Id = 4,
                    Name = "Faktura korygująca",
                },
                new InvoiceType()
                {
                    Id = 5,
                    Name = "Faktura VAT marża",
                }
            );
        }
    }
}
