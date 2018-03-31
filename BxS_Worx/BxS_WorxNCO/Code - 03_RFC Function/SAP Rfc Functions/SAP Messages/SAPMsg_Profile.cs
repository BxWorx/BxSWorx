using System;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_Profile : RfcFncProfile
		{
			#region "Function Parameters"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPMsg_Profile(		string						fncName
																	, SAPMsg_Factory		factory					)	: base( fncName )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(SAPMsg_Profile).Namespace}:- Factory null" );
						//.............................................
						this.FNCIndex		= this._Factory.CreateIndexFNC();
						this.TXTIndex		= this._Factory.CreateIndexTXT();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	SAPMsg_Factory	_Factory;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	readonly	SAPMsg_IndexFNC		FNCIndex;
				internal	readonly	SAPMsg_IndexTXT		TXTIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal SAPMsg_Header CreateBDCCallHeader( bool withDefaults = true )
				//	{
				//		this.ReadyProfile();

				//		return	this._Factory.CreateBDCHeader(	this._RfcDestination.CreateRfcStructure( cz_StrCTU )
				//																					,	this.TXTIndex
				//																					,	withDefaults																					);
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal SAPMsg_Lines CreateBDCCallLines()
				//	{
				//		this.ReadyProfile();

				//		return	this._Factory.CreateBDCLines(		this._RfcDestination.CreateRfcTable( cz_StrBDC )
				//																					,	this._RfcDestination.CreateRfcTable( cz_StrSPA )
				//																					,	this._RfcDestination.CreateRfcTable( cz_StrMSG )
				//																					,	this.SPAIndex
				//																					,	this.BDCIndex																		);
				//	}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				public override void ReadyProfile()
					{
						try
							{
								//this.LoadFunctionIndexing		( this.FNCIndex )	;

								//this.LoadStructureIndexing	( this.TXTIndex )	;

								this.IsReady	=	true;
							}
						catch
							{
								this.IsReady	=	false;
							}
					}

			#endregion

		}
}