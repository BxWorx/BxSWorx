using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class BDCCallTransaction : IBDCCallTransaction
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTransaction(	IRFCFunction	RfcFunction	,
																			DTO_CTUParams	dto_CTUParm	,
																			DTO_BDCData		dto_BDCData	,
																			DTO_SPAData		dto_SPAData ,
																			DTO_MsgData		dto_MsgData		)
					{
						this._RFCFunction	= RfcFunction	;
						this.DTO_CTUParm	= dto_CTUParm	;
						this.DTO_BDCData	= dto_BDCData	;
						this.DTO_SPAData	= dto_SPAData	;
						this.DTO_MsgData	= dto_MsgData	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRFCFunction	_RFCFunction		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	DTO_CTUParams		DTO_CTUParm	{ get; }
				public	DTO_BDCData			DTO_BDCData	{ get; }
				public	DTO_SPAData			DTO_SPAData	{ get; }
				public	DTO_MsgData			DTO_MsgData	{ get; }

				public	string	RFCFunctionName	{ get	{ return	this._RFCFunction.Name	;	} }
				public	int			BDCDataCount		{ get	{ return	this.DTO_BDCData.Count	;	} }
				public	int			SPADataCount		{ get	{ return	this.DTO_SPAData.Count	;	} }
				public	int			MsgDataCount		{ get	{ return	this.DTO_MsgData.Count	;	} }

				//public	SMC.RfcDestination	RfcDestination	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Invoke()
					{
						bool	lb_Ret	= true;
						//.............................................
						try
							{
								this._RFCFunction.Invoke();
							}
						catch (System.Exception)
							{
								lb_Ret	= false;
							}
						//.............................................
						return	lb_Ret;
					}






				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SPAEntry CreateSPAEntry(	string	MemoryID		,
																						string	MemoryValue	,
																						bool		autoAdd			= true )
					{
						var	lo_Entry	= new DTO_SPAEntry(	MemoryID, MemoryValue );

						if (autoAdd)	this.DTO_SPAData.Add(lo_Entry);
						//.............................................
						return	lo_Entry;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddSPAEntry(DTO_SPAEntry entry)
					{
						this.DTO_SPAData.Add(entry);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDCEntry CreateBDCEntry(	string	programName	= BDCConstants.lz_E	,
																						int			dynpro			= 0									,
																						bool		begin				= false							,
																						string	field				= BDCConstants.lz_E	,
																						string	value				= BDCConstants.lz_E	,
																						bool		autoAdd			= true								)
					{
						var	lo_Entry	= new DTO_BDCEntry(	programName, dynpro, begin, field, value );

						if (autoAdd)	this.DTO_BDCData.Add(lo_Entry);
						//.............................................
						return	lo_Entry;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddBDCEntry(DTO_BDCEntry entry)
					{
						this.DTO_BDCData.Add(entry);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this.DTO_BDCData.Reset();
						this.DTO_SPAData.Reset();
						this.DTO_MsgData.Reset();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadMessages()
					{
						this.DTO_MsgData.Reset();
						this._RFCFunction.RfcFunction.GetTable()


					}

			#endregion

		}
}
