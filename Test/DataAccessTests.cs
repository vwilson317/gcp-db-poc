using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task GetShouldReturnOnly4Objs()
        {
            //arrange
            var count = 4;
            var randomName = Guid.NewGuid().ToString();
            var city = new City
            {
                Name = randomName,
                FoundedDt = DateTime.UtcNow
            };
            var items = Enumerable.Repeat(city, count);
            items = items.Append(new City {Name = "something different", FoundedDt = DateTime.UtcNow});

            List<string> ids = new List<string>();
            foreach (var currentItem in items)
            {
                ids.Add(await DataAccess.Add(currentItem));
            }

            //act
            var objs = await DataAccess.Get<City>((nameof(City.Name), randomName));

            //assert
            Assert.Equal(count, objs.Count());

            //cleanup
            foreach(var currentId in ids)
            {
                await DataAccess.Delete<City>(currentId);
            }
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
}
