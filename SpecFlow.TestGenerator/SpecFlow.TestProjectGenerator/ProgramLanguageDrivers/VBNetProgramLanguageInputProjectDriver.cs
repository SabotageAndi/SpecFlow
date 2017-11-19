using SpecFlow.TestProjectGenerator.Inputs;

namespace SpecFlow.TestProjectGenerator.ProgramLanguageDrivers
{
    class VBNetProgramLanguageInputProjectDriver : ProgramLanguageInputProjectDriver
    {
        public override string GetBindingCode(string eventType, string code, string methodName, int hookOrder)
        {
            var staticKeyword = IsStaticEvent(eventType) ? "Shared" : "";
            return string.Format(@"<{0}(Order = {4})> {1} Public Sub {3}() 
                                {{
                                    Console.WriteLine(""BindingExecuted:{0}"")
                                    {2}
                                End Sub",
                                eventType,
                                staticKeyword,
                                code,
                                methodName,
                                hookOrder);
        }

        public override string GetProjectFileName(string projectName)
        {
            return $"{projectName}.vbproj";
        }

        public override BindingClassInput GetDefaultBindingClass()
        {
            return new BindingClassInput("DefaultBindings.vb");
        }

        public override string CodeFileExtension => "vb";

    }
}
