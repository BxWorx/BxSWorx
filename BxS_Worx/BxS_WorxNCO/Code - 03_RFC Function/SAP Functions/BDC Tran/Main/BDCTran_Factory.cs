﻿using System;
//.........................................................
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal sealed class BDCTran_Factory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static BDCTran_Factory Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCTran_Factory()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< BDCTran_Factory >		_Instance		= new Lazy< BDCTran_Factory >	(	()=>	new BDCTran_Factory() , cz_LM );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				//.................................................
				// Index objects
				//.................................................
				internal	BDCTran_IndexFNC		CreateIndexFNC	( BDCTran_Profile profile )	=>	new BDCTran_IndexFNC( profile );
				internal	BDC_IndexSPA				CreateIndexSPA	( BDCTran_Profile profile )	=>	new BDC_IndexSPA( profile );
				internal	BDC_IndexBDC				CreateIndexBDC	( BDCTran_Profile profile )	=>	new BDC_IndexBDC( profile );
				internal	BDC_IndexMSG				CreateIndexMSG	( BDCTran_Profile profile )	=>	new BDC_IndexMSG( profile );

				//.................................................
				//.................................................
				// Profile objects
				//.................................................
				internal	BDCTran_Data			CreateBDCData		(		BDCTran_IndexSPA	spaIndex
																											,	BDCTran_IndexBDC	bdcIndex
																											, BDCTran_IndexMSG	msgIndex							)=>		new	BDCTran_Data	(		spaIndex
																																																													, bdcIndex
																																																													, msgIndex	);

				internal	BDCTran_Header		CreateBDCHeader	(		BDCTran_IndexCTU	ctuIndex
																											,	bool							withDefaults	= true	)=>		new BDCTran_Header(		ctuIndex
																																																													, withDefaults );

			#endregion

		}
}