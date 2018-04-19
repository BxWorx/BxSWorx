using System.Collections.Generic;
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
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary< string , IList< DTO_DDICInfo_Field > >	_DDICInfo;

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
						if ( this._DDICInfo.TryGetValue( tableName, out IList< DTO_DDICInfo_Field > lt_DTO ) )
							{
								return	lt_DTO;
							}
						else
							{
								return	new List< DTO_DDICInfo_Field >();
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateText( string tableName , string	fieldName , string text = null )
					{
						if ( ! this._DDICInfo.ContainsKey( tableName ) )
							{
								this._DDICInfo.Add( tableName, new List< DTO_DDICInfo_Field >() );
							}
						//.............................................
						if ( this._DDICInfo.TryGetValue( tableName, out IList< DTO_DDICInfo_Field > lt_DTO ) )
							{
								DTO_DDICInfo_Field x = this.CreateDTO( tableName , fieldName , text );

								int i = lt_DTO.IndexOf( x );

								if ( i < 0 )
									{
										lt_DTO.Add( x );
									}
								else
									{
										lt_DTO[i]	= x;
									}
								return	true;
							}
						else
							{
								return	false;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						this._DDICInfo.Clear();
					}

			#endregion

		}
}
