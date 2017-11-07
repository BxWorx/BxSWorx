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
				internal DLController(string dirPath)
					{
						this._DirPath	= dirPath;
						//.............................................
						this._SchemaFullName	= Path.Combine(this._DirPath	,	_SchemaFileName		);
						this._DSFullName			= Path.Combine(this._DirPath	,	_DatasetFileName	);
						//.............................................
						this._Ref			= new	Lazy<References>	(	()	=>	new References	()													);
						this._Parser	= new	Lazy<Parser>			(	()	=>	new Parser			(this._Ref.Value)						);
						this._DS			= new Lazy<DataSet>			( ()	=>	new Schema			(this._Ref.Value).Create()	);
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
				private readonly	Lazy<References>	_Ref		;
				private	readonly	Lazy<Parser>			_Parser	;
				private readonly	Lazy<DataSet>			_DS			;

			#endregion

			//===========================================================================================
			#region "Properties"


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
				internal bool SaveDataset()
					{
						try
							{
								using (var SW = new StreamWriter(this._DSFullName))
									{
										this._DS.Value.WriteXml(SW);
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
							{	this._DS.Value.ReadXml(this._DSFullName, XmlReadMode.IgnoreSchema); }
						catch
							(System.IO.FileNotFoundException)	{	/* do nothing as this will be a new repository */ }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DataSet LoadSchema()
					{
						var lo_DS	= new DataSet();
						//.............................................
						try
							{
								lo_DS.ReadXmlSchema	(this._SchemaFullName);
							}
						catch (System.IO.FileNotFoundException)
							{
								lo_DS	= this._DS.Value;

								using (var SW = new StreamWriter(this._SchemaFullName))
									{
										lo_DS.WriteXmlSchema(SW);
										//SW.Close();
									}
							}
						//.............................................
						return	lo_DS;
					}

			#endregion

		}
}
