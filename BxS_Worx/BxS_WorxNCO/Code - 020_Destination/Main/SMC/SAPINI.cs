using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main
{
	internal sealed class SAPINI
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Singleton
				//
				private	static readonly	Lazy< SAPINI >		_Instance		= new Lazy< SAPINI >( ()=>	new SAPINI() , cz_LM );

				internal static SAPINI Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private SAPINI()
					{
						this._SAPINI	= new Lazy< SMC.SapLogonIniConfiguration >(	()=> SMC.SapLogonIniConfiguration.Create() , cz_LM);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< SMC.SapLogonIniConfiguration >	_SAPINI;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList<string>	GetSAPINIList()	=>	this._SAPINI.Value.GetEntries();
				//...
				public SMC.RfcConfigParameters	GetIniParameters( string lc_SAPID )	=>	this._SAPINI.Value.GetParameters( lc_SAPID )	?? new SMC.RfcConfigParameters();

			#endregion

		}
}