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
				internal	BDC_IndexFNC	CreateIndexFNC	( bool tranVersion = false )=>	new BDC_IndexFNC( tranVersion );
				internal	BDC_IndexSPA	CreateIndexSPA	( bool tranVersion = false )=>	new BDC_IndexSPA( tranVersion );
				internal	BDC_IndexBDC	CreateIndexBDC	( bool tranVersion = false )=>	new BDC_IndexBDC( tranVersion );
				internal	BDC_IndexMSG	CreateIndexMSG	( bool tranVersion = false )=>	new BDC_IndexMSG( tranVersion );

				internal	BDC_IndexCTU	CreateIndexCTU	()=>	new BDC_IndexCTU();

				//.................................................
				//.................................................
				// Profile objects
				//.................................................
				internal	BDC_Data		CreateBDCData		(		Lazy<	BDC_IndexSPA >	spaIndex
																								,	Lazy<	BDC_IndexBDC > 	bdcIndex
																								, Lazy<	BDC_IndexMSG >	msgIndex	)=>		new	BDC_Data	( spaIndex , bdcIndex , msgIndex );

				internal	BDC_Header	CreateBDCHeader	(		Lazy<	BDC_IndexCTU >	ctuIndex
																								,	bool									withDefaults	= true	)=>		new BDC_Header( ctuIndex , withDefaults );

			#endregion

		}
}