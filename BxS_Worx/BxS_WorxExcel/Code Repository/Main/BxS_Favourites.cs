using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.NCO;
using BxS_WorxUtil.General;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class BxS_Favourites<T>	where	T: ISAP_Logon
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BxS_Favourites(	TopTenList<T>		topTen
																,	Serializer			serializer
																,	Func<T>					createEntry	)
					{
						this.TopTen				= topTen;
						this._serializer	= serializer;
						this._CreateEntry	= createEntry;
						////...
						//this._Types	= new	List<Type>() {	typeof( DataTable<int , ISAP_Logon> )
						//																,	typeof( SAP_Logon										) };
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IList<T>				List		{ get { return	this.TopTen.List;	} }
				internal	TopTenList<T>		TopTen	{ get; }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Serializer		_serializer ;
				private	readonly	Func<T>				_CreateEntry;
				//private	readonly	List<Type>	_Types;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal T	CreateEntry()	=> this._CreateEntry();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load()
						{
							string				x		= Properties.Settings.Default.XML_FavList;
							TopTenList<T> y		=	this._serializer.DeSerialize<TopTenList<T>>( x , this.TopTen.Types );
						}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Save()
						{
							string x =	this._serializer.Serialize( this.TopTen , this.TopTen.Types );
							Properties.Settings.Default.XML_FavList	= x;
						}

			#endregion

		}
}
