﻿namespace UndyneFight_Ex.Remake.Network
{ 
    public interface IMessageResult
    {
        public void Analysis(string message);   
    }
    public class Empty : IMessageResult
    {
        public void Analysis(string message)
        { 
        }
    }
    public class Message<T> where T : IMessageResult, new()
    {
        public Message(bool success, string info) {
            this.Success = success; this.Info = info;
        }

        public bool Success { get; internal set; }
        public string Info { get; internal set; }
        public T Data { get; internal set; } = new();
    }
}