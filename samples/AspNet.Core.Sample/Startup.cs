using System;
using FeatureSwitch;
using FeatureSwitch.Strategies;
using FeatureSwitch.Strategies.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNet.Core.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            services.AddTransient<QueryStringStrategyImpl>();

            //var builder = new FeatureSetBuilder(new NewDependencyContainer(services));
            //builder.Build();

            services.AddSingleton(typeof(IFeatureFactory), typeof(FeatureFactory));
            services.AddTransient<FeatureContext2>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
                       {
                           routes.MapRoute(name: "default",
                                           template: "{controller=Home}/{action=Index}/{id?}");
                       });
        }
    }

    public class FeatureContext2
    {
        private readonly IFeatureFactory _factory;

        public FeatureContext2(IFeatureFactory factory)
        {
            _factory = factory;
        }

        public bool IsEnabled<T>() where T: BaseFeature
        {
            var strategy = _factory.Resolve(typeof(QueryStringStrategyImpl)) as IStrategyStorageReader;

            return strategy != null ? strategy.Read() : false;
        }
    }

    public interface IFeatureFactory
    {
        object Resolve(Type target);
    }

    class FeatureFactory : IFeatureFactory
    {
        private readonly IServiceProvider _provider;

        public FeatureFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public object Resolve(Type target)
        {
            return _provider.GetService(target);
        }
    }

    public class NewDependencyContainer : IDependencyContainer
    {
        private readonly IServiceCollection _provider;
        private readonly IServiceProvider _serviceProvider;

        public NewDependencyContainer(IServiceCollection provider)
        {
            _provider = provider;
            _serviceProvider = _provider.BuildServiceProvider();
        }

        public void RegisterType(Type requestedType, Type implementation)
        {
            _provider.AddTransient(requestedType, implementation);
        }

        public object Resolve(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
