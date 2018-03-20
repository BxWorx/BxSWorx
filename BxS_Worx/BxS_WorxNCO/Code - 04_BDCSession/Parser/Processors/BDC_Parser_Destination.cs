using System;
//.........................................................
using BxS_WorxIPX.API.BDC;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class BDC_Parser_Destination : BDC_Parser_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Parser_Destination(	Lazy< BDC_Parser_Factory > factory ) : base( factory )
					{	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	IBDCSessionRequest	dtoRequest
															, DTO_BDC_Session			Session			)
					{
						IConfigSetupDestination lo_Cfg	= this._Factory.Value.CreateDestConfig();
						//.............................................
						lo_Cfg.Client		= int.Parse( dtoRequest.Client );
						lo_Cfg.Language	= dtoRequest.Lang;
						lo_Cfg.Password	= dtoRequest.Pwrd;
						lo_Cfg.User			= dtoRequest.User;
						//.............................................
						lo_Cfg.IdleTimeout			=	dtoRequest.IdleTimeout			;
						lo_Cfg.IdleCheckTime		=	dtoRequest.IdleCheckTime		;
						lo_Cfg.MaxPoolWaitTime	=	dtoRequest.MaxPoolWaitTime	;
						lo_Cfg.PeakConnLimit		=	dtoRequest.PeakConnLimit		;
						lo_Cfg.PoolIdleTimeout	=	dtoRequest.PoolIdleTimeout	;
						lo_Cfg.PoolSize					=	dtoRequest.PoolSize					;
						lo_Cfg.RepoIdleTimeout	=	dtoRequest.RepoIdleTimeout	;
						lo_Cfg.DoLogonCheck			= dtoRequest.DoLogonCheck			;
						//.............................................
						Session.DestConfig	= lo_Cfg;
					}

			#endregion

		}
}
