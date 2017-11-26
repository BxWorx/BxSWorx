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
			 internal	Guid ItmID { get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IRepository CreateRepository(bool emptyOne = false)
					{
						return	emptyOne	? new Repository(new DataContainer()) :	new Repository(CreateAndFill_DC());
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//-----------------------------------------------------------------------------------------
				private DataContainer CreateAndFill_DC()
					{
						var	lo_DC	= new DataContainer();

						DTOMsgServer	lo_MsgDTO	= this.Create_MsgSvrDTO();
						DTOService		lo_SrvDTO	= this.Create_SrvDTO(lo_MsgDTO.UUID);
						DTOWorkspace	lo_WspDTO	= this.Create_WspDTO();

						DTONode	lo_WSNDTO	= this.Create_WSNodeDTO();
						DTOItem	lo_WSIDTO	= this.Create_WSItemDTO(lo_SrvDTO.UUID);
						DTOItem	lo_WSxDTO	= this.Create_WSItemDTO(lo_SrvDTO.UUID);

						this.WSpID	= lo_WspDTO.UUID;
						this.MsgID	= lo_MsgDTO.UUID;
						this.SrvID	= lo_SrvDTO.UUID;
						this.NdeID	= lo_WSNDTO.UUID;
						this.ItmID	= lo_WSxDTO.UUID;

						lo_WSNDTO.Items	.Add (lo_WSIDTO.UUID, lo_WSIDTO);
						lo_WspDTO.Nodes	.Add (lo_WSNDTO.UUID, lo_WSNDTO);
						lo_WspDTO.Items	.Add (lo_WSxDTO.UUID, lo_WSxDTO);

						lo_DC.MsgServers	.Add	(lo_MsgDTO.UUID, lo_MsgDTO);
						lo_DC.Services		.Add	(lo_SrvDTO.UUID, lo_SrvDTO);
						lo_DC.WorkSpaces	.Add	(lo_WspDTO.UUID, lo_WspDTO);

						return	lo_DC;
					}

				//-----------------------------------------------------------------------------------------
				private DTOMsgServer Create_MsgSvrDTO()
					{
						return	new DTOMsgServer	{	UUID				= Guid.NewGuid()	,
																				Name				= "name1"					,
																				Description = "desc1"					,
																				Host				= "host1"					,
																				Port				= "port1"						};
					}

				//-----------------------------------------------------------------------------------------
				private DTOService Create_SrvDTO(Guid	msgSvr)
					{
						return	new DTOService	{	UUID	= Guid.NewGuid()	,
																			Name	= "NameS"					,
																			MSID	= msgSvr						};
					}

				//-----------------------------------------------------------------------------------------
				private DTOWorkspace Create_WspDTO()
					{
						return	new DTOWorkspace	{	UUID				= Guid.NewGuid()	,
																				Description	= "DescWS"					};
					}

				//-----------------------------------------------------------------------------------------
				private DTONode Create_WSNodeDTO()
					{
						return	new DTONode	{	UUID				= Guid.NewGuid()	,
																	Description	= "DescNode"				};
					}

				//-----------------------------------------------------------------------------------------
				private DTOItem Create_WSItemDTO(Guid srvID)
					{
						return	new DTOItem	{	UUID			= Guid.NewGuid()	,
																	ServiceID	= srvID								};
					}

			#endregion

		}
}