using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.LiveTests.Containers
{
    public class ExampleServiceImage : IDockerImage, IAsyncLifetime
    {
        static ExampleServiceImage()
        {
            TestcontainersSettings.Logger = new MyLogger();
        }

        public sealed class MyLogger : ILogger, IDisposable
        {
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                File.AppendAllText("diagnosticDOCKERBUILD.log", formatter.Invoke(state, exception));
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return this;
            }

            public void Dispose()
            {
            }
        }





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
