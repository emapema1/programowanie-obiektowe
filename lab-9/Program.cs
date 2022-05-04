using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net;

namespace lab_9
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = new AppContext();
            context.Database.EnsureCreated();
            Console.WriteLine(context.Books.Find(1));
            //context.Books.Add(new Book() { Title = "PHP",EditionYear = 2000, AuthorId= 1});
            //context.Books.Remove(context.Books.Find(1));
            //Book book = context.Books.Find(2);
            //book.EditionYear = 2010;
            //context.Books.Update(book);
            //context.SaveChanges();
            IQueryable<Book> books = from b in context.Books
            select b;
            Console.WriteLine("=======================================");
            Console.WriteLine(string.Join("\n", books));
            var booksWithAuthors = from book in context.Books
            join author in context.Authors
            on book.AuthorId equals author.Id
            select new { Title = book.Title, Author = author.Name };
            Console.WriteLine("=======================================");
            Console.WriteLine(string.Join("\n", booksWithAuthors));
            foreach(var item in booksWithAuthors)
            {
                Console.WriteLine(item.Author);
            }
            //zapisz linq ktore zwroci liste rekordow z polami Id ksiazki i nazwisko autora dla
            //ksiazek wydanych po 2019
            var booksRlsdAfter2019 = from book in context.Books
            join author in context.Authors
            on book.AuthorId equals author.Id 

            select new { Id = book.Id, Author = author.Name };
            foreach (var item in booksRlsdAfter2019)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=======================================");

            //context.Books.Join(
            //    context.Authors,
            //    book => book.AuthorId,
            //    author = > author.Id,
            //    (book,author) = new {Title = book.Title,Author = author.Name} 

            //    );

            string xml =
                "<books>" +
                    "<book>" +
                        "<id>1</id>" +
                        "<title>C#</title>" +
                        "<editionYear>2000</editionYear>" +
                    "</book>" +
                    "<book>" +
                        "<id>2</id>" +
                        "<title>Java</title>" +
                        "<editionYear>2002</editionYear>" +
                    "</book>" +
                "</books>";

            XDocument doc = XDocument.Parse(xml);
            var xmlBooks = doc
                .Elements("books")
                .Elements("book")
                .Select(x => new
                {
                    Id = x.Element("id").Value,
                    Title = x.Element("title").Value,
                    EditionYear = x.Element("editionYear").Value
                }
                );
            Console.WriteLine(string.Join("\n", xmlBooks));

            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xmlRate = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C/");
            XDocument rateDoc = XDocument.Parse(xmlRate);
            var rates = rateDoc
                .Element("ArrayOfExchangeRatesTable")
                .Element("ExchangeRatesTable")
                .Element("Rates")
                .Element("Rate")
                .Select(x = new
                {
                    Code=x.Element("Code").Value,
                    Bid =


                }
                );

        }
    }

    public record Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int EditionYear { get; set; }


    }

    public record Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class AppContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DATASOURCE=d:\\database1\\data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Book>()
                .ToTable("books")
                .HasData(
                 new Book() { Id = 1, AuthorId = 1, EditionYear = 2020, Title = "C#" },
                 new Book() { Id = 2, AuthorId = 1, EditionYear = 2018, Title = "Asp.Net" },
                 new Book() { Id = 3, AuthorId = 2, EditionYear = 2021, Title = "Java" },
                 new Book() { Id = 4, AuthorId = 2, EditionYear = 2019, Title = "F#" }
               );
            modelBuilder
                .Entity<Author>()
                .ToTable("authors")
                .HasData(
                new Author() { Id = 1, Name = "jdjad" },
                new Author() { Id = 2, Name = "bububu" }
               );
        }
    }
}
