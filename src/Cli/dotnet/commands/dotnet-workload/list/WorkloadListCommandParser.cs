// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.CommandLine;
using Microsoft.DotNet.Workloads.Workload;
using Microsoft.DotNet.Workloads.Workload.List;
using LocalizableStrings = Microsoft.DotNet.Workloads.Workload.List.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadListCommandParser
    {
        // arguments are a list of workload to be detected
        public static readonly Option<bool> MachineReadableOption = new("--machine-readable") { Hidden = true };

        public static readonly Option<string> VersionOption = InstallingWorkloadCommandParser.VersionOption;

        public static readonly Option<string> TempDirOption = new Option<string>("--temp-dir")
        {
            Description = Workloads.Workload.Install.LocalizableStrings.TempDirOptionDescription
        }.Hide();

        public static readonly Option<bool> IncludePreviewsOption = new Option<bool>("--include-previews")
        {
            Description = Workloads.Workload.Install.LocalizableStrings.IncludePreviewOptionDescription
        }.Hide();

        private static readonly Command Command = ConstructCommand();

        public static Command GetCommand()
        {
            return Command;
        }

        private static Command ConstructCommand()
        {
            Command command = new("list", LocalizableStrings.CommandDescription);
            command.Options.Add(MachineReadableOption);
            command.Options.Add(CommonOptions.HiddenVerbosityOption);
            command.Options.Add(VersionOption);
            command.Options.Add(TempDirOption);
            command.Options.Add(IncludePreviewsOption);
            command.AddWorkloadCommandNuGetRestoreActionConfigOptions(true);

            command.SetAction((parseResult) => new WorkloadListCommand(parseResult).Execute());

            return command;
        }
    }
}
