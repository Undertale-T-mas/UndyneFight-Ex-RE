using UndyneFight_Ex.Server;

Command.AddCommand(new Login());
Command.AddCommand(new KeepAlive());

SocketReceiver receiver = new();

if (!receiver.Start())
{
    Console.ReadKey();
    return;
}
while (true)
{
    string? str = Console.ReadLine();
    if (string.IsNullOrEmpty(str))
    {
        receiver.DoCommand(null, str);
    }
}