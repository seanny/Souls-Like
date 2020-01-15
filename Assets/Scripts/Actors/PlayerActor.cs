namespace SoulsLike
{
    class PlayerActor : Actor
    {
        public static PlayerActor instance { get; private set; }

        public Sword sword { get; private set; }

        protected override void Start()
        {
            base.Start();
            sword = GetComponentInChildren<Sword>();
            instance = this;
        }
    }
}
