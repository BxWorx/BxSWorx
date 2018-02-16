using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.RfcFunction;
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranProfile : RfcFncProfileBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranProfile(	DestinationRfc					destRfc
																		,	string									functionName
																		, BDCCallTranIndex				indexer
																		, Func< DTO_RFCHeader >		createRfcHead
																		, Func< DTO_RFCTran		>		createRFCTran

																		, Func<		SMC.RfcFunctionMetadata
																						, BDCCallTranIndexSetup		>		CreateIndexConfigurator	)	: base( destRfc , functionName )
					{
						this.DestinationRfc.RegisterProfile(this);
						//.............................................
						this.Indexer				= indexer									;
						this.CreateRfcHead	= createRfcHead						;
						this.CreateRFCTran	= createRFCTran						;
						//.............................................
						this.CreateIdxCnfg	= CreateIndexConfigurator	;
					}

			#endregion

			//===========================================================================================
			#region "Properties:  Parameters Indicies"

				internal	Func< DTO_RFCHeader >		CreateRfcHead		{ get; }
				internal	Func< DTO_RFCTran		>		CreateRFCTran		{ get; }

				internal  Func<		SMC.RfcFunctionMetadata
												,	BDCCallTranIndexSetup		>		CreateIdxCnfg		{ get; }
				//.................................................
				internal	BDCCallTranIndex		Indexer		{ get; }
				//.................................................
				internal	SMC.IRfcStructure		GetCTUStr	{	get	{ return	this.Metadata[this.Indexer.ParIdx_CTUOpt].ValueMetadataAsStructureMetadata.CreateStructure()	; } }
				internal	SMC.IRfcTable				GetBDCTbl	{	get	{ return	this.Metadata[this.Indexer.ParIdx_TabBDC].ValueMetadataAsTableMetadata.CreateTable()					; } }
				internal	SMC.IRfcTable				GetSPATbl	{	get	{ return	this.Metadata[this.Indexer.ParIdx_TabSPA].ValueMetadataAsTableMetadata.CreateTable()					; } }
				internal	SMC.IRfcTable				GetMSGTbl	{	get	{ return	this.Metadata[this.Indexer.ParIdx_TabMSG].ValueMetadataAsTableMetadata.CreateTable()					; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Configure(BDCCallTranProcessor processor)
					{
						if (this.Ready())
							{
								processor.Header.CTUParms		= this.GetCTUStr;
								//.........................................
								processor.Transaction.BDCData		= this.GetBDCTbl;
								processor.Transaction.SPAData		= this.GetSPATbl;
								processor.Transaction.MSGData		= this.GetMSGTbl;
							}
						//.............................................
						return	this.IsReady;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override bool Setup()
					{
						try
							{
								BDCCallTranIndexSetup lo_IndxCnfg	= this.CreateIdxCnfg( this.Metadata );
								lo_IndxCnfg.Configure( this.Indexer );
								return	true;
							}
						catch (System.Exception)
							{
								return	false;
							}
					}

			#endregion

		}
}
