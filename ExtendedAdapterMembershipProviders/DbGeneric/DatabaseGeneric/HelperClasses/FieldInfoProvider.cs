///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 3.5
// Code is generated on: Friday, November 09, 2012 11:04:35 PM
// Code is generated using templates: SD.TemplateBindings.SharedTemplates.NET20
// Templates vendor: Solutions Design.
// Templates version: 
//////////////////////////////////////////////////////////////
using System;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace MyCorp.ExtendedAdapterMembershipProviders.HelperClasses
{
	
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	
	/// <summary>Singleton implementation of the FieldInfoProvider. This class is the singleton wrapper through which the actual instance is retrieved.</summary>
	/// <remarks>It uses a single instance of an internal class. The access isn't marked with locks as the FieldInfoProviderBase class is threadsafe.</remarks>
	internal static class FieldInfoProviderSingleton
	{
		#region Class Member Declarations
		private static readonly IFieldInfoProvider _providerInstance = new FieldInfoProviderCore();
		#endregion

		/// <summary>Dummy static constructor to make sure threadsafe initialization is performed.</summary>
		static FieldInfoProviderSingleton()
		{
		}

		/// <summary>Gets the singleton instance of the FieldInfoProviderCore</summary>
		/// <returns>Instance of the FieldInfoProvider.</returns>
		public static IFieldInfoProvider GetInstance()
		{
			return _providerInstance;
		}
	}

	/// <summary>Actual implementation of the FieldInfoProvider. Used by singleton wrapper.</summary>
	internal class FieldInfoProviderCore : FieldInfoProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="FieldInfoProviderCore"/> class.</summary>
		internal FieldInfoProviderCore()
		{
			Init();
		}

		/// <summary>Method which initializes the internal datastores.</summary>
		private void Init()
		{
			this.InitClass( (6 + 0));
			InitUserProfileEntityInfos();
			InitWebpagesMembershipEntityInfos();
			InitWebpagesOauthMembershipEntityInfos();
			InitWebpagesOauthTokenEntityInfos();
			InitWebpagesRoleEntityInfos();
			InitWebpagesUsersInRoleEntityInfos();

			this.ConstructElementFieldStructures(InheritanceInfoProviderSingleton.GetInstance());
		}

		/// <summary>Inits UserProfileEntity's FieldInfo objects</summary>
		private void InitUserProfileEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(UserProfileFieldIndex), "UserProfileEntity");
			this.AddElementFieldInfo("UserProfileEntity", "UserId", typeof(System.Int32), true, false, true, false,  (int)UserProfileFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("UserProfileEntity", "UserName", typeof(System.String), false, false, false, false,  (int)UserProfileFieldIndex.UserName, 56, 0, 0);
		}
		/// <summary>Inits WebpagesMembershipEntity's FieldInfo objects</summary>
		private void InitWebpagesMembershipEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(WebpagesMembershipFieldIndex), "WebpagesMembershipEntity");
			this.AddElementFieldInfo("WebpagesMembershipEntity", "ConfirmationToken", typeof(System.String), false, false, false, true,  (int)WebpagesMembershipFieldIndex.ConfirmationToken, 128, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "CreateDate", typeof(Nullable<System.DateTime>), false, false, false, true,  (int)WebpagesMembershipFieldIndex.CreateDate, 0, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "IsConfirmed", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)WebpagesMembershipFieldIndex.IsConfirmed, 0, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "LastPasswordFailureDate", typeof(Nullable<System.DateTime>), false, false, false, true,  (int)WebpagesMembershipFieldIndex.LastPasswordFailureDate, 0, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "Password", typeof(System.String), false, false, false, false,  (int)WebpagesMembershipFieldIndex.Password, 128, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "PasswordChangedDate", typeof(Nullable<System.DateTime>), false, false, false, true,  (int)WebpagesMembershipFieldIndex.PasswordChangedDate, 0, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "PasswordFailuresSinceLastSuccess", typeof(System.Int32), false, false, false, false,  (int)WebpagesMembershipFieldIndex.PasswordFailuresSinceLastSuccess, 0, 0, 10);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "PasswordSalt", typeof(System.String), false, false, false, false,  (int)WebpagesMembershipFieldIndex.PasswordSalt, 128, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "PasswordVerificationToken", typeof(System.String), false, false, false, true,  (int)WebpagesMembershipFieldIndex.PasswordVerificationToken, 128, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "PasswordVerificationTokenExpirationDate", typeof(Nullable<System.DateTime>), false, false, false, true,  (int)WebpagesMembershipFieldIndex.PasswordVerificationTokenExpirationDate, 0, 0, 0);
			this.AddElementFieldInfo("WebpagesMembershipEntity", "UserId", typeof(System.Int32), true, true, false, false,  (int)WebpagesMembershipFieldIndex.UserId, 0, 0, 10);
		}
		/// <summary>Inits WebpagesOauthMembershipEntity's FieldInfo objects</summary>
		private void InitWebpagesOauthMembershipEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(WebpagesOauthMembershipFieldIndex), "WebpagesOauthMembershipEntity");
			this.AddElementFieldInfo("WebpagesOauthMembershipEntity", "Provider", typeof(System.String), true, false, false, false,  (int)WebpagesOauthMembershipFieldIndex.Provider, 30, 0, 0);
			this.AddElementFieldInfo("WebpagesOauthMembershipEntity", "ProviderUserId", typeof(System.String), true, false, false, false,  (int)WebpagesOauthMembershipFieldIndex.ProviderUserId, 100, 0, 0);
			this.AddElementFieldInfo("WebpagesOauthMembershipEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)WebpagesOauthMembershipFieldIndex.UserId, 0, 0, 10);
		}
		/// <summary>Inits WebpagesOauthTokenEntity's FieldInfo objects</summary>
		private void InitWebpagesOauthTokenEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(WebpagesOauthTokenFieldIndex), "WebpagesOauthTokenEntity");
			this.AddElementFieldInfo("WebpagesOauthTokenEntity", "Secret", typeof(System.String), false, false, false, false,  (int)WebpagesOauthTokenFieldIndex.Secret, 100, 0, 0);
			this.AddElementFieldInfo("WebpagesOauthTokenEntity", "Token", typeof(System.String), true, false, false, false,  (int)WebpagesOauthTokenFieldIndex.Token, 100, 0, 0);
		}
		/// <summary>Inits WebpagesRoleEntity's FieldInfo objects</summary>
		private void InitWebpagesRoleEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(WebpagesRoleFieldIndex), "WebpagesRoleEntity");
			this.AddElementFieldInfo("WebpagesRoleEntity", "RoleId", typeof(System.Int32), true, false, true, false,  (int)WebpagesRoleFieldIndex.RoleId, 0, 0, 10);
			this.AddElementFieldInfo("WebpagesRoleEntity", "RoleName", typeof(System.String), false, false, false, false,  (int)WebpagesRoleFieldIndex.RoleName, 256, 0, 0);
		}
		/// <summary>Inits WebpagesUsersInRoleEntity's FieldInfo objects</summary>
		private void InitWebpagesUsersInRoleEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(WebpagesUsersInRoleFieldIndex), "WebpagesUsersInRoleEntity");
			this.AddElementFieldInfo("WebpagesUsersInRoleEntity", "RoleId", typeof(System.Int32), true, true, false, false,  (int)WebpagesUsersInRoleFieldIndex.RoleId, 0, 0, 10);
			this.AddElementFieldInfo("WebpagesUsersInRoleEntity", "UserId", typeof(System.Int32), true, true, false, false,  (int)WebpagesUsersInRoleFieldIndex.UserId, 0, 0, 10);
		}
		
	}
}




