using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC.Session
{
	internal class DTO_RFCSessionHeader
		{
			#region "Properties"

				internal	string	SAPTCode	{ get;	set; }
				internal	string	Skip1st		{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUOpts	{ get;	set; }

			#endregion

		}
}
