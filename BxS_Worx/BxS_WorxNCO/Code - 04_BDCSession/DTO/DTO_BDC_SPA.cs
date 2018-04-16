using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.TableReader;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••

namespace BxS_WorxNCO.BDCSession.DTO
{
	internal class SAPSession
		{
			#region "Documentation"
			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPSession()
					{
						this._TR_Header		= new	Lazy<TblRdr_Function>();
						this._TR_Data			= new	Lazy<TblRdr_Function>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				//public	string	MemoryID		{	get; set; }
				//public	string	MemoryValue	{	get; set; }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy< TblRdr_Function >		_TR_Header	;
				private readonly	Lazy< TblRdr_Function >		_TR_Data		;

			#endregion

		}
}
