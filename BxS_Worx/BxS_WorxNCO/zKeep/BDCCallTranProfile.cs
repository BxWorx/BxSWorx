﻿using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Common;
using BxS_WorxNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCCall
{
	internal class BDCCallTranProfile : RfcFncProfileBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranProfile(	string							functionName
																		, BDCCallTranIndex		indexer
																		, BDCCallTranParser		parser
																		, BDC_OpFnc						opFnc					)	: base( functionName )
					{
						this.Parser		= parser	;
						this.Indexer	= indexer ;
						this.OpFncts	= opFnc		;
					}

			#endregion

			//===========================================================================================
			#region "Properties:  Parameters Indicies"

				internal	BDCCallTranIndex		Indexer		{ get; }
				internal	BDCCallTranParser		Parser		{ get; }
				internal	BDC_OpFnc						OpFncts		{ get; }
				//.................................................
				internal	SMC.IRfcStructure		GetCTUStr		{	get	{ return	this.Metadata[this.Indexer.ParIdx_CTUOpt].ValueMetadataAsStructureMetadata.CreateStructure()	; } }
				internal	SMC.IRfcTable				GetBDCTbl		{	get	{ return	this.Metadata[this.Indexer.ParIdx_TabBDC].ValueMetadataAsTableMetadata.CreateTable()					; } }
				internal	SMC.IRfcTable				GetSPATbl		{	get	{ return	this.Metadata[this.Indexer.ParIdx_TabSPA].ValueMetadataAsTableMetadata.CreateTable()					; } }
				internal	SMC.IRfcTable				GetMSGTbl		{	get	{ return	this.Metadata[this.Indexer.ParIdx_TabMSG].ValueMetadataAsTableMetadata.CreateTable()					; } }

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
								BDCCallTranIndexSetup lo_IndxCnfg	= this.OpFncts.CreateIndxSetup( this.Metadata );
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
