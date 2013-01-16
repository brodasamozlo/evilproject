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
using System.Collections;
using System.Collections.Generic;
using MyCorp.ExtendedAdapterMembershipProviders;
using MyCorp.ExtendedAdapterMembershipProviders.FactoryClasses;
using MyCorp.ExtendedAdapterMembershipProviders.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace MyCorp.ExtendedAdapterMembershipProviders.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: UserProfile. </summary>
	public partial class UserProfileRelations
	{
		/// <summary>CTor</summary>
		public UserProfileRelations()
		{
		}

		/// <summary>Gets all relations of the UserProfileEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.WebpagesOauthMembershipEntityUsingUserId);
			toReturn.Add(this.WebpagesUsersInRoleEntityUsingUserId);
			toReturn.Add(this.WebpagesMembershipEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between UserProfileEntity and WebpagesOauthMembershipEntity over the 1:n relation they have, using the relation between the fields:
		/// UserProfile.UserId - WebpagesOauthMembership.UserId
		/// </summary>
		public virtual IEntityRelation WebpagesOauthMembershipEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "WebpagesOauthMemberships" , true);
				relation.AddEntityFieldPair(UserProfileFields.UserId, WebpagesOauthMembershipFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserProfileEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("WebpagesOauthMembershipEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserProfileEntity and WebpagesUsersInRoleEntity over the 1:n relation they have, using the relation between the fields:
		/// UserProfile.UserId - WebpagesUsersInRole.UserId
		/// </summary>
		public virtual IEntityRelation WebpagesUsersInRoleEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "WebpagesUsersInRoles" , true);
				relation.AddEntityFieldPair(UserProfileFields.UserId, WebpagesUsersInRoleFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserProfileEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("WebpagesUsersInRoleEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserProfileEntity and WebpagesMembershipEntity over the 1:1 relation they have, using the relation between the fields:
		/// UserProfile.UserId - WebpagesMembership.UserId
		/// </summary>
		public virtual IEntityRelation WebpagesMembershipEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, "WebpagesMembership", true);

				relation.AddEntityFieldPair(UserProfileFields.UserId, WebpagesMembershipFields.UserId);



				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserProfileEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("WebpagesMembershipEntity", false);
				return relation;
			}
		}

		/// <summary>stub, not used in this entity, only for TargetPerEntity entities.</summary>
		public virtual IEntityRelation GetSubTypeRelation(string subTypeEntityName) { return null; }
		/// <summary>stub, not used in this entity, only for TargetPerEntity entities.</summary>
		public virtual IEntityRelation GetSuperTypeRelation() { return null;}
		#endregion

		#region Included Code

		#endregion
	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticUserProfileRelations
	{
		internal static readonly IEntityRelation WebpagesOauthMembershipEntityUsingUserIdStatic = new UserProfileRelations().WebpagesOauthMembershipEntityUsingUserId;
		internal static readonly IEntityRelation WebpagesUsersInRoleEntityUsingUserIdStatic = new UserProfileRelations().WebpagesUsersInRoleEntityUsingUserId;
		internal static readonly IEntityRelation WebpagesMembershipEntityUsingUserIdStatic = new UserProfileRelations().WebpagesMembershipEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticUserProfileRelations()
		{
		}
	}
}
