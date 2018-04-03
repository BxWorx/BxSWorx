using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigLogonx : ConfigBase , IConfigLogon
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ConfigLogonx( bool forRepository = false )
					{
						this._ForRepository	= forRepository;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	bool	_ForRepository;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	Language	{ set { this.Settings[ SMC.RfcConfigParameters.Language	]	= value	; } }
				//.................................................
				public	string	Client		{ set { this.Settings[ SMC.RfcConfigParameters.Client		]	= value	; } }
				//.................................................
				public	string	User			{ set	{ this.Settings[	this._ForRepository ? SMC.RfcConfigParameters.RepositoryUser
																																							:	SMC.RfcConfigParameters.User								] = value	; } }
				//.................................................
				public	string	Password	{ set	{ this.Settings[	this._ForRepository ? SMC.RfcConfigParameters.RepositoryPassword
																																							:	SMC.RfcConfigParameters.Password						] = value	; } }
				//.................................................
				public	SecureString	SecurePassword	{ get; set;	}

			#endregion

		}
}
