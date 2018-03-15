using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Header
		{
			#region "Properties"

				internal	string	SAPTCode	{ get;	set; }
				internal	bool		Skip1st		{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUParms	{ get;	set; }

			#endregion

		}
}
