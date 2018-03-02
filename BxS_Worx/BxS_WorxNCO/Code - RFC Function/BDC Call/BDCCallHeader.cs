using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCCall
{
	internal class BDCCallHeader
		{
			#region "Properties"

				internal	string	SAPTCode	{ get;	set; }
				internal	string	Skip1st		{ get;	set; }
				//.................................................
				internal	SMC.IRfcStructure		CTUParms	{ get;	set; }

			#endregion

		}
}
