using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_IndexSPA : BDCCall_IndexBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_IndexSPA( BDCCall_Profile profile ) : base( profile )
					{
						this._Metadata	=	new	Lazy< SMC.RfcStructureMetadata >( ()=> this._Profile.SPAStructure	);
						//.............................................
						this._MID		= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "PARID"	) );
						this._Val		= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "PARVAL" ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_MID;
				private	readonly	Lazy<int>		_Val;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		MID		{ get { return	this._Profile.IsReady	?	this._MID.Value	:	0	; } }
				internal	int		Val		{ get { return	this._Profile.IsReady	?	this._Val.Value	:	0	; } }

			#endregion

		}
}
