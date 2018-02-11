using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class DTO_RFCHeader
		{
			#region "Properties"

				internal	string	SAPTCode	{ get;	set; }
				internal	string	Skip1st		{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUParms	{ get;	set; }

			#endregion

		}
}
