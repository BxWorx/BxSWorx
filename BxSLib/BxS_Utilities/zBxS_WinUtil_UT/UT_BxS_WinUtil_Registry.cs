using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using zBxS_WinUtil_UT;

namespace zBxS_WinUtil_UT
{
	[TestClass]
	public class UT_BxS_WinUtil_Registry
	{
		[TestMethod]
		public void UT_WinRegistryReadWriteRemove()
			{
				const string registryPath			= @"Software\BxS";

				const string registryNameErr	= "wwww";
				const string registryNameOK		= "BxSTest";
				const string registryDefVal		= "AAAA";
				const string registryNewVal		= "XXXX";

				BxS_WinUtilFRM.WinRegistry registry = new BxS_WinUtilFRM.WinRegistry();

				var regvalErr = registry.Read(registryPath, registryNameErr	, registryDefVal);
				Assert.AreEqual			(regvalErr,	registryDefVal, "Read: Error");

				registry.Write(registryPath, registryNameOK, registryNewVal);
				var regvalOK	= registry.Read(registryPath, registryNameOK	, registryDefVal);
				Assert.AreEqual	(regvalOK	,	registryNewVal, "Write: Error");

				registry.Remove(registryPath, registryNameOK);
				var regvalRem = registry.Read(registryPath, registryNameOK	, registryDefVal);
				Assert.AreEqual	(regvalRem,	registryDefVal, "Remove: Error");
			}
	}
}
