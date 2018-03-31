using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.BDCTran.SAPMsg_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_IndexTXT : SAPMsg_IndexBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPMsg_IndexTXT( SAPMsg_Profile profile ) : base( profile )
					{
						this._Metadata	=	new	Lazy< SMC.RfcStructureMetadata >( ()=> this._Profile.LNEStructure	);
						//.............................................
						this._Fmt		= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "TDFORMAT" ) );
						this._Lne		= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "TDLINE"		) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_Fmt;
				private	readonly	Lazy<int>		_Lne;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	Fmt		{ get { return	this._Profile.IsReady ?	this._Fmt.Value	:	0	; } }
				internal	int	Lne		{ get { return	this._Profile.IsReady ?	this._Lne.Value	:	0	; } }

			#endregion

		}
}
