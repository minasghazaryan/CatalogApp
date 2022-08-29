using CatalogApi.Logic;
using CatalogApi.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Catalog.Test.Common
{
    public class Fixture
    {
        private IServiceCollection _services = new ServiceCollection();
        private ServiceProvider _serviceProvider { get; set; }
        private bool _initialized;

        public Fixture()
        {
            //_services.AddSingleton<IHostEnvironment, HostingEnvironment>(x => new HostingEnvironment() { EnvironmentName = "Development" });
            _services.AddSingleton<ICatalogService, CatalogService>();
        }

        public void AddService<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>();
        }

        public TService GetService<TService>()
            where TService : class
        {
            if (!_initialized) Build();
            return _serviceProvider.GetRequiredService<TService>();
        }

        private void Build()
        {
            _serviceProvider = _services.BuildServiceProvider();
            _initialized = true;
        }
    }
}
