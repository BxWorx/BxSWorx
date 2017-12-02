using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public interface IFavourites
		{
			#region "Properties"

				IList<IDTOFavourite>	List	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				IDTOFavourite Create(Guid ID = default(Guid));
				//.................................................
				void Add(IDTOFavourite DTO);

			#endregion

		}
}