using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPConn.API
{
	internal class DTOConnParameters : IDTOConnParameters
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTOConnParameters()
					{
						this.ID					= Guid.NewGuid();
						this.Parameters	= new	Dictionary<string, string>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid													ID					{ get; }
				public Dictionary<string, string>		Parameters	{	get; }

			#endregion

		}
}
