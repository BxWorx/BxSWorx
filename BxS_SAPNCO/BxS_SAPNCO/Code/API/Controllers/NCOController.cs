using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC;
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
				private	readonly
					Lazy<SAPFncConstants>		_SAPFncConst
						= new	Lazy<SAPFncConstants>(	() => new SAPFncConstants()
																					, LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"
			#endregion

		}
}
