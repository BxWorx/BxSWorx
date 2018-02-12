using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Common;
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.BDCProcess;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public NCOController(	bool	loadSAPGUIConfig	= true	,
															bool	firstReset				= false	,
															bool	autoLoad					= false		)
					{
						this._LoadSAPGUICfg		= loadSAPGUIConfig;
						this._FirstReset			= firstReset;
						//.............................................
						this._Started		= false;
						//.............................................
						if (autoLoad)	this.Startup();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private bool	_Started;
				//.................................................
				private	readonly	bool	_LoadSAPGUICfg;
				private	readonly	bool	_FirstReset;
				//.................................................
				private static readonly
					Lazy<SAPFncConstants>					_SAPFncConst
						= new	Lazy<SAPFncConstants>				(	() => new SAPFncConstants()
																								, LazyThreadSafetyMode.ExecutionAndPublication );

				private	readonly
					Lazy<NCOCntlr_Destination>		_Cntlr_Dest
						= new	Lazy<NCOCntlr_Destination>	(	() => new NCOCntlr_Destination()
																								, LazyThreadSafetyMode.ExecutionAndPublication );

				private	readonly
					Lazy<NCOController_BDC>		_Cntlr_BDC
						= new	Lazy<NCOController_BDC>			(	() => new NCOController_BDC(_SAPFncConst)
																								, LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

		}
}
