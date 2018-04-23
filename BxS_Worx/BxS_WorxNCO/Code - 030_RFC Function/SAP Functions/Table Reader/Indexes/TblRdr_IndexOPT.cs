using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main										.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.TableReader	.TblRdr_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_IndexOPT : RfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_IndexOPT()
					{
						this.Name		=	cz_StrOPT;
						//.............................................
						this._Text	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TEXT" ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_Text	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	Text	{ get { return	this.IsLoaded	?	this._Text.Value : cz_Neg1	; } }

			#endregion

		}
}
