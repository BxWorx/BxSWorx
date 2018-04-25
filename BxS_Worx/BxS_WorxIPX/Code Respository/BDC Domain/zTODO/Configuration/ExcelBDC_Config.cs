using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]
	public class ExcelBDC_Config : IExcelBDC_Config
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelBDC_Config()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public	int			IdleTimeout				{ get; set; }
				[DataMember]	public	int			IdleCheckTime			{ get; set; }
				[DataMember]	public	int			MaxPoolWaitTime		{ get; set; }
				[DataMember]	public	int			PeakConnLimit			{ get; set; }
				[DataMember]	public	int			PoolIdleTimeout		{ get; set; }
				[DataMember]	public	int			PoolSize					{ get; set; }
				[DataMember]	public	int			RepoIdleTimeout		{ get; set; }
				[DataMember]	public	bool		DoLogonCheck			{ get; set; }

			#endregion

		}
}
