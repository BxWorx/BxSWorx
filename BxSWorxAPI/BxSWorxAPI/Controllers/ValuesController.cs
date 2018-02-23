using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//using SMC	= SAP.Middleware.Connector;

namespace BxSWorxAPI.Controllers
{
		[Route("api/[controller]")]
		public class ValuesController : Controller
		{
				public ValuesController()
					{

					var x = new ClassLibrary1.Class1();

					//var _SAPINI		= SMC.SapLogonIniConfiguration.Create();


						//this._NCOCntlr	= new NCOController( true, false , true);
					}

				//private readonly NCOController	_NCOCntlr;

				// GET api/values
				[HttpGet]
				public IEnumerable<string> Get()
				{
						return new string[] { "value1", "value2" };
				}

				// GET api/values/5
				[HttpGet("{id}")]
				//public IList<IDTORefEntry> Get(int id)
				public string Get(int id)
				{
					//return	this._NCOCntlr.ConnectionReferenceList();
						return "value";
				}

				// POST api/values
				[HttpPost]
				public void Post([FromBody]string value)
				{
				}

				// PUT api/values/5
				[HttpPut("{id}")]
				public void Put(int id, [FromBody]string value)
				{
				}

				// DELETE api/values/5
				[HttpDelete("{id}")]
				public void Delete(int id)
				{
				}
		}
}
