using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
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

		private string m_LocalPath = "";
		private string m_ConfigFileName = "TrafficInfoSettings.json";

		public TrafficInfo(string localPath)
		{
			m_LocalPath = localPath;
		}

		#region GetSettings : 설정 파일을 읽어온다
		public TrafficInfoSettingsModel GetSettings
		{
			get
			{
				if (string.IsNullOrEmpty(m_LocalPath)) throw new Exception("Config 파일의 디렉토리 경로는 반드시 제공되어야 합니다.");

				TrafficInfoSettingsModel model = null;
				string configPath = Path.Combine(m_LocalPath, m_ConfigFileName);

				if (File.Exists(configPath))
				{
					string configStr = File.ReadAllText(configPath);
					model = JsonConvert.DeserializeObject<TrafficInfoSettingsModel>(configStr);

					// 스와핑 되는 XML 파일이 위치 할 디렉토리가 없으면 만들어야 한다.
					if (!Directory.Exists(model.ApiXmlPath))
						Directory.CreateDirectory(model.ApiXmlPath);
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

		#region GetResultXml : Http GET 통신을 수행 후 결과 XML 문자열을 반환한다.
		public string GetResultXml(TrafficInfoDirection direction, string urlParams)
		{
			TrafficInfoSettingsModel settings = GetSettings;
			TrafficInfoApiUrl apiUrl = settings.ApiUrlList.FirstOrDefault<TrafficInfoApiUrl>(a => a.name.Equals(direction.ToString()));
			string reqUrl = $"{apiUrl.url}?{urlParams}";

			string result = string.Empty;

			try
			{
				HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(reqUrl);
				req.Method = "GET";
				req.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
				req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
				req.Timeout = 10000;        // 10초만 기다린다. ms 이기 때문에 10초는 10000ms 이다.
				using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
				{
					using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
					{
						result = sr.ReadToEnd();
					}
				}
			}
			catch (Exception ex)
			{
				string exStr = ex.ToString();
			}

			return result;
		}
		#endregion

		#region GetResultXmlWithUpdate : 통신 후 가져온 xml 내용을 로컬 XML 파일에 갱신하면서 반환한다
		public ResultXmlWithUpdateModel GetResultXmlWithUpdate(TrafficInfoDirection direction, string xmlPath, string urlParams)
		{
			ResultXmlWithUpdateModel rtn = new ResultXmlWithUpdateModel(false, ResultXmlWithUpdateErrorKind.None, string.Empty);

			TrafficInfoSettingsModel settings = GetSettings;
			// 혹시라도 xmlPath변수에 저장 디렉토리가 있는지 확인해서 없으면 연결해준다.
			if (xmlPath.IndexOf(settings.ApiXmlPath) == -1)
			{
				xmlPath = Path.Combine(settings.ApiXmlPath, xmlPath);
			}

			XmlDocument xdRtn = new XmlDocument();
			XmlHandling xmlHand = new XmlHandling();

			string xml = GetResultXml(direction, urlParams);
			if (!string.IsNullOrEmpty(xml))      // 통신해서 가져온 내용이 있으면
			{
				// XML 내용이 있긴 있는데 에러 관련 XML이면
				if (xml.IndexOf("<rs>") != -1)
				{
					xdRtn.LoadXml(xml);
					rtn = new ResultXmlWithUpdateModel(false, ResultXmlWithUpdateErrorKind.RsXml, xdRtn.SelectSingleNode("//rs").InnerText);
				}
				else
				{
					if (File.Exists(xmlPath))               // 파일이 있는지 찾아서 있으면
					{
						// 파일 내용을 받아온 xml을 로컬 xml 파일에 갱신한다.
						XmlDocument xdResult = xmlHand.GetXmlDocByString(xml);
						XmlDocument xdTarget = xmlHand.GetXmlDocByFilePath(xmlPath);
						xdRtn = xmlHand.GetXmlDocWithUpdateProceed(xmlPath, xdTarget, xdResult);
					}
					else
					{
						// XML 파일이 없으면 생성하고 거기다가 갱신한다.
						XmlDocument xdResult = xmlHand.GetXmlDocByString(xml);
						XmlNode resultResp = xdResult.SelectSingleNode("//response");

						XmlDocument xdTmp = new XmlDocument();
						XmlDeclaration xDeclare = xdTmp.CreateXmlDeclaration("1.0", "UTF-8", null);
						xdTmp.AppendChild(xDeclare);

						XmlNode responselist = xdTmp.CreateElement("responselist");
						responselist.InnerXml = resultResp.OuterXml;

						xdTmp.AppendChild(responselist);
						xdTmp.Save(xmlPath);

						// 리턴 될 XmlDocument에도 전달해준다
						xdRtn.LoadXml(xml);
						rtn = new ResultXmlWithUpdateModel(true, ResultXmlWithUpdateErrorKind.None, xdRtn.OuterXml);
					}
				}
			}
			else
			{
				rtn = new ResultXmlWithUpdateModel(false, ResultXmlWithUpdateErrorKind.Empty, string.Empty);
			}

			return rtn;
		} 
		#endregion

		#region XmlToModel : xml 스트링을 객체화(직렬화) 한다
		/// <summary>
		/// XmlToModel : xml 스트링을 객체화(직렬화) 한다
		/// </summary>
		/// <typeparam name="T">객체화를 희망하는 객체타입</typeparam>
		/// <param name="xmlStr">객체화 시킬 XML 내용</param>
		/// <returns>객체화 된 객체</returns>
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
		#endregion

		#region NTrafficInfo : 교통소통정보
		public NTrafficInfo_R NTrafficInfo(NTrafficInfo_P p)
		{
			string result = "";
			NTrafficInfo_R model = new NTrafficInfo_R();
			p.key = GetSettings.CertKey;
			ResultXmlWithUpdateModel xmlResult = GetResultXmlWithUpdate(TrafficInfoDirection.NTrafficInfo, p.ToXmlFileName(), ClassSerialize(p));
			if (xmlResult.IsSuccess)
			{
				model = XmlToModel<NTrafficInfo_R>(xmlResult.ResultText);
			}
			
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