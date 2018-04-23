using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.Helpers.ObjectPool;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_900_Helpers
		{
			private	readonly IIPXController	co_Cntlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_900_Helpers()
				{
					this.co_Cntlr		= IPXController.Instance;
					Assert.IsNotNull	( this.co_Cntlr	, "" );
				}
		}

	//.

	}
