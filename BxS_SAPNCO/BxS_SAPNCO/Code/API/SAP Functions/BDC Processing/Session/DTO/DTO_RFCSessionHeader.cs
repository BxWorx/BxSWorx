using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class DTO_RFCSessionHeader
		{
			#region "Properties"

				public	string	SAPTCode	{ get;	set; }
				public	string	Skip1st		{ get;	set; }
				//.................................................
				public	SMC.IRfcStructure		CTUOpts	{ get;	set; }

			#endregion

		}
}
