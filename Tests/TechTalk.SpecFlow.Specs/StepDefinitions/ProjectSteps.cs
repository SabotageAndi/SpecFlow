using System;
using System.Linq;
using System.Text;
using FluentAssertions;
using SpecFlow.TestProjectGenerator;
using TechTalk.SpecFlow.Specs.Drivers;

namespace TechTalk.SpecFlow.Specs.StepDefinitions
{
    [Binding]
    public class ProjectSteps
    {
        private readonly InputProjectDriver inputProjectDriver;
        private readonly ProjectCompiler projectCompiler;
        private readonly HooksDriver _hooksDriver;

        public ProjectSteps(InputProjectDriver inputProjectDriver, ProjectCompiler projectCompiler, HooksDriver hooksDriver)
        {
            this.inputProjectDriver = inputProjectDriver;
            this.projectCompiler = projectCompiler;
            _hooksDriver = hooksDriver;
        }

        [Given(@"there is a SpecFlow project")]
        public void GivenThereIsASpecFlowProject()
        {

        }

        //[Given(@"there is a SpecFlow project '(.*)'")]
        //public void GivenThereIsASpecFlowProject(string projectName)
        //{
        //    inputProjectDriver.ProjectName = projectName;
        //}

        [Given(@"I have a '(.*)' test project")]
        public void GivenIHaveATestProject(string language)
        {
            switch (language)
            {
                case "C#":
                    inputProjectDriver.ProgrammingLanguage = global::SpecFlow.TestProjectGenerator.ProgrammingLanguage.CSharp;

                    break;
                case "VB.Net":
                    inputProjectDriver.ProgrammingLanguage = global::SpecFlow.TestProjectGenerator.ProgrammingLanguage.VB;
                    break;
            }

        }


        private bool isCompiled = false;
        private Exception CompilationError;

        //        [BeforeScenarioBlock]
        //        public void CompileProject()
        //        {
        //            if ((ScenarioContext.Current.CurrentScenarioBlock == ScenarioBlock.When))
        //            {
        //                EnsureCompiled();
        //            }
        //        }
        //
        public void EnsureCompiled()
        {
            if (!isCompiled)
            {
                try
                {
                    CompileInternal();
                }
                finally
                {
                    isCompiled = true;
                }
            }
        }

        private void CompileInternal()
        {
            projectCompiler.Compile(inputProjectDriver);

            _hooksDriver.EnsureInitialized();
        }

        [When(@"the project is compiled")]
        public void WhenTheProjectIsCompiled()
        {
            try
            {
                CompilationError = null;
                CompileInternal();
            }
            catch (Exception ex)
            {
                CompilationError = ex;
            }
        }

        [Then(@"no compilation errors are reported")]
        public void ThenNoCompilationErrorsAreReported()
        {
            CompilationError.Should().BeNull();
        }
    }
}
