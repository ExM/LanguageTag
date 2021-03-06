﻿using NUnit.Framework;
using System;

namespace AbbyyLS.Globalization.Bcp47
{
	[TestFixture]
	public class RegionTests
	{
		[Test]
		public void CheckSwitches()
		{
			foreach (var text in TestContent.GetRegions())
			{
				var region = text.TryParseFromRegion().Value;
				
				Assert.NotNull(region.ToText());
			}
		}

		[Test]
		public void ToTextFail()
		{
			Assert.Throws<NotImplementedException>(() =>
			{
				var en = (Region)(-1);
				en.ToText();
			});
		}

		[TestCase("xxx", null)]
		[TestCase("RU", Region.RU)]
		[TestCase("gb", Region.GB)]
		public void ParseFromRegion(string text, Region? expected)
		{
			Assert.AreEqual(expected, text.TryParseFromRegion());
		}
	}
}
