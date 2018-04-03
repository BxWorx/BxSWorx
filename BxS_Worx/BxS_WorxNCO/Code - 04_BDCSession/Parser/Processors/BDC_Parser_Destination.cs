using System;
//.........................................................
using BxS_WorxIPX.BDC;

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
				internal void	Process(	IExcelBDCSessionRequest	dtoRequest
															, IConfigDestination sessionCfg )
					{
						//sessionCfg.Client		= int.Parse( dtoRequest.Client );
						//sessionCfg.Language	= dtoRequest.Lang;
						//sessionCfg.Password	= dtoRequest.Pwrd;
						//sessionCfg.User			= dtoRequest.User;
						////.............................................
						//sessionCfg.IdleTimeout			=	dtoRequest.IdleTimeout			;
						//sessionCfg.IdleCheckTime		=	dtoRequest.IdleCheckTime		;
						//sessionCfg.MaxPoolWaitTime	=	dtoRequest.MaxPoolWaitTime	;
						//sessionCfg.PeakConnLimit		=	dtoRequest.PeakConnLimit		;
						//sessionCfg.PoolIdleTimeout	=	dtoRequest.PoolIdleTimeout	;
						//sessionCfg.PoolSize					=	dtoRequest.PoolSize					;
						//sessionCfg.RepoIdleTimeout	=	dtoRequest.RepoIdleTimeout	;
						//sessionCfg.DoLogonCheck			= dtoRequest.DoLogonCheck			;
					}

			#endregion

		}
}
