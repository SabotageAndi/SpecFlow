using System.Collections.Generic;

namespace SpecFlow.TestProjectGenerator.Inputs
{
    public class BindingClassInput : FileInput
    {
        public BindingClassInput(string fileName, string folder = ".")
            : base(fileName, folder)
        {
            StepBindings = new List<StepBindingInput>();
            OtherBindings = new List<string>();
        }

        public BindingClassInput(string fileName, string rawClass, string folder) : base(fileName, folder)
        {
            RawClass = rawClass;
        }

        public string RawClass { get; set; }

        public List<StepBindingInput> StepBindings { get; }
        public List<string> OtherBindings { get; }


    }
}