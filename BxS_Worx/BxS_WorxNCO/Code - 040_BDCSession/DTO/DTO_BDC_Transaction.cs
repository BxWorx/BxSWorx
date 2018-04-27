using System;
using System.Collections.Generic;
//.........................................................
using static	BxS_WorxNCO.Main	.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.DTO
{
	internal class DTO_BDC_Transaction
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Transaction( int tranNo = 0 )
					{
						this.TranNo		= tranNo;
						//.............................................
						this.BDCData	= new List<DTO_BDC_Data>();
						this.SPAData	= new List<DTO_BDC_SPA>	();
						this.MSGData	= new List<DTO_BDC_Msg>	();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid	ID			{ get; }
				public	int   TranNo	{ get; }
				//.................................................
				public	bool	Processed		{ get; set;	}
				public	bool	Successful	{ get; set;	}
				//.................................................
				public	IList< DTO_BDC_Data >	BDCData	{ get; }		//TO-DO: implement BDC Data Collection
				public	IList< DTO_BDC_SPA	>	SPAData	{ get; }
				public	IList< DTO_BDC_Msg	>	MSGData	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDC_Data	CreateDataDTO	()=>	new DTO_BDC_Data();
				public DTO_BDC_SPA	CreateSPADTO	()=>	new	DTO_BDC_SPA()	;
				public DTO_BDC_Msg	CreateMsgDTO	()=>	new DTO_BDC_Msg()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddBDCData(		string	programName	= cz_Null
																,	int			dynpro			= 0
																,	bool		begin				= false
																,	string	field				= cz_Null
																,	string	value				= cz_Null	)
					{
						DTO_BDC_Data lo_Data = this.CreateDataDTO();

						lo_Data.ProgramName		= programName;
						lo_Data.Dynpro				= dynpro.ToString( cz_DefDyn );
						lo_Data.Begin					= begin ? cz_True : cz_False ;
						lo_Data.FieldName			= field;
						lo_Data.FieldValue		= value;

						this.BDCData.Add( lo_Data );
					}

			#endregion

		}
}
