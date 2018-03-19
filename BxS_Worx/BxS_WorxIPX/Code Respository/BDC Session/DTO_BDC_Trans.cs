using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public class DTO_BDC_Trans
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDC_Trans( int tranNo = 0 )
					{
						this.TranNo		= tranNo;
						//.............................................
						this.BDCData	= new List<	DTO_BDC_Data >();
						this.SPAData	= new List<	DTO_BDC_SPA >	();
						this.MSGData	= new List<	DTO_BDC_Msg >	();
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
				public	IList< DTO_BDC_Data >	BDCData	{ get; }
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
				public void AddBDCData( DTO_BDC_Data dto )
					{
						this.BDCData.Add( dto );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddBDCData(		string	programName	= IPX_Constants.cz_Null
																,	int			dynpro			= 0
																,	bool		begin				= false
																,	string	field				= IPX_Constants.cz_Null
																,	string	value				= IPX_Constants.cz_Null	)
					{
						DTO_BDC_Data lo_Data = this.CreateDataDTO();

						lo_Data.ProgramName		= programName;
						lo_Data.Dynpro				= dynpro;
						lo_Data.Begin					= begin;
						lo_Data.FieldName			= field;
						lo_Data.FieldValue		= value;

						this.BDCData.Add( lo_Data );
					}

			#endregion

		}
}
