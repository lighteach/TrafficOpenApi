using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using TrafficOpenApi.Core.Models;

namespace TrafficOpenApi.Core.Libs
{
	public class XmlHandling
	{
		#region GetXmlDocWithUpdateProceed : 로컬 XML 파일을 갱신 후 가져온 XML문서를 반환한다.
		/// <summary>
		/// GetXmlDocWithUpdateProceed : 로컬 XML 파일을 갱신 후 가져온 XML문서를 반환한다.
		/// </summary>
		/// <param name="targetXmlPath">갱신한 데이터를 저장할 XML 파일의 경로</param>
		/// <param name="target">로컬에 저장된 XML 파일을 XmlDocument로 변환한 갱신 대상 데이터</param>
		/// <param name="source">Http 통신 후 받아온 결과 XML을 XmlDocument로 변환한 데이터</param>
		/// <returns>갱신 처리된 target 파라미터 변수</returns>
		public XmlDocument GetXmlDocWithUpdateProceed(string targetXmlPath, XmlDocument target, XmlDocument source)
		{
			XmlNode srcResponse = source.SelectSingleNode("//response");
			XmlNode srcCoordType = srcResponse.SelectSingleNode("//coordtype");
			XmlNode srcDatacount = srcResponse.SelectSingleNode("//datacount");
			XmlNodeList srcDataList = srcResponse.SelectNodes("//data");

			// [작업개요]
			// response 노드는 coordtype 값을 구분으로 하여 여러개를 가지고 있을 수 있다
			// 그래서 갱신해야 할 대상 response 노드 는 반드시 받아온 xml의 coordtype에 일치하는 노드가 되어야 한다.
			XmlNode tarResponse = target.SelectSingleNode($"//responselist/response[coordtype={srcCoordType.InnerText}]");
			if (tarResponse != null)    // 갱신 할 response 노드가 존재한다면
			{
				XmlNode tarCoordtype = tarResponse.SelectSingleNode("//coordtype");
				XmlNode tarDatacount = tarResponse.SelectSingleNode("//datacount");
				XmlNodeList tarDataList = tarResponse.SelectNodes("//data");

				if (tarCoordtype.InnerText == srcCoordType.InnerText)
				{
					// datacount를 업데이트 한다.
					tarDatacount.InnerText = srcDatacount.InnerText;

					#region data 노드 업데이트
					//기존 data 노드들을 모두 삭제한다.
					for (int intCnt = (tarDataList.Count - 1); intCnt >= 0; intCnt--)
					{
						XmlNode data = tarDataList[intCnt];
						tarResponse.RemoveChild(data);
					}

					// data 노드들이 모두 삭제되었으면, 새로 받아온 xml에서 data 노드들을 집어넣어준다.
					if (tarResponse.SelectNodes("//response/data").Count == 0)
					{
						foreach (XmlNode srcData in srcDataList)
						{
							XmlNode data = target.CreateElement("data");
							data.InnerXml = srcData.InnerXml;
							tarResponse.AppendChild(data);
						}
					}
					#endregion

					target.Save(targetXmlPath);
				}
			}

			return source;
		} 
		#endregion

		#region GetXmlDocByFilePath : XML 파일을 기반으로 XmlDocument를 생성
		public XmlDocument GetXmlDocByFilePath(string path)
		{
			XmlDocument rtn = new XmlDocument();
			if (File.Exists(path))
				rtn.Load(path);
			return rtn;
		}
		#endregion

		#region GetXmlDocByString : XML 문자열을 기반으로 XmlDocument를 생성
		public XmlDocument GetXmlDocByString(string xml)
		{
			XmlDocument rtn = new XmlDocument();
			if (!string.IsNullOrEmpty(xml) && !string.IsNullOrWhiteSpace(xml))
				rtn.LoadXml(xml);
			return rtn;
		} 
		#endregion
	}
}
