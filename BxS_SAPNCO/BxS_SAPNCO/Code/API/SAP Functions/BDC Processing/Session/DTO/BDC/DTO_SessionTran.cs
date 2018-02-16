using System;
using System.Collections.Generic;
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class DTO_SessionTran
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionTran(Guid ID	= default(Guid))
					{
						this.ID	= ID.Equals(Guid.Empty)	?	Guid.NewGuid() : ID;
						//.............................................
						this.BDCData	= new List<	DTO_SessionTranData	>	();
						this.SPAData	= new List<	DTO_SessionTranSPA	>	();
						this.MSGData	= new List<	DTO_SessionTranMsg	>	();
						//.............................................
						this.ProcessedStatus	= false;
						this.SuccesStatus			= false;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid	ID	{ get; }
				//.................................................
				public	bool	ProcessedStatus	{ get; set;	}
				public	bool	SuccesStatus		{ get; set;	}
				//.................................................
				public	int		BDCCount	{ get { return	this.BDCData.Count; } }
				public	int		SPACount	{ get { return	this.SPAData.Count; } }
				public	int		MSGCount	{ get { return	this.MSGData.Count; } }
				//.................................................
				public	IList< DTO_SessionTranData >	BDCData	{ get; }
				public	IList< DTO_SessionTranSPA	 >	SPAData	{ get; }
				public	IList< DTO_SessionTranMsg	 >	MSGData	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: BDC Data"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void AddBDCData(	string	programName	= BDC_Constants.lz_E	,
																	int			dynpro			= 0									,
																	bool		begin				= false							,
																	string	field				= BDC_Constants.lz_E	,
																	string	value				= BDC_Constants.lz_E		)
						{
							this.AddBDCData( new	DTO_SessionTranData(	programName, dynpro, begin, field, value ) );
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddBDCData(DTO_SessionTranData entry)
						{
							this.BDCData.Add(entry);
						}

				#endregion

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: SPA Data"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddSPAData(	string	memoryID		,
																	string	memoryValue		)
						{
							this.SPAData.Add(	new DTO_SessionTranSPA( memoryID, memoryValue ) );
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddSPAData(DTO_SessionTranSPA entry)
						{
							this.SPAData.Add(entry);
						}

				#endregion

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: MSG Data"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddMSGData(	string	TCode	,
																	string	DynNm	,	string	DynNo	,
																	string	MsgTp	,	string	MsgLg	,
																	string	MsgID	,	string	MsgNr	,
																	string	MsgV1	,	string	MsgV2	,	string	MsgV3	,	string	MsgV4	,
																	string	Envir	,	string	FldNm																		)
						{
							this.MSGData.Add(	new DTO_SessionTranMsg( TCode,
																												DynNm, DynNo,
																												MsgTp, MsgLg,
																												MsgID, MsgNr,
																												MsgV1, MsgV2, MsgV3, MsgV4,
																												Envir, FldNm								) );
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddMSGData(DTO_SessionTranMsg entry)
						{
							this.MSGData.Add(entry);
						}

				#endregion

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: General"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	Reset()
						{
							this.BDCData.Clear();
							this.SPAData.Clear();
							this.MSGData.Clear();
						}

				#endregion

			#endregion

		}
}
