using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using KilnApi.Helpers;
using System.Security.Authentication;

namespace KilnApi {
    /// <summary>
    /// Represents methods for interacting with Kiln.
    /// An instance is always associated with only one Kiln account.
    /// </summary>
    public sealed partial class Kiln : IDisposable {
        /// <summary>
        /// Since an instance of Kiln is created by static factory,
        /// the constructor is private.
        /// </summary>
        /// <param name="baseUrl">Base URL of the server where Kiln is hosted.</param>
        /// <param name="user">Username or email address of the user.</param>
        /// <param name="token">Authentication token.</param>
        private Kiln(string baseUrl, string user, SecureString token) {
            m_baseUrl = baseUrl;
            m_token = token;
            m_user = user;
        }

        /// <summary>
        /// Release all resources occupied by Kiln instance.
        /// </summary>
        public void Dispose() {
            m_token.Dispose();
        }

        #region Credentials
        /// <summary>
        /// Authentication token of the session.
        /// </summary>
        private string Token {
            get { return m_token.Decrypt(); }
        }

        /// <summary>
        /// Authentication token of the session.
        /// </summary>
        private readonly SecureString m_token;

        /// <summary>
        /// Username or email address of the Kiln user.
        /// </summary>
        private readonly string m_user;
        #endregion

        #region Static Factories
        /// <summary>
        /// Authenticate the user with specified credentials.
        /// </summary>
        /// <param name="server">The server address where the Kiln is hosted.</param>
        /// <param name="token">Authentication token issued by Kiln.</param>
        /// <returns>Kiln instance associated with specified Kiln account.</returns>
        public static Kiln Authenticate(string server, string token) {
            // Save the token in the safe place.
            SecureString secureToken;
            SecureStringHelper.InitializeSecureString(token, out secureToken);
            
            // Delegate responsibility to create an instance
            // to more secure version of static factory.
            return Authenticate(server, secureToken);
        }

        /// <summary>
        /// Authenticate the user with specified credentials.
        /// </summary>
        /// <param name="server">The server address where the Kiln is hosted.</param>
        /// <param name="token">Authentication token issued by Kiln.</param>
        /// <returns>Kiln instance associated with specified Kiln account.</returns>
        public static Kiln Authenticate(string server, SecureString token) {
            return new Kiln(GetServerUrl(server), null, token);
        }

        /// <summary>
        /// Authenticate On Demand user with specified credentials.
        /// </summary>
        /// <param name="account">Kiln On Demand account name.</param>
        /// <param name="token">Authentication token issued by Kiln.</param>
        /// <returns>Kiln instance associated with specified Kiln account.</returns>
        public static Kiln AuthenticateOnDemand(string account, string token) {
            // Save the token in the safe place.
            SecureString secureToken;
            SecureStringHelper.InitializeSecureString(token, out secureToken);
            
            // Delegate responsibility to create an instance
            // to more secure version of static factory.
            return AuthenticateOnDemand(account, secureToken);
        }

        /// <summary>
        /// Authenticate On Demand user with specified credentials.
        /// </summary>
        /// <param name="account">Kiln On Demand account name.</param>
        /// <param name="token">Authentication token issued by Kiln.</param>
        /// <returns>Kiln instance associated with specified Kiln account.</returns>
        public static Kiln AuthenticateOnDemand(string account, SecureString token) {
            return new Kiln(GetOnDemandUrl(account), null, token);
        }

        /// <summary>
        /// Authenticate the user with specified credentials.
        /// </summary>
        /// <param name="server">The server address where the Kiln is hosted.</param>
        /// <param name="user">Username of email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>Kiln instance associated with specified Kiln account.</returns>
        public static Kiln Authenticate(string server, string user, string password) {
            // Save the password in the safe place.
            SecureString securePassword;
            SecureStringHelper.InitializeSecureString(password, out securePassword);

            // Delegate responsibility to create an instance
            // to more secure version of static factory.
            return Authenticate(server, user, securePassword);
        }

