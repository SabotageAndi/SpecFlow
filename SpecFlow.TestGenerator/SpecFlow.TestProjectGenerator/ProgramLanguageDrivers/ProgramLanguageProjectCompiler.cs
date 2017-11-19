using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Build.Evaluation;
using SpecFlow.TestProjectGenerator.Inputs;

namespace SpecFlow.TestProjectGenerator.ProgramLanguageDrivers
{
    internal abstract class ProgramLanguageProjectCompiler : IProgramLanguageProjectCompiler
    {
        protected ProjectCompilerHelper ProjectCompilerHelper;

        protected ProgramLanguageProjectCompiler(ProjectCompilerHelper projectCompilerHelper)
        {
            ProjectCompilerHelper = projectCompilerHelper;
        }

        protected abstract string BindingClassFileName { get; }
        protected abstract string BindingFileName { get; }


        public void AddBindingClass(InputProjectDriver inputProjectDriver, Project project, BindingClassInput bindingClassInput)
        {
            if (bindingClassInput.RawClass != null && bindingClassInput.RawClass.Contains("[assembly:"))
            {
                var outputPath = Path.Combine(inputProjectDriver.ProjectFolder, bindingClassInput.ProjectRelativePath);
                File.WriteAllText(outputPath, bindingClassInput.RawClass, Encoding.UTF8);
            }
            else if (bindingClassInput.RawClass != null)
            {
                ProjectCompilerHelper.SaveFileFromResourceTemplate(inputProjectDriver.ProjectFolder, BindingClassFileName, bindingClassInput.FileName,
                    new Dictionary<string, string>
                    {
                        {"AdditionalUsings", ""},
                        {"BindingClass", bindingClassInput.RawClass}
                    });
            }
            else
            {
                var bindingsCode = GetBindingsCode(bindingClassInput);
                ProjectCompilerHelper.SaveFileFromResourceTemplate(inputProjectDriver.ProjectFolder, BindingFileName, bindingClassInput.FileName,
                    new Dictionary<string, string>
                    {
                        {"ClassName", bindingClassInput.Name},
                        {"Bindings", bindingsCode}
                    });
            }
            project.AddItem("Compile", bindingClassInput.ProjectRelativePath);
        }

        public abstract string FileEnding { get; }
        public abstract string ProjectFileName { get; }
        public abstract void AdditionalAdjustments(Project project, InputProjectDriver inputProjectDriver);

        protected abstract string GetBindingsCode(BindingClassInput bindingClassInput);
    }
}