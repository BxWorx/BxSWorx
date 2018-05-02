using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]
	public class Request_Config : IRequest_Config
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Request_Config()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public	bool	UseAltBDC			{ get; set; }
				[DataMember]	public	bool	IsSequential	{ get; set; }

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
