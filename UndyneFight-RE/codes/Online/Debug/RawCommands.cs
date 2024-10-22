﻿using System;
using col = Microsoft.Xna.Framework.Color;

namespace UndyneFight_Ex.Remake.UI.DEBUG
{
    internal class RawCommand : Command
    {
        public RawCommand(ArgumentMatcher startMatcher, string title) : base(title) { 
            this.CommandState = CommandState.Raw; 
            this._startMatcher = startMatcher;
        }

        ArgumentMatcher _startMatcher;

        internal sealed override col Analyze(Semantics semantics)
        {
            if(semantics.Extra == null)
            {
                semantics.Extra = _startMatcher;
                return col.Gold;
            }
            ArgumentMatcher matcher = (ArgumentMatcher)semantics.Extra; 
            var pair = matcher.TryMatch(semantics.CurrentText);
            semantics.Extra = pair.Item2; 

            return pair.Item1;
        }
    }
    internal class LogCommand : RawCommand
    {
        public LogCommand( ) : base(
            ArgumentMatcher.Link(
                new BranchArgument(new() {  
                    ["in"] = new Tuple<col, ArgumentMatcher>(col.Lime, 
                        ArgumentMatcher.Link(new AbsoluteArgument(col.White), new AbsoluteArgument(col.White))
                    ),
                    ["reg"] = new Tuple<col, ArgumentMatcher>(col.Aqua, 
                        ArgumentMatcher.Link(new AbsoluteArgument(col.White), new AbsoluteArgument(col.White))
                    ),
                    ["key"] = new Tuple<col, ArgumentMatcher>(col.Aqua, new AbsoluteArgument(col.White)),
                    ["out"] = new Tuple<col, ArgumentMatcher>(col.Aqua, new AbsoluteArgument(col.White)),
                })
            )

            ,"Log")
        {
        } 
    }

    internal class ChampionshipCommand : RawCommand
    {
        public ChampionshipCommand() : base(
            ArgumentMatcher.Link(
                new BranchArgument(new() {
                    ["Insert"] = new Tuple<col, ArgumentMatcher>(col.Lime, new AbsoluteArgument(col.White)),
                    ["SignUp"] = new Tuple<col, ArgumentMatcher>(col.Lime, ArgumentMatcher.Link(
                            new AbsoluteArgument(col.White),
                            new AbsoluteArgument(col.White)
                        ))
                    }
                    
                    )
                )
            , "Championship")
        { }
    }
    internal class EnquireCommand : RawCommand
    {
        public EnquireCommand( ) : base(
            ArgumentMatcher.Link(
                new BranchArgument(new() {  
                    ["Scoreboard"] = new Tuple<col, ArgumentMatcher>(col.Lime, 
                        ArgumentMatcher.Link(new AbsoluteArgument(col.White), new SelectiveArgument(new() { 
                            ["0"] = col.White, 
                            ["1"] = col.LimeGreen, 
                            ["2"] = col.CornflowerBlue, 
                            ["3"] = col.MediumPurple, 
                            ["4"] = col.Gold, 
                        }))
                    ),
                    ["Self"] = new Tuple<col, ArgumentMatcher>(col.Lime, ArgumentMatcher.Empty),
                    ["Championship"] = new Tuple<col, ArgumentMatcher>(col.Lime, ArgumentMatcher.Empty),
                    ["ChampionshipInfo"] = new Tuple<col, ArgumentMatcher>(col.Lime, ArgumentMatcher.Empty),
                })
            )

            ,"Enquire")
        {
        } 
    }
    internal class KeepAliveCommand : RawCommand
    {
        public KeepAliveCommand( ) : base(
            ArgumentMatcher.Link(new AbsoluteArgument(col.White)), 
            
            "keepAlive")
        {
        } 
    }
    internal class TimeCommand : RawCommand
    {
        public TimeCommand( ) : base(
            ArgumentMatcher.Link(new AbsoluteArgument(col.White)), 
            
            "Time")
        {
        } 
    }
}