using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public class DTO_BDC_Trans
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDC_Trans()
					{
						this.BDCData	= new List<	DTO_BDC_Data >();
						this.SPAData	= new List<	DTO_BDC_SPA >	();
						this.MSGData	= new List<	DTO_BDC_Msg >	();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid	ID	{ get; }
				//.................................................
				public	bool	Processed		{ get; set;	}
				public	bool	Successful	{ get; set;	}
				//.................................................
				public	IList< DTO_BDC_Data >	BDCData	{ get; }
				public	IList< DTO_BDC_SPA	>	SPAData	{ get; }
				public	IList< DTO_BDC_Msg	>	MSGData	{ get; }

			#endregion

		}
}
