using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public class DTO_BDC_Header
		{
			#region "Properties"

				public	Guid	ID		{ get; set;	}
				public	Guid	SAPID	{ get; set;	}
				//.................................................
				public	string	SAPTCode	{ get; set;	}
				public	string	Skip1st		{ get; set;	}
				//.................................................
				public	DTO_BDC_CTU	CTUParms	{ get; set; }

			#endregion

		}
}
