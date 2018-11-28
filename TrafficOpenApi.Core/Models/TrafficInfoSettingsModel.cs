using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficOpenApi.Core.Models
{
	/// <summary>
	/// OpenAPI 업데이트 셋팅 정보 모델
	/// </summary>
	public class TrafficInfoSettingsModel
	{
		/// <summary>
		/// 각 용도별 API 리스트
		/// </summary>
		public List<TrafficInfoApiUrl> ApiUrlList { get; set; }
		/// <summary>
		/// XML 파일 갱신 주기(millisecond)
		/// </summary>
		public int UpdateTime { get; set; }
		/// <summary>
		/// XML 파일이 저장 될 경로
		/// </summary>
		public string ApiXmlPath { get; set; }
		/// <summary>
		/// OpenAPI 인증키
		/// </summary>
		public string CertKey { get; set; }
	}

	public class TrafficInfoApiUrl
	{
		public string name { get; set; }
		public string url { get; set; }
		public string description { get; set; }
	}
}
