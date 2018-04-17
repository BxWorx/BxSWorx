using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcFncProfile
		{
			#region "Properties"

				string	FunctionName		{	get; }
				bool		IsReady					{ get; set; }
				//.................................................
				SMC.RfcFunctionMetadata		Metadata	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void ReadyProfile();

				void LoadFunctionIndex	( IRfcFunctionIndex		index );
				bool LoadStructureIndex	( IRfcStructureIndex	index );

				SMC.IRfcFunction	CreateFunction();

			#endregion

		}
}
