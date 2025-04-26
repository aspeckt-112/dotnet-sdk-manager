using System.Diagnostics;
using SdkManager.ProcessManagers.Abstractions;

namespace SdkManager.ProcessManagers;

public class MacOsProcessManager : IProcessManager
{
    public void OpenDirectory(string path)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("open", path);
        Process.Start(startInfo);
    }
}