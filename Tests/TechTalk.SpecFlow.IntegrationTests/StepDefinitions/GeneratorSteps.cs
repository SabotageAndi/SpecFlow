using System;
using FluentAssertions;
using SpecFlow.TestProjectGenerator;

namespace TechTalk.SpecFlow.IntegrationTests.StepDefinitions
{
    [Binding]
    public class GeneratorSteps : Steps
    {
        private readonly InputProjectDriver _inputProjectDriver;
        private readonly ProjectCompiler _projectCompiler;
        private Exception _compilationError;

        public GeneratorSteps(InputProjectDriver inputProjectDriver, ProjectCompiler projectCompiler)
        {
            _inputProjectDriver = inputProjectDriver;
            _projectCompiler = projectCompiler;
        }

        [When(@"the feature files in the project are generated")]
        public void WhenTheFeatureFilesInTheProjectAreGenerated()
        {
            try
            {
                _projectCompiler.Compile(_inputProjectDriver);
            }
            catch (Exception ex)
            {
                _compilationError = ex;
            }
        }

        [Then(@"no generation error is reported")]
        public void ThenNoGenerationErrorIsReported()
        {
            _compilationError.Should().BeNull();
        }
    }
}