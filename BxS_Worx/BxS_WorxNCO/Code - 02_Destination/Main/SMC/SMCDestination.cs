using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM	= SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main
{
	internal sealed class SMCDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Singleton
				//
				private	static	readonly	Lazy< SMCDestination >	_Instance		= new Lazy< SMCDestination >(	()=>	new SMCDestination() , cz_LM );

				internal static SMCDestination Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private SMCDestination()
					{
					}

			#endregion

			//===========================================================================================
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public SMC.RfcCustomDestination	GetCustomDestination( SMC.RfcConfigParameters rfcConfig )
					{
						return	SDM.GetDestination( rfcConfig ).CreateCustomDestination();
					}

			#endregion

		}
}