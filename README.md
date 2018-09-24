# automapper-flattened-datasource
Reproduction of an [issue](https://github.com/AutoMapper/AutoMapper.Extensions.ExpressionMapping/issues/11) with UseAsDataSource() when a mapping flattens a many-to-many collection. 

Clone and run `dotnet test` to see the test `TestWhereOnFlattenedCollection.UseAsDataSource_ThenWhereAny` fail.
