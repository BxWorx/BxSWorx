using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.RfcFunction
{
	internal abstract class RfcFncProfileBase : IRfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncProfileBase(	string	functionName	)
					{
						this.FunctionName	= functionName;
						//.............................................
						this._IsReady	= false;
						this._Lock	= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				protected bool _IsReady;

				private readonly object		_Lock;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	FunctionName	{	get; }
				public	bool		IsReady				{ get { return	this._IsReady; } }
				//.................................................
				public	DestinationRfc						DestinationRfc	{ get; set ; }

				public	SMC.RfcDestination				RfcDestination	{ get	{ return	this.DestinationRfc
																																								.RfcDestination; } }

				public	SMC.RfcFunctionMetadata		Metadata				{ get { return 	this.RfcDestination
																																								.Repository
																																									.GetFunctionMetadata( this.FunctionName ); } }

			#endregion

			//===========================================================================================
			#region "Methods"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ReadyProfile()
					{
						if (this.IsReady)		return;
						//.............................................
						lock (this._Lock)
							{
								if (this.IsReady)										return;
								if (this.DestinationRfc == null)		return;
								if (!this.DestinationRfc.IsProcured)	this.DestinationRfc.Procure();
								//.........................................
								if (this.DestinationRfc.IsProcured)
									{
										if (this.DestinationRfc.Ping())
											{
												this.SetupProfile();
												this._IsReady	= true;
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected virtual void SetupProfile()
					{
					}

			#endregion

		}
}
