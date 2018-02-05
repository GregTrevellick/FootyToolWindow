using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using FootieData.Common;
using FootieData.Common.Options;
using FootieData.Entities.ReferenceData;
using FootieData.Vsix.Options;

namespace FootieData.Vsix
{
    [ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, "General", 0, 0, true)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]//[ProvideMenuResource(1000, 1)]
    [ProvideToolWindow(typeof(ToolWindow1b), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    [Guid(ToolWindow1Package.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ToolWindow1Package : Package
    {
        public const string PackageGuidString = "4431588e-199d-477f-b3c4-c0b9603602b0";

        public ToolWindow1Package()
        {
        }

        //gregt The Action<> types are simply Func<> with no return type e.g. Action<string> output = x => Console.WriteLine(x);
        protected override void Initialize()
        {
            ToolWindow1Command.Initialize(this);
            base.Initialize();

            ToolWindow1b.GetOptionsFromStoreAndMapToInternalFormatMethod =
                (string any)
                    => 
                {
                    var generalOptions = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));
                    ToolWindow1Control.GeneralOptions2 = GetGeneralOptions2(generalOptions);
                    return "also not needed - change Func to Action";
                };
        }

        private GeneralOptions2 GetGeneralOptions2(GeneralOptions generalOptions)
        {
            return new GeneralOptions2
            {
                LeagueOptions = new List<LeagueOption>
                {
                    GetLeagueOption(generalOptions.InterestedInBr1, InternalLeagueCode.BR1),
                    GetLeagueOption(generalOptions.InterestedInDe1, InternalLeagueCode.DE1),
                    GetLeagueOption(generalOptions.InterestedInDe2, InternalLeagueCode.DE2),
                    GetLeagueOption(generalOptions.InterestedInEs1, InternalLeagueCode.ES1),
                    GetLeagueOption(generalOptions.InterestedInFr1, InternalLeagueCode.FR1),
                    GetLeagueOption(generalOptions.InterestedInFr2, InternalLeagueCode.FR2),
                    GetLeagueOption(generalOptions.InterestedInIt1, InternalLeagueCode.IT1),
                    GetLeagueOption(generalOptions.InterestedInIt2, InternalLeagueCode.IT2),
                    GetLeagueOption(generalOptions.InterestedInNl1, InternalLeagueCode.NL1),
                    GetLeagueOption(generalOptions.InterestedInPt1, InternalLeagueCode.PT1),
                    GetLeagueOption(generalOptions.InterestedInUefa1, InternalLeagueCode.UEFA1),
                    GetLeagueOption(generalOptions.InterestedInUk1, InternalLeagueCode.UK1),
                    GetLeagueOption(generalOptions.InterestedInUk2, InternalLeagueCode.UK2),
                    GetLeagueOption(generalOptions.InterestedInUk3, InternalLeagueCode.UK3),
                    GetLeagueOption(generalOptions.InterestedInUk4, InternalLeagueCode.UK4),
                }
            };
        }

        private static LeagueOption GetLeagueOption(bool interestedIn, InternalLeagueCode internalLeagueCode)
        {
            return new LeagueOption
            {
                InternalLeagueCode = internalLeagueCode,
                ShowLeague = interestedIn,
                LeagueSubOptions = GetLeagueSubOptions()
            };
        }

        private static List<LeagueSubOption> GetLeagueSubOptions()
        {
            return new List<LeagueSubOption>
            {
                new LeagueSubOption
                {
                    GridType = GridType.Standing,
                    Expand = true
                },
                new LeagueSubOption
                {
                    GridType = GridType.Result,
                    Expand = false
                },
                new LeagueSubOption
                {
                    GridType = GridType.Fixture,
                    Expand = false
                }
            };
        }
    }
}
