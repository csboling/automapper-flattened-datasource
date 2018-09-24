using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper.Extensions.ExpressionMapping;
using Xunit;

namespace automapper_flattened_datasource
{
    public class TestWhereOnFlattenedCollection : BaseTestFixture
    {
        // passes, but evaluated client side
        [Fact]
        public void ProjectTo_ThenWhereAny()
        {
            using (var db = new ExampleContext(this.Connection))
            {
                var before = db.Set<Library>()
                    .Where(l => l.LibraryBooks.Select(lb => lb.Book).Any())
                    .ProjectTo<LibraryDto>(this.Mapper.ConfigurationProvider)
                    .ToList();
                var after = db.Set<Library>()
                    .ProjectTo<LibraryDto>(this.Mapper.ConfigurationProvider)
                    .Where(l => l.Books.Any())
                    .ToList();

                Assert.Equal(
                    before.Select(l => l.Id),
                    after.Select(l => l.Id));
            }
        }

        // fails when referencing a flattened collection
        [Fact]
        public void UseAsDataSource_ThenWhereAny()
        {
            using (var db = new ExampleContext(this.Connection))
            {
                var before = db.Set<Library>()
                    .Where(l => l.LibraryBooks.Select(lb => lb.Book).Any())
                    .UseAsDataSource(this.Mapper)
                    .For<LibraryDto>()
                    .ToList();
                var after = db.Set<Library>()
                    .UseAsDataSource(this.Mapper)
                    .For<LibraryDto>()
                    .Where(l => l.Books.Any())
                    .ToList();

                Assert.Equal(
                    before.Select(l => l.Id),
                    after.Select(l => l.Id));
            }
        }

        // passes when referencing an "ordinary" collection, evaluated client side
        [Fact]
        public void UnflattenedCollection_ProjectTo_ThenWhereAny()
        {
            using (var db = new ExampleContext(this.Connection))
            {
                var before = db.Set<Library>()
                    .Where(l => l.LibrarySections.Any())
                    .ProjectTo<LibraryDto>(this.Mapper.ConfigurationProvider)
                    .ToList();
                var after = db.Set<Library>()
                    .ProjectTo<LibraryDto>(this.Mapper.ConfigurationProvider)
                    .Where(l => l.Sections.Any())
                    .ToList();

                Assert.Equal(
                    before.Select(l => l.Id),
                    after.Select(l => l.Id));
            }
        }

        // passes when referencing an "ordinary" collection
        [Fact]
        public void UnflattenedCollection_UseAsDataSource_ThenWhereAny()
        {
            using (var db = new ExampleContext(this.Connection))
            {
                var before = db.Set<Library>()
                    .Where(l => l.LibrarySections.Any())
                    .UseAsDataSource(this.Mapper)
                    .For<LibraryDto>()
                    .ToList();
                var after = db.Set<Library>()
                    .UseAsDataSource(this.Mapper)
                    .For<LibraryDto>()
                    .Where(l => l.Sections.Any())
                    .ToList();

                Assert.Equal(
                    before.Select(l => l.Id),
                    after.Select(l => l.Id));
            }
        }
    }
}
