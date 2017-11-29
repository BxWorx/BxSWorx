using SAPGUI.COM.DL;
using Toolset.Serialize;
using Toolset.IO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
		internal class DLController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DLController(string dirPath, IO FileIO, DCSerializer dcSerializer)
					{
						this._DirPath	= dirPath			;
						this._IO			= FileIO			;
						this._DCSer		= dcSerializer;
						//.............................................
						this._DCFullName	= this._IO.PathFileCombine( this._DirPath	,	_DCFileName	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const	string	_DCFileName	= "SAPGUI_USR_DC.xml"	;
				//.................................................
				private readonly string	_DirPath		;
				private readonly string	_DCFullName	;
				//.................................................
				private readonly IO							_IO		 ;
				private	readonly DCSerializer		_DCSer ;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool DCXMLExists	{ get { return	this._IO.FileExists(this._DCFullName); } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Save(DataContainer dc)
					{
						this._IO.WriteFile(this._DCFullName, this._DCSer.Serialize(dc));
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load(ref DataContainer dc)
					{
						this._DCSer.DeSerialize<DataContainer>(this._IO.ReadFile(this._DCFullName), ref dc);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void DeleteDCXMLFile()
					{
						this._IO.DeleteFile(this._DCFullName);
					}

			#endregion

		}
}
