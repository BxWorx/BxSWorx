using System;
using System.Threading;
//.........................................................
using	BxS_WorxIPX.Helpers;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Config;
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main
{
	internal sealed class Destination_Factory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Singleton
				//
				private	static readonly	Lazy< Destination_Factory >	_Instance	= new Lazy< Destination_Factory >( ()=>	new	Destination_Factory() , cz_LM );
				//
				internal static Destination_Factory Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Destination_Factory()
					{
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public IConfigLogon					CreateLogonConfig				()=>	new ConfigLogon()				;
				public IConfigDestination		CreateDestinationConfig	()=>	new ConfigDestination()	;
				public IConfigGlobal				CreateGlobalConfig			()=>	new ConfigGlobal()			;

			#endregion

		}
}