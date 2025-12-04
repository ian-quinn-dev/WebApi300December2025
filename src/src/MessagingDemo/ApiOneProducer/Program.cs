using Messages;
using Wolverine;
using Wolverine.Kafka;


var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddOpenApi();


builder.Host.UseWolverine(options =>
{
    options.UseKafka("localhost:9092");
    options.PublishMessage<PublishMessage>().ToKafkaTopic("demo-messages").PublishRawJson();
    
});
var app = builder.Build();

app.MapPost("/send", async (SomeMessage message, IMessageBus bus) =>
{
    // reduce the inventory, so I call that api.
    // update my own database, so I do that,
    // charge the credit card, so I can an api for that.
    // send confirmation email.
    // write all the work to be done to something that is TRANSACTIONAL and durable, like a database.
    // And then use that as your transaction - if you can write it there, later you can get it,
    // if there are errors, you can have retries and retry strategies and all that.

  
    await bus.PublishAsync(new PublishMessage(message.Content)); // Not: No Durability in this demo.
    return TypedResults.Ok("Sent " + message.Content + " To the Broker");
});
app.MapOpenApi("/openapi/v1.json");
app.MapDefaultEndpoints();
app.Run();



public record PublishMessage(string Content);