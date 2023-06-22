using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UndyneFight_Ex
{
    public class ModDynamic
    {
        public const string UFExVersion = "0.1.7";

        private List<Type> _fightTypes = new List<Type>();

        public ModDynamic(string path)
        {
            Assembly assem = Assembly.LoadFile(path);
            Type[] types = assem.GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(typeof(Fight.IClassicFight))) _fightTypes.Add(type);
                if (type.Name == "Program")
                {
                    MethodInfo info = type.GetMethod("Main");
                    if (info.GetParameters().Length == 0) info.Invoke(null, null);
                    else info.Invoke(null, new object[] { UFExVersion });
                }
            }
        }
    }
}