using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public interface IFavourites
		{
			#region "Properties"

				int Count	{	get;	}
				int Max		{	get;	set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				IList<IDTOFavourite>	List();
				IDTOFavourite					Create(Guid ID = default(Guid));
				//.................................................
				void Add(IDTOFavourite DTO);

			#endregion

		}
}