using Confluent.Kafka;
using Messages;
using Wolverine;
using Wolverine.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(options =>
{
    options.UseKafka("localhost:9092").ConfigureConsumers(consumer =>
    {
        consumer.GroupId = "api-client"; // "Consumer Groups"
        //consumer.AutoOffsetReset = AutoOffsetReset.Earliest; // earliest means give me all the messages (me being "api-client") that haven't been processed yet since the last time I was around.

        consumer.AutoOffsetReset = AutoOffsetReset.Latest; // forget about anything I missed. Just start now.
        // I can say exactly what message I want to start with, by either ID or date and time.
    });
    

    options.ListenToKafkaTopic("demo-messages").ReceiveRawJson<SomeMessage>();
});
var app = builder.Build();


app.Run();

public static class MessageHandler
{
    public static void Handle(SomeMessage message, ILogger logger)
    {
        logger.LogWarning($"Received message: {message.Content}");
    }
}