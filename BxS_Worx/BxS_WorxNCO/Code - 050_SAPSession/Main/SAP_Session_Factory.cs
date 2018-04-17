using System;
//.........................................................
using BxS_WorxNCO.Common;

using BxS_WorxNCO.SAPSession.API;
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.Main
{
	internal sealed class SAP_Session_Factory
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private SAP_Session_Factory()
					{	}
				//.................................................
				private	static readonly		Lazy< SAP_Session_Factory >	_Instance		= new	Lazy< SAP_Session_Factory >( ()=> new SAP_Session_Factory() , cz_LM );
				public	static						SAP_Session_Factory					Instance			{	get { return _Instance.Value; }	}

			#endregion

			//===========================================================================================
			#region "Methods: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ISAP_Session_Header		CreateSAPHeader	()	=>	new SAP_Session_Header();
				internal ISAP_Session_Profile		CreateSAPProfile()	=>	new SAP_Session_Profile( this.CreateCTU() , this.CreateBDCData() );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	DTO_BDC_CTU			CreateCTU			()	=>	new DTO_BDC_CTU();
				private	BDC_Collection	CreateBDCData	()	=>	new BDC_Collection();

			#endregion

		}
}