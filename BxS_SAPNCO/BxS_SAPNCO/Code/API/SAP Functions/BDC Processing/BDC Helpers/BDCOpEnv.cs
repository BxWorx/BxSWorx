using System;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC.Session;
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class BDCOpEnv
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpEnv(	DestinationRfc							destination							,
														IBDCProfile									profile									,
														BDC2RfcParser								parser									,
														BDCProfileConfigurator			configurator						,
														Func<Guid, BDCSessionTran>	createBDCSessionTran		,
														Func<DTO_RFCSessionHeader>	createRFCSessionHeader		)
					{
						this.Destination							= destination		;
						this.Profile									= profile				;
						this.Parser										= parser				;
						this.Configurator							= configurator	;

						this.CreateSessionBDCTran		= createBDCSessionTran		;
						this.CreateSessionRFCHeader	= createRFCSessionHeader	;
						//.............................................
						this.IsStarted	= false;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool	IsStarted	{ get;	private set; }

				internal	DestinationRfc						Destination		{	get; }
				internal	BDCProfileConfigurator		Configurator	{	get; }
				internal	BDC2RfcParser							Parser				{	get; }
				internal	IBDCProfile								Profile				{	get; }

				internal Func<Guid, BDCSessionTran>	CreateSessionBDCTran		{ get; }
				internal Func<DTO_RFCSessionHeader>	CreateSessionRFCHeader	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Start()
					{
						if (this.IsStarted)	return;
						//.............................................
						this.Destination.Procure();
						this.Configurator.Configure(this.Profile);
						//.............................................
						this.IsStarted	= true;
					}

			#endregion

		}
}
