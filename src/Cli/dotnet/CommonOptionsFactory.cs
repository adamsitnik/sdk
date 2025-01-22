﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.CommandLine;

namespace Microsoft.DotNet.Cli
{
    /// <summary>
    /// Creates common options.
    /// </summary>
    internal static class CommonOptionsFactory
    {
        /// <summary>
        /// Creates common diagnositcs option (-d|--diagnostics).
        /// </summary>
        public static Option<bool> CreateDiagnosticsOption(bool recursive) => new("--diagnostics", "-d")
        {
            Description = Tools.Help.LocalizableStrings.SDKDiagnosticsCommandDefinition,
            Recursive = recursive
        };
    }
}
