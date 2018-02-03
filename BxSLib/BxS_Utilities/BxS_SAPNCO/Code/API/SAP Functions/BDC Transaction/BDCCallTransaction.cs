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
				internal BDCCallTransaction(	IRFCFunction	RfcFunction		,
																			DTO_CTUParams	dto_CTUParms	,
																			BDCData						BDCData			)
					{
						this._RFCFunction	= RfcFunction		;
						this.DTO_CTUParms	= dto_CTUParms	;

						this.BDCData				= BDCData				;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRFCFunction				_RFCFunction		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	BDCData						BDCData					{ get; }
				public	DTO_CTUParams		DTO_CTUParms	{ get; }

				public	string	Name	{ get	{ return	this._RFCFunction.Name	;	} }
				public	int			Count	{ get	{ return	this.BDCData.Count			;	} }

				//public	SMC.IRfcFunction		RfcFunction			{ get; set; }
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
				public BDCEntry CreateBDCEntry(	string	programName	= BDCConstants.lz_E	,
																				int			dynpro			= 0									,
																				bool		begin				= false							,
																				string	field				= BDCConstants.lz_E	,
																				string	value				= BDCConstants.lz_E	,
																				bool		autoAdd			= true								)
					{
						bool	lb_Fail		= false;
						var		lo_Entry	= new BDCEntry(	programName, dynpro, begin, field, value );
						//.............................................
						if (autoAdd)
							{	lb_Fail = !this.BDCData.Add(lo_Entry);
								if (lb_Fail)	lo_Entry.Reset();
							}
						//.............................................
						return	lo_Entry;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddBDCEntry(BDCEntry entry)
					{
						return	this.BDCData.Add(entry);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this.BDCData.Reset();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
