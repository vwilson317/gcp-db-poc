using System;
using System.Net;
using System.Threading.Tasks;
using ClassLibrary1;
using Xunit;

namespace Test
{
    public class IndexTests
    {
        [Fact]
        public async Task CreateIndex()
        {
            //act
            var response = await IndexManagement.CreateIndex<City>(ConfigValues.project, 
                new IndexField
            {
                fieldPath = nameof(City.Name),
                mode = nameof(ModeValues.ASCENDING)
            },
                new IndexField
                {
                    fieldPath = nameof(City.PopSize),
                    mode = nameof(ModeValues.DESCENDING)
                });

            //assert
            Assert.Equal(HttpStatusCode.OK ,response.StatusCode);
        }

        [Fact]
        public void CreateWithOneIndexThrowsException()
        {
            //assert
            Assert.ThrowsAsync<ArgumentException>( () => IndexManagement.CreateIndex<City>(
                ConfigValues.project,
                new IndexField
                {
                    fieldPath = nameof(City.Name),
                    mode = nameof(ModeValues.ASCENDING)
                }));
        }
    }
}