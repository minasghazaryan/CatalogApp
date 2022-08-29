using Catalog.Test.Common;
using CatalogApi.Controllers;
using CatalogApi.Logic;
using CatalogApi.Logic.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test
{
    public class UnitTest1 : IClassFixture<Fixture>
    {
        private readonly ICatalogService _catalogService;
        public UnitTest1(Fixture fixture)
        {
            fixture.AddService<ICatalogService, CatalogService>();
            _catalogService = fixture.GetService<ICatalogService>();
        }
        [Fact]
        public async Task CatalogCreateTest()
        {
            CatalogController catalog = new CatalogController(_catalogService);
            var res = catalog.Create(new CatalogApi.Data.Models.CatalogModel());

        }
    }
}
