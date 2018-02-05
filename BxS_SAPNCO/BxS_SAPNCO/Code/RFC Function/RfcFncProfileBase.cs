using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.Function
{
	internal class RfcFncProfileBase : IRfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncProfileBase(	DestinationRfc	destinationRFC	,
																		string					functionName			)
					{
						this.FunctionName			= functionName;
						this._DestinationRFC	= destinationRFC;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	DestinationRfc	_DestinationRFC;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	FunctionName	{	get; }
				//.................................................
				public	SMC.RfcDestination				RfcDestination	{ get	{ return	this._DestinationRFC
																																								.RfcDestination; } }

				public	SMC.RfcFunctionMetadata		Metadata				{ get { return	this.RfcDestination
																																								.Repository
																																									.GetFunctionMetadata(this.FunctionName); } }

			#endregion

		}
}
