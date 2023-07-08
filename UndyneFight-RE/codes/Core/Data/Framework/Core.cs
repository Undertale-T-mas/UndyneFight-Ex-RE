using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.Remake.Data
{
    public abstract class Datum : ISaveLoad
    {
        public string Name { get; private set; }
        public string InitialText { get; set; }
        public Datum(string name)
        {
            this.Name = name;
        }

        public virtual List<ISaveLoad> Children => null;

        public abstract void Load(SaveInfo info);
        public abstract SaveInfo Save();
    }
    public abstract class Datum<T> : Datum
    { 
        public Datum(string name) : base(name)
        {  }
        public T Value { get; set; }

        public override SaveInfo Save() 
        {
            return new SaveInfo(Name + ":" + "value=" + Value.ToString());
        }
        public static implicit operator T(Datum<T> origin) { return origin.Value; } 
    }

    public class DatumInt : Datum<int>
    {
        public DatumInt(string name) : base(name) {  
        } 

        public override void Load(SaveInfo info)
        {
            this.Value = info.IntValue;
        } 
    }
    public class DatumFloat : Datum<float>
    {
        public DatumFloat(string name) : base(name) {
        } 

        public override void Load(SaveInfo info)
        {
            this.Value = info.FloatValue;
        } 
    }    
    public class DatumString : Datum<string>
    {
        public DatumString(string name) : base(name) {  
        } 

        public override void Load(SaveInfo info)
        {
            this.Value = info.StringValue;
        } 
    }
    public class DatumBool : Datum<bool>
    {
        public DatumBool(string name) : base(name) {  
        } 

        public override void Load(SaveInfo info)
        {
            this.Value = info.BoolValue;
        }

        public override SaveInfo Save()
        {
            return new SaveInfo(Name + ":" + "value=" + (Value ? "True" : "False"));
        }
    }
    public class DatumVector2 : Datum<Vector2>
    {
        public DatumVector2(string name) : base(name) {  
        } 

        public override void Load(SaveInfo info)
        {
            this.Value = info.VectorValue;
        }

        public override SaveInfo Save()
        {
            return new SaveInfo(Name + ":" + "x=" + Value.X + ",y=" + Value.Y);
        }
    }
    public class DatumDictionary : Datum<Dictionary<string, string>>
    {
        public DatumDictionary(string name) : base(name) {  
        } 

        public override void Load(SaveInfo info)
        {
            this.Value = new();
            var v = info.keysForIndexs;
            foreach ( var key in v ) { this.Value.Add(key.Key, info[key.Value]); }
        }

        public override SaveInfo Save()
        {
            string str = Name + ":";
            int i = 0;
            foreach(var kvp in this.Value)
            {
                str += kvp.Key + "=" + kvp.Value;
                i++;
                if(i != this.Value.Count) str += ",";
            }
            SaveInfo result = new(str);
            return result;
        }
    }

    public abstract class DataBranch : Datum
    {
        public new List<Datum> Children { get; private set; } = new List<Datum>();

        public DataBranch(string name) : base(name)
        {
            this.InitialText = "{";
        } 

        public override SaveInfo Save()
        {
            SaveInfo info = new(Name + "{");
            this.Children.ForEach(s => info.PushNext(s.Save()));
            return info;
        }
        public override void Load(SaveInfo info)
        {
            /*
            if (!info.Nexts.ContainsKey("UserMemory")) info.PushNext(new("UserMemory{")); 
            this.Memory.Load(info.Nexts["UserMemory"]);*/
            this.Children.ForEach(s => {
                if (!info.Nexts.ContainsKey(s.Name)) info.PushNext(new(s.Name + s.InitialText));
                s.Load(info.Nexts[s.Name]);
            });
        }
    }
}