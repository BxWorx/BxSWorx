﻿using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal interface IRfcFncManager
		{
			#region "Properties"

				SMC.RfcRepository	NCORepository { get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void RegisterProfile	( IRfcFncProfile	rfcFncProfile );
				bool PrepareFunction	( IRfcFncBase			rfcFunc );

				void LoadFncParmIndex<T>( T fncParmIndex );

			#endregion

		}
}
