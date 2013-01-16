using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using MyCorp.ExtendedAdapterMembershipProviders.EntityClasses;
using MyCorp.ExtendedAdapterMembershipProviders.FactoryClasses;
using MyCorp.ExtendedAdapterMembershipProviders.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using WebMatrix.WebData;

namespace MyCorp.ExtendedAdapterMembershipProviders.Providers
{
    public class ExtendedAdapterMembershipProvider: ExtendedMembershipProvider
    {
        private const int TokenSizeInBytes = 0x10;

        #region Constructor(s)
        private IDataAccessAdapterFactory adapterFactory;

        public ExtendedAdapterMembershipProvider(): this(new DataAccessAdapterFactory())
        {
        }

        public ExtendedAdapterMembershipProvider(IDataAccessAdapterFactory dataAccessAdapterFactory)
        {
            this.adapterFactory = dataAccessAdapterFactory;
        }
        #endregion

        #region Initialize
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (string.IsNullOrEmpty(name))
            {
                name = "ExtendedAdapterMembershipProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Adapter Extended Membership Provider");
            }
            base.Initialize(name, config);

            ApplicationName = GetValueOrDefault(config, "applicationName", o => o.ToString(), "MySampleApp");

            this.ConnectionStringName = GetValueOrDefault(config, "connectionStringName", o => o.ToString(), string.Empty);
            adapterFactory.Initialize(ConnectionStringName);

            this.EnablePasswordRetrievalInternal = GetValueOrDefault(config, "enablePasswordRetrieval", Convert.ToBoolean, false);
            this.EnablePasswordResetInternal = GetValueOrDefault(config, "enablePasswordReset", Convert.ToBoolean, true);
            this.RequiresQuestionAndAnswerInternal = GetValueOrDefault(config, "requiresQuestionAndAnswer", Convert.ToBoolean, false);
            this.RequiresUniqueEmailInternal = GetValueOrDefault(config, "requiresUniqueEmail", Convert.ToBoolean, true);
            this.MaxInvalidPasswordAttemptsInternal = GetValueOrDefault(config, "maxInvalidPasswordAttempts", Convert.ToInt32, 3);
            this.PasswordAttemptWindowInternal = GetValueOrDefault(config, "passwordAttemptWindow", Convert.ToInt32, 10);
            this.PasswordFormatInternal = GetValueOrDefault(config, "passwordFormat", o =>
                {
                    MembershipPasswordFormat format;
                    return Enum.TryParse(o.ToString(), true, out format) ? format : MembershipPasswordFormat.Hashed;
                }, MembershipPasswordFormat.Hashed);
            this.MinRequiredPasswordLengthInternal = GetValueOrDefault(config, "minRequiredPasswordLength", Convert.ToInt32, 6);
            this.MinRequiredNonAlphanumericCharactersInternal = GetValueOrDefault(config, "minRequiredNonalphanumericCharacters",
                                                                          Convert.ToInt32, 1);
            this.PasswordStrengthRegularExpressionInternal = GetValueOrDefault(config, "passwordStrengthRegularExpression",
                                                                       o => o.ToString(), string.Empty);
            this.HashAlgorithmType = GetValueOrDefault(config, "hashAlgorithmType", o => o.ToString(), "SHA1");

            config.Remove("name");
            config.Remove("description");
            config.Remove("applicationName");
            config.Remove("connectionStringName");
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("passwordFormat");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            config.Remove("hashAlgorithmType");

            if (config.Count <= 0)
                return;
            var key = config.GetKey(0);
            if (string.IsNullOrEmpty(key))
                return;

