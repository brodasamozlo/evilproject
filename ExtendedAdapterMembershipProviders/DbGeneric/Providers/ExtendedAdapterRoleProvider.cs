using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using MyCorp.ExtendedAdapterMembershipProviders.EntityClasses;
using MyCorp.ExtendedAdapterMembershipProviders.FactoryClasses;
using MyCorp.ExtendedAdapterMembershipProviders.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace MyCorp.ExtendedAdapterMembershipProviders.Providers
{
    public partial class ExtendedAdapterRoleProvider: RoleProvider
    {
        #region Constructor(s)
        private IDataAccessAdapterFactory adapterFactory;

        public ExtendedAdapterRoleProvider(): this(new DataAccessAdapterFactory())
        {
        }

        public ExtendedAdapterRoleProvider(IDataAccessAdapterFactory dataAccessAdapterFactory)
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
                name = "ExtendedAdapterRoleProvider";
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Adapter Extended Role Provider");
            }
            base.Initialize(name, config);

            ApplicationName = GetValueOrDefault(config, "applicationName", o => o.ToString(), "MySampleApp");

            this.ConnectionStringName = GetValueOrDefault(config, "connectionStringName", o => o.ToString(), string.Empty);
            adapterFactory.Initialize(ConnectionStringName);

            config.Remove("name");
            config.Remove("description");
            config.Remove("applicationName");
            config.Remove("connectionStringName");

            if (config.Count <= 0)
                return;
            var key = config.GetKey(0);
            if (string.IsNullOrEmpty(key))
                return;

            throw new ProviderException(string.Format(CultureInfo.CurrentCulture,
                                                      "The role provider does not recognize the configuration attribute {0}.",
                                                      key));
        }

        public string ConnectionStringName { get; set; }
        #endregion

        #region Abstract Property Overrides
        public override string ApplicationName { get; set; }
        #endregion

        #region Abstract Method Overrides
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var users = GetUsers(adapter, usernames);
                var roles = GetRoles(adapter, roleNames);

                var userIds = users.Select(u => u.UserId).ToArray();
                var roleIds = roles.Select(r => r.RoleId).ToArray();

                var bucket = new RelationPredicateBucket();
                bucket.PredicateExpression.Add(WebpagesUsersInRoleFields.UserId == userIds);
                bucket.PredicateExpression.AddWithAnd(WebpagesUsersInRoleFields.RoleId == roleIds);
                var userroles = new EntityCollection<WebpagesUsersInRoleEntity>(new WebpagesUsersInRoleEntityFactory());
                adapter.FetchEntityCollection(userroles, bucket);

                var userroleIds = userroles.Select(u => new { UserId = u.UserId, RoleId = u.RoleId }).ToArray();
                foreach (var userId in userIds)
                {
                    foreach (var roleId in roleIds)
                    {
                        if (!userroleIds.Any(ur => ur.UserId == userId && ur.RoleId == roleId))
                            userroles.Add(new WebpagesUsersInRoleEntity { UserId = userId, RoleId = roleId });
                    }
                }
                adapter.SaveEntityCollection(userroles);
            }
        }

        public override void CreateRole(string roleName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var role = new WebpagesRoleEntity { RoleName = roleName };
                if (adapter.FetchEntityUsingUniqueConstraint(role, role.ConstructFilterForUCRoleName()))
                    throw new ProviderException(string.Format("Role {0} already exists!", roleName));
                adapter.SaveEntity(role);
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var role = GetRoles(adapter, new[] { roleName }).FirstOrDefault();
                if (role == null)
                    throw new ProviderException(string.Format("Role {0} does not exist!", roleName));

                var bucket = new RelationPredicateBucket(WebpagesUsersInRoleFields.RoleId == role.RoleId);

                if (throwOnPopulatedRole &&
                    adapter.GetDbCount(new EntityCollection<WebpagesUsersInRoleEntity>(new WebpagesUsersInRoleEntityFactory()), bucket) > 0)
                    throw new ProviderException(string.Format("Role {0} is not empty!", roleName));

                adapter.DeleteEntitiesDirectly(typeof(WebpagesUsersInRoleEntity), bucket);
                return adapter.DeleteEntity(role);
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var users = new EntityCollection<UserProfileEntity>(new UserProfileEntityFactory());

                var prefetchPath = new PrefetchPath2(EntityType.UserProfileEntity);
                prefetchPath.Add(UserProfileEntity.PrefetchPathRoleMemberships);

                var filter = new RelationPredicateBucket();
                filter.PredicateExpression.AddWithAnd(new FieldLikePredicate(UserProfileFields.UserName, null, usernameToMatch));
                filter.PredicateExpression.AddWithAnd(WebpagesRoleFields.RoleName == roleName);

                adapter.FetchEntityCollection(users, filter, prefetchPath);

                return users.Select(u => u.UserName).OrderBy(u => u).ToArray();
            }
        }

        public override string[] GetAllRoles()
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var roles = new EntityCollection<WebpagesRoleEntity>(new WebpagesRoleEntityFactory());
                adapter.FetchEntityCollection(roles, null);
                return roles.Select(r => r.RoleName).OrderBy(r => r).ToArray();
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = new UserProfileEntity { UserName = username };
                if (!adapter.FetchEntity(user, new PrefetchPath2(EntityType.UserProfileEntity) { UserProfileEntity.PrefetchPathRoleMemberships }))
                    throw new ProviderException(string.Format("User {0} does not exist!", username));
                    
                return user.RoleMemberships.Select(r => r.RoleName).OrderBy(r => r).ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var role = new WebpagesRoleEntity { RoleName = roleName };

                var prefetchPath = new PrefetchPath2(EntityType.WebpagesRoleEntity);
                prefetchPath.Add(WebpagesRoleEntity.PrefetchPathUserMembers);

                if (!adapter.FetchEntityUsingUniqueConstraint(role, role.ConstructFilterForUCRoleName(), prefetchPath))
                    throw new ProviderException(string.Format("Role {0} does not exist!", roleName));

                return role.UserMembers.Select(u => u.UserName).OrderBy(u => u).ToArray();
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var user = GetUsers(adapter, new[] { username }).FirstOrDefault();
                var role = GetRoles(adapter, new[] { roleName }).FirstOrDefault();

                if (user == null)
                    throw new ProviderException(string.Format("User {0} does not exist!", username));
                if (role == null)
                    throw new ProviderException(string.Format("Role {0} does not exist!", roleName));

                var bucket = new RelationPredicateBucket();
                bucket.PredicateExpression.AddWithAnd(WebpagesUsersInRoleFields.UserId == user.UserId);
                bucket.PredicateExpression.AddWithAnd(WebpagesUsersInRoleFields.RoleId == role.RoleId);

                return adapter.GetDbCount(new EntityCollection<WebpagesUsersInRoleEntity>(new WebpagesUsersInRoleEntityFactory()),
                    bucket) > 0;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                var users = GetUsers(adapter, usernames);
                var roles = GetRoles(adapter, roleNames);

                var bucket = new RelationPredicateBucket();
                bucket.PredicateExpression.AddWithAnd(WebpagesUsersInRoleFields.UserId == users.Select(u => u.UserId).ToArray());
                bucket.PredicateExpression.AddWithAnd(WebpagesUsersInRoleFields.RoleId == roles.Select(u => u.RoleId).ToArray());
                adapter.DeleteEntitiesDirectly(typeof(WebpagesUsersInRoleEntity), bucket);
            }
        }

        public override bool RoleExists(string roleName)
        {
            using (var adapter = adapterFactory.CreateDataAccessAdapter())
            {
                return adapter.GetDbCount(new EntityCollection<WebpagesUsersInRoleEntity>(new WebpagesUsersInRoleEntityFactory()),
                    new RelationPredicateBucket(WebpagesRoleFields.RoleName == roleName)) > 0;
            }
        }
        #endregion

        #region Helper Methods
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

        private EntityCollection<UserProfileEntity> GetUsers(IDataAccessAdapter adapter, string[] usernames, PrefetchPath2 prefetchPath = null)
        {
            var users = new EntityCollection<UserProfileEntity>(new UserProfileEntityFactory());
            adapter.FetchEntityCollection(users, new RelationPredicateBucket(UserProfileFields.UserName == usernames), prefetchPath);
            return users;
        }

        private EntityCollection<WebpagesRoleEntity> GetRoles(IDataAccessAdapter adapter, string[] rolenames, PrefetchPath2 prefetchPath = null)
        {
            var roles = new EntityCollection<WebpagesRoleEntity>(new WebpagesRoleEntityFactory());
            adapter.FetchEntityCollection(roles, new RelationPredicateBucket(WebpagesRoleFields.RoleName == rolenames), prefetchPath);
            return roles;
        }
        #endregion
    }
}
