using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_IndexOUT : TblRdr_IndexBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_IndexOUT( TblRdr_Profile profile ) : base( profile )
					{
						this._Metadata	=	new	Lazy< SMC.RfcStructureMetadata >( ()=> this._Profile.OutTableStructure );
						//.............................................
						this._WA				= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "WA" ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_WA	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		WA	{ get { return	this._Profile.IsReady ?	this._WA.Value : 0	; } }

			#endregion

		}
}
