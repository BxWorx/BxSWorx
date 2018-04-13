using System;
//.........................................................
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal sealed class BDC_Factory
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static BDC_Factory Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Factory()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< BDC_Factory >		_Instance		= new Lazy< BDC_Factory >	(	()=>	new BDC_Factory() , cz_LM );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				//.................................................
				// Index objects
				//.................................................
				internal	BDC_IndexSPA				CreateIndexSPA	()	=>	new BDC_IndexSPA();
				internal	BDC_IndexBDC				CreateIndexBDC	()	=>	new BDC_IndexBDC();
				internal	BDC_IndexMSG				CreateIndexMSG	()	=>	new BDC_IndexMSG();

				//.................................................
				//.................................................
				// Profile objects
				//.................................................
				internal	BDC_Data						CreateBDCData		(		Lazy<	BDC_IndexSPA >	spaIndex
																												,	Lazy<	BDC_IndexBDC > 	bdcIndex
																												, Lazy<	BDC_IndexMSG >	msgIndex	)=>		new	BDC_Data	(		spaIndex
																																																			, bdcIndex
																																																			, msgIndex	);

			#endregion

		}
}