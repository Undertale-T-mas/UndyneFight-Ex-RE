using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UndyneFight_Ex.Entities;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Remake.Components;
using UndyneFight_Ex.Remake.Effects;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.UserService;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.Remake.Network;
using System.Security.Principal;
using UndyneFight_Ex.Remake.UI.DEBUG;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace UndyneFight_Ex.Remake.UI.DEBUG
{
    internal abstract class ArgumentMatcher
    {
        private class EmptyArgumentMatcher : ArgumentMatcher
        {
            public EmptyArgumentMatcher() {
                this.Next = this;
            }

            protected override col GetColor(string val)
            {
                return col.Silver;
            }
        }
        public ArgumentMatcher()
        {
            _linkFront = this;
        }

        public static readonly ArgumentMatcher Empty = new EmptyArgumentMatcher();

        protected abstract col GetColor(string val);
        protected ArgumentMatcher Next { private get; set; } = Empty;
        private ArgumentMatcher _linkFront;

        public Tuple<col, ArgumentMatcher> TryMatch(string val)
        {
            col res = GetColor(val);
            return new(res, Next);
        }

        public virtual void SetNext(ArgumentMatcher next)
        {
            this._linkFront.Next = next;
        }

        public static ArgumentMatcher Link(params ArgumentMatcher[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                args[i].SetNext(args[i + 1]);
                args[0]._linkFront = args[^1];
            }
            return args[0];
        }
    }
    internal class AbsoluteArgument : ArgumentMatcher
    {
        col _c;
        public AbsoluteArgument(col c) { this._c = c; }
        protected override col GetColor(string val)
        {
            return _c;
        }
    }
    internal class SelectiveArgument : ArgumentMatcher
    {
        Dictionary<string, col> _map;
        public SelectiveArgument(Dictionary<string, col> map)
        {
            this._map = map;
        }
        protected override col GetColor(string val)
        {
            if (_map.ContainsKey(val))
            {
                return _map[val];
            }
            return col.DarkRed;
        }
    } 
    internal class BranchArgument : ArgumentMatcher
    {
        Dictionary<string, Tuple<col, ArgumentMatcher>> _map;
        public BranchArgument(Dictionary<string, Tuple<col, ArgumentMatcher>> map)
        {
            this._map = map;
        }
        public override void SetNext(ArgumentMatcher next)
        {
            foreach(var pair in _map.Values)
            {
                pair.Item2.SetNext(next);
            }
        }
        protected override col GetColor(string val)
        {
            if (_map.ContainsKey(val))
            {
                var pair = _map[val];
                Next = pair.Item2;
                return pair.Item1;
            }
            else
            {
                Next = Empty;
                return col.DarkRed;
            }
        }
    }
}