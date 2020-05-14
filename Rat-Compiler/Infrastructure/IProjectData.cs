namespace Rat_Compiler.Infrastructure
{
    public interface IProjectData
    {
        string ProjectName { get;  }
        string GetEntryPointFileName { get; }
        string GetOutputFileName { get; }
    }
}