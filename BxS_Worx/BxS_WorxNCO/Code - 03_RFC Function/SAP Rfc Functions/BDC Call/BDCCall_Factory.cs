using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
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

				private	static readonly	Lazy< BDCCall_Factory >	_Instance
																	= new Lazy< BDCCall_Factory >	(	()=>	new BDCCall_Factory() , cz_LM );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				//.................................................
				// Index objects
				//.................................................
				internal	BDCCall_IndexFNC		CreateIndexFNC	()=>	new BDCCall_IndexFNC();
				internal	BDCCall_IndexCTU		CreateIndexCTU	()=>	new BDCCall_IndexCTU();
				internal	BDCCall_IndexSPA		CreateIndexSPA	()=>	new BDCCall_IndexSPA();
				internal	BDCCall_IndexBDC		CreateIndexBDC	()=>	new BDCCall_IndexBDC();
				internal	BDCCall_IndexMSG		CreateIndexMSG	()=>	new BDCCall_IndexMSG();

				//.................................................
				//.................................................
				// Profile objects
				//.................................................
				internal	BDCCall_Data			CreateBDCLines	(		SMC.IRfcTable	bdcData
																											,	SMC.IRfcTable	spaData
																											,	SMC.IRfcTable	msgData
																											, BDCCall_IndexSPA	spaIndex
																											,	BDCCall_IndexBDC	bdcIndex
																											, BDCCall_IndexMSG	msgIndex	)=>	new	BDCCall_Data(		bdcData
																																																					,	spaData
																																																					,	msgData
																																																					, spaIndex
																																																					, bdcIndex
																																																					, msgIndex	);

				internal	BDCCall_Header		CreateBDCHeader	(		SMC.IRfcStructure ctuParms
																											,	BDCCall_IndexCTU	ctuIndex
																											,	bool							withDefaults	= true )=>	new BDCCall_Header(		ctuParms
																																																												, ctuIndex
																																																												, withDefaults );

			#endregion

		}
}