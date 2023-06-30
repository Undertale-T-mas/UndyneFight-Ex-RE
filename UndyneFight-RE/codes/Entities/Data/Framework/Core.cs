using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.Remake.Data
{
    public abstract class Datum<T> : ISaveLoad
    {
        public Datum(string name){
            this.Name = name;
        }

        public T Value { get; set; }
        protected string Name { get; private set; }

        public virtual List<ISaveLoad> Children => null;

        public abstract void Load(SaveInfo info);

        public virtual SaveInfo Save() 
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
}