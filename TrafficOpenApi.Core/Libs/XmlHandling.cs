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
		/// <summary>
		/// XmlUpdateProceed : 로컬 XML 파일을 갱신한다.
		/// </summary>
		/// <param name="target">XML 파일 내용을 XmlDocument로 변환한 갱신 대상 데이터</param>
		/// <param name="source">Http 통신 후 받아온 결과 XML을 XmlDocument로 변환한 데이터</param>
		/// <returns>갱신 처리된 target 파라미터 변수</returns>
		public XmlDocument GetXmlDocWithUpdateProceed(XmlDocument target, XmlDocument source)
		{
			XmlNode srcResponse = source.SelectSingleNode("//response");
			XmlNode srcCoordType = srcResponse.SelectSingleNode("//coordtype");
			XmlNode srcDatacount = srcResponse.SelectSingleNode("//datacount");
			XmlNodeList srcDataList = srcResponse.SelectNodes("//data");

			// [작업개요]
			// response 노드는 coordtype 값을 구분으로 하여 여러개를 가지고 있을 수 있다
			// 그래서 갱신해야 할 대상 response 노드 는 반드시 받아온 xml의 coordtype에 일치하는 노드가 되어야 한다.
			XmlNode tarResponse = target.SelectSingleNode($"//responselist/response[coordtype={srcCoordType.Value}]");
			if (tarResponse != null)	// 갱신 할 response 노드가 존재한다면
			{
				XmlNode tarCoordtype = tarResponse.SelectSingleNode("//coordtype");
				XmlNode tarDatacount = tarResponse.SelectSingleNode("//datacount");
				XmlNodeList tarDataList = tarResponse.SelectNodes("//data");

				if (tarCoordtype.Value == srcCoordType.Value)
				{
					// datacount를 업데이트 한다.
					tarDatacount.Value = srcDatacount.Value;

					#region data 노드 업데이트
					//기존 data 노드들을 모두 삭제한다.
					while (tarDataList.Count > 0)
					{
						XmlNode firstChild = tarDataList.Cast<XmlNode>().FirstOrDefault();
						if (firstChild != null)
						{
							tarResponse.RemoveChild(firstChild);
						}
					}
					// data 노드들이 모두 삭제되었으면, 새로 받아온 xml에서 data 노드들을 집어넣어준다.
					if (tarDataList.Count == 0)
					{
						foreach (XmlNode srcData in srcDataList)
						{
							tarResponse.AppendChild(srcData);
						}
					}
					#endregion
				}
			}
			else    // 갱신 할 response 노드가 없어서 신규로 등록해야한다면
			{
				XmlNode responseList = target.SelectSingleNode("//responselist");
				responseList.AppendChild(srcResponse);
			}

			return target;
		}

		public XmlDocument GetXmlDocByFilePath(string path)
		{
			XmlDocument rtn = new XmlDocument();
			rtn.Load(path);
			return rtn;
		}

		public XmlDocument GetXmlDocByString(string xml)
		{
			XmlDocument rtn = new XmlDocument();
			rtn.LoadXml(xml);
			return rtn;
		}
	}
}
