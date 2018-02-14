using BxS_SAPNCO.Destination;
using BxS_SAPNCO.RfcFunction;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCTranProcessor	: RFCFunctionBase
		//: IBDCTranProcessor
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTranProcessor( BDCCallTranProfile	profile	)
									: base(	profile.RfcDestination )
					{
						this._Profile	=	profile;
						//.............................................
						this._FncCreated		= false	;
						this._IsConfigured	= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCallTranProfile	_Profile;

				private	bool	_FncCreated		;
				private	bool	_IsConfigured	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Config( DTO_RFCHeader Config )
					{
						if (!this._FncCreated)
							{
								try
									{
										this._RfcFunction	= this._Profile.Metadata.CreateFunction();
										this._FncCreated	= !this._FncCreated;
									}
								catch (System.Exception)
									{
									throw;
									}
							}
						//.............................................
						this._RfcFunction.SetValue(	this._Profile.ParIdx_TCode	,	Config.SAPTCode	)	;
						this._RfcFunction.SetValue(	this._Profile.ParIdx_Skip1	, Config.Skip1st	)	;
						this._RfcFunction.SetValue(	this._Profile.ParIdx_CTUOpt	, Config.CTUParms	)	;
						//.............................................
						this._IsConfigured	= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Process( DTO_RFCTran Transaction )
					{
						if (!this._IsConfigured)	return;
						//.............................................
						try
							{
								this._RfcFunction.SetValue(	this._Profile.ParIdx_TabSPA	, Transaction.SPAData	)	;
								this._RfcFunction.SetValue(	this._Profile.ParIdx_TabBDC	, Transaction.BDCData	)	;
								//.........................................
								Transaction.SuccesStatus	=	this.Invoke();
								Transaction.MSGData				=	this._RfcFunction.GetTable( this._Profile.ParIdx_TabMSG );
							}
						catch (System.Exception)
							{
								Transaction.SuccesStatus	= false;
							}
						finally
							{
								Transaction.ProcessedStatus	= true;
							}
					}

			#endregion

		}
}
