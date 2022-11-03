using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.LiveTests.Containers
{
    public class ExampleServiceContainer : HttpClient, IAsyncLifetime
    {
        private static readonly ExampleServiceImage Image = new();

        private readonly IDockerNetwork _exampleServiceNetwork;

        private readonly IDockerContainer _exampleServiceContainer;

        public ExampleServiceContainer()
        {
            _exampleServiceNetwork = new TestcontainersNetworkBuilder()
              .WithName(Guid.NewGuid().ToString("D"))
              .Build();

            _exampleServiceContainer = new TestcontainersBuilder<TestcontainersContainer>()
              .WithImage(Image)
              .WithNetwork(_exampleServiceNetwork)
              .WithPortBinding(ExampleServiceImage.HttpsPort, true)
              .WithEnvironment("ASPNETCORE_URLS", "https://+")
              .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(ExampleServiceImage.HttpsPort))
              .Build();
        }

        public async Task InitializeAsync()
        {
            // It is not necessary to clean up resources immediately (still good practice). The Resource Reaper will take care of orphaned resources.
            await Image.InitializeAsync()
              .ConfigureAwait(false);

            await _exampleServiceNetwork.CreateAsync()
              .ConfigureAwait(false);

            await _exampleServiceContainer.StartAsync()
              .ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Image.DisposeAsync()
              .ConfigureAwait(false);

            await _exampleServiceContainer.DisposeAsync()
              .ConfigureAwait(false);

            await _exampleServiceNetwork.DeleteAsync()
              .ConfigureAwait(false);
        }

        public void SetBaseAddress()
        {
            try
            {
                var uriBuilder = new UriBuilder("https", _exampleServiceContainer.Hostname, _exampleServiceContainer.GetMappedPublicPort(ExampleServiceImage.HttpsPort));
                BaseAddress = uriBuilder.Uri;
            }
            catch
            {
                // Set the base address only once.
            }
        }
    }

}
