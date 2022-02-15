// See https://aka.ms/new-console-template for more information
using ShatteredNetworking;

Console.WriteLine("Hello, World!");

try
{
    var client = new Client();

    client.Connect(ShatteredNetworking.Globals.Port, ShatteredNetworking.Globals.Host);
    while (true)
    {
        Console.WriteLine("Send a message?");
        string data = Console.ReadLine();
        client.SendMessage(data);
        client.WaitForMessage();
    }

}
catch (Exception e)
{
    Console.WriteLine($@"error:{e.ToString()}");
}
Console.Read();