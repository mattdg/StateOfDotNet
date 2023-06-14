using Grpc.Core;
using Grpc.Net.Client;

using GrpcGreeter;

using System;

using static GrpcGreeter.Greeter;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7145");
var client = new GreeterClient(channel);

while (true)
{
    Console.WriteLine();
    Console.WriteLine(" === Select test to run (or Q to stop) ===");
    Console.WriteLine("  1. Single Request, Single Response");
    Console.WriteLine("  2. Single Request, Multiple Response");
    Console.WriteLine("  3. Multiple Request, Single Response");
    Console.WriteLine("  4. Bi-directional Requests and Responses");

    Console.WriteLine();

    var key = Console.ReadKey(true);
    switch (key.Key)
    {
        case ConsoleKey.Q:
            Console.WriteLine("Exiting");
            return;
        case ConsoleKey.D1:
            Console.WriteLine("\nCASE 1: Requesting a single greeting");
            await DemoUnaryCall(client);
            break;
        
        case ConsoleKey.D2:
            Console.WriteLine("\nCASE 2: Requesting multiple greetings, press CTRL-C to stop receiving greetings");
            await DemoServerStreamingCall(client);
            break;
        
        case ConsoleKey.D3:
            Console.WriteLine("\nCASE 3: Sending multiple names, get a single greeting");
            await DemoClientStreamingCall(client);
            break;

        case ConsoleKey.D4:
            Console.WriteLine("\nCASE 4: Bidirectional, press CTRL-C to stop receiving greetings");
            await DemoBidirectionalCall(client);
            break;
    }
}

async Task DemoUnaryCall(GreeterClient greeterClient)
{
    var singleGreeting = await greeterClient.SayHelloUnaryAsync(new HelloRequest { Name = "GreeterClient" });
    Console.WriteLine("Greeting: " + singleGreeting.Message);
}

async Task DemoServerStreamingCall(GreeterClient greeterClient)
{
    var cts = new CancellationTokenSource();

    Console.CancelKeyPress += ConsoleOnCancelKeyPress;

    try
    {
        var responses = greeterClient.SayHelloServerStreaming(
            new HelloRequest { Name = "GreeterClient2" },
            cancellationToken: cts.Token).ResponseStream;

        await foreach (var greeting in responses.ReadAllAsync(cts.Token))
        {
            Console.WriteLine("Greeting: " + greeting.Message);
        }
    }
    catch (RpcException e) when (e.InnerException is OperationCanceledException)
    {
        Console.WriteLine("Stopped at user request.");
    }
    finally
    {
        Console.CancelKeyPress -= ConsoleOnCancelKeyPress;
    }

    void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs args)
    {
        args.Cancel = true; // Don't actually kill the process in this case.
        cts.Cancel();
    }
}

async Task DemoClientStreamingCall(GreeterClient greeterClient)
{
    var call = greeterClient.SayHelloClientStreaming();

    var peopleToGreet = new [] { "Matt", "Julie", "Zoe", "Heidi", "Bryan" };
    var requests = call.RequestStream;
    var resultTask = call.ResponseAsync.ContinueWith(t => Console.WriteLine("Greeting: " + t.Result.Message));

    foreach (var person in peopleToGreet)
    {
        Console.WriteLine($"Sending {person} to be greeted");
        await requests.WriteAsync(new HelloRequest { Name = person });
        await Task.Delay(500);
    }
    await requests.CompleteAsync();
    Console.WriteLine();

    await resultTask;
}

async Task DemoBidirectionalCall(GreeterClient greeterClient)
{
    var cts = new CancellationTokenSource();

    Console.CancelKeyPress += ConsoleOnCancelKeyPress;

    try
    {
        var call = greeterClient.SayHelloBidirectionalStreaming(cancellationToken: cts.Token);

        var receiverTask = Task.Run(async () =>
        {
            await foreach (var greeting in call.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
            {
                Console.WriteLine("Greeting: " + greeting.Message);
            }
        }, cts.Token);

        var senderTask = Task.Run(async () =>
        {
            int i = 1;
            while (!cts.Token.IsCancellationRequested)
            {
                await call.RequestStream.WriteAsync(new HelloRequest { Name = "Matt" + i });
                i++;
                await Task.Delay(1000);
            }
        }, cts.Token);

        await Task.WhenAll(receiverTask, senderTask);
    }
    catch (RpcException e) when (e.InnerException is OperationCanceledException)
    {
        Console.WriteLine("Stopped at user request.");
    }
    finally
    {
        Console.CancelKeyPress -= ConsoleOnCancelKeyPress;
    }

    void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs args)
    {
        args.Cancel = true; // Don't actually kill the process in this case.
        cts.Cancel();
    }
}