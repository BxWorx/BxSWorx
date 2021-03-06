﻿using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_IndexSPA : RfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_IndexSPA( bool useAltVersion = false )
					{
						this.Name		=	useAltVersion	?	cz_SPACall :	cz_SPATran;
						//.............................................
						this._MID		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "PARID"	 ) );
						this._Val		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "PARVAL" ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_MID;
				private	readonly	Lazy<int>		_Val;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	MID		{ get { return	this.IsLoaded	?	this._MID.Value	: cz_Neg1	; } }
				internal	int	Val		{ get { return	this.IsLoaded	?	this._Val.Value	: cz_Neg1	; } }

			#endregion

		}
}
