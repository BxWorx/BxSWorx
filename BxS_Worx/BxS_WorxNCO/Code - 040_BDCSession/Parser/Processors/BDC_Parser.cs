﻿using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxUtil.ObjectPool;

using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class BDC_Parser : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Parser( Lazy< BDC_Parser_Factory >	factory )
					{
						this._PFactory	= factory	??	throw		new	ArgumentException( $"{typeof(BDC_Parser).Namespace}:- Factory null" );
						//.............................................
						this._Tkn		=	this._PFactory.Value.GetTokenParser				();
						this._Col		= this._PFactory.Value.GetColumnParser			();
						this._Grp		= this._PFactory.Value.GetGroupParser				();
						this._Trn		= this._PFactory.Value.GetTransactionParser	();
						this._Ssn		= this._PFactory.Value.GetSessionParser			();
						//this._Des		= this._PFactory.Value.GetDestinationParser	();
						//.............................................
						this.PoolID		= Guid.NewGuid();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				protected	readonly	Lazy< BDC_Parser_Factory			>		_PFactory;
				//.................................................
				private		readonly	Lazy< BDC_Parser_Tokens				>		_Tkn;
				private		readonly	Lazy< BDC_Parser_Columns			>		_Col;
				private		readonly	Lazy< BDC_Parser_Groups				>		_Grp;
				private		readonly	Lazy< BDC_Parser_Transaction	>		_Trn;
				private		readonly	Lazy< BDC_Parser_Session			>		_Ssn;
				//private		readonly	Lazy< BDC_Parser_Destination	>		_Des;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Parse(	IExcelBDC_Request	BDCRequest
														, DTO_BDC_Session					BDCSession		)
					{
						DTO_ParserRequest	lo_DTOParserReq	= this._PFactory.Value.CreateDTOParserRequest();

						if ( ! this.Parse1Dto2D( BDCRequest , lo_DTOParserReq ) )
							{
								return	false;
							}
						//.............................................
						DTO_ParserProfile	lo_DTOProfile		= this._PFactory.Value.CreateDTOProfile();
						//.............................................
						this._Tkn.Value.Process( lo_DTOParserReq	,	lo_DTOProfile	);
						this._Col.Value.Process( lo_DTOParserReq	,	lo_DTOProfile	);
						this._Grp.Value.Process( lo_DTOParserReq	,	lo_DTOProfile	);

						this._Trn.Value.Process( lo_DTOParserReq	, lo_DTOProfile	, BDCSession );
						//.............................................
						//if ( ! BDCRequest.IgnoreSessionConfig )
						//	{
						//		//this._Ssn.Value.Process( lo_DTOProfile , BDCSession.Header );
						//	}
						//.............................................
						//if ( ! BDCRequest.IgnoreDestinationConfig )
						//	{
						//		//this._Des.Value.Process( BDCRequest , BDCSession.DestinationConfig );
						//	}
						//.............................................
						return	true;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool Parse1Dto2D(		IExcelBDC_Request request
																	, DTO_ParserRequest				dto			)
					{
						bool	lb_Ret	= true;
						//.............................................
						if ( request.WSData1D.Count.Equals(0) )
							{
								lb_Ret	= false;
							}
						else
							{
								int[]	lt_UB	= new int[2];
								int[]	lt_LB = new int[2];

								lt_UB[0]	=	request.RowUB;
								lt_UB[1]	=	request.ColUB;
								lt_LB[0]	=	request.RowLB;
								lt_LB[1]	=	request.ColLB;

								dto.WSData	= ( string[,] ) Array.CreateInstance( typeof( string ) , lt_UB, lt_LB );
								//.............................................
								int	ln_Row	= 0;
								int ln_Col	= 0;

								foreach ( KeyValuePair<string, string> ls_kvp in request.WSData1D )
									{
										string[] lt_Idx = ls_kvp.Key.Split(',');

										ln_Row	= int.Parse(lt_Idx[0]);
										ln_Col	= int.Parse(lt_Idx[1]);

										dto.WSData[ln_Row,ln_Col]	= ls_kvp.Value;
									}
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Override"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnResetState()
					{	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnReleaseResources()
					{	}

			#endregion

		}
}