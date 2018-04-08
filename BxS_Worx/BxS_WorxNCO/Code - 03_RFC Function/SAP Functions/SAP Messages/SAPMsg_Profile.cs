using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_Profile : RfcFncProfile
		{
			#region "Function Parameters"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPMsg_Profile(		string					fncName
																	, SAPMsg_Factory	factory	)	: base( fncName )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(SAPMsg_Profile).Namespace}:- Factory null" );
						//.............................................
						this.FNCIndex		= this._Factory.CreateIndexFNC( this );
						this.TXTIndex		= this._Factory.CreateIndexTXT( this );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	SAPMsg_Factory		_Factory;
				//.................................................
				internal	readonly	SAPMsg_IndexFNC		FNCIndex;
				internal	readonly	SAPMsg_IndexTXT		TXTIndex;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	SMC.RfcStructureMetadata	LNEStructure	{ get	{	return	this.Metadata[this.FNCIndex.MsgLT].ValueMetadataAsTableMetadata.LineType	; } }

			#endregion

		}
}