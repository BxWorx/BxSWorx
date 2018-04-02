using System.Security;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigLogon : IConfigLogon
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ConfigLogon()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool		ForRepository		{ get; set; }
				public	string	Language				{ get; set; }
				//.................................................
				public	string	Client		{ get; set; }
				public	string	User			{ get; set; }
				public	string	Password	{ get; set; }
				//.................................................
				public	SecureString	SecurePassword	{ get; set;	}

			#endregion

		}
}
