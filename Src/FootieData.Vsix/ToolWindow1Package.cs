using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using FootieData.Vsix.Options;

namespace FootieData.Vsix
{
    [ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, "General", 0, 0, true)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]//[ProvideMenuResource(1000, 1)]
    [ProvideToolWindow(typeof(ToolWindow1), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    [Guid(ToolWindow1Package.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ToolWindow1Package : Package
    {
        public const string PackageGuidString = "4431588e-199d-477f-b3c4-c0b9603602b0";

        public static GeneralOptions GeneralOptions { get; private set; }
        
        public ToolWindow1Package()
        {
        }

        protected override void Initialize()
        {
            ToolWindow1Command.Initialize(this);
            base.Initialize();
            
            GeneralOptions = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));

            //show here ????
        }
    }
}