            throw new ProviderException(string.Format(CultureInfo.CurrentCulture,
                                                      "The membership provider does not recognize the configuration attribute {0}.",
                                                      key));
        }

        public string ConnectionStringName { get; set; }
        public string HashAlgorithmType { get; set; }

        #endregion

        #region Abstract Property Overrides
        public override string ApplicationName { get; set; }

        public override bool EnablePasswordReset
        {
            get { return EnablePasswordResetInternal; }
        }
        private bool EnablePasswordResetInternal { get; set; }

        public override bool EnablePasswordRetrieval
        {
            get { return EnablePasswordRetrievalInternal; }
        }
        private bool EnablePasswordRetrievalInternal { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { return MaxInvalidPasswordAttemptsInternal; }
        }
        private int MaxInvalidPasswordAttemptsInternal { get; set; }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return MinRequiredNonAlphanumericCharactersInternal; }
        }
        private int MinRequiredNonAlphanumericCharactersInternal { get; set; }

        public override int MinRequiredPasswordLength
        {
            get { return MinRequiredPasswordLengthInternal; }
        }
        private int MinRequiredPasswordLengthInternal { get; set; }

        public override int PasswordAttemptWindow
        {
            get { return PasswordAttemptWindowInternal; }
        }
        private int PasswordAttemptWindowInternal { get; set; }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return PasswordFormatInternal; }
        }
        private MembershipPasswordFormat PasswordFormatInternal { get; set; }

        public override string PasswordStrengthRegularExpression
        {
            get { return PasswordStrengthRegularExpressionInternal; }
        }
        private string PasswordStrengthRegularExpressionInternal { get; set; }

        public override bool RequiresQuestionAndAnswer
        {
            get { return RequiresQuestionAndAnswerInternal; }
        }
        private bool RequiresQuestionAndAnswerInternal { get; set; }

        public override bool RequiresUniqueEmail
        {
            get { return RequiresUniqueEmailInternal; }
        }
        private bool RequiresUniqueEmailInternal { get; set; }
        #endregion

        #region Abstract Method Overrides
        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var memberships = new EntityCollection<WebpagesMembershipEntity>(new WebpagesMembershipEntityFactory());
                adapter.FetchEntityCollection(memberships, new RelationPredicateBucket(WebpagesMembershipFields.ConfirmationToken == accountConfirmationToken));

                var membership = memberships.Where(m => m.ConfirmationToken.Equals(accountConfirmationToken, StringComparison.Ordinal)).FirstOrDefault();
                if(membership == null) {
                    return false;
                }

                membership.IsConfirmed= true;
                return adapter.SaveEntity(membership);
            }
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] { userName }, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesMembership }).FirstOrDefault();
                if (user == null)
                    return false;
                if (user.WebpagesMembership == null)
                    return false;

                if (user.WebpagesMembership.ConfirmationToken.Equals(accountConfirmationToken, StringComparison.Ordinal))
                {
                    user.WebpagesMembership.IsConfirmed = true;
                    return adapter.SaveEntity(user.WebpagesMembership);
                }
                return false;
            }
        }

        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidPassword);
            }
            string hashedPassword = Crypto.HashPassword(password);
            if (hashedPassword.Length > 0x80)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidPassword);
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidUserName);
            }
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                return this.CreateAccount(adapter, userName, password, requireConfirmationToken);
            }
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                this.CreateUser(adapter, userName, values);
                return this.CreateAccount(adapter, userName, password, requireConfirmation);
            }
        }

        public override bool DeleteAccount(string userName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] { userName }).FirstOrDefault();
                if (user == null)
                    return false;

                return 1 == adapter.DeleteEntitiesDirectly(typeof(WebpagesMembershipEntity),
                    new RelationPredicateBucket(WebpagesMembershipFields.UserId == user.UserId));
            }
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be empty", "username");
            }
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                bool throwException = true;
                var userId = this.VerifyUserNameHasConfirmedAccount(adapter, userName, throwException);

                var user = new UserProfileEntity(userId);
                adapter.FetchEntity(user, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesMembership });

                var membership = user.WebpagesMembership;
                if (membership.PasswordVerificationTokenExpirationDate.HasValue && membership.PasswordVerificationTokenExpirationDate.Value > DateTime.UtcNow)
                {
                    return membership.PasswordVerificationToken;
                }
                var token = GenerateToken();
                membership.PasswordVerificationToken = token;
                membership.PasswordVerificationTokenExpirationDate = DateTime.UtcNow.AddMinutes((double)tokenExpirationInMinutesFromNow);

                if (!adapter.SaveEntity(membership))
                {
                    throw new ProviderException("Unable to generate password reset token");
                }

                return token;
            }
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] { userName }, new PrefetchPath2(EntityType.UserProfileEntity){ UserProfileEntity.PrefetchPathWebpagesOauthMemberships }).FirstOrDefault();
                if (user != null)
                {
                    var list = new List<OAuthAccountData>();
                    foreach (var oauth in user.WebpagesOauthMemberships)
                    {
                        list.Add(new OAuthAccountData(oauth.Provider, oauth.ProviderUserId));
                    }
                    return list;
                }
            }
            return new OAuthAccountData[0];
        }

        public override DateTime GetCreateDate(string userName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] { userName }, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesMembership }).FirstOrDefault();
                if (user != null && user.WebpagesMembership != null && user.WebpagesMembership.CreateDate.HasValue)
                {
                    return user.WebpagesMembership.CreateDate.Value;
                }
            }
            return DateTime.MinValue;
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] { userName }, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesMembership }).FirstOrDefault();
                if (user == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "User {0} does not exist!", userName));
                }
                if (user.WebpagesMembership != null && user.WebpagesMembership.LastPasswordFailureDate.HasValue)
                {
                    return user.WebpagesMembership.LastPasswordFailureDate.Value;
                }
            }
            return DateTime.MinValue;
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] { userName }, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesMembership }).FirstOrDefault();
                if (user == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "User {0} does not exist!", userName));
                }
                if (user.WebpagesMembership != null && user.WebpagesMembership.PasswordChangedDate.HasValue)
                {
                    return user.WebpagesMembership.PasswordChangedDate.Value;
                }
            }
            return DateTime.MinValue;
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[]{userName}).FirstOrDefault();
                if (user == null)
                {
                    throw new InvalidOperationException(string.Format("User {0} does not exist!", userName));
                }
                return GetPasswordFailuresSinceLastSuccess(adapter, user.UserId);
            }
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var users = new EntityCollection<WebpagesMembershipEntity>(new WebpagesMembershipEntityFactory());
                var bucket = new RelationPredicateBucket(WebpagesMembershipFields.PasswordVerificationToken == token);
                adapter.FetchEntityCollection(users, bucket);
                if(users.Count == 0)
                    return -1;
                return users.First().UserId;
            }
        }

        public override bool IsConfirmed(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be empty", "username");
            }
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                bool throwException = false;
                return (this.VerifyUserNameHasConfirmedAccount(adapter, userName, throwException) != -1);
            }
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("NewPassword cannot be empty", "newPassword");
            }
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var memberships = new EntityCollection<WebpagesMembershipEntity>(new WebpagesMembershipEntityFactory());
                var bucket = new RelationPredicateBucket();
                bucket.PredicateExpression.AddWithAnd(WebpagesMembershipFields.PasswordVerificationToken == token);
                bucket.PredicateExpression.AddWithAnd(new FieldCompareValuePredicate(WebpagesMembershipFields.PasswordVerificationTokenExpirationDate, null, ComparisonOperator.GreaterThan, DateTime.UtcNow));
                adapter.FetchEntityCollection(memberships, bucket);

                if (memberships.Count == 1)
                {
                    var membership = memberships[0];
                    var passwordSet = SetPassword(adapter, memberships[0].UserId, newPassword);
                    if (passwordSet)
                    {
                        membership.PasswordVerificationToken = null;
                        membership.PasswordVerificationTokenExpirationDate = null;
                        if (!adapter.SaveEntity(membership))
                            throw new ProviderException("Unable to reset password with token!");
                    }
                    return passwordSet;
                }

                return false;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be empty", "username");
            }
            if (string.IsNullOrEmpty(oldPassword))
            {
                throw new ArgumentException("OldPassword cannot be empty", "oldPassword");
            }
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("NewPassword cannot be empty", "newPassword");
            }
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[]{username}).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                if (!this.CheckPassword(adapter, user.UserId, oldPassword))
                {
                    return false;
                }
                return SetPassword(adapter, user.UserId, newPassword);
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] { username }).FirstOrDefault();
                if(user == null)
                {
                    return false;
                }

                if (deleteAllRelatedData)
                {
                    //TODO: delete some stuff here
                }

                var userId = user.UserId;
                adapter.DeleteEntitiesDirectly(typeof(WebpagesOauthMembershipEntity), new RelationPredicateBucket(WebpagesOauthMembershipFields.UserId == userId));
                adapter.DeleteEntitiesDirectly(typeof(WebpagesMembershipEntity), new RelationPredicateBucket(WebpagesMembershipFields.UserId == userId));
                return 1 == adapter.DeleteEntitiesDirectly(typeof(UserProfileEntity), new RelationPredicateBucket(UserProfileFields.UserId == userId));
            }
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] {username}).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                return new MembershipUser(Membership.Provider.Name, username, user.UserId, null, null, null, true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be empty", "username");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be empty", "password");
            }
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                bool throwException = false;
                int userId = this.VerifyUserNameHasConfirmedAccount(adapter, username, throwException);
                if (userId == -1)
                {
                    return false;
                }
                return this.CheckPassword(adapter, userId, password);
            }
        }
        #endregion

        #region Required ExtendedMembershipProvider Overrides
        public override string CreateAccount(string userName, string password)
        {
            // let the base class handle this one
            return base.CreateAccount(userName, password);
        }
        public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be empty", "username");
            }
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = this.GetUsers(adapter, new[] { userName }, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesOauthMemberships }).FirstOrDefault();
                if (user == null)
                {
                    user = new UserProfileEntity { UserName = userName };
                    adapter.SaveEntity(user);
                    user = this.GetUsers(adapter, new[] { userName }, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesOauthMemberships }).FirstOrDefault();
                    //throw new MembershipCreateUserException(MembershipCreateStatus.InvalidUserName);
                }
                var oAuth = user.WebpagesOauthMemberships.FirstOrDefault(o => 
                    o.Provider.Equals(provider, StringComparison.OrdinalIgnoreCase) && 
                    o.ProviderUserId.Equals(providerUserId, StringComparison.OrdinalIgnoreCase));
                if (oAuth == null)
                {
                    oAuth = new WebpagesOauthMembershipEntity { ProviderUserId = providerUserId, Provider = provider, UserId = user.UserId };
                    if(!adapter.SaveEntity(oAuth))
                    {
                        throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
                    }
                }
                else
                {
                    oAuth.UserId = user.UserId;
                    if(!adapter.SaveEntity(oAuth))
                    {
                        throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
                    }
                }
            }
        }
        public override string CreateUserAndAccount(string userName, string password)
        {
            // let the base class handle this one
            return base.CreateUserAndAccount(userName, password);
        }
        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation)
        {
            // let the base class handle this one
            return base.CreateUserAndAccount(userName, password, requireConfirmation);
        }
        public override string CreateUserAndAccount(string userName, string password, IDictionary<string, object> values)
        {
            // let the base class handle this one
            return base.CreateUserAndAccount(userName, password, values);
        }

        public override void DeleteOAuthAccount(string provider, string providerUserId)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var filter = new RelationPredicateBucket();
                var compare1 = new FieldCompareValuePredicate(WebpagesOauthMembershipFields.Provider, null, ComparisonOperator.Equal, provider);
                compare1.CaseSensitiveCollation = true; // makes it case-insensitive
                var compare2 = new FieldCompareValuePredicate(WebpagesOauthMembershipFields.ProviderUserId, null, ComparisonOperator.Equal, providerUserId);
                compare2.CaseSensitiveCollation = true; // makes it case-insensitive
                filter.PredicateExpression.AddWithAnd(compare1);
                filter.PredicateExpression.AddWithAnd(compare2);
                if(1 != adapter.DeleteEntitiesDirectly(typeof(WebpagesOauthMembershipEntity), filter))
                {
                    throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
                }
            }
        }

        public override void DeleteOAuthToken(string token)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                adapter.DeleteEntitiesDirectly(typeof(WebpagesOauthTokenEntity), new RelationPredicateBucket(WebpagesOauthTokenFields.Token == token));
            }
        }

        public override string GeneratePasswordResetToken(string userName)
        {
            // let the base class handle this one
            return base.GeneratePasswordResetToken(userName);
        }
        public override string GetOAuthTokenSecret(string token)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var oauthToken = new WebpagesOauthTokenEntity(token);
                if (adapter.FetchEntity(oauthToken))
                    return oauthToken.Secret;
                return null;
            }
        }
        public override int GetUserIdFromOAuth(string provider, string providerUserId)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var filter = new RelationPredicateBucket();
                var compare1 = new FieldCompareValuePredicate(WebpagesOauthMembershipFields.Provider, null, ComparisonOperator.Equal, provider);
                compare1.CaseSensitiveCollation = true; // makes it case-insensitive
                var compare2 = new FieldCompareValuePredicate(WebpagesOauthMembershipFields.ProviderUserId, null, ComparisonOperator.Equal, providerUserId);
                compare2.CaseSensitiveCollation = true; // makes it case-insensitive
                filter.PredicateExpression.AddWithAnd(compare1);
                filter.PredicateExpression.AddWithAnd(compare2);
                var oauthMemberships = new EntityCollection<WebpagesOauthMembershipEntity>(new WebpagesOauthMembershipEntityFactory());
                adapter.FetchEntityCollection(oauthMemberships, filter);
                if(oauthMemberships.Count != 1)
                    return -1;
                return oauthMemberships.Single().UserId;
            }
        }
        public override string GetUserNameFromId(int userId)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var userProfile = new UserProfileEntity(userId);
                if (adapter.FetchEntity(userProfile))
                    return userProfile.UserName;
                return null;
            }
        }
        public override bool HasLocalAccount(int userId)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var userProfile = new UserProfileEntity(userId);
                if (adapter.FetchEntity(userProfile, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesMembership }))
                    return userProfile.WebpagesMembership != null;
                return false;
            }
        }
        public override void ReplaceOAuthRequestTokenWithAccessToken(string requestToken, string accessToken, string accessTokenSecret)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                adapter.DeleteEntitiesDirectly(typeof(WebpagesOauthTokenEntity), new RelationPredicateBucket(WebpagesOauthTokenFields.Token == requestToken));
                this.StoreOAuthRequestToken(accessToken, accessTokenSecret);
            }
        }
        public override void StoreOAuthRequestToken(string requestToken, string requestTokenSecret)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var tokenEntity = new WebpagesOauthTokenEntity(requestToken);
                if (adapter.FetchEntity(tokenEntity) && tokenEntity.Secret == requestTokenSecret)
                {
                    return;
                }

                tokenEntity.Secret = requestTokenSecret;
                if (!adapter.SaveEntity(tokenEntity))
                {
                    throw new ProviderException("Unable to store OAuth token");
                }
                return;
            }
        }
        #endregion

        #region Helper Methods

        private EntityCollection<UserProfileEntity> GetUsers(IDataAccessAdapter adapter, string[] usernames, PrefetchPath2 prefetchPath = null)
        {
            var users = new EntityCollection<UserProfileEntity>(new UserProfileEntityFactory());
            adapter.FetchEntityCollection(users, new RelationPredicateBucket(UserProfileFields.UserName == usernames), prefetchPath);
            return users;
        }

        private void CreateUser(IDataAccessAdapter adapter, string userName, IDictionary<string, object> values)
        {
            if (GetUsers(adapter, new[] { userName }) != null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
            }

            var user = new UserProfileEntity { UserName = userName };
            foreach (var value in values)
            {
                if (value.Key.Equals("username", StringComparison.OrdinalIgnoreCase))
                    continue;

                var field = user.Fields.SingleOrDefault(f => f.Name.Equals(value.Key, StringComparison.OrdinalIgnoreCase));
                if (field != null)
                {
                    user.Fields[field.FieldIndex].CurrentValue = value.Value;
                }
            }
            if (!adapter.SaveEntity(user))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
            }
        }

        private string CreateAccount(IDataAccessAdapter adapter, string userName, string password, bool requireConfirmationToken)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidPassword);
            }
            string hashedPassword = Crypto.HashPassword(password);
            if (hashedPassword.Length > 0x80)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidPassword);
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidUserName);
            }

            var user = GetUsers(adapter, new[] { userName }, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathWebpagesMembership }).FirstOrDefault();
            if (user == null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
            }
            if (user.WebpagesMembership != null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
            }
            string token = null;
            if (requireConfirmationToken)
            {
                token = GenerateToken();
            }

            var membership = new WebpagesMembershipEntity
            {
                UserId = user.UserId,
                Password = hashedPassword,
                PasswordSalt = string.Empty,
                IsConfirmed = !requireConfirmationToken,
                ConfirmationToken = token,
                CreateDate = DateTime.UtcNow,
                PasswordChangedDate = DateTime.UtcNow,
                PasswordFailuresSinceLastSuccess = 0
            };

            if (!adapter.SaveEntity(membership))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
            }
            return token;
        }

        private bool SetPassword(IDataAccessAdapter adapter,int userId,string newPassword)
        {            
            var hashedPassword = Crypto.HashPassword(newPassword);
            if (hashedPassword.Length > 0x80)
            {
                throw new ArgumentException("Password is too long!");
            }

            var membership = new WebpagesMembershipEntity {
                Password = hashedPassword,
                PasswordSalt = string.Empty,
                PasswordChangedDate = DateTime.UtcNow
            };

            return adapter.UpdateEntitiesDirectly(membership, new RelationPredicateBucket(WebpagesMembershipFields.UserId == userId)) > 0;
        }

        private bool CheckPassword(IDataAccessAdapter adapter,int userId,string password)
        {
            var hashedPassword = this.GetHashedPassword(adapter, userId);
            var matches = (hashedPassword != null) && Crypto.VerifyHashedPassword(hashedPassword, password);
            if (matches)
            {
                var membership = new WebpagesMembershipEntity {
                    PasswordFailuresSinceLastSuccess = 0
                };
                adapter.UpdateEntitiesDirectly(membership, new RelationPredicateBucket(WebpagesMembershipFields.UserId == userId));
                return matches;
            }
            int passwordFailuresSinceLastSuccess = GetPasswordFailuresSinceLastSuccess(adapter, userId);
            if (passwordFailuresSinceLastSuccess != -1)
            {
                var membership = new WebpagesMembershipEntity {
                    PasswordFailuresSinceLastSuccess = passwordFailuresSinceLastSuccess + 1,
                    LastPasswordFailureDate = DateTime.UtcNow
                };
                adapter.UpdateEntitiesDirectly(membership, new RelationPredicateBucket(WebpagesMembershipFields.UserId == userId));
            }
            return matches;
        }

        private int GetPasswordFailuresSinceLastSuccess(IDataAccessAdapter adapter,int userId)
        {
            var membership = new WebpagesMembershipEntity(userId);
            if(!adapter.FetchEntity(membership))
                return -1;
            return membership.PasswordFailuresSinceLastSuccess;
        }

        private string GetHashedPassword(IDataAccessAdapter adapter, int userId)
        {
            var membership = new WebpagesMembershipEntity(userId);
            if(!adapter.FetchEntity(membership))
                return null;
            return membership.Password;
        }

        private static T GetValueOrDefault<T>(NameValueCollection nvc, string key, Func<object, T> converter, T defaultIfNull)
        {
            var val = nvc[key];

            if (val == null)
                return defaultIfNull;

            try
            {
                return converter(val);
            }
            catch
            {
                return defaultIfNull;
            }
        }

        private string GenerateToken()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                return GenerateToken(provider);
            }
        }

        internal static string GenerateToken(RandomNumberGenerator generator)
        {
            var data = new byte[0x10];
            generator.GetBytes(data);
            return HttpServerUtility.UrlTokenEncode(data);
        }

        private int VerifyUserNameHasConfirmedAccount(IDataAccessAdapter adapter, string username, bool throwException)
        {
            var user = GetUsers(adapter, new[] { username }).FirstOrDefault();
            if (user == null)
            {
                if (throwException)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "User {0} does not exist!", new object[] { username }));
                }
                return -1;
            }

            var bucket = new RelationPredicateBucket(WebpagesMembershipFields.UserId == user.UserId);
            bucket.PredicateExpression.AddWithAnd(WebpagesMembershipFields.IsConfirmed == true);
            var count = adapter.GetDbCount(new EntityCollection<WebpagesMembershipEntity>(new WebpagesMembershipEntityFactory()),
                bucket);
            if (count != 0)
            {
                return user.UserId;
            }
            if (throwException)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "User {0} does not exist!", new object[] { username }));
            }
            return -1;
        }
        #endregion

        #region Unsupported methods in the SimpleMembershipProvider model

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            throw new NotSupportedException();
        }
      
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
