using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestContracts
{
	public interface ITestPluginBuilder
	{
		ITestPlugin Build();
	}

	public interface ITestPlugin
	{
		int TestValue { get; set; }
	}
}
