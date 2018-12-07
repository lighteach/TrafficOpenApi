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

		#region TrafficInfoInstance : TrafficInfo 객체의 싱글톤 객체를 가져오는 프로퍼티
		private TrafficInfo TrafficInfoInstance
		{
			get
			{
				TrafficInfo tInfo = TrafficInfo.CreateInstance(Server.MapPath("/"));
				return tInfo;
			}
		} 
		#endregion

		#region NTrafficInfo : 교통소통정보
		// 테스트 참고 : http://openapi.its.go.kr/api/NTrafficInfo?key=1540911606392&ReqType=2&MinX=127.649805&MaxX=127.650227&MinY=37.252124&MaxY=37.252533
		public JsonResult NTrafficInfo(NTrafficInfo_P p)
		{
			WebReturnModel<NTrafficInfo_R> model = new WebReturnModel<NTrafficInfo_R>()
			{
				ReturnModel = TrafficInfoInstance.NTrafficInfo(p)
			};

			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region NEventIdentity : 공사정보
		public JsonResult NEventIdentity(NEventIdentity_P p)
		{
			WebReturnModel< NEventIdentity_R > model = new WebReturnModel<NEventIdentity_R>()
			{
				ReturnModel = TrafficInfoInstance.NEventIdentity(p)
			};
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region NIncidentIdentity : 사고정보
		public JsonResult NIncidentIdentity(NIncidentIdentity_P p)
		{
			WebReturnModel<NIncidentIdentity_R> model = new WebReturnModel<NIncidentIdentity_R>()
			{
				ReturnModel = TrafficInfoInstance.NIncidentIdentity(p)
			};
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region NCCTVInfo : CCTV영상
		public JsonResult NCCTVInfo(NCCTVInfo_P p)
		{
			WebReturnModel<NCCTVInfo_R> model = new WebReturnModel<NCCTVInfo_R>()
			{
				ReturnModel = TrafficInfoInstance.NCCTVInfo(p)
			};
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region NCCTVImage : CCTV정지영상
		public JsonResult NCCTVImage(NCCTVImage_P p)
		{
			WebReturnModel<NCCTVInfo_R> model = new WebReturnModel<NCCTVInfo_R>()
			{
				ReturnModel = TrafficInfoInstance.NCCTVImage(p)
			};
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region VMSInfo : VMS 표출정보
		public JsonResult VMSInfo(VMSInfo_P p)
		{
			WebReturnModel<VMSInfo_R> model = new WebReturnModel<VMSInfo_R>()
			{
				ReturnModel = TrafficInfoInstance.VMSInfo(p)
			};
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region fcldata : 교통소통정보
		public JsonResult fcldata(fcldata_P p)
		{
			WebReturnModel<fcldata_R> model = new WebReturnModel<fcldata_R>()
			{
				ReturnModel = TrafficInfoInstance.fcldata(p)
			};
			return Json(model, JsonRequestBehavior.AllowGet);
		}
		#endregion
	}
}