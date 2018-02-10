using System;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.Function;
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Methods: Internal: BDC Processing"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpEnv	CreateBDCOpEnv(Guid destID)
					{
						DestinationRfc					lo_DS	= this.CreateDestinationRFC			(destID)	;
						IBDCProfile							lo_PR	= this.GetAddBDCProfile					(lo_DS)		;
						BDC2RfcParser						lo_PS	= this.CreateBDC2RfcParser			(lo_PR)		;
						BDCProfileConfigurator	lo_PC	= this.CreateProfileConfigurator()				;

						return	new BDCOpEnv( lo_DS, lo_PR, lo_PS, lo_PC		,
																	this.CreateSessionTransaction	,
																	this.CreateSessionRFCHeader			);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IBDCTranData	CreateBDCTranData(Guid ID)
					{
						return	new BDCTranData(ID)	{	CTUOptions = new DTO_CTUOptions()	};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IBDCProfile GetAddBDCProfile(DestinationRfc destination)
					{
						IBDCProfile	lo_Profile	= null;
						//.............................................
						destination.TryGetProfile(	this._SAPFncConst.Value.BDCCallTransaction	,
																				out object lo_ProfileObj											);

						if (lo_ProfileObj == null)
							{
								lo_Profile	= new BDCFncProfile(	destination																	,
																									this._SAPFncConst.Value.BDCCallTransaction		);
								destination.RegisterProfile(lo_Profile);
								destination.TryGetProfile(	this._SAPFncConst.Value.BDCCallTransaction	,
																						out lo_ProfileObj															);
							}
						//.............................................
						return	lo_Profile;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_RFCData CreateRFCTranData(	IBDCProfile	profile	)
					{
						return	new DTO_RFCData()	{	CTUOpts	= profile.CTUStr	,
																				BDCData = profile.BDCTbl	,
																				SPAData = profile.SPATbl	,
																				MSGData = profile.MSGTbl		};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC2RfcParser CreateBDC2RfcParser(IBDCProfile	profile)
					{
						return	new BDC2RfcParser(profile);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IBDCTranProcessor CreateBDCTransactionProcessor(IBDCProfile profile)
					{
						return	new BDCTranProcessor( new RFCFunction()	, profile);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCProfileConfigurator	CreateProfileConfigurator()
					{
						return	new BDCProfileConfigurator();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal CTU_Parameters	CreateCTUParameters()
					{
						return	new CTU_Parameters();
					}

			#endregion

		}
}