        /// <summary>
        /// Authenticate the user with specified credentials.
        /// The return value indicates whether the authentication succeeded.
        /// </summary>
        /// <param name="server">The server address where the Kiln is hosted.</param>
        /// <param name="user">Username of email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="kiln">
        /// An instance of Kiln, that will be initialized if authentication succeeded. Otherwise - null.
        /// </param>
        public static bool TryAuthenticate(string server, string user, string password, out Kiln kiln) {
            // Save the password in the safe place.
            SecureString securePassword;
            SecureStringHelper.InitializeSecureString(password, out securePassword);

            // Delegate responsibility to create an instance
            // to more secure version of static factory.
            return TryAuthenticate(server, user, securePassword, out kiln);
        }

        /// <summary>
        /// Authenticate the user with specified credentials.
        /// </summary>
        /// <param name="server">The server address where the Kiln is hosted.</param>
        /// <param name="user">Username of email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>Kiln instance associated with specified Kiln account.</returns>
        public static Kiln Authenticate(string server, string user, SecureString password) {
            return AuthenticateInternal(GetServerUrl(server), user, password);
        }

        /// <summary>
        /// Authenticate the user with specified credentials.
        /// The return value indicates whether the authentication succeeded.
        /// </summary>
        /// <param name="server">The server address where the Kiln is hosted.</param>
        /// <param name="user">Username of email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="kiln">
        /// An instance of Kiln, that will be initialized if authentication succeeded. Otherwise - null.
        /// </param>
        public static bool TryAuthenticate(string server, string user, SecureString password, out Kiln kiln) {
            kiln = null;
            try {
                kiln = AuthenticateInternal(GetServerUrl(server), user, password);
            }
            catch {
                // Ignore any exceptions.
            }
            return kiln != null;
        }

        /// <summary>
        /// Authenticate On Demand user with specified credentials.
        /// </summary>
        /// <param name="account">Kiln On Demand account name.</param>
        /// <param name="user">Username of email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        public static Kiln AuthenticateOnDemand(string account, string user, string password) {
            // Save the password in the safe place.
            SecureString securePassword;
            SecureStringHelper.InitializeSecureString(password, out securePassword);

            // Delegate responsibility to create an instance
            // to more secure version of static factory.
            return AuthenticateOnDemand(account, user, securePassword);
        }

        /// <summary>
        /// Authenticate On Demand user with specified credentials.
        /// The return value indicates whether the authentication succeeded.
        /// </summary>
        /// <param name="account">Kiln On Demand account name.</param>
        /// <param name="user">Username of email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="kiln">
        /// An instance of Kiln, that will be initialized if authentication succeeded. Otherwise - null.
        /// </param>
        public static bool TryAuthenticateOnDemand(string account, string user, string password, out Kiln kiln) {
            // Save the password in the safe place.
            SecureString securePassword;
            SecureStringHelper.InitializeSecureString(password, out securePassword);

            // Delegate responsibility to create an instance
            // to more secure version of static factory.
            return TryAuthenticateOnDemand(account, user, securePassword, out kiln);
        }

