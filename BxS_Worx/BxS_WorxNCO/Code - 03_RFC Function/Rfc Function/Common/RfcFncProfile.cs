using System;
//.........................................................
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
						this.FunctionName			= functionName		??	throw		new	ArgumentException( $"{typeof(RfcFncProfile).Namespace}:- Function Name null"	);
						this._RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(RfcFncProfile).Namespace}:- Destination null"		);
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

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public virtual void ReadyProfile()
					{	}

			#endregion

		}
}
