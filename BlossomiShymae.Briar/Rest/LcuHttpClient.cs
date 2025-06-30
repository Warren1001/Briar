using BlossomiShymae.Briar.Utils;

namespace BlossomiShymae.Briar.Rest
{
    /// <summary>
    /// A simple HTTP client for the League Client.
    /// </summary>
    public class LcuHttpClient : HttpClient
    {
        //private static readonly Lazy<LcuHttpClient> _instance = new(() => new LcuHttpClient(new()));
        private static LcuHttpClient? instance = null;

        private LcuHttpClientHandler _handler { get; }

        /// <summary>
        /// Gets instance of LcuHttpClient, creating it if it does not already exist
        /// </summary>
        /// <returns></returns>
        public static LcuHttpClient GetInstance()
        {
            return instance ??= new LcuHttpClient(new());
        }

        /// <summary>
        /// Forces a new instance of the HttpClient to be created
        /// </summary>
        public static void DisposeInstance()
        {
            if (instance != null)
            {
                instance.Dispose();
                instance = null;
            }
            
        }

        /// <summary>
        /// The information of the most recent League Client process.
        /// </summary>
        public ProcessInfo? ProcessInfo
        {
            get => _handler.ProcessInfo;
            internal set
            {
                _handler.ProcessInfo = value;
            }
        }

        /// <summary>
        /// The authentication of the most recent League Client process.
        /// </summary>
        public RiotAuthentication? RiotAuthentication => _handler.RiotAuthentication;

        internal LcuHttpClient(LcuHttpClientHandler lcuHttpClientHandler) : base(lcuHttpClientHandler) 
        { 
            _handler = lcuHttpClientHandler;
            BaseAddress = new("https://127.0.0.1"); // This is only done to make PrepareRequestMessage not throw.
        }   
    }
}