using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficOpenApi.Core.Models
{
	public enum ResultXmlWithUpdateErrorKind
	{
		None = 0
		, RsXml = 1
		, Empty = 2
		, Null = 3
	}

	public class ResultXmlWithUpdateModel
	{
		public ResultXmlWithUpdateModel() { }

		public ResultXmlWithUpdateModel(bool success, ResultXmlWithUpdateErrorKind kind, string text)
		{
			IsSuccess = success;
			WhatError = kind;
			ResultText = text;
		}

		public bool IsSuccess { get; set; } = false;
		public ResultXmlWithUpdateErrorKind WhatError { get; set; }
		public string ResultText { get; set; }
	}
}
