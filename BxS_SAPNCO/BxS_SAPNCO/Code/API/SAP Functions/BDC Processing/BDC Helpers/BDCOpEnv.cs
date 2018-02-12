using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCOpEnv
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpEnv(	DestinationRfc	destination	,
														IBDCProfile			profile			,
														BDCOpFnc				opFnc					)
					{
						this.Destination	= destination		;
						this.Profile			= profile				;
						this.OpFnc				= opFnc					;
						//.............................................
						this.IsStarted	= false;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool	IsStarted	{ get;	private set; }

				internal	BDCOpFnc				OpFnc				{	get; }
				internal	DestinationRfc	Destination	{	get; }
				internal	IBDCProfile			Profile			{	get; }

				internal	BDCProfileConfigurator			Configurator	{	get; private set;	}
				internal	BDC2RfcParser								Parser				{	get; private set;	}
				internal	CancellationTokenSource			CTS						{	get; private set;	}
				internal	IProgress<DTO_ProgressInfo>	ProgressHndlr	{	get; private set;	}

				internal	PipelineOpEnv	< DTO_RFCTran , DTO_ProgressInfo >	PLOpEnv		{	get; private set;	}
				internal	Pipeline			<	DTO_RFCTran , DTO_ProgressInfo >	Pipeline	{	get; private set;	}

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
						this.Configurator		= this.OpFnc.CreateProfileConfigurator	();
						this.Parser					= this.OpFnc.CreateParser								(this.Profile);
						this.ProgressHndlr	= this.OpFnc.CreateProgressHandler			();
						this.CTS						= new	CancellationTokenSource						();
						//.............................................
						this.PLOpEnv	= this.OpFnc.CreatePLOpEnv(	this );
						this.Pipeline	= this.OpFnc.CreatePipeline(this.PLOpEnv);
						//.............................................
						try
							{
								if (!this.Destination.Procure())									throw	new Exception();
								if (!this.Configurator.Configure(	this.Profile ))	throw	new Exception();
								//.............................................
								this.IsStarted	= true;
							}
						catch (Exception)
							{
								this.Configurator		= null;
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
