using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_Profile : RfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPMsg_Profile( SAPMsg_Factory	factory	)	: base( cz_SAPMsgCompiler )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(SAPMsg_Profile).Namespace}:- Factory null" );
						//.............................................
						this._FNCIndex	=	new Lazy<SAPMsg_IndexFNC>( ()=>	this._Factory.CreateIndexFNC()	);
						this._TXTIndex	=	new	Lazy<SAPMsg_IndexTXT>( ()=>	this._Factory.CreateIndexTXT()	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	SAPMsg_Factory		_Factory;
				//.................................................
				internal	readonly	Lazy<	SAPMsg_IndexFNC	>		_FNCIndex;
				internal	readonly	Lazy<	SAPMsg_IndexTXT >		_TXTIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void ReadyProfile()
					{
						this.LoadFunctionIndex	( this._FNCIndex.Value );
						this.LoadStructureIndex	( this._TXTIndex.Value );
						//.............................................
						base.ReadyProfile();
					}

			#endregion

		}
}