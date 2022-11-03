using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.LiveTests.Containers
{
    public class ExampleServiceImage : IDockerImage, IAsyncLifetime
    {
        public const ushort HttpsPort = 443;

        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

        private readonly IDockerImage _image = new DockerImage(string.Empty, "example-service", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());

        public async Task InitializeAsync()
        {
            await _semaphoreSlim.WaitAsync()
              .ConfigureAwait(false);

            try
            {
                _ = await new ImageFromDockerfileBuilder()
                  .WithName(this)
                  .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), string.Empty)
                  .WithDockerfile("src/Example.Service/Dockerfile")
                  .WithDeleteIfExists(false)
                  .Build()
                  .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public string Repository => _image.Repository;

        public string Name => _image.Name;

        public string Tag => _image.Tag;

        public string FullName => _image.FullName;

        public string GetHostname()
        {
            return _image.GetHostname();
        }
    }
}
