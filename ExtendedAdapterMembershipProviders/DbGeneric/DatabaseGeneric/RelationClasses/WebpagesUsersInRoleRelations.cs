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
	/// <summary>Implements the relations factory for the entity: WebpagesUsersInRole. </summary>
	public partial class WebpagesUsersInRoleRelations
	{
		/// <summary>CTor</summary>
		public WebpagesUsersInRoleRelations()
		{
		}

		/// <summary>Gets all relations of the WebpagesUsersInRoleEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.UserProfileEntityUsingUserId);
			toReturn.Add(this.WebpagesRoleEntityUsingRoleId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between WebpagesUsersInRoleEntity and UserProfileEntity over the m:1 relation they have, using the relation between the fields:
		/// WebpagesUsersInRole.UserId - UserProfile.UserId
		/// </summary>
		public virtual IEntityRelation UserProfileEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "UserProfile", false);
				relation.AddEntityFieldPair(UserProfileFields.UserId, WebpagesUsersInRoleFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserProfileEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("WebpagesUsersInRoleEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between WebpagesUsersInRoleEntity and WebpagesRoleEntity over the m:1 relation they have, using the relation between the fields:
		/// WebpagesUsersInRole.RoleId - WebpagesRole.RoleId
		/// </summary>
		public virtual IEntityRelation WebpagesRoleEntityUsingRoleId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "WebpagesRole", false);
				relation.AddEntityFieldPair(WebpagesRoleFields.RoleId, WebpagesUsersInRoleFields.RoleId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("WebpagesRoleEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("WebpagesUsersInRoleEntity", true);
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
	internal static class StaticWebpagesUsersInRoleRelations
	{
		internal static readonly IEntityRelation UserProfileEntityUsingUserIdStatic = new WebpagesUsersInRoleRelations().UserProfileEntityUsingUserId;
		internal static readonly IEntityRelation WebpagesRoleEntityUsingRoleIdStatic = new WebpagesUsersInRoleRelations().WebpagesRoleEntityUsingRoleId;

		/// <summary>CTor</summary>
		static StaticWebpagesUsersInRoleRelations()
		{
		}
	}
}
