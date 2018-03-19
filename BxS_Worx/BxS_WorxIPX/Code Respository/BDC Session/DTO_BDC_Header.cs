using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public class DTO_BDC_Header
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDC_Header( DTO_BDC_CTU ctuDTO )
					{
						this.CTUParms	= ctuDTO;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid	ID		{ get; set;	}
				public	Guid	SAPID	{ get; set;	}
				//.................................................
				public	string	SAPTCode	{ get; set;	}
				public	bool		Skip1st		{ get; set;	}
				//.................................................
				public	DTO_BDC_CTU	CTUParms	{ get; }

			#endregion

		}
}
