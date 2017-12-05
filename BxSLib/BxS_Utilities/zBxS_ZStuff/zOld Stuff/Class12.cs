using System;
using SMC = SAP.Middleware.Connector;

namespace BxS_SAPNCOz
{
		public class Class1
		{
		//private SMC.RfcDestinationManager	dm;
		//private readonly SMC.RfcConfigParameters				x = new SMC.RfcConfigParameters();
		private SMC.SapLogonIniConfiguration t = SMC.SapLogonIniConfiguration.Create();

		public void Tree()
				{
			//var x = SMC.RfcDestinationManager.TryGetDestination("ZZ");
			var x = this.t.GetEntries();
			}
		}
}
