﻿using System;
//.........................................................
using BxS_SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal sealed class UT_BxS_DataContainerFiller
		{
			#region "Declarations"

				private readonly ControllerFactory _Fac		= new ControllerFactory();

			#endregion

			//===========================================================================================
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
						return	emptyOne	? new ReposSAPGui(	this._Fac.CreateDC()	)
															:	new ReposSAPGui(	CreateAndFill_DC()		);
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
						DCSapGui lo_DC	= this._Fac.CreateDC();

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

						lo_DC.MsgServers	.AddUpdate	(lo_MsgDTO.UUID, lo_MsgDTO);
						lo_DC.Services		.AddUpdate	(lo_SrvDTO.UUID, lo_SrvDTO);
						lo_DC.Workspaces	.AddUpdate	(lo_WspDTO.UUID, lo_WspDTO);
						lo_DC.Nodes				.AddUpdate	(lo_NdeDTO.UUID, lo_NdeDTO);
						lo_DC.Items				.AddUpdate	(lo_NItDTO.UUID, lo_NItDTO);
						lo_DC.Items				.AddUpdate	(lo_WItDTO.UUID, lo_WItDTO);

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