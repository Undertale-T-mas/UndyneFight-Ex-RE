﻿using UndyneFight_Ex.Server; 

Command.AddCommand(new Login());
Command.AddCommand(new KeepAlive());
Command.AddCommand(new SongUpdate());
Command.AddCommand(new Enquire());
Command.AddCommand(new Championship());
Command.AddCommand(new Time());

UFUpdater.Initialize();

SocketReceiver receiver = new();
Console.CancelKeyPress += myHandler;

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

void myHandler(object? sender, ConsoleCancelEventArgs args)
{ 
    UFConsole.WriteLine("\0#Red]Quit pending!");
    UFConsole.WriteLine("\0#Green]Saving Data ... ");
    UserLibrary.SaveAll();
    SongResultUpload.SaveAll();
    ChampionshipManager.Save();
    UFConsole.WriteLine("\0#Red]Quitting!");
}

