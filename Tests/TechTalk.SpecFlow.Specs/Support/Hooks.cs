using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecFlow.TestProjectGenerator;

namespace TechTalk.SpecFlow.Specs.Support
{
    [Binding]
    public class Hooks
    {
        private readonly Folders _folders;
        private readonly InputProjectDriver _inputProjectDriver;

        public Hooks(Folders folders, InputProjectDriver inputProjectDriver)
        {
            _folders = folders;
            _inputProjectDriver = inputProjectDriver;
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            _folders.SpecFlow = Path.Combine(_folders.SpecFlow, "SpecFlow");
            _inputProjectDriver.TestingFrameworkPackage = String.Empty;
            _inputProjectDriver.UnitTestProvider = global::SpecFlow.TestProjectGenerator.UnitTestProvider.NUnit3;
        }
    }
}
