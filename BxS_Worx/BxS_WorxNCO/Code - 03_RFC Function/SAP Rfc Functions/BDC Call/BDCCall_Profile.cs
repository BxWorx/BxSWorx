using System;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.RfcFunction.BDCTran.BDCCall_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Profile : RfcFncProfile
		{
			#region "Function Parameters"

//																	,	IRfcDestination		rfcDestination
//																																								, rfcDestination )

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Profile(		string						fncName
																	, BDCCall_Factory		factory					)	: base(		fncName )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(BDCCall_Profile).Namespace}:- Factory null" );
						//.............................................
						this.FNCIndex		= this._Factory.CreateIndexFNC( this );
						this.CTUIndex		= this._Factory.CreateIndexCTU();
						this.SPAIndex		= this._Factory.CreateIndexSPA();
						this.BDCIndex		= this._Factory.CreateIndexBDC();
						this.MSGIndex		= this._Factory.CreateIndexMSG();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCall_Factory		_Factory;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	readonly	BDCCall_IndexFNC		FNCIndex;
				internal	readonly	BDCCall_IndexCTU		CTUIndex;
				internal	readonly	BDCCall_IndexSPA		SPAIndex;
				internal	readonly	BDCCall_IndexBDC		BDCIndex;
				internal	readonly	BDCCall_IndexMSG		MSGIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Header CreateBDCCallHeader( bool withDefaults = true )
					{
						this.ReadyProfile();

						return	this._Factory.CreateBDCHeader(	this.CTUIndex
																									,	withDefaults	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Data CreateBDCCallLines()
					{
						this.ReadyProfile();

						return	this._Factory.CreateBDCLines(		this.SPAIndex
																									,	this.BDCIndex
																									,	this.MSGIndex	);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				public override void ReadyProfile()
					{
						try
							{
								//this.FNCIndex.Metadata	= this.Metadata;
								
								//this.LoadFunctionIndexing		( this.FNCIndex )	;

								//this.LoadStructureIndexing	( this.CTUIndex )	;
								//this.LoadStructureIndexing	( this.SPAIndex )	;
								//this.LoadStructureIndexing	( this.BDCIndex )	;
								//this.LoadStructureIndexing	( this.MSGIndex )	;

								this.IsReady	=	true;
							}
						catch
							{
								this.IsReady	=	false;
							}

						//this.IsReady	=				this._RfcDestination.LoadFunctionIndexing		( this.FNCIndex )

						//									&&	this._RfcDestination.LoadStructureIndexing	( this.CTUIndex )
						//									&&	this._RfcDestination.LoadStructureIndexing	( this.SPAIndex )
						//									&&	this._RfcDestination.LoadStructureIndexing	( this.BDCIndex )
						//									&&	this._RfcDestination.LoadStructureIndexing	( this.MSGIndex );
					}

			#endregion

		}
}
