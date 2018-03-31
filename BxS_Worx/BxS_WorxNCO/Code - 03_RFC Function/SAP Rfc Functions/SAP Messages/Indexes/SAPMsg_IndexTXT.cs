using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.BDCTran.SAPMsg_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_IndexTXT
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPMsg_IndexTXT()
					{
						this._Fmt		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TDFORMAT" ) );
						this._Lne		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TDLINE"	 ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_Fmt;
				private	readonly	Lazy<int>		_Lne;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	Name { get { return	cz_StrLne; } }

				internal	SMC.RfcStructureMetadata	Metadata	{ get; set;	}
				//.................................................
				internal	int		Fmt		{ get { return	this._Fmt.Value; } }
				internal	int		Lne		{ get { return	this._Lne.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.IRfcStructure	Create()
					{
						return	this.Metadata.CreateStructure();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.IRfcTable	CreateTable()
					{
						return	this.Metadata.CreateTable();
					}

			#endregion

		}
}
