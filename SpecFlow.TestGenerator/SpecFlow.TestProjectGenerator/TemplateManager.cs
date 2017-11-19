using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace SpecFlow.TestProjectGenerator
{
    public class TemplateManager
    {
        public string LoadTemplate(string templateName, Dictionary<string, string> replacements = null)
        {
            var assembly = Assembly.GetAssembly(typeof(TemplateManager));
            Debug.Assert(assembly != null);

            string resourceNameToFind = "TestProjectTemplates." + templateName;
            var resourceName = assembly.GetManifestResourceNames().Where(i => i.EndsWith(resourceNameToFind)).SingleOrDefault();
            Debug.Assert(resourceName != null);

            var projectTemplateStream = assembly.GetManifestResourceStream(resourceName);
            Debug.Assert(projectTemplateStream != null);
            string fileContent = new StreamReader(projectTemplateStream).ReadToEnd();

            if (replacements != null)
            {
                fileContent = replacements.Aggregate(fileContent, (current, replacement) => current.Replace("{" + replacement.Key + "}", replacement.Value));
            }

            return fileContent;
        }

        public XDocument LoadXmlTemplate(string templateName, Dictionary<string, string> replacements = null)
        {
            var fileContent = LoadTemplate(templateName, replacements);
            return XDocument.Parse(fileContent);
        }
    }
}
