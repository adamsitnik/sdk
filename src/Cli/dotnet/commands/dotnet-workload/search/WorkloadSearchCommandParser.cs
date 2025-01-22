// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.CommandLine;
using Microsoft.DotNet.Workloads.Workload;
using Microsoft.DotNet.Workloads.Workload.Search;
using LocalizableStrings = Microsoft.DotNet.Workloads.Workload.Search.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadSearchCommandParser
    {
        public static readonly Argument<string> WorkloadIdStubArgument =
            new(LocalizableStrings.WorkloadIdStubArgumentName)
            {
                Arity = ArgumentArity.ZeroOrOne,
                Description = LocalizableStrings.WorkloadIdStubArgumentDescription
            };

        public static readonly Option<string> VersionOption = InstallingWorkloadCommandParser.VersionOption;

        private static readonly Command Command = ConstructCommand();

        public static Command GetCommand()
        {
            return Command;
        }

        private static Command ConstructCommand()
        {
            var command = new Command("search", LocalizableStrings.CommandDescription);
            command.Subcommands.Add(WorkloadSearchVersionsCommandParser.GetCommand());
            command.Arguments.Add(WorkloadIdStubArgument);
            command.Options.Add(CommonOptions.HiddenVerbosityOption);
            command.Options.Add(VersionOption);

            command.SetAction((parseResult) => new WorkloadSearchCommand(parseResult).Execute());

            return command;
        }
    }
}
