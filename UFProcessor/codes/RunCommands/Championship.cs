﻿using System.Net;
using System.Net.Sockets;
using System.Text;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Server
{
    public class Championship : Command
    {
        public Championship() : base("Championship")
        {
        }

        public override void Processor(string[] args, Client? client)
        {
            if (client == null)
            {
                return;
            }
            if (args.Length < 1) goto A;
            string arg0 = args[0];
            switch (arg0)
            {
                case "Insert":
                    //Championship Insert {JsonChampionshipInfo}
                    if(args.Length != 2) goto A;
                    try
                    {
                        ChampionshipManager.Insert(args[1]);
                    }
                    catch { 
                        goto A;
                    }
                    return;

                case "SignUp":
                    //Championship SignUp <Name> <Div> 
                    if (args.Length != 3) goto A;
                    if (client.BindUser == null) {
                        client.Reply("F please login first");
                        return;
                    }
                    ChampionshipManager.SignUp(client.BindUser, args[1], args[2]);
                    return;
            }

        A:
            client.Reply("E argument wrong");
        }
    }
}