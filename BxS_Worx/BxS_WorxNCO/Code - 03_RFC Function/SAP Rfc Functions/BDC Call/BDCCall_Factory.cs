using System;
//.........................................................
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal sealed class BDCCall_Factory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static BDCCall_Factory Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCCall_Factory()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< BDCCall_Factory >		_Instance		= new Lazy< BDCCall_Factory >	(	()=>	new BDCCall_Factory() , cz_LM );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				//.................................................
				// Index objects
				//.................................................
				internal	BDCCall_IndexFNC		CreateIndexFNC	( BDCCall_Profile profile )	=>	new BDCCall_IndexFNC( profile );
				internal	BDCCall_IndexCTU		CreateIndexCTU	( BDCCall_Profile profile )	=>	new BDCCall_IndexCTU( profile );
				internal	BDCCall_IndexSPA		CreateIndexSPA	( BDCCall_Profile profile )	=>	new BDCCall_IndexSPA( profile );
				internal	BDCCall_IndexBDC		CreateIndexBDC	( BDCCall_Profile profile )	=>	new BDCCall_IndexBDC( profile );
				internal	BDCCall_IndexMSG		CreateIndexMSG	( BDCCall_Profile profile )	=>	new BDCCall_IndexMSG( profile );

				//.................................................
				//.................................................
				// Profile objects
				//.................................................
				internal	BDCCall_Data			CreateBDCData		(		BDCCall_IndexSPA	spaIndex
																											,	BDCCall_IndexBDC	bdcIndex
																											, BDCCall_IndexMSG	msgIndex							)=>		new	BDCCall_Data	(		spaIndex
																																																													, bdcIndex
																																																													, msgIndex	);

				internal	BDCCall_Header		CreateBDCHeader	(		BDCCall_IndexCTU	ctuIndex
																											,	bool							withDefaults	= true	)=>		new BDCCall_Header(		ctuIndex
																																																													, withDefaults );

			#endregion

		}
}