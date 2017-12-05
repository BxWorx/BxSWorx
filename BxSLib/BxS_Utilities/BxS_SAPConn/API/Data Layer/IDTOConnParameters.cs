using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPConn.API
{
	public interface IDTOConnParameters
		{
			#region "Properties"

				Guid												ID					{ get; }
				Dictionary<string, string>	Parameters	{	get; }

			#endregion

		}
}