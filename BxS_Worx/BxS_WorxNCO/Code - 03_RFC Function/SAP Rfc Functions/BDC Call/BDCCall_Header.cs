using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Header
		{
			#region "Properties"

				internal	bool		ShowSAPGui	{ get;	set; }
				//.................................................
				internal	string	SAPTCode		{ get;	set; }
				internal	bool		Skip1st			{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUParms	{ get;	set; }

			#endregion

		}
}
