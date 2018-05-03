using System;
using System.Collections.Generic;
//.........................................................
using BxS_Toolset.DataContainer;
using BxS_WorxIPX.NCO;
using BxS_WorxUtil.General;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class BxS_Favourites
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BxS_Favourites(	Func<ISAP_Logon>	create
																,	Serializer				serializer )
					{
						this._serializer	= serializer;
						//...
						this.List		= new	DataTable<int , ISAP_Logon>( create );
						//...
						this._Types	= new	List<Type>() {	typeof( DataTable<int , ISAP_Logon> )
																						,	typeof( SAP_Logon										) };
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal DataTable<int , ISAP_Logon> List { get; set; }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Serializer _serializer ;
				private	readonly	List<Type>	_Types;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load()
						{
							string x	= Properties.Settings.Default.XML_FavList;
							this.List	=	this._serializer.DeSerialize<DataTable<int , ISAP_Logon>>( x , this._Types );
						}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Save()
						{
							string x =	this._serializer.Serialize( this.List , this._Types );
							Properties.Settings.Default.XML_FavList	= x;
						}

			#endregion

		}
}
