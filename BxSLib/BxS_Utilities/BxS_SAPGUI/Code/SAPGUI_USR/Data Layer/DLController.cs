using System;
using System.IO;
using System.Data;
//.........................................................
using SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
		internal class DLController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DLController(string path)
					{
						this.FullPath	= path;
						//.............................................
						this._SchemaFullName	= Path.Combine(this.FullPath,	this.SchemaFileName);
						//.............................................
						this._Ref			= new	Lazy<References>	(	()	=>	new References	()																					);
						this._Map			= new	Lazy<Mapping>			(	()	=>	new Mapping			(this._Ref.Value)														);
						this._Parser	= new	Lazy<Parser>			(	()	=>	new Parser			(this._Map.Value)														);
						this._Schema	= new Lazy<DataSet>			( ()	=>	new Schema			(this._Ref.Value).Create()									);
						this._UsrDS		= new Lazy<UsrDataSet>	( ()	=>	new UsrDataSet	(this._Ref.Value, this._Schema.Value, path)	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	string		_SchemaFullName;
				//.................................................
				private readonly	Lazy<References>	_Ref;
				private	readonly	Lazy<Mapping>			_Map;
				private	readonly	Lazy<Parser>			_Parser;
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
				internal bool Save(Repository	repository)
					{
						return true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository Load()
					{
						var lo_Rep	= new Repository();
						//.............................................



						//.............................................
						return	lo_Rep;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load(Repository repository)
					{


					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddUpdate(DTOWorkspace dto)
					{
						//DataTable	lo_Tbl	= this.UsrDataSet.Tables["Services"];
						return true;
						//return this.ParseTableRow(lo_Tbl, Mapping.Servic, dto);
					}


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
