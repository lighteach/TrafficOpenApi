using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TrafficOpenApi.Core.Models
{
	public class TrafficInfoResultModel
	{
	}

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
	public class NEventIdentity_R
	{
		public string CoordType { get; set; } // 좌표타입
		public int DataCount { get; set; } // 공사정보 개수
		public string type { get; set; } // 도로정보 (its : 국도 / ex : 고속도로 / police : 경찰청)
		public string EventId { get; set; } // 공사 고유 식별번호
		public string EventType { get; set; } // 공사정보유형
		public string CoordX { get; set; } // 경도좌표
		public string CoordY { get; set; } // 위도좌표
		public string LanesBlockType { get; set; } // 공사로 인한 차로 차단 방법( 0 : 통제없음 / 1 : 갓길통제/ 2 : 차로부분통제 / 3 : 전면통제 )
		public string LanesBlocked { get; set; } // 공사로 인해 차단된 차로 수
		public string EventStartDay { get; set; } // 공사 시작일
		public string EventEndDay { get; set; } // 공사 종료일
		public string EventStartTime { get; set; } // 공사 실제 개시 시간
		public string EventEndTime { get; set; } // 공사 실제 종료 시간
		public string EventStatusMsg { get; set; } // 공사 상황정보 메시지
		public int ExpectedCnt { get; set; } // 우회정보개수
		public string ExpectedDetourMsg { get; set; } // 우회정보 메시지
		public string EventDirection { get; set; } // 진행방향
	}
	#endregion

	#region NIncidentIdentity_R : 사고정보
	public class NIncidentIdentity_R
	{
		public string CoordType { get; set; } // 좌표타입
		public int DataCount { get; set; } // 공사정보 개수
		public string type { get; set; } // 도로정보 (its : 국도 / ex : 고속도로 / police : 경찰청)
		public string IncidentType { get; set; } // 
		public string CoordX { get; set; } // 경도좌표
		public string CoordY { get; set; } // 위도좌표
		public string LanesBlockType { get; set; } // 차로 차단 방법 ( 0 : 통제없음 / 1 : 갓길통제 / 2 : 차로부분통제 / 3 : 전면통제 )
		public string IncidentMsg { get; set; } // 돌발 상황정보 메시지
		public string IncidentDuration { get; set; } // 돌발상황지속여부 ( 0 : 조치중 / 1 : 종료 )
		public int ExpectedCnt { get; set; } // 우회정보개수
		public string ExpectedDetourMsg { get; set; } // 우회정보 메시지
		public string EventDirection { get; set; } // 진행방향
	}
	#endregion

	#region NCCTVInfo_R : CCTV영상
	public class NCCTVInfo_R
	{
		public string CoordType { get; set; } // 좌표타입
		public int DataCount { get; set; } // CCTV 개수
		public string CoordX { get; set; } // 경도좌표
		public string CoordY { get; set; } // 위도좌표
		public string CCTVType { get; set; } // 1 : 실시간 스트리밍 / 2 : 동영상 파일 / 3 : 정지영상
		public string FileCreateTime { get; set; } // 
		public string CCTVFormat { get; set; } // 
		public string CCTVResolution { get; set; } // CCTV 해상도
		public string RoadSectionId { get; set; } // 도로구간의 고유식별번호
		public string CCTVName { get; set; } // CCTV 설치 지점명칭
		public string CCTVurl { get; set; } // CCTV 영상의 url 주소
	}
	#endregion

	#region VMS_R : VMS 표출정보
	public class VMS_R
	{
		public string vmsmessage { get; set; } // VMS정보 메세지
		public string vmsid { get; set; } // VMSID
		public string grdate { get; set; } // 문안생성일시(YYYYMMDDHH24MISS)
		public int vmsseqno { get; set; } // VMS DISPLAY 순서
	}
	#endregion

	#region fcldata_R : 우회도로 예측정보
	public class fcldata_R
	{
		public string fcastYmd { get; set; } // 예측일자
		public string fcastH { get; set; } // 예측시간 (24시간기준)
		public string sectionId { get; set; } // 구간 ID
		public string sectionType { get; set; } // 구간 구분 (M:메인도로, D:우회도로)
		public string detourId { get; set; } // 우회 도로 ID
		public string linkId { get; set; } // 링크 ID
		public int length { get; set; } // 길이
		public string trvSpd { get; set; } // 통행 속도
	}
	#endregion
}