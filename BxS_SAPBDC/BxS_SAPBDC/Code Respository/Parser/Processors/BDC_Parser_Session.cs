using System;
//.........................................................
using	BxS_SAPBDC.BDC;
using	BxS_SAPIPX.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Parser_Session
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Parser_Session(	Lazy< BDC_Parser_Factory > factory )
					{
						this._Factory	= factory;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly Lazy< BDC_Parser_Factory > 	_Factory;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	DTO_BDCSessionRequest dtoRequest
															,	DTO_ParserProfile				dtoProfile
															, BDC_Session						Session			)
					{
						Session.SessionHeader.Client		= dtoRequest.Client;
						Session.SessionHeader.Lang			= dtoRequest.Lang;
						Session.SessionHeader.Name			= dtoRequest.WSID;
						Session.SessionHeader.Pwrd			= dtoRequest.Pwrd;
						Session.SessionHeader.SAPSysID	= dtoRequest.SAPSysID;
						Session.SessionHeader.User			= dtoRequest.User;

						Session.SessionHeader.SAPTCode	= dtoProfile.XMLConfig.SAPTCode	;
						Session.SessionHeader.Skip1st		= dtoProfile.XMLConfig.Skip1st	;
					}

			#endregion

		}
}
