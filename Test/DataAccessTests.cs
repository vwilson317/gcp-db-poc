using System;
using System.Net;
using System.Threading.Tasks;
using ClassLibrary1;
using Xunit;

namespace Test
{
    public class DataAccessTests
    {
        [Fact]
        public async Task AddShouldReturnId()
        {
            //act
            var id = await DataAccess.Add(new City
            {
                FoundedDt = DateTime.UtcNow
            });

            //assert
            Assert.NotNull(id);

            //cleanup
            await DataAccess.Delete<City>(id);
        }

        [Fact]
        public async Task GetShouldReturnObj()
        {
            //arrange
            var id = await DataAccess.Add(new City
            {
                FoundedDt = DateTime.UtcNow
            });

            //act
            var obj = await DataAccess.Get<City>(id);

            //assert
            Assert.NotNull(obj);

            //cleanup
            await DataAccess.Delete<City>(id);
        }

        [Fact]
        public async Task DeleteShouldDeleteObj()
        {
            //arrange
            var id = await DataAccess.Add(new City
            {
                FoundedDt = DateTime.UtcNow
            });

            //assert
            await DataAccess.Delete<City>(id);
        }

        [Fact]
        public async Task UpdateShouldChangeName()
        {
            //arrange
            var city = new City
            {
                FoundedDt = DateTime.UtcNow
            };
            var id = await DataAccess.Add(city);

            city.Name = "unit-test";

            //act
            await DataAccess.Update(id, city);
            var updatedObj = await DataAccess.Get<City>(id);

            //assert
            Assert.Equal(city.Name, updatedObj.Name);

            //cleanup
            await DataAccess.Delete<City>(id);
        }
    }

    public class IndexTests
    {
        [Fact]
        public async Task CreateIndexFromExample()
        {
            //act
            var response = await IndexManagement.CreateIndex<City>(ConfigValues.project, 
                nameof(City.Name), nameof(ModeValues.ASCENDING), 
                nameof(City.Country), nameof(ModeValues.ASCENDING));

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateIndex()
        {
            //act
            var response = await IndexManagement.CreateIndex<City>(ConfigValues.project, new IndexField
            {
                fieldPath = nameof(City.Name),
                mode = nameof(ModeValues.ASCENDING)
            });

            //assert
            Assert.Equal(HttpStatusCode.OK ,response.StatusCode);
        }
    }
}
