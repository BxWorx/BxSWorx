using System;
using SMC = SAP.Middleware.Connector;

namespace BxS_SAPNCO
{
		public class Class1
		{
			//private readonly SMC.RfcConfigParameters				x = new SMC.RfcConfigParameters();
			private SMC.SapLogonIniConfiguration		t = SMC.SapLogonIniConfiguration.Create();

			public string[] Tree()
				{
					return	this.t.GetEntries();
				}
		}
}
