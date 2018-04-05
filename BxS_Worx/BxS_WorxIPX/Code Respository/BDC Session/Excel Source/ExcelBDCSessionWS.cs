using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]
	public class ExcelBDCSessionWS : IExcelBDCSessionWS
		{
			#region "Properties"

				[DataMember]	public	string	WBID					{ get; set;	}
				[DataMember]	public	string	WSID					{ get; set;	}
				[DataMember]	public	int			WSNo					{ get; set;	}
				[DataMember]	public	string	UsedAddress		{ get; set;	}
				[DataMember]	public  bool		IsBDCSession	{ get; set;	}
				[DataMember]	public  bool		IsActive			{ get; set;	}
				[DataMember]	public  bool		IsTest				{ get; set;	}
				//.................................................
				[DataMember]	public  object[][]	WSData		{ get; set;	}
											public	object[,]		WSCells		{ get; set;	}

			#endregion

			//===========================================================================================
			#region "Methods: Serialising"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public void BeforeSerializing( StreamingContext ctx )
				[OnSerializing]
				public void BeforeSerializing()
					{
						int dimension0	= this.WSCells.GetLength(0);
						int dimension1	= this.WSCells.GetLength(1);
						//.............................................
						this.WSData			= new object[dimension0][];

						Parallel.For ( 0 , dimension0 ,	i => {	this.WSData[i] = new object[dimension1];
																										for (int j = 0; j < dimension1; j++)
																											{
																												this.WSData[i][j] = this.WSCells[i, j];
																											}
																									} );

						//for (int i = 0; i < dimension0; i++)
						//	{
						//		this.WSData[i] = new object[dimension1];
						//		for (int j = 0; j < dimension1; j++)
						//			{
						//				this.WSData[i][j] = this.WSCells[i, j];
						//			}
						//	}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//	public void AfterDeserializing( StreamingContext ctx )
				[OnDeserialized]
				public void AfterDeserializing()
					{
						if (this.WSData == null)
							{
								this.WSCells = null;
							}
						else
							{
								int dimension0 = this.WSData.Length;
								if (dimension0 == 0)
									{
										this.WSCells = new object[0, 0];
									}
								else
									{
										int dimension1 = this.WSData[0].Length;
										for (int i = 1; i < dimension0; i++)
											{
												if (this.WSData[i].Length != dimension1)
													{
														throw		new InvalidOperationException( "Surrogate (jagged) array does not correspond to a rectangular one" );
													}
											}

										this.WSCells = new object[dimension0, dimension1];

										Parallel.For ( 0 , dimension0 ,	i => {	this.WSData[i] = new object[dimension1];
																														for (int j = 0; j < dimension1; j++)
																															{
																																this.WSCells[i, j] = this.WSData[i][j];
																															}
																													} );

										//for (int i = 0; i < dimension0; i++)
										//	{
										//		for (int j = 0; j < dimension1; j++)
										//			{
										//				this.WSCells[i, j] = this.WSData[i][j];
										//			}
										//	}

									}
							}
					}

			#endregion

		}
}
