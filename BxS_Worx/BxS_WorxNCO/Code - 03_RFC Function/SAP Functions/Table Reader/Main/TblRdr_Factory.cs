using System;
//.........................................................
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal sealed class TblRdr_Factory
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static TblRdr_Factory Instance
					{
						get { return	_Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private TblRdr_Factory()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< TblRdr_Factory >		_Instance		= new Lazy< TblRdr_Factory >	(	()=>	new TblRdr_Factory() , cz_LM );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				//.................................................
				// Index objects
				//.................................................
				internal	TblRdr_IndexFNC		CreateIndexFNC	( TblRdr_Profile profile )	=>	new TblRdr_IndexFNC( profile );
				internal	TblRdr_IndexOPT		CreateIndexOPT	( TblRdr_Profile profile )	=>	new TblRdr_IndexOPT( profile );
				internal	TblRdr_IndexFLD		CreateIndexFLD	( TblRdr_Profile profile )	=>	new TblRdr_IndexFLD( profile );
				internal	TblRdr_IndexTBL		CreateIndexTBL	( TblRdr_Profile profile )	=>	new TblRdr_IndexTBL( profile );

				//.................................................
				//.................................................
				// Profile objects
				//.................................................
				//internal	BDCCall_Data			CreateBDCData		(		BDCCall_IndexSPA	spaIndex
				//																							,	BDCCall_IndexBDC	bdcIndex
				//																							, BDCCall_IndexMSG	msgIndex							)=>		new	BDCCall_Data	(		spaIndex
				//																																																									, bdcIndex
				//																																																									, msgIndex	);

				//internal	BDCCall_Header		CreateBDCHeader	(		BDCCall_IndexCTU	ctuIndex
				//																							,	bool							withDefaults	= true	)=>		new BDCCall_Header(		ctuIndex
				//																																																									, withDefaults );

			#endregion

		}
}