using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace FootieData.Ui
{
    //[ProvideToolWindow(typeof(PersistedFootieWindowPane), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    //[ProvideMenuResource(1000, 1)]
    //[PackageRegistration(UseManagedResourcesOnly = true)]
    //[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    //[Guid(VSPackage.PackageGuidString)]
    //[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    //[ProvideMenuResource("1000", 1)]
    //[ProvideToolWindow(typeof(FootieData.Ui.ToolWindow1))]
    public sealed class zVSPackage : Package
    {
        // Cache the Menu Command Service since we will use it multiple times
        private OleMenuCommandService menuService;

        //public const string PackageGuidString = "f06fb8e8-110a-4d12-b93a-0addf00146e5";

        //public VSPackage()
        //{
        //}

        protected override void Initialize()
        {
            //base.Initialize();

            // Create one object derived from MenuCommand for each command defined in the VSCT file and add it to the command service.
            CommandID id = new CommandID(GuidsList.guidClientCmdSet, PkgCmdId.cmdidPersistedWindow);
            DefineCommandHandler(new EventHandler(ShowPersistedWindow), id);
            FootieData.Ui.Command1.Initialize(this);
            //FootieData.Ui.ToolWindow1Command.Initialize(this);

        }

        internal OleMenuCommand DefineCommandHandler(EventHandler handler, CommandID id)
        {
            // if the package is zombied, we don't want to add commands
            if (Zombied)
            {
                return null;
            }

            if (menuService == null)
            {
                menuService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            }

            OleMenuCommand command = null;

            if (menuService != null)
            {
                command = new OleMenuCommand(handler, id);
                menuService.AddCommand(command);
            }

            return command;
        }

        private void ShowPersistedWindow(object sender, EventArgs arguments)
        {
            // Get the 1 (index 0) and only instance of our tool window (if it does not already exist it will get created)
            ToolWindowPane pane = FindToolWindow(typeof(PersistedFootieWindowPane), 0, true);

            if (pane == null)
            {
                throw new COMException("blah1");
            }

            IVsWindowFrame frame = pane.Frame as IVsWindowFrame;

            if (frame == null)
            {
                throw new COMException("blah2");
            }

            // Bring the tool window to the front and give it focus
            ErrorHandler.ThrowOnFailure
                (
                    frame.Show()
                );
        }

    }
}
