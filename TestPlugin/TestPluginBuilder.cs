using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using TestContracts;

namespace TestPlugin
{
	[Export(typeof(ITestPluginBuilder))]
	public class TestPluginBuilder : ITestPluginBuilder
	{
		private Random r;

		public TestPluginBuilder()
		{
			this.r = new Random();
		}

		public ITestPlugin Build()
		{
			return new TestPlugin(r.Next());
		}
	}
}
