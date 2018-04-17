using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main								.NCO_Constants;
using	static	BxS_WorxNCO.RfcFunction.BDCTran	.SAPMsg_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_IndexTXT : RfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPMsg_IndexTXT()
					{
						this.Name		=	cz_StrLne;
						//.............................................
						this._Fmt		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TDFORMAT" ) );
						this._Lne		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TDLINE"	 ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_Fmt;
				private	readonly	Lazy<int>		_Lne;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	Fmt		{ get { return	this.IsLoaded	?	this._Fmt.Value	:	cz_Neg	; } }
				internal	int	Lne		{ get { return	this.IsLoaded	?	this._Lne.Value	:	cz_Neg	; } }

			#endregion

		}
}
