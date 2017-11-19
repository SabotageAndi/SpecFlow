using SpecFlow.TestProjectGenerator.Inputs;

namespace SpecFlow.TestProjectGenerator.ProgramLanguageDrivers
{
    public interface IProgramLanguageInputProjectDriver
    {
        string GetBindingCode(string eventType, string code, string methodName, int hookOrder);
        string GetProjectFileName(string projectName);
        BindingClassInput GetDefaultBindingClass();
        string CodeFileExtension { get; }
    }
}