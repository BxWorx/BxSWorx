using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal abstract class RfcFncProfile : IRfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncProfile(		string					functionName
																,	IRfcDestination rfcDestination )
					{
						this.FunctionName			= functionName;
						this._RfcDestination	= rfcDestination;
						//.............................................
						this.IsReady	= false;
						//.............................................
						this._Lock		= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	object						_Lock;
				protected	readonly	IRfcDestination		_RfcDestination;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	FunctionName	{	get; }
				public	bool		IsReady				{ get; set; }
				//.................................................
				public	SMC.RfcCustomDestination	NCODestination	{ get { return	this._RfcDestination.NCODestination; } }

			#endregion

			//===========================================================================================
			#region "Methods"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ReadyProfile()
					{
						if ( ! this.IsReady )
							{
								this._RfcDestination.LoadRfcFunctionProfileMetadata( this );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcFunction	CreateRfcFunction()
					{
						return	this._RfcDestination.CreateRfcFunction( this.FunctionName );
					}

			#endregion

		}
}
