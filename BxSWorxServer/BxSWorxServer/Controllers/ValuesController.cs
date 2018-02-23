using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SAP.Middleware.Connector;

namespace BxSWorxServer.Controllers
	{
	public class ValuesController : ApiController
		{

			private readonly Lazy<SapLogonIniConfiguration>	_SAP	= new Lazy<SapLogonIniConfiguration>( () => SapLogonIniConfiguration.Create() );

			public ValuesController()
				{
				}


		// GET api/values
		public IEnumerable<string> Get()
			{
				return	this._SAP.Value.GetEntries();
				//return new string[] { "value1", "value2" };
			}

		// GET api/values/5
		public string Get(int id)
			{
			return "value";
			}

		// POST api/values
		public void Post([FromBody]string value)
			{
			}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
			{
			}

		// DELETE api/values/5
		public void Delete(int id)
			{
			}
		}
	}
