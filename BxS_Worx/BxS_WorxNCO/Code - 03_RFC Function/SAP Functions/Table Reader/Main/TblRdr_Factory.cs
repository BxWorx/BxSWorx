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
				internal	TblRdr_IndexFNC		CreateIndexFNC	()	=>	new TblRdr_IndexFNC();
				internal	TblRdr_IndexOPT		CreateIndexOPT	()	=>	new TblRdr_IndexOPT();
				internal	TblRdr_IndexFLD		CreateIndexFLD	()	=>	new TblRdr_IndexFLD();

				internal	TblRdr_IndexOUT		CreateIndexOUT	( string name )	=>	new TblRdr_IndexOUT( name )	;

				//.................................................
				//.................................................
				// Profile objects
				//.................................................
				internal	TblRdr_Data				CreateTblRdrData	(		Lazy<	TblRdr_IndexOPT	>	optIndex
																												, Lazy<	TblRdr_IndexFLD	>	fldIndex	)=>		new TblRdr_Data	( optIndex , fldIndex )	;

			#endregion

		}
}