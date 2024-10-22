﻿namespace UndyneFight_Ex.Server
{
    public class Login : Command
    {
        public Login() : base("Log") { Log = true; }
        public override void Processor(string[] args, Client? client)
        {
            if (client == null) return;
            if (args.Length <= 1) client.Reply("F message disturbed");
            else
            {
                string arg2 = args[0], account = args[1], password = "";
                if (arg2 != "key")
                {
                    if (args.Length <= 2)
                    {
                        client.Reply("F message disturbed");
                        return;
                    }
                    if (string.IsNullOrEmpty(client.RSABuffer))
                    {
                        client.Reply("E you must require a key");
                        return;
                    }
                    password = MathUtil.Decrypt(args[2], client.RSABuffer);
                }
                if (arg2 == "in")
                {
                    var tuple = UserLibrary.Auth(account, password);
                    if (tuple.Item1[0] == 'S')
                    {
                        client.BindUser = tuple.Item2;
                        client.UserName = client.BindUser.Name;
                        client.Reply(tuple.Item1);
                    }
                    else
                    {
                        client.Reply(tuple.Item1);
                    }
                }
                else if (arg2 == "reg")
                {
                    var tuple = UserLibrary.Register(account, password);
                    if (tuple.Item1[0] == 'S')
                    {
                        client.BindUser = tuple.Item2;
                        client.UserName = client.BindUser.Name;
                        client.Reply(tuple.Item1);
                    }
                    else
                    {
                        client.Reply(tuple.Item1);
                    }
                }
                else if (arg2 == "key")
                {
                    client.Reply(UserLibrary.GenerateRSA(client));
                }
            }
        }
    }
}