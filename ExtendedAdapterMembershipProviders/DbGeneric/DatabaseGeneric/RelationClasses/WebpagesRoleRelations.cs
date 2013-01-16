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
	/// <summary>Implements the relations factory for the entity: WebpagesRole. </summary>
	public partial class WebpagesRoleRelations
	{
		/// <summary>CTor</summary>
		public WebpagesRoleRelations()
		{
		}

		/// <summary>Gets all relations of the WebpagesRoleEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.WebpagesUsersInRoleEntityUsingRoleId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between WebpagesRoleEntity and WebpagesUsersInRoleEntity over the 1:n relation they have, using the relation between the fields:
		/// WebpagesRole.RoleId - WebpagesUsersInRole.RoleId
		/// </summary>
		public virtual IEntityRelation WebpagesUsersInRoleEntityUsingRoleId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "WebpagesUsersInRoles" , true);
				relation.AddEntityFieldPair(WebpagesRoleFields.RoleId, WebpagesUsersInRoleFields.RoleId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("WebpagesRoleEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("WebpagesUsersInRoleEntity", false);
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
	internal static class StaticWebpagesRoleRelations
	{
		internal static readonly IEntityRelation WebpagesUsersInRoleEntityUsingRoleIdStatic = new WebpagesRoleRelations().WebpagesUsersInRoleEntityUsingRoleId;

		/// <summary>CTor</summary>
		static StaticWebpagesRoleRelations()
		{
		}
	}
}
