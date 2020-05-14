namespace Rat_Compiler.Infrastructure
{
    public class ProjectData : IProjectData
    {
        // this class in future will be created by .yaml config file or relevant anguments 
        public ProjectData(string projectName)
        {
            ProjectName = projectName;
        }

        public string ProjectName { get;  }
        public string GetEntryPointFileName => $"{ProjectName}.rat";
        public string GetOutputFileName => $"{ProjectName}.dll";

        // IN WORK
        public string Author { get; set; }
    }
}