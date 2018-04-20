using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.DDIC
{
	internal class DDICInfo_Profile : RfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DDICInfo_Profile( DDICInfo_Factory	factory	)	: base( cz_DDICInfo )
					{
						this._Factory		= factory	??	throw		new	ArgumentException( $"{typeof(DDICInfo_Profile).Namespace}:- Factory null" );
						//.............................................
						this._FNCIndex	=	new Lazy<DDICInfo_IndexFNC>		( ()=>	this._Factory.CreateIndexFNC()	);
						this._DFSIndex	=	new	Lazy<DDICInfo_IndexDFIES>	( ()=>	this._Factory.CreateIndexDFS()	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	DDICInfo_Factory		_Factory;
				//.................................................
				internal	readonly	Lazy<	DDICInfo_IndexFNC	>		_FNCIndex;
				internal	readonly	Lazy<	DDICInfo_IndexDFIES >	_DFSIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DDICInfo_FieldCollection CreateFieldCollection()	=> this._Factory.CreateFieldCollection();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void ReadyProfile()
					{
						this.LoadFunctionIndex	( this._FNCIndex.Value );
						this.LoadStructureIndex	( this._DFSIndex.Value );
						//.............................................
						base.ReadyProfile();
					}

			#endregion

		}
}