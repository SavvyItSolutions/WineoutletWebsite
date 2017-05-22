﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wineoutlet.DataAccess
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Wineoutlet")]
	public partial class WineOutletDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public WineOutletDataContext() : 
				base(global::Wineoutlet.DataAccess.Properties.Settings.Default.WineoutletConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public WineOutletDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WineOutletDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WineOutletDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WineOutletDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetItemDetails")]
		public ISingleResult<GetItemDetailsResult> GetItemDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Sku", DbType="Int")] System.Nullable<int> sku)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sku);
			return ((ISingleResult<GetItemDetailsResult>)(result.ReturnValue));
		}
	}
	
	public partial class GetItemDetailsResult
	{
		
		private int _Sku;
		
		private string _Name;
		
		private System.Nullable<int> _Vintage;
		
		private string _CountryName;
		
		private string _RegionName;
		
		private string _SubRegionName;
		
		private string _GrapeTypeName;
		
		private string _WineTypeName;
		
		public GetItemDetailsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sku", DbType="Int NOT NULL")]
		public int Sku
		{
			get
			{
				return this._Sku;
			}
			set
			{
				if ((this._Sku != value))
				{
					this._Sku = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(MAX)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Vintage", DbType="Int")]
		public System.Nullable<int> Vintage
		{
			get
			{
				return this._Vintage;
			}
			set
			{
				if ((this._Vintage != value))
				{
					this._Vintage = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CountryName", DbType="VarChar(128)")]
		public string CountryName
		{
			get
			{
				return this._CountryName;
			}
			set
			{
				if ((this._CountryName != value))
				{
					this._CountryName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RegionName", DbType="VarChar(128)")]
		public string RegionName
		{
			get
			{
				return this._RegionName;
			}
			set
			{
				if ((this._RegionName != value))
				{
					this._RegionName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SubRegionName", DbType="VarChar(128)")]
		public string SubRegionName
		{
			get
			{
				return this._SubRegionName;
			}
			set
			{
				if ((this._SubRegionName != value))
				{
					this._SubRegionName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GrapeTypeName", DbType="VarChar(128)")]
		public string GrapeTypeName
		{
			get
			{
				return this._GrapeTypeName;
			}
			set
			{
				if ((this._GrapeTypeName != value))
				{
					this._GrapeTypeName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WineTypeName", DbType="VarChar(128)")]
		public string WineTypeName
		{
			get
			{
				return this._WineTypeName;
			}
			set
			{
				if ((this._WineTypeName != value))
				{
					this._WineTypeName = value;
				}
			}
		}
	}
}
#pragma warning restore 1591