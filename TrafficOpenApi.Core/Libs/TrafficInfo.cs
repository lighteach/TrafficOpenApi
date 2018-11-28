using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using TrafficOpenApi.Core.Models;

namespace TrafficOpenApi.Core.Libs
{
	// 이 라이브러리에서는 각 API 에 해당하는 호출에 따라
	// 스케쥴링 되어 저장되는 XML 파일을 Read하여 모델링 후
	// 반환하는 메서드를 구현한다. ==> 그건 별도 구현으로
	// 여기서는 단순히 교통정보공개서비스와 핵심 통신함
	public class TrafficInfo
	{
		#region TrafficInfoDirection : API 통신 목적 정의 열거형
		public enum TrafficInfoDirection
		{
			NTrafficInfo
			, NEventIdentity
			, NIncidentIdentity
			, NCCTVInfo
			, VMS
			, fcldata
		}
		#endregion

		private string m_ConfigFileName = "TrafficInfoSettings.json";

		#region GetSettings : 설정 파일을 읽어온다
		public TrafficInfoSettingsModel GetSettings
		{
			get
			{
				TrafficInfoSettingsModel model = null;
				string configPath = Directory.GetCurrentDirectory();
				configPath = Path.Combine(configPath, m_ConfigFileName);

				if (File.Exists(configPath))
				{
					string configStr = File.ReadAllText(configPath);
					model = JsonConvert.DeserializeObject<TrafficInfoSettingsModel>(configStr);
				}
				else
				{
					throw new Exception($"{m_ConfigFileName} 파일을 찾을 수 없습니다.");
				}

				return model;

			}
		}
		#endregion

		#region ClassSerialize : 클래스를 직렬화하여 URL 파라미터 형식으로 변환
		public string ClassSerialize(object obj)
		{
			string[] arrStr = obj.GetType().GetProperties().Where(p => p.GetValue(obj, null) != null)
				.Select(p => $"{p.Name}={p.GetValue(obj, null).ToString()}").ToArray();
			string result = string.Join("&", arrStr);
			return result;
		}
		#endregion

		#region GetResultXml : 비동기 GET 통신을 수행 후 결과 XML 문자열을 반환한다.
		public string GetResultXml(TrafficInfoDirection direction, string urlParams)
		{
			TrafficInfoSettingsModel settings = GetSettings;
			TrafficInfoApiUrl apiUrl = settings.ApiUrlList.FirstOrDefault<TrafficInfoApiUrl>(a => a.name.Equals(direction.ToString()));
			string reqUrl = $"{apiUrl.url}?{urlParams}";
			WebClient web = new WebClient();
			string result = web.DownloadString(reqUrl);

			return result;
		}
		#endregion

		public string GetResultXmlWithUpdate(TrafficInfoDirection direction, string xmlPath, string urlParams)
		{
			XmlDocument xdRtn = new XmlDocument();
			XmlHandling xmlHand = new XmlHandling();

			string xml = GetResultXml(direction, urlParams);
			if (!string.IsNullOrEmpty(xml))      // 통신해서 가져온 내용이 있으면
			{
				if (File.Exists(xmlPath))               // 파일이 있는지 찾아서 있으면
				{
					// 파일 내용을 받아온 xml을 로컬 xml 파일에 갱신한다.
					XmlDocument xdResult = xmlHand.GetXmlDocByString(xml);
					XmlDocument xdTarget = xmlHand.GetXmlDocByFilePath(xmlPath);
					xdRtn = xmlHand.GetXmlDocWithUpdateProceed(xdTarget, xdResult);
				}
			}

			return xdRtn.OuterXml;
		}

		XmlSerializer serializer = null;

		public T XmlToModel<T>(string xmlStr)
		{
			T rtn = default(T);
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (TextReader reader = new StringReader(xmlStr))
			{
				rtn = (T)serializer.Deserialize(reader);
			}
			return rtn;
		}

		#region NTrafficInfo : 교통소통정보
		public NTrafficInfo_R NTrafficInfo(NTrafficInfo_P p)
		{
			p.key = GetSettings.CertKey;
			string result = GetResultXmlWithUpdate(TrafficInfoDirection.NTrafficInfo, p.ToXmlFileName(), ClassSerialize(p));
			NTrafficInfo_R model = XmlToModel<NTrafficInfo_R>(result);
			return model;
		}
		#endregion

		#region NEventIdentity : 공사정보
		public string NEventIdentity(NEventIdentity_P p)
		{
			p.key = GetSettings.CertKey;
			return GetResultXmlWithUpdate(TrafficInfoDirection.NEventIdentity, p.ToXmlFileName(), ClassSerialize(p));
		}
		#endregion

		#region NIncidentIdentity : 사고정보
		public string NIncidentIdentity(NIncidentIdentity_P p)
		{
			p.key = GetSettings.CertKey;
			return GetResultXmlWithUpdate(TrafficInfoDirection.NIncidentIdentity, p.ToXmlFileName(), ClassSerialize(p));
		}
		#endregion

		#region NCCTVInfo : CCTV영상
		public string NCCTVInfo(NCCTVInfo_P p)
		{
			p.key = GetSettings.CertKey;
			return GetResultXmlWithUpdate(TrafficInfoDirection.NCCTVInfo, p.ToXmlFileName(), ClassSerialize(p));
		}
		#endregion

		#region VMS : VMS 표출정보
		public string VMS(VMS_P p)
		{
			p.key = GetSettings.CertKey;
			return GetResultXmlWithUpdate(TrafficInfoDirection.VMS, p.ToXmlFileName(), ClassSerialize(p));
		}
		#endregion

		#region fcldata : 교통소통정보
		public string fcldata(fcldata_P p)
		{
			p.key = GetSettings.CertKey;
			return GetResultXmlWithUpdate(TrafficInfoDirection.fcldata, p.ToXmlFileName(), ClassSerialize(p));
		}
		#endregion
	}
}