using System.Collections.Frozen;
using CliWrapper.Models;

namespace CliWrapper;

public interface IDotnetCliWrapper
{
    Task<bool> IsAnyVersionInstalled();

    Task<FrozenSet<InstalledSdk>?> GetInstalledSdks();
}