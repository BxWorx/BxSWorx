using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions
{
	public class BDCCallTransaction : IBDCCallTransaction
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTransaction(	IRFCFunction	RfcFunction				,
																			BDCData				Data							,
																			BDCCTU_Parameters	CTUParameters		)
					{
						this._RFCFunction		= RfcFunction		;
						this._BDCData				= Data					;
						this._CTUParameters	= CTUParameters	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRFCFunction				_RFCFunction		;
				private readonly	BDCData							_BDCData				;
				private	readonly	BDCCTU_Parameters		_CTUParameters	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	Name	{ get;																	}
				public	int			Count	{ get	{ return	this._BDCData.Count;	} }

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
						bool lb_Fail	= false;
						var lo_Entry	= new BDCEntry(	programName, dynpro, begin, field, value );
						if (autoAdd)	lb_Fail = !this._BDCData.Add(lo_Entry);
						if (lb_Fail)	lo_Entry.Reset();
						return	lo_Entry;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCEntry CreateBDCEntry(	string	programName	= BDCConstants.lz_E	,
																				string	dynpro			= BDCConstants.lz_D	,
																				string	begin				= BDCConstants.lz_F	,
																				string	field				= BDCConstants.lz_E	,
																				string	value				= BDCConstants.lz_E	,
																				bool		autoAdd			= true								)
					{
						bool lb_Fail	= false;
						var lo_Entry	= new BDCEntry(	programName, dynpro, begin, field, value );
						if (autoAdd)	lb_Fail = !this._BDCData.Add(lo_Entry);
						if (lb_Fail)	lo_Entry.Reset();
						return	lo_Entry;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddBDCEntry(BDCEntry entry)
					{
						return	this._BDCData.Add(entry);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this._BDCData.Reset();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
