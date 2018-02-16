using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCOpEnv
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpEnv(	DestinationRfc	destRFC
													,	IBDCProfile			profile
													,	BDCOpFnc				opFnc		)
					{
						this.DestRFC	= destRFC	;
						this.Profile	= profile	;
						this.OpFnc		= opFnc		;
						//.............................................
						this.IsStarted	= false;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	BDCOpFnc				OpFnc		{	get; }
				internal	DestinationRfc	DestRFC	{	get; }
				internal	IBDCProfile			Profile	{	get; }

				internal	BDC2RfcParser								Parser				{	get; private set;	}
				internal	IProgress<DTO_ProgressInfo>	ProgressHndlr	{	get; private set;	}
				internal	CancellationTokenSource			CTS						{	get; private set;	}

				internal	PipelineOpEnv	< DTO_RFCTran , DTO_ProgressInfo >	PLOpEnv		{	get; private set;	}
				internal	Pipeline			<	DTO_RFCTran , DTO_ProgressInfo >	Pipeline	{	get; private set;	}

				internal	bool	IsStarted	{ get;	private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Cancel()
					{
						this.CTS?.Cancel();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Start()
					{
						if (this.IsStarted)		return	this.IsStarted;
						//.............................................
						this.Parser					= this.OpFnc.Parser						(this.Profile);
						this.ProgressHndlr	= this.OpFnc.ProgressHndlr		();
						this.CTS						= new	CancellationTokenSource	();
						//.............................................
						this.PLOpEnv	= this.OpFnc.PLOpEnv(	this );
						this.Pipeline	= this.OpFnc.Pipeline(this.PLOpEnv);
						//.............................................
						try
							{
								//if (!this.DestRFC.Procure())		throw	new Exception();

								//BDCProfileConfigurator lo_Cnf	= this.OpFnc.ProfileConfig();
								//if (!lo_Cnf.Configure( this.Profile ))	throw	new Exception();
								//.............................................
								this.IsStarted	= true;
							}
						catch (Exception)
							{
								this.Parser					= null;
								this.ProgressHndlr	= null;
								this.CTS						= null;
							}
						//.............................................
						return	this.IsStarted;
					}

			#endregion

		}
}
