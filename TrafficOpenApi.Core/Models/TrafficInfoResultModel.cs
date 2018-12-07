using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TrafficOpenApi.Core.Models
{
	#region WebReturnModel : 실제 컨트롤러에서 JSon으로 반환되어질 모델
	public class WebReturnModel<T>
	{
		public T ReturnModel { get; set; }
		public bool IsSuccess
		{
			get
			{
				return (ReturnModel == null ? false : true);
			}
		}
	} 
	#endregion

	#region NTrafficInfo_R : 교통소통정보
	[XmlRoot("response")]
	public class NTrafficInfo_R
	{
		[XmlElement("coordtype")]
		public string coordtype { get; set; } // 좌표타입
		[XmlElement("datacount")]
		public int datacount { get; set; } // 도로구간 개수
		[XmlElement("data")]
		public List<NTrafficInfoData> data { get; set; } // 도로구간의 고유식별번호
	}

	public class NTrafficInfoData
	{
		[XmlElement("roadsectionid")]
		public string roadsectionid { get; set; }
		[XmlElement("avgspeed")]
		public int avgspeed { get; set; }
		[XmlElement("startnodeid")]
		public string startnodeid { get; set; }
		[XmlElement("roadnametext")]
		public string roadnametext { get; set; }
		[XmlElement("traveltime")]
		public int traveltime { get; set; }
		[XmlElement("endnodeid")]
		public string endnodeid { get; set; }
		[XmlElement("generatedate")]
		public string generatedate { get; set; }
	}
	#endregion

	#region NEventIdentity_R : 공사정보
	[XmlRoot("response")]
	public class NEventIdentity_R
	{
		[XmlElement("coordtype")]
		public string coordtype { get; set; } // 좌표타입
		[XmlElement("datacount")]
		public int datacount { get; set; } // 공사정보 개수
		[XmlElement("data")]
		public List<NEventIdentityData> data { get; set; }
	}

	public class NEventIdentityData
	{
		[XmlElement("type")]
		public string type { get; set; } // 도로정보 (its : 국도 / ex : 고속도로 / police : 경찰청)
		[XmlElement("eventid")]
		public string eventid { get; set; } // 공사 고유 식별번호
		[XmlElement("eventtype")]
		public string eventtype { get; set; } // 공사정보유형
		[XmlElement("coordx")]
		public string coordx { get; set; } // 경도좌표
		[XmlElement("coordy")]
		public string coordy { get; set; } // 위도좌표
		[XmlElement("lanesblocktype")]
		public string lanesblocktype { get; set; } // 공사로 인한 차로 차단 방법( 0 : 통제없음 / 1 : 갓길통제/ 2 : 차로부분통제 / 3 : 전면통제 )
		[XmlElement("lanesblocked")]
		public string lanesblocked { get; set; } // 공사로 인해 차단된 차로 수
		[XmlElement("eventstartday")]
		public string eventstartday { get; set; } // 공사 시작일
		[XmlElement("eventendday")]
		public string eventendday { get; set; } // 공사 종료일
		[XmlElement("eventstarttime")]
		public string eventstarttime { get; set; } // 공사 실제 개시 시간
		[XmlElement("eventendtime")]
		public string eventendtime { get; set; } // 공사 실제 종료 시간
		[XmlElement("eventstatusmsg")]
		public string eventstatusmsg { get; set; } // 공사 상황정보 메시지
		[XmlElement("expectedcnt")]
		public int expectedcnt { get; set; } // 우회정보개수
		[XmlElement("expecteddetourmsg")]
		public string expecteddetourmsg { get; set; } // 우회정보 메시지
		[XmlElement("eventdirection")]
		public string eventdirection { get; set; } // 진행방향
	}
	#endregion

	#region NIncidentIdentity_R : 사고정보
	[XmlRoot("response")]
	public class NIncidentIdentity_R
	{
		[XmlElement("coordtype")]
		public string coordtype { get; set; } // 좌표타입
		[XmlElement("datacount")]
		public int datacount { get; set; } // 공사정보 개수
		[XmlElement("data")]
		public List<NEventIdentityData> data { get; set; }
	}

	public class NIncidentIdentityData
	{
		[XmlElement("type")]
		public string type { get; set; } // 도로정보 (its : 국도 / ex : 고속도로 / police : 경찰청)
		[XmlElement("incidenttype")]
		public string incidenttype { get; set; } // 
		[XmlElement("coordx")]
		public string coordx { get; set; } // 경도좌표
		[XmlElement("coordy")]
		public string coordy { get; set; } // 위도좌표
		[XmlElement("lanesblocktype")]
		public string lanesblocktype { get; set; } // 차로 차단 방법 ( 0 : 통제없음 / 1 : 갓길통제 / 2 : 차로부분통제 / 3 : 전면통제 )
		[XmlElement("incidentmsg")]
		public string incidentmsg { get; set; } // 돌발 상황정보 메시지
		[XmlElement("incidentduration")]
		public string incidentduration { get; set; } // 돌발상황지속여부 ( 0 : 조치중 / 1 : 종료 )
		[XmlElement("expectedcnt")]
		public int expectedcnt { get; set; } // 우회정보개수
		[XmlElement("expecteddetourmsg")]
		public string expecteddetourmsg { get; set; } // 우회정보 메시지
		[XmlElement("eventdirection")]
		public string eventdirection { get; set; } // 진행방향
	}
	#endregion

	#region NCCTVInfo_R : CCTV영상/CCTV 정지영상
	[XmlRoot("response")]
	public class NCCTVInfo_R
	{
		[XmlElement("coordtype")]
		public string coordtype { get; set; } // 좌표타입
		[XmlElement("datacount")]
		public int datacount { get; set; } // CCTV 개수
		[XmlElement("data")]
		public List<NCCTVInfoData> data { get; set; }
	}

	public class NCCTVInfoData
	{
		[XmlElement("coordx")]
		public string coordx { get; set; } // 경도좌표
		[XmlElement("coordy")]
		public string coordy { get; set; } // 위도좌표
		[XmlElement("cctvtype")]
		public string cctvtype { get; set; } // 1 : 실시간 스트리밍 / 2 : 동영상 파일 / 3 : 정지영상
		[XmlElement("filecreatetime")]
		public string filecreatetime { get; set; } // 
		[XmlElement("cctvformat")]
		public string cctvformat { get; set; } // 
		[XmlElement("cctvresolution")]
		public string cctvresolution { get; set; } // cctv 해상도
		[XmlElement("roadsectionid")]
		public string roadsectionid { get; set; } // 도로구간의 고유식별번호
		[XmlElement("cctvname")]
		public string cctvname { get; set; } // cctv 설치 지점명칭
		[XmlElement("cctvurl")]
		public string cctvurl { get; set; } // cctv 영상의 url 주소
	}
	#endregion

	#region VMSInfo_R : VMS 표출정보
	[XmlRoot("response")]
	public class VMSInfo_R
	{
		[XmlElement("coordtype")]
		public string coordtype { get; set; } // 좌표타입
		[XmlElement("datacount")]
		public int datacount { get; set; } // 데이터 개수
		[XmlElement("data")]
		public List<VMSInfoData> data { get; set; }
	}

	public class VMSInfoData
	{
		[XmlElement("vmsmessage")]
		public string vmsmessage { get; set; } // VMS정보 메세지
		[XmlElement("vmsid")]
		public string vmsid { get; set; } // VMSID
		[XmlElement("grdate")]
		public string grdate { get; set; } // 문안생성일시(YYYYMMDDHH24MISS)
		[XmlElement("vmsseqno")]
		public int vmsseqno { get; set; } // VMS DISPLAY 순서
	}
	#endregion

	#region fcldata_R : 우회도로 예측정보
	[XmlRoot("response")]
	public class fcldata_R
	{
		[XmlElement("coordtype")]
		public string coordtype { get; set; } // 좌표타입
		[XmlElement("datacount")]
		public int datacount { get; set; } // 데이터 개수
		[XmlElement("data")]
		public List<fcldataData> data { get; set; }
		
	}

	public class fcldataData
	{
		[XmlElement("fcastYmd")]
		public string fcastYmd { get; set; } // 예측일자
		[XmlElement("fcastH")]
		public string fcastH { get; set; } // 예측시간 (24시간기준)
		[XmlElement("sectionId")]
		public string sectionId { get; set; } // 구간 ID
		[XmlElement("sectionType")]
		public string sectionType { get; set; } // 구간 구분 (M:메인도로, D:우회도로)
		[XmlElement("detourId")]
		public string detourId { get; set; } // 우회 도로 ID
		[XmlElement("linkId")]
		public string linkId { get; set; } // 링크 ID
		[XmlElement("length")]
		public int length { get; set; } // 길이
		[XmlElement("trvSpd")]
		public string trvSpd { get; set; } // 통행 속도
	}
	#endregion
}