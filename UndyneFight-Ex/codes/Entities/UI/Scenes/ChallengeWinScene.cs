namespace UndyneFight_Ex.Entities
{
    internal class ChallengeWinScene : Scene
    {
        private Challenge challenge;

        public ChallengeWinScene(Challenge challenge) : base(new ChallengeResult(challenge))
        {
            this.challenge = challenge;
            if (PlayerManager.CurrentUser != null)
            {
                PlayerManager.CurrentUser.ChallengeData.FinishChallenge(challenge);
                PlayerManager.Save();
            }
        }
    }
}