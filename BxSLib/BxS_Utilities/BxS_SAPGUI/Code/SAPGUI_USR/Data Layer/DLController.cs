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
				internal DLController(string dirPath, Schema schema, Parser parser)
					{
						this._DirPath	= dirPath;
						this._Schema	= schema;
						this._Parser	= parser;
						//.............................................
						this._SchemaFullName	= Path.Combine(this._DirPath	,	_SchemaFileName		);
						this._DSFullName			= Path.Combine(this._DirPath	,	_DatasetFileName	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const	string	_SchemaFileName		= "SAPGUI_USR_Schema.xml"		;
				private const	string	_DatasetFileName	= "SAPGUI_USR_DataSet.xml"	;
				//.................................................
				private readonly string	_DirPath				;
				private readonly string	_SchemaFullName	;
				private readonly string	_DSFullName			;
				//.................................................
				private readonly Schema	_Schema;
				private	readonly Parser	_Parser;
				//.................................................
				private DataSet	_DS;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool SchemaXMLExists		{ get { return	File.Exists(this._SchemaFullName)	;	} }
				internal bool DatasetXMLExists	{ get { return	File.Exists(this._DSFullName		)	;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Save(Repository	repository)
					{
						this.LoadSchema();
						this._Parser.ParseRep2DS(repository, this._DS);
						this.SaveDataset();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository GetRepository()
					{
						var lo_Rep	= new Repository();
						//.............................................
						this.LoadRepository(lo_Rep);
						//.............................................
						return	lo_Rep;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadRepository(Repository repository)
					{
						this.LoadSchema();
						this.LoadData();
						this._Parser.ParseDS2Rep(this._DS, repository);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void DeleteSchemaXMLFile()
					{
						if (this.SchemaXMLExists)		File.Delete(this._SchemaFullName);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void DeleteDatasetXMLFile()
					{
						if (this.DatasetXMLExists)	File.Delete(this._DSFullName);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool SaveDataset()
					{
						try
							{
								using (var SW = new StreamWriter(this._DSFullName))
									{
										this._DS.WriteXml(SW);
										//SW.Close();
									}
							}
						catch (Exception)
							{
								int x = 1;
								x++;
							}
						return true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadData()
					{
						try
							{	this._DS.ReadXml(this._DSFullName, XmlReadMode.IgnoreSchema); }
						catch	(System.IO.FileNotFoundException)
							{	/* do nothing as this will be a new repository */ }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadSchema()
					{
						try
							{
								this._DS	= new DataSet();
								this._DS.ReadXmlSchema	(this._SchemaFullName);
							}
						catch (System.IO.FileNotFoundException)
							{
								this._DS = this._Schema.Create();

								using (var SW = new StreamWriter(this._SchemaFullName))
									{
										this._DS.WriteXmlSchema(SW);
										//SW.Close();
									}
							}
					}

			#endregion

		}
}
