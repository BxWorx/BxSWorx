using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class BDCTranData	: IBDCTranData
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCTranData(Guid ID	= default(Guid))
					{
						this.ID	= ID.Equals(Guid.Empty)	?	Guid.NewGuid()	:	ID;
						//.............................................
						this.BDCData	= new List<DTO_BDCEntry>();
						this.SPAData	= new List<DTO_SPAEntry>();
						this.MSGData	= new List<DTO_MSGEntry>();
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
				public	int	BDCCount	{ get { return	this.BDCData.Count; } }
				public	int	SPACount	{ get { return	this.SPAData.Count; } }
				public	int	MSGCount	{ get { return	this.MSGData.Count; } }
				//.................................................
				public	string					SAPTCode		{ get;	set;	}
				public	string					Skip1st			{ get;	set;	}
				public	DTO_CTUOptions	CTUOptions	{ get;	set;	}
				//.................................................
				public	IList<DTO_BDCEntry>	BDCData	{ get; }
				public	IList<DTO_SPAEntry>	SPAData	{ get; }
				public	IList<DTO_MSGEntry>	MSGData	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: BDC Data"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void AddBDCData(	string	programName	= BDCConstants.lz_E	,
																	int			dynpro			= 0									,
																	bool		begin				= false							,
																	string	field				= BDCConstants.lz_E	,
																	string	value				= BDCConstants.lz_E		)
						{
							this.AddBDCData(	new	DTO_BDCEntry(	programName, dynpro, begin, field, value ) );
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddBDCData(DTO_BDCEntry entry)
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
							this.SPAData.Add(	new DTO_SPAEntry( memoryID, memoryValue ) );
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddSPAData(DTO_SPAEntry entry)
						{
							this.SPAData.Add(entry);
						}

				#endregion

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: MSG Data"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddMSGData(	string	TCode	,
																	string	DynNm	,
																	string	DynNo	,
																	string	MsgTp	,
																	string	MsgLg	,
																	string	MsgID	,
																	string	MsgNr	,
																	string	MsgV1	,
																	string	MsgV2	,
																	string	MsgV3	,
																	string	MsgV4	,
																	string	Envir	,
																	string	FldNm		)
						{
							this.MSGData.Add(	new DTO_MSGEntry( TCode, DynNm, DynNo, MsgTp, MsgLg, MsgID, MsgNr, MsgV1, MsgV2, MsgV3, MsgV4, Envir, FldNm) );
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	AddMSGData(DTO_MSGEntry entry)
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
