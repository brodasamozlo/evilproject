﻿///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 3.5
// Code is generated on: Friday, November 09, 2012 11:04:38 PM
// Code is generated using templates: SD.TemplateBindings.SharedTemplates.NET20
// Templates vendor: Solutions Design.
// Templates version: 
//////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Data;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace MyCorp.ExtendedAdapterMembershipProviders.MySql.DatabaseSpecific
{
	/// <summary>Singleton implementation of the PersistenceInfoProvider. This class is the singleton wrapper through which the actual instance is retrieved.</summary>
	/// <remarks>It uses a single instance of an internal class. The access isn't marked with locks as the PersistenceInfoProviderBase class is threadsafe.</remarks>
	internal static class PersistenceInfoProviderSingleton
	{
		#region Class Member Declarations
		private static readonly IPersistenceInfoProvider _providerInstance = new PersistenceInfoProviderCore();
		#endregion

		/// <summary>Dummy static constructor to make sure threadsafe initialization is performed.</summary>
		static PersistenceInfoProviderSingleton()
		{
		}

		/// <summary>Gets the singleton instance of the PersistenceInfoProviderCore</summary>
		/// <returns>Instance of the PersistenceInfoProvider.</returns>
		public static IPersistenceInfoProvider GetInstance()
		{
			return _providerInstance;
		}
	}

	/// <summary>Actual implementation of the PersistenceInfoProvider. Used by singleton wrapper.</summary>
	internal class PersistenceInfoProviderCore : PersistenceInfoProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="PersistenceInfoProviderCore"/> class.</summary>
		internal PersistenceInfoProviderCore()
		{
			Init();
		}

		/// <summary>Method which initializes the internal datastores with the structure of hierarchical types.</summary>
		private void Init()
		{
			this.InitClass((6 + 0));
			InitUserProfileEntityMappings();
			InitWebpagesMembershipEntityMappings();
			InitWebpagesOauthMembershipEntityMappings();
			InitWebpagesOauthTokenEntityMappings();
			InitWebpagesRoleEntityMappings();
			InitWebpagesUsersInRoleEntityMappings();

		}


		/// <summary>Inits UserProfileEntity's mappings</summary>
		private void InitUserProfileEntityMappings()
		{
			this.AddElementMapping( "UserProfileEntity", @"MyCorpExtendedAdapterMembershipProviders", @"Default", "UserProfile", 2 );
			this.AddElementFieldMapping( "UserProfileEntity", "UserId", "UserId", false, "Int", 0, 0, 10, true, "LAST_INSERT_ID()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "UserProfileEntity", "UserName", "UserName", false, "VarChar", 56, 0, 0, false, "", null, typeof(System.String), 1 );
		}
		/// <summary>Inits WebpagesMembershipEntity's mappings</summary>
		private void InitWebpagesMembershipEntityMappings()
		{
			this.AddElementMapping( "WebpagesMembershipEntity", @"MyCorpExtendedAdapterMembershipProviders", @"Default", "WebpagesMembership", 11 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "ConfirmationToken", "ConfirmationToken", true, "VarChar", 128, 0, 0, false, "", null, typeof(System.String), 0 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "CreateDate", "CreateDate", true, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 1 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "IsConfirmed", "IsConfirmed", false, "SmallInt", 0, 0, 5, false, "",  new SD.LLBLGen.Pro.ORMSupportClasses.BooleanNumericConverter(), typeof(System.Int16), 2 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "LastPasswordFailureDate", "LastPasswordFailureDate", true, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 3 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "Password", "Password", false, "VarChar", 128, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "PasswordChangedDate", "PasswordChangedDate", true, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "PasswordFailuresSinceLastSuccess", "PasswordFailuresSinceLastSuccess", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "PasswordSalt", "PasswordSalt", false, "VarChar", 128, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "PasswordVerificationToken", "PasswordVerificationToken", true, "VarChar", 128, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "PasswordVerificationTokenExpirationDate", "PasswordVerificationTokenExpirationDate", true, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
			this.AddElementFieldMapping( "WebpagesMembershipEntity", "UserId", "UserId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 10 );
		}
		/// <summary>Inits WebpagesOauthMembershipEntity's mappings</summary>
		private void InitWebpagesOauthMembershipEntityMappings()
		{
			this.AddElementMapping( "WebpagesOauthMembershipEntity", @"MyCorpExtendedAdapterMembershipProviders", @"Default", "WebpagesOauthMembership", 3 );
			this.AddElementFieldMapping( "WebpagesOauthMembershipEntity", "Provider", "Provider", false, "VarChar", 30, 0, 0, false, "", null, typeof(System.String), 0 );
			this.AddElementFieldMapping( "WebpagesOauthMembershipEntity", "ProviderUserId", "ProviderUserId", false, "VarChar", 100, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "WebpagesOauthMembershipEntity", "UserId", "UserId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
		}
		/// <summary>Inits WebpagesOauthTokenEntity's mappings</summary>
		private void InitWebpagesOauthTokenEntityMappings()
		{
			this.AddElementMapping( "WebpagesOauthTokenEntity", @"MyCorpExtendedAdapterMembershipProviders", @"Default", "WebpagesOauthToken", 2 );
			this.AddElementFieldMapping( "WebpagesOauthTokenEntity", "Secret", "Secret", false, "VarChar", 100, 0, 0, false, "", null, typeof(System.String), 0 );
			this.AddElementFieldMapping( "WebpagesOauthTokenEntity", "Token", "Token", false, "VarChar", 100, 0, 0, false, "", null, typeof(System.String), 1 );
		}
		/// <summary>Inits WebpagesRoleEntity's mappings</summary>
		private void InitWebpagesRoleEntityMappings()
		{
			this.AddElementMapping( "WebpagesRoleEntity", @"MyCorpExtendedAdapterMembershipProviders", @"Default", "WebpagesRole", 2 );
			this.AddElementFieldMapping( "WebpagesRoleEntity", "RoleId", "RoleId", false, "Int", 0, 0, 10, true, "LAST_INSERT_ID()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "WebpagesRoleEntity", "RoleName", "RoleName", false, "VarChar", 256, 0, 0, false, "", null, typeof(System.String), 1 );
		}
		/// <summary>Inits WebpagesUsersInRoleEntity's mappings</summary>
		private void InitWebpagesUsersInRoleEntityMappings()
		{
			this.AddElementMapping( "WebpagesUsersInRoleEntity", @"MyCorpExtendedAdapterMembershipProviders", @"Default", "WebpagesUsersInRole", 2 );
			this.AddElementFieldMapping( "WebpagesUsersInRoleEntity", "RoleId", "RoleId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "WebpagesUsersInRoleEntity", "UserId", "UserId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
		}

	}
}