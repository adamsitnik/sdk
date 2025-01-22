// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.CommandLine;
using Microsoft.DotNet.Workloads.Workload;
using Microsoft.DotNet.Workloads.Workload.Search;
using LocalizableStrings = Microsoft.DotNet.Workloads.Workload.Search.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadSearchVersionsCommandParser
    {
        public static readonly Argument<string> WorkloadVersionArgument =
            new(LocalizableStrings.WorkloadVersionArgument)
            {
                Arity = ArgumentArity.ZeroOrOne,
                Description = LocalizableStrings.WorkloadVersionArgumentDescription
            };

        public static readonly Option<int> TakeOption = new("--take") { DefaultValueFactory = (_) => 5 };

        public static readonly Option<string> FormatOption = new("--format")
        {
            Description = LocalizableStrings.FormatOptionDescription
        };

        public static readonly Option<bool> IncludePreviewsOption = new("--include-previews");

        private static readonly Command Command = ConstructCommand();

        public static Command GetCommand()
        {
            return Command;
        }

        private static Command ConstructCommand()
        {
            var command = new Command("version", LocalizableStrings.PrintSetVersionsDescription);
            command.Arguments.Add(WorkloadVersionArgument);
            command.Options.Add(FormatOption);
            command.Options.Add(TakeOption);
            command.Options.Add(IncludePreviewsOption);

            TakeOption.Validators.Add(optionResult =>
            {
                if (optionResult.GetValueOrDefault<int>() <= 0)
                {
                    throw new ArgumentException("The --take option must be positive.");
                }
            });

            command.Validators.Add(result =>
            {
                if (result.GetValue(WorkloadSearchCommandParser.WorkloadIdStubArgument) != null)
                {
                    result.AddError(string.Format(LocalizableStrings.CannotCombineSearchStringAndVersion, WorkloadSearchCommandParser.WorkloadIdStubArgument.Name, command.Name));
                }
            });

            command.Validators.Add(result =>
            {
                var versionArgument = result.GetValue(WorkloadVersionArgument);
                if (versionArgument is not null && !WorkloadSetVersion.IsWorkloadSetPackageVersion(versionArgument))
                {
                    result.AddError(string.Format(CommandLineValidation.LocalizableStrings.UnrecognizedCommandOrArgument, versionArgument));
                }
            });

            command.SetAction(parseResult => new WorkloadSearchVersionsCommand(parseResult).Execute());

            return command;
        }
    }
}
