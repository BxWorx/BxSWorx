using System;
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
						this._FNCIndex	=	new Lazy<SAPMsg_IndexFNC>(	()=>	this._Factory.CreateIndexFNC()	);
						this._TXTIndex	=	new	Lazy<SAPMsg_IndexTXT>(	()=>	this._Factory.CreateIndexTXT()	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	SAPMsg_Factory		_Factory;
				//.................................................
				internal	readonly	Lazy<	SAPMsg_IndexFNC	>		_FNCIndex;
				internal	readonly	Lazy<	SAPMsg_IndexTXT >		_TXTIndex;

			#endregion

			//===========================================================================================
			#region "Properties"

				//internal	SMC.RfcStructureMetadata	LNEStructure	{ get	{	return	this.Metadata[this._FNCIndex.Value.MsgLT].ValueMetadataAsTableMetadata.LineType	; } }

			#endregion

		}
}