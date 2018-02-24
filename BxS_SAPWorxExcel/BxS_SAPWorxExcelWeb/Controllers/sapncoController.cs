﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SAP.Middleware.Connector;

namespace BxS_SAPWorxExcelWeb.Controllers
	{
	public class SapNcoController : ApiController
		{
		// GET api/<controller>
		[HttpGet()]
		public IEnumerable<string> GetSystemList()
			{
				try
					{
						var x = SapLogonIniConfiguration.Create();
						return x.GetEntries();
					}
				catch (Exception)
					{
						return new string[] {"ERROR"};
					}

			//return new string[] { "value1", "value2" };
			}

		// GET api/<controller>/5
		public string Get(int id)
			{
			return "value";
			}

		// POST api/<controller>
		public void Post([FromBody]string value)
			{
			}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
			{
			}

		// DELETE api/<controller>/5
		public void Delete(int id)
			{
			}
		}

	public class NCOData
		{
			public string ID	{ get; set; }
		}

	}