﻿using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcFncBase
		{
			#region "Properties"

				Guid MyID	{	get; }
				//.................................................
				string						SAPFncName			{ get; }
				IRfcFncProfile		Profile					{ get; }
				SMC.IRfcFunction	NCORfcFunction	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				bool	Invoke( SMC.RfcDestination rfcDestination );

			#endregion

		}
}
