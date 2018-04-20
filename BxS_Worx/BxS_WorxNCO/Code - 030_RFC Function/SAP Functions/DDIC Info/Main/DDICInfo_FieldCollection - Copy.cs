using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
//.........................................................

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.DDIC
{
	internal class DDICInfo_FieldCollection
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DDICInfo_FieldCollection()
					{
						this._DDICInfo	= new	Dictionary< string , IList< DTO_DDICInfo_Field > >();
						this._DDIC			= new ConcurrentDictionary<string , ConcurrentDictionary<string , string>>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary< string , IList< DTO_DDICInfo_Field > >	_DDICInfo;

				private readonly ConcurrentDictionary< string , ConcurrentDictionary<string , string> >	_DDIC;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	TableCount	{ get { return	this._DDICInfo.Count; } }
				//.................................................
				public	Dictionary< string , IList< DTO_DDICInfo_Field > >.KeyCollection	TableNames	{ get	{	return	this._DDICInfo.Keys; } }

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
				public void AddData( DTO_DDICInfo_Field dto )
					{
						if ( ! this._DDICInfo.ContainsKey( dto.TabName ) )
							{
								this._DDICInfo.Add( dto.TabName, new List< DTO_DDICInfo_Field >() );
							}
						//.............................................
						if ( this._DDICInfo.TryGetValue( dto.TabName, out IList< DTO_DDICInfo_Field > lt_DTO ) )
							{
								lt_DTO.Add( dto );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddData( string	tableName , string	fieldName	)
					{
						this.AddData( this.CreateDTO( tableName , fieldName ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<DTO_DDICInfo_Field> GetFieldList( string tableName )
					{
						return	this._DDIC.TryGetValue( tableName , out ConcurrentDictionary<string,string>	lo_Flds )
											?	lo_Flds.ToList()
											: new List< DTO_DDICInfo_Field >();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ConcurrentDictionary<string , string> Make( string key )
					{
						var lo = new	ConcurrentDictionary<string , string>();
						lo.TryAdd(key , string.Empty);
						return lo;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddUpdateText( string tableName , string	fieldName , string text = null )
					{
						ConcurrentDictionary<string , string> q = this._DDIC.GetOrAdd( tableName , key =>  this.Make(key) );
						string s = q.AddOrUpdate( fieldName , text , (k,v)=> text );


						//var q = new Dictionary<string , string>();
						//this._DDIC.AddOrUpdate( tableName , q );



						//var x = this._DDIC.GetOrAdd(	tableName
						//														, ( key , val )=>		{ var t = new Dictionary<string , string>();
						//																									t.Add(key , string.Empty); );


						//var x = this._DDIC.GetOrAdd( tableName , ( key , val )=>  new Dictionary<string , string>().Add(key , string.Empty); );

						//if ( ! this._DDIC.ContainsKey( tableName ) )
						//	{
						//		this._DDIC.Add( tableName, new Dictionary<string, string>() );
						//	}
						////.............................................
						//if ( this._DDIC.TryGetValue( tableName , out Dictionary<string , string> lt_DTO ) )
						//	{
						//		if ( lt_DTO.ContainsKey(fieldName) )
						//	}


						//if ( ! this._DDICInfo.ContainsKey( tableName ) )
						//	{
						//		this._DDICInfo.Add( tableName, new List< DTO_DDICInfo_Field >() );
						//	}
						////.............................................
						//if ( this._DDICInfo.TryGetValue( tableName, out IList< DTO_DDICInfo_Field > lt_DTO ) )
						//	{
						//		DTO_DDICInfo_Field x = this.CreateDTO( tableName , fieldName , text );

						//		int i = lt_DTO.IndexOf( x );

						//		if ( i < 0 )
						//			{
						//				lt_DTO.Add( x );
						//			}
						//		else
						//			{
						//				lt_DTO[i]	= x;
						//			}
						//		return	true;
						//	}
						//else
						//	{
						//		return	false;
						//	}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						this._DDIC.Clear();
					}

			#endregion

		}
}