        /// <summary>
        /// Authenticate On Demand user with specified credentials.
        /// </summary>
        /// <param name="account">Kiln On Demand account name.</param>
        /// <param name="user">Username of email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        public static Kiln AuthenticateOnDemand(string account, string user, SecureString password) {
            return AuthenticateInternal(GetOnDemandUrl(account), user, password);
        }

        /// <summary>
        /// Authenticate On Demand user with specified credentials.
        /// The return value indicates whether the authentication succeeded.
        /// </summary>
        /// <param name="account">Kiln On Demand account name.</param>
        /// <param name="user">Username of email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="kiln">
        /// An instance of Kiln, that will be initialized if authentication succeeded. Otherwise - null.
        /// </param>
        public static bool TryAuthenticateOnDemand(string account, string user, SecureString password, out Kiln kiln) {
            kiln = null;
            try {
                kiln = AuthenticateInternal(GetOnDemandUrl(account), user, password);
            }
            catch {
                // Ignore any exceptions.
            }
            return kiln != null;
        }

        /// <summary>
        /// Internal version of Authenticate method. Authenticates an user
        /// with specified credentials using pre-defined base URL.
        /// </summary>
        private static Kiln AuthenticateInternal(string baseUrl, string user, SecureString password) {
            var authResponse = PerformRequest(baseUrl +
                KilnApiCall.AuthLogin.GetPathAndQuery( new { sUser = user, sPassword = password.Decrypt() }),
                KilnApiCall.AuthLogin.HttpMethod);

            // Retrieve authentication token from response.
            string authToken = authResponse.Trim('"');

            // Validate authentication token format.
            if (!Regex.IsMatch(authToken, "^[A-Za-z0-9]{30}$")) {
                return null;
            }

            // Save the token in SecureString.
            SecureString secureAuthToken;
            SecureStringHelper.InitializeSecureString(authToken, out secureAuthToken);

            return new Kiln(baseUrl, user, secureAuthToken);
        }
        #endregion

        #region URL Stuff
        /// <summary>
        /// Base URL of the server where Kiln is hosted.
        /// </summary>
        private readonly string m_baseUrl;

        /// <summary>
        /// Represents the common URL part with the gap
        /// for a server address where Kiln is hosted.
        /// </summary>
        private const string SERVER_URL = "https://{0}/";

        /// <summary>
        /// Represents the common URL part for all
        /// On Demand accounts with the gap for an account name.
        /// </summary>
        private const string ON_DEMAND_ACCOUNT_URL = "https://{0}.kilnhg.com/";

        /// <summary>
        /// Returns the base URL for specified On Demand account.
        /// </summary>
        private static string GetOnDemandUrl(string account) {
            return string.Format(ON_DEMAND_ACCOUNT_URL, account);
        }

        /// <summary>
        /// Returns base URL for specified server address.
        /// </summary>
        private static string GetServerUrl(string server) {
			if (!server.StartsWith("http")) {
				return string.Format(SERVER_URL, server);
			}
			return server;
        }
        #endregion

        /// <summary>
        /// Perform HTTP request with specified parameters.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="method">HTTP method.</param>
        private static string PerformRequest(string url, string method) {
            // Initialize WebRequest with specified parameters.
            WebRequest request = WebRequest.Create(url);
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
			ServicePointManager.ServerCertificateValidationCallback =
				new System.Net.Security.RemoteCertificateValidationCallback(
					(a, b, c, d) => true);
			//ServicePointManager.ServerCertificateValidationCallback = 
			//    new System.Net.Security.RemoteCertificateValidationCallback(
			//        (a, b, c, d) => {
			//            return true;
			//});
			request.Method = method;

            // If the method of request is POST, we need to write some data into request stream,
            // or if we have nothing to write, at least we have to set length of request stream.
            if (string.Equals(request.Method, "POST", StringComparison.InvariantCultureIgnoreCase) &&
                request.ContentLength == -1) {
                request.ContentLength = 0;
            }

            // Perform request and read response message.
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            return reader.ReadToEnd();
        }

        /// <summary>
        /// Deserializes an object of specified type from JSON formatted Stream.
        /// </summary>
        private static T DeserializeJson<T>(Stream jsonStream) {
            DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(T));
            return (T)jsonSer.ReadObject(jsonStream);
        }

        /// <summary>
        /// Deserializes an object of specified type from JSON formatted String.
        /// </summary>
        private static T DeserializeJson<T>(string jsonString) {
            MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return DeserializeJson<T>(memStream);
        }

        /// <summary>
        /// Deserializes an object of specified type from JSON formatted String.
        /// A return value indicates whether the deserialization succeeded.
        /// </summary>
        private static bool TryDeserializeJson<T>(string jsonString, out T result) {
            bool success = true;
            result = default(T);

            try {
                result = DeserializeJson<T>(jsonString);
            }
            catch {
                success = false;
            }
            return success;
        }
    }
}
