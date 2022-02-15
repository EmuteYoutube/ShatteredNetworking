// See https://aka.ms/new-console-template for more information
using ShatteredNetworking;

Console.WriteLine("Hello, World!");

try
{
    var server = new Server();

    server.Host(ShatteredNetworking.Globals.Port, ShatteredNetworking.Globals.Host);

}
catch (Exception e)
{
    Console.WriteLine($@"error:{e.ToString()}");
}
Console.Read();