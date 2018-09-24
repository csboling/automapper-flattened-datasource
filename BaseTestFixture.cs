using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace automapper_flattened_datasource
{
    public abstract class BaseTestFixture : IDisposable
    {
        public BaseTestFixture()
        {
            this.Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Library, LibraryDto>()
                    .ForMember(
                        dst => dst.Sections,
                        opt => opt.MapFrom(src => src.LibrarySections))
                    .ForMember(
                        dst => dst.Books,
                        opt => opt.MapFrom(src => src.LibraryBooks.Select(lb => lb.Book)));

                cfg.CreateMap<Book, BookDto>();
            })
                .CreateMapper();

            this.Connection = new SqliteConnection("Data Source=:memory:");
            this.Connection.Open();

            using (var db = new ExampleContext(this.Connection))
            {
                db.Database.EnsureCreated();
                this.Seed(db);
            }
        }

        protected IMapper Mapper { get; }
        
        protected SqliteConnection Connection { get; }

        public void Dispose()
        {
            this.Connection.Dispose();
        }

        protected virtual void Seed(DbContext db)
        {
            db.Add(new Library
            {
                LibrarySections = new List<LibrarySection>
                {
                    new LibrarySection
                    {
                        Name = "History",
                    },
                    new LibrarySection
                    {
                        Name = "Fiction",
                    },
                },
                LibraryBooks = new List<LibraryBook>
                {
                    new LibraryBook
                    {
                        Book = new Book
                        {
                            Title = "The Cat in the Hat",
                        }
                    }
                },
            });
            db.SaveChanges();
        }
    }
}
