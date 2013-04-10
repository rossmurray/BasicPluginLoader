using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestContracts;

namespace TestPlugin
{
	public class TestPlugin : ITestPlugin
	{
		public int TestValue { get; set; }

		public TestPlugin(int i)
		{
			this.TestValue = i;
		}
	}
}
