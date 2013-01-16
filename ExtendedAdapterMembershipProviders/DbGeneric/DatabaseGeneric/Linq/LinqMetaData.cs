///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 3.5
// Code is generated on: Friday, November 09, 2012 11:04:35 PM
// Code is generated using templates: SD.TemplateBindings.SharedTemplates.NET35
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

using MyCorp.ExtendedAdapterMembershipProviders;
using MyCorp.ExtendedAdapterMembershipProviders.EntityClasses;
using MyCorp.ExtendedAdapterMembershipProviders.FactoryClasses;
using MyCorp.ExtendedAdapterMembershipProviders.HelperClasses;
using MyCorp.ExtendedAdapterMembershipProviders.RelationClasses;

namespace MyCorp.ExtendedAdapterMembershipProviders.Linq
{
	/// <summary>Meta-data class for the construction of Linq queries which are to be executed using LLBLGen Pro code.</summary>
	public partial class LinqMetaData: ILinqMetaData
	{
		#region Class Member Declarations
		private IDataAccessAdapter _adapterToUse;
		private FunctionMappingStore _customFunctionMappings;
		private Context _contextToUse;
		#endregion
		
		/// <summary>CTor. Using this ctor will leave the IDataAccessAdapter object to use empty. To be able to execute the query, an IDataAccessAdapter instance
		/// is required, and has to be set on the LLBLGenProProvider2 object in the query to execute. </summary>
		public LinqMetaData() : this(null, null)
		{
		}
		
		/// <summary>CTor which accepts an IDataAccessAdapter implementing object, which will be used to execute queries created with this metadata class.</summary>
		/// <param name="adapterToUse">the IDataAccessAdapter to use in queries created with this meta data</param>
		/// <remarks> Be aware that the IDataAccessAdapter object set via this property is kept alive by the LLBLGenProQuery objects created with this meta data
		/// till they go out of scope.</remarks>
		public LinqMetaData(IDataAccessAdapter adapterToUse) : this (adapterToUse, null)
		{
		}

		/// <summary>CTor which accepts an IDataAccessAdapter implementing object, which will be used to execute queries created with this metadata class.</summary>
		/// <param name="adapterToUse">the IDataAccessAdapter to use in queries created with this meta data</param>
		/// <param name="customFunctionMappings">The custom function mappings to use. These take higher precedence than the ones in the DQE to use.</param>
		/// <remarks> Be aware that the IDataAccessAdapter object set via this property is kept alive by the LLBLGenProQuery objects created with this meta data
		/// till they go out of scope.</remarks>
		public LinqMetaData(IDataAccessAdapter adapterToUse, FunctionMappingStore customFunctionMappings)
		{
			_adapterToUse = adapterToUse;
			_customFunctionMappings = customFunctionMappings;
		}
	
		/// <summary>returns the datasource to use in a Linq query for the entity type specified</summary>
		/// <param name="typeOfEntity">the type of the entity to get the datasource for</param>
		/// <returns>the requested datasource</returns>
		public IDataSource GetQueryableForEntity(int typeOfEntity)
		{
			IDataSource toReturn = null;
			switch((MyCorp.ExtendedAdapterMembershipProviders.EntityType)typeOfEntity)
			{
				case MyCorp.ExtendedAdapterMembershipProviders.EntityType.UserProfileEntity:
					toReturn = this.UserProfile;
					break;
				case MyCorp.ExtendedAdapterMembershipProviders.EntityType.WebpagesMembershipEntity:
					toReturn = this.WebpagesMembership;
					break;
				case MyCorp.ExtendedAdapterMembershipProviders.EntityType.WebpagesOauthMembershipEntity:
					toReturn = this.WebpagesOauthMembership;
					break;
				case MyCorp.ExtendedAdapterMembershipProviders.EntityType.WebpagesOauthTokenEntity:
					toReturn = this.WebpagesOauthToken;
					break;
				case MyCorp.ExtendedAdapterMembershipProviders.EntityType.WebpagesRoleEntity:
					toReturn = this.WebpagesRole;
					break;
				case MyCorp.ExtendedAdapterMembershipProviders.EntityType.WebpagesUsersInRoleEntity:
					toReturn = this.WebpagesUsersInRole;
					break;
				default:
					toReturn = null;
					break;
			}
			return toReturn;
		}

		/// <summary>returns the datasource to use in a Linq query when targeting UserProfileEntity instances in the database.</summary>
		public DataSource2<UserProfileEntity> UserProfile
		{
			get { return new DataSource2<UserProfileEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting WebpagesMembershipEntity instances in the database.</summary>
		public DataSource2<WebpagesMembershipEntity> WebpagesMembership
		{
			get { return new DataSource2<WebpagesMembershipEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting WebpagesOauthMembershipEntity instances in the database.</summary>
		public DataSource2<WebpagesOauthMembershipEntity> WebpagesOauthMembership
		{
			get { return new DataSource2<WebpagesOauthMembershipEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting WebpagesOauthTokenEntity instances in the database.</summary>
		public DataSource2<WebpagesOauthTokenEntity> WebpagesOauthToken
		{
			get { return new DataSource2<WebpagesOauthTokenEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting WebpagesRoleEntity instances in the database.</summary>
		public DataSource2<WebpagesRoleEntity> WebpagesRole
		{
			get { return new DataSource2<WebpagesRoleEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting WebpagesUsersInRoleEntity instances in the database.</summary>
		public DataSource2<WebpagesUsersInRoleEntity> WebpagesUsersInRole
		{
			get { return new DataSource2<WebpagesUsersInRoleEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		
		#region Class Property Declarations
		/// <summary> Gets / sets the IDataAccessAdapter to use for the queries created with this meta data object.</summary>
		/// <remarks> Be aware that the IDataAccessAdapter object set via this property is kept alive by the LLBLGenProQuery objects created with this meta data
		/// till they go out of scope.</remarks>
		public IDataAccessAdapter AdapterToUse
		{
			get { return _adapterToUse;}
			set { _adapterToUse = value;}
		}

		/// <summary>Gets or sets the custom function mappings to use. These take higher precedence than the ones in the DQE to use</summary>
		public FunctionMappingStore CustomFunctionMappings
		{
			get { return _customFunctionMappings; }
			set { _customFunctionMappings = value; }
		}
		
		/// <summary>Gets or sets the Context instance to use for entity fetches.</summary>
		public Context ContextToUse
		{
			get { return _contextToUse;}
			set { _contextToUse = value;}
		}
		#endregion
	}
}