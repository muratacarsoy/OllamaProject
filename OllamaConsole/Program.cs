// See https://aka.ms/new-console-template for more information
using OllamaSharp;

Console.WriteLine("llama3.1 ile konuşun!");
var uri = new Uri("http://localhost:11434");
var apiClient = new OllamaApiClient(uri)
{
    SelectedModel = "llama3.1"
};
var chat = new Chat(apiClient);
while (true)
{
    var message = Console.ReadLine();
    if (message == null) { continue; }
    await foreach (var answerToken in chat.Send(message))
        Console.Write(answerToken);
    Console.Write('\n');
}



