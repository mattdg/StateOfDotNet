using Grpc.Core;

using GrpcGreeter;

namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHelloUnary(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayHelloServerStreaming(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            int count = 1;
            while (!context.CancellationToken.IsCancellationRequested && count <= 10)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {request.Name}! ({count}) It is now {DateTime.Now}"
                });

                count++;
                await Task.Delay(1000);
            }

            await responseStream.WriteAsync(new HelloReply
            {
                Message = $"OK {request.Name}, that's enough"
            });
        }

        public override async Task<HelloReply> SayHelloClientStreaming(
            IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            var names = new List<string>();

            await foreach (var request in requestStream.ReadAllAsync())
            {
                names.Add(request.Name);
            }

            names.Sort();

            var message = "Hello to y'all: " + string.Join(", ", names);
            return new HelloReply { Message = message };
        }

        public override async Task SayHelloBidirectionalStreaming(
            IAsyncStreamReader<HelloRequest> requestStream,
            IServerStreamWriter<HelloReply> responseStream,
            ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                await responseStream.WriteAsync(
                    new HelloReply { Message = "Hello " + request.Name });
            }
        }
    }
}