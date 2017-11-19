using SpecFlow.TestProjectGenerator.Inputs;

namespace SpecFlow.TestProjectGenerator.ProgramLanguageDrivers
{
    public class CSharpProgramLanguageInputProjectDriver : ProgramLanguageInputProjectDriver
    {
        public override string GetBindingCode(string eventType, string code, string methodName, int hookOrder)
        {
            var staticKeyword = IsStaticEvent(eventType) ? "static" : "";
            return string.Format(@"[{0}(Order = {4})] {1} public void {3}() 
                                {{
                                    Console.WriteLine(""BindingExecuted:{0}"");
                                    {2}
                                }}", 
                                eventType, 
                                staticKeyword, 
                                code,
                                methodName,
                                hookOrder);
        }

        public override string GetProjectFileName(string projectName)
        {
            return $"{projectName}.csproj";
        }

        public override BindingClassInput GetDefaultBindingClass()
        {
            return new BindingClassInput("DefaultBindings.cs");
        }

        public override string CodeFileExtension => "cs";
    }
}