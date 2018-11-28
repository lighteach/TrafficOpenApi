using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrafficOpenApi.Core.Models
{
	public class TrafficInfoParamModel
	{
	}

	#region NTrafficInfo_P : 교통소통정보
	public class NTrafficInfo_P
	{
		public string key { get; set; } // 공개키
		public string ReqType { get; set; } // boundary 요청여부(2)
		public string TrafficInfo { get; set; } // 
		public string MinX { get; set; } // boundary MinX
		public string MinY { get; set; } // boundary MinY
		public string MaxX { get; set; } // boundary MaxX
		public string MaxY { get; set; } // boundary MaxY
		public int MapLevel { get; set; } // 지도 level(이미지 정보 요청시)
		public int ImageSize { get; set; } // 이미지크기(이미지 정보 요청시)
		public string ToXmlFileName()
		{
			string strRtn = string.Empty;
			if (this.MinX != null && this.MaxX != null && this.MinY != null && this.MaxY != null)
			{
				strRtn = $"{this.MinX},{this.MaxX},{this.MinY},{this.MaxY},{this.ReqType}.xml";
			}
			return strRtn;
		}
	}
	#endregion

	#region NEventIdentity_P : 공사정보
	public class NEventIdentity_P
	{
		public string key { get; set; } // 공개키
		public string ReqType { get; set; } // boundary 요청여부(2)
		public string type { get; set; } // 도로정보 (its : 국도 / ex : 고속도로)
		public string EventIdentity { get; set; } // 
		public string MinX { get; set; } // boundary MinX
		public string MinY { get; set; } // boundary MinY
		public string MaxX { get; set; } // boundary MaxX
		public string MaxY { get; set; } // boundary MaxY
		public string ToXmlFileName()
		{
			string strRtn = string.Empty;
			if (this.MinX != null && this.MaxX != null && this.MinY != null && this.MaxY != null)
			{
				strRtn = $"{this.MinX},{this.MaxX},{this.MinY},{this.MaxY},{this.ReqType}.xml";
			}
			return strRtn;
		}
	}
	#endregion

	#region NIncidentIdentity_P : 사고정보
	public class NIncidentIdentity_P
	{
		public string key { get; set; } // 공개키
		public string ReqType { get; set; } // boundary 요청여부(2)
		public string type { get; set; } // 도로정보 (its : 국도 / ex : 고속도로)
		public string IncidentIdentity { get; set; } // 
		public string MinX { get; set; } // boundary MinX
		public string MinY { get; set; } // boundary MinY
		public string MaxX { get; set; } // boundary MaxX
		public string MaxY { get; set; } // boundary MaxY
		public string ToXmlFileName()
		{
			string strRtn = string.Empty;
			if (this.MinX != null && this.MaxX != null && this.MinY != null && this.MaxY != null)
			{
				strRtn = $"{this.MinX},{this.MaxX},{this.MinY},{this.MaxY},{this.ReqType}.xml";
			}
			return strRtn;
		}
	}
	#endregion

	#region NCCTVInfo_P : CCTV영상
	public class NCCTVInfo_P
	{
		public string key { get; set; } // 공개키
		public string ReqType { get; set; } // boundary 요청여부(2)
		public string type { get; set; } // 도로정보 (its : 국도 / ex : 고속도로)
		public string CCTVInfo { get; set; } // 
		public string MinX { get; set; } // boundary MinX
		public string MinY { get; set; } // boundary MinY
		public string MaxX { get; set; } // boundary MaxX
		public string MaxY { get; set; } // boundary MaxY
		public string ToXmlFileName()
		{
			string strRtn = string.Empty;
			if (this.MinX != null && this.MaxX != null && this.MinY != null && this.MaxY != null)
			{
				strRtn = $"{this.MinX},{this.MaxX},{this.MinY},{this.MaxY},{this.ReqType}.xml";
			}
			return strRtn;
		}
	}
	#endregion

	#region VMS_P : VMS 표출정보
	public class VMS_P
	{
		public string key { get; set; } // 공개키
		public string getType { get; set; } // 포맷 ( xml : xml방식(기본값) / json : json방식 )
		public string ToXmlFileName()
		{
			return "VMS.xml";
		}
	}
	#endregion

	#region fcldata_P : 우회도로 예측정보
	public class fcldata_P
	{
		public string key { get; set; } // 공개키
		public string mainSectionId { get; set; } // 본선 구간 ID
		public string fCastYmd { get; set; } // 예측일자
		public string fCastH { get; set; } // 예측시간 (24시간기준)
		public string getType { get; set; } // 포맷 ( xml : xml방식(기본값) / json : json방식 )
		public string ToXmlFileName()
		{
			return "fcldata.xml";
		}
	}
	#endregion
}