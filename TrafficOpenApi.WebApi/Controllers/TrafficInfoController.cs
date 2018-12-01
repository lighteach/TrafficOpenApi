using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TrafficOpenApi.Core.Libs;
using TrafficOpenApi.Core.Models;

namespace TrafficOpenApi.Controllers
{
	public class TrafficInfoController : Controller
	{
	      // GET: TrafficInfo
		public ActionResult Index()
		{
		    return View();
		}

		#region NTrafficInfo : 교통소통정보
		public JsonResult NTrafficInfo(NTrafficInfo_P p)
		{
			//foreach (string key in Request.ServerVariables)
			//{
			//	Response.Write($"{key} : {Request.ServerVariables[key]}<br />");
			//}
			//Response.Write($"{Server.MapPath("/")}");

			//var model = new
			//{
			//	ReturnCode = 0
			//};

			TrafficInfo tInfo = new TrafficInfo(Server.MapPath("/"));
			NTrafficInfo_R model = tInfo.NTrafficInfo(p);

			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region NEventIdentity : 공사정보
		public JsonResult NEventIdentity(NEventIdentity_P p)
		{
			NEventIdentity_R model = new NEventIdentity_R();
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region NIncidentIdentity : 사고정보
		public JsonResult NIncidentIdentity(NIncidentIdentity_P p)
		{
			NIncidentIdentity_R model = new NIncidentIdentity_R();
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region NCCTVInfo : CCTV영상
		public JsonResult NCCTVInfo(NCCTVInfo_P p)
		{
			NCCTVInfo_R model = new NCCTVInfo_R();
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region VMS : VMS 표출정보
		public JsonResult VMS(VMS_P p)
		{
			VMS_R model = new VMS_R();
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region fcldata : 교통소통정보
		public JsonResult fcldata(fcldata_P p)
		{
			fcldata_R model = new fcldata_R();
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion
	}
}