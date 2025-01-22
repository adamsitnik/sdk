// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.CommandLine;
using Microsoft.DotNet.Cli.NuGetPackageDownloader;
using Microsoft.DotNet.Tools;
using LocalizableStrings = Microsoft.DotNet.Tools.Restore.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class WorkloadCommandNuGetRestoreActionConfigOptions
    {
        public static Option<bool> DisableParallelOption = new ForwardedOption<bool>("--disable-parallel")
        {
            Description = LocalizableStrings.CmdDisableParallelOptionDescription
        };

        public static Option<bool> NoCacheOption = new ForwardedOption<bool>("--no-cache")
        {
            Description = LocalizableStrings.CmdNoCacheOptionDescription,
            Hidden = true
        };

        public static Option<bool> NoHttpCacheOption = new ForwardedOption<bool>("--no-http-cache")
        {
            Description = LocalizableStrings.CmdNoCacheOptionDescription,
        };

        public static Option<bool> IgnoreFailedSourcesOption = new ForwardedOption<bool>("--ignore-failed-sources")
        {
            Description = LocalizableStrings.CmdIgnoreFailedSourcesOptionDescription
        };

        public static Option<bool> InteractiveRestoreOption = new ForwardedOption<bool>("--interactive")
        {
            Description = CommonLocalizableStrings.CommandInteractiveOptionDescription
        };

        public static Option<bool> HiddenDisableParallelOption = new ForwardedOption<bool>("--disable-parallel")
        {
            Description = LocalizableStrings.CmdDisableParallelOptionDescription
        }.Hide();

        public static Option<bool> HiddenNoCacheOption = new ForwardedOption<bool>("--no-cache")
        {
            Description = LocalizableStrings.CmdNoCacheOptionDescription
        }.Hide();

        public static Option<bool> HiddenNoHttpCacheOption = new ForwardedOption<bool>("--no-http-cache")
        {
            Description = LocalizableStrings.CmdNoCacheOptionDescription
        }.Hide();

        public static Option<bool> HiddenIgnoreFailedSourcesOption = new ForwardedOption<bool>("--ignore-failed-sources")
        {
            Description = LocalizableStrings.CmdIgnoreFailedSourcesOptionDescription
        }.Hide();

        public static Option<bool> HiddenInteractiveRestoreOption = new ForwardedOption<bool>("--interactive")
        {
            Description = CommonLocalizableStrings.CommandInteractiveOptionDescription,
        }.Hide();

        public static RestoreActionConfig ToRestoreActionConfig(this ParseResult parseResult)
        {
            return new RestoreActionConfig(DisableParallel: parseResult.GetValue(DisableParallelOption),
                NoCache: parseResult.GetValue(NoCacheOption) || parseResult.GetValue(NoHttpCacheOption),
                IgnoreFailedSources: parseResult.GetValue(IgnoreFailedSourcesOption),
                Interactive: parseResult.GetValue(InteractiveRestoreOption));
        }

        public static void AddWorkloadCommandNuGetRestoreActionConfigOptions(this Command command, bool Hide = false)
        {
            command.Options.Add(Hide ? HiddenDisableParallelOption : DisableParallelOption);
            command.Options.Add(Hide ? HiddenIgnoreFailedSourcesOption : IgnoreFailedSourcesOption);
            command.Options.Add(Hide ? HiddenNoCacheOption : NoCacheOption);
            command.Options.Add(Hide ? HiddenNoHttpCacheOption : NoHttpCacheOption);
            command.Options.Add(Hide ? HiddenInteractiveRestoreOption : InteractiveRestoreOption);
        }
    }
}
