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
    [ProvideToolWindow(typeof(VsixToolWindowPane), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    [Guid(ToolWindow1Package.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ToolWindow1Package : Package
    {
        public const string PackageGuidString = "4431588e-199d-477f-b3c4-c0b9603602b0";

        public ToolWindow1Package()
        {
        }

        protected override void Initialize()
        {
            VsixToolWindowCommand.Initialize(this);
            base.Initialize();

            VsixToolWindowPane.GetOptionsFromStoreAndMapToInternalFormatMethod =
                (string any)
                    => 
                {
                    var generalOptions = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));
                    ToolWindow1Control.LeagueGeneralOptions = GetLeagueGeneralOptions(generalOptions);
                    //return string.Empty;
                };
        }

        private LeagueGeneralOptions GetLeagueGeneralOptions(GeneralOptions generalOptions)
        {
            return new LeagueGeneralOptions
            {
                LeagueOptions = new List<LeagueOption>
                {
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInBr1, InternalLeagueCode.BR1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInDe1, InternalLeagueCode.DE1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInDe2, InternalLeagueCode.DE2),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInEs1, InternalLeagueCode.ES1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInFr1, InternalLeagueCode.FR1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInFr2, InternalLeagueCode.FR2),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInIt1, InternalLeagueCode.IT1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInIt2, InternalLeagueCode.IT2),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInNl1, InternalLeagueCode.NL1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInPt1, InternalLeagueCode.PT1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInUefa1, InternalLeagueCode.UEFA1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInUk1, InternalLeagueCode.UK1),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInUk2, InternalLeagueCode.UK2),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInUk3, InternalLeagueCode.UK3),
                    WpfHelper.GetLeagueOption(generalOptions.InterestedInUk4, InternalLeagueCode.UK4),
                }
            };
        }
    }
}
