using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigLogon : ConfigBase , IConfigLogon
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ConfigLogon( bool forRepository = false )
					{
						this.ForRepository	= forRepository	;
						//.............................................
						if ( forRepository )
							{
								this._User			=	SMC.RfcConfigParameters.RepositoryUser			;
								this._Password	=	SMC.RfcConfigParameters.RepositoryPassword	;
							}
						else
							{
								this._User			=	SMC.RfcConfigParameters.User			;
								this._Password	=	SMC.RfcConfigParameters.Password	;
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly string		_User						;
				private readonly string		_Password				;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	ForRepository { get; }
				//.................................................
				public	string	Language	{ set { this.Set( SMC.RfcConfigParameters.Language	, value	); } }
				public	string	Client		{ set	{ this.Set( SMC.RfcConfigParameters.Client		, value	); } }
				//.................................................
				public	string	User			{ set { this.Set( this._User			, value	); } }
				public	string	Password	{ set { this.Set( this._Password	, value	); } }
				//.................................................
				public	SecureString	SecurePassword	{ get; set;	}

			#endregion

		}
}
