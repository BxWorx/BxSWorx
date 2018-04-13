using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main										.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.TableReader	.TblRdr_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_IndexOUT : RfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_IndexOUT()
					{
						this.Name	=	cz_StrOUT;
						//.............................................
						this._WA	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "WA" ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_WA	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		WA	{ get { return	this.IsLoaded	?	this._WA.Value : cz_Neg	; } }

			#endregion

		}
}
