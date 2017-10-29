using System;
using System.IO;
using System.Data;
//.........................................................
using SAPGUI.API.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class DataLayerController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataLayerController(string path)
					{
						this.FullPath	= path;
						//.............................................
						this._SchemaFullName	= Path.Combine(this.FullPath,	this.SchemaFileName);
						//.............................................
						this._Ref			= new	Lazy<References>	(	() => new References()						);
						this._Schema	= new Lazy<DataSet>			( () => new Schema(this._Ref.Value).Create() );
						this._UsrDS		= new Lazy<UsrDataSet>	( () => new UsrDataSet(this._Ref.Value, this._Schema.Value, path)	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	string		_SchemaFullName;
				//.................................................
				private readonly	Lazy<References>	_Ref;
				private readonly	Lazy<DataSet>			_Schema;
				private readonly	Lazy<UsrDataSet>	_UsrDS;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal string			SchemaFileName	{ get	{ return "SAPGUI_USR_Schema.xml"; } }
				internal UsrDataSet	UsrDataSet			{ get { return	this._UsrDS.Value;			} }
				internal string			FullPath				{ get;																		}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Save()
					{
						return true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddUpdate(DTOService dto)
					{
						//DataTable	lo_Tbl	= this.UsrDataSet.Tables["Services"];
						return true;
						//return this.ParseTableRow(lo_Tbl, Mapping.Servic, dto);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddUpdate(DTOMsgServer dto)
					{
						//DataTable	lo_Tbl	= this.UsrDataSet.Tables["MsgServer"];
						return true;
						//return this.ParseTableRow(lo_Tbl, Mapping.ServicesMap, dto);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DataSet GetSchema()
					{
						var lo_DS	= new DataSet();
						//.............................................
						try
							{	lo_DS.ReadXmlSchema	(this._SchemaFullName); }
						catch (System.IO.FileNotFoundException)
							{
								lo_DS	= this._Schema.Value;

								using (var SW = new StreamWriter(this._SchemaFullName))
									{
										lo_DS.WriteXmlSchema(SW);
										SW.Close();
									}
							}
						//.............................................
						return	lo_DS;
					}

			#endregion

		}
}
