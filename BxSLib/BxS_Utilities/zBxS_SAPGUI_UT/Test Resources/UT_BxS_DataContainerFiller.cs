using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal sealed class UT_BxS_DataContainerFiller
		{
			#region "Properties"

			 internal	Guid WSpID { get; private set; }
			 internal	Guid MsgID { get; private set; }
			 internal	Guid SrvID { get; private set; }
			 internal	Guid NdeID { get; private set; }
			 internal	Guid ItNID { get; private set; }
			 internal	Guid ItWID { get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IReposSAPGui CreateRepository(bool emptyOne = false)
					{
						return	emptyOne	? new ReposSAPGui(new DCSapGui()) :	new ReposSAPGui(CreateAndFill_DC());
					}

				//-----------------------------------------------------------------------------------------
				internal DTOMsgServer Create_MsgSvrDTO()
					{
						return	new DTOMsgServer	{	UUID				= Guid.NewGuid()	,
																				Name				= "name1"					,
																				Description = "desc1"					,
																				Host				= "host1"					,
																				Port				= "port1"						};
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//-----------------------------------------------------------------------------------------
				private DCSapGui CreateAndFill_DC()
					{
						var	lo_DC	= new DCSapGui();

						DTOMsgServer	lo_MsgDTO	= this.Create_MsgSvrDTO();
						DTOService		lo_SrvDTO	= this.Create_SrvDTO(lo_MsgDTO.UUID);
						DTOWorkspace	lo_WspDTO	= this.Create_WspDTO();

						DTONode	lo_NdeDTO	= this.Create_NodeDTO();
						DTOItem	lo_NItDTO	= this.Create_ItemDTO(lo_SrvDTO.UUID);
						DTOItem	lo_WItDTO	= this.Create_ItemDTO(lo_SrvDTO.UUID);

						this.WSpID	= lo_WspDTO.UUID;
						this.MsgID	= lo_MsgDTO.UUID;
						this.SrvID	= lo_SrvDTO.UUID;
						this.NdeID	= lo_NdeDTO.UUID;
						this.ItNID	= lo_NItDTO.UUID;
						this.ItWID	= lo_WItDTO.UUID;

						lo_DC.XMsgServers	.AddUpdate	(lo_MsgDTO.UUID, lo_MsgDTO);
						lo_DC.XServices		.AddUpdate	(lo_SrvDTO.UUID, lo_SrvDTO);
						lo_DC.XWorkspaces	.AddUpdate	(lo_WspDTO.UUID, lo_WspDTO);
						lo_DC.XNodes			.AddUpdate	(lo_NdeDTO.UUID, lo_NdeDTO);
						lo_DC.XItems			.AddUpdate	(lo_NItDTO.UUID, lo_NItDTO);
						lo_DC.XItems			.AddUpdate	(lo_WItDTO.UUID, lo_WItDTO);

						return	lo_DC;
					}

				//-----------------------------------------------------------------------------------------
				private DTOService Create_SrvDTO(Guid	msgSvr)
					{
						return	new DTOService	{	UUID				= Guid.NewGuid()	,
																			Name				= "NameS"					,
																			Description	= "DescS"					,
																			MSID				= msgSvr						};
					}

				//-----------------------------------------------------------------------------------------
				private DTOWorkspace Create_WspDTO()
					{
						return	new DTOWorkspace	{	UUID				= Guid.NewGuid()	,
																				Description	= "DescWS"					};
					}

				//-----------------------------------------------------------------------------------------
				private DTONode Create_NodeDTO()
					{
						return	new DTONode	{	UUID				= Guid.NewGuid()	,
																	Description	= "DescNode"				};
					}

				//-----------------------------------------------------------------------------------------
				private DTOItem Create_ItemDTO(Guid srvID)
					{
						return	new DTOItem	{	UUID			= Guid.NewGuid()	,
																	ServiceID	= srvID								};
					}

			#endregion

		}
}