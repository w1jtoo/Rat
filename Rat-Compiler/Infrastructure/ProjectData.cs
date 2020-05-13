namespace Rat_Compiler.Infrastructure
{
    public class ProjectData : IProjectData
    {
        public ProjectData(string projectName)
        {
            ProjectName = projectName;
        }

        public string ProjectName { get;  }
        public string GetProjectEntryPoint => $"{ProjectName}.rat";

        // IN WORK
        public string Author { get; set; }
    }
}