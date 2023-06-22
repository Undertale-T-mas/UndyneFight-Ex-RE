using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.UserService
{
    public class SingleChallenge : ISaveLoad
    {
        public List<ISaveLoad> Children => null;

        private Challenge challenge;
        public float TripleAccuracy { get; private set; }

        public void Update(float tripleAccuracy)
        {
            TripleAccuracy = MathF.Max(tripleAccuracy, TripleAccuracy);
        }
        public SingleChallenge(Challenge challenge)
        {
            this.challenge = challenge;
        }

        public void Load(SaveInfo info)
        {
            TripleAccuracy = info.FloatValue;
        }

        public SaveInfo Save()
        {
            SaveInfo result = new(challenge.Title + ":value=" + MathUtil.FloatToString(TripleAccuracy, 4));
            return result;
        }
    }
    public class ChallengeData : ISaveLoad
    {
        public Dictionary<string, SingleChallenge> AllData { get; init; }
        public List<ISaveLoad> Children => null;

        public ChallengeData()
        {
            AllData = new();
        }
        public void FinishChallenge(Challenge challenge)
        {
            float sum = 0;
            foreach (var v in challenge.ResultBuffer)
                sum += v.Accuracy;
            SingleChallenge challengeData;
            if (AllData.ContainsKey(challenge.Title)) challengeData = AllData[challenge.Title];
            else
            {
                challengeData = new(challenge);
                AllData.Add(challenge.Title, challengeData);
            }
            challengeData.Update(sum);
        }
        public void Load(SaveInfo info)
        {
            foreach (var next in info.Nexts.Values)
            {
                SingleChallenge challenge;
                AllData.Add(next.Title, challenge = new SingleChallenge(FightSystem.challengeDictionary[next.Title]));
                challenge.Load(next);
            }
        }

        public SaveInfo Save()
        {
            SaveInfo result = new("ChallengeData{");
            foreach (SingleChallenge challenge in AllData.Values)
                result.PushNext(challenge.Save());
            return result;
        }
    }
}