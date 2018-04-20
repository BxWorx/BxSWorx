using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.DDIC
{
	public class DDICInfo_FieldCollection
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DDICInfo_FieldCollection()
					{
						this._DDIC	= new ConcurrentDictionary<string , ConcurrentDictionary<string , string>>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly ConcurrentDictionary<string , ConcurrentDictionary<string , string>>	_DDIC;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	TableCount	{ get { return	this._DDIC.Count; } }
				//.................................................
				public	IList<string> TableNames	{ get	{	return	this._DDIC.Keys.ToList(); } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_DDICInfo_Field	CreateDTO()	=>	new DTO_DDICInfo_Field();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_DDICInfo_Field	CreateDTO( string	tableName , string	fieldName , string text = null )
					{
						return	new DTO_DDICInfo_Field	{
																								TabName	= tableName
																							,	FldName	= fieldName
																							, FldText	= text
																						};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ConcurrentDictionary<string , string> GetTableFieldData( string tableName )
					{
						return	this._DDIC.TryGetValue( tableName , out ConcurrentDictionary<string , string>	lo_Flds )
							?	lo_Flds
							:	new ConcurrentDictionary<string , string>();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<string> GetTableFieldList( string tableName )
					{
						return	this._DDIC.TryGetValue( tableName , out ConcurrentDictionary<string , string>	lo_Flds )
							?	lo_Flds.Keys.ToList()
							:	new List<string>();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<DTO_DDICInfo_Field> GetFields( string tableName = null )
					{
						var lt_List = new List< DTO_DDICInfo_Field >();
						//.............................................
						string	lc_SrchStr	= string.IsNullOrEmpty( tableName ) ? "" : tableName ;
						var			lt_TabNames = this._DDIC	.Where(	kvp => kvp.Key.StartsWith( lc_SrchStr , StringComparison.OrdinalIgnoreCase ) )
																								.Select( kvp => kvp.Key )
																									.ToList();

						foreach ( string lc_TabNme in lt_TabNames )
							{
								if ( this._DDIC.TryGetValue( lc_TabNme , out ConcurrentDictionary<string , string>	lo_Flds ) )
									{
										lt_List.AddRange( lo_Flds.Select( ls_kvp => this.CreateDTO( lc_TabNme , ls_kvp.Key , ls_kvp.Value ) ).ToList() );
									}
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddUpdateText( string tableName , string fieldName , string text = null )
					{
						string lc_TN	= tableName.Trim().ToUpper();
						string lc_FN	= fieldName.Trim().ToUpper();

						ConcurrentDictionary<string , string> q = this._DDIC.GetOrAdd( lc_TN , new	ConcurrentDictionary<string , string>() );

						#pragma warning disable RCS1163
						q.AddOrUpdate( lc_FN , text , (k,v)=> text );
						#pragma warning restore	RCS1163
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						this._DDIC.Clear();
					}

			#endregion

		}
}
