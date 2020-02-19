namespace SoulsLike
{
    public interface IService
    {
        /// <summary>
        /// Is this a persistent service?
        /// </summary>
        bool IsPersistent { get; }

        /// <summary>
        /// Called when the service is started (i.e first called)
        /// </summary>
        void OnStart();

        /// <summary>
        /// Called when the service is ended (i.e. game quit)
        /// </summary>
        void OnEnd();
    }
}
