using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace Microsoft.Samples.VisualStudio.IDE.ToolWindow
{
    [ProvideToolWindow(typeof(PersistedWindowPane), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
	[ProvideMenuResource(1000, 1)]
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[Guid("01069CDD-95CE-4620-AC21-DDFF6C57F012")]
	public class PackageToolWindow : Package
	{
		// Cache the Menu Command Service since we will use it multiple times
		private OleMenuCommandService menuService;

		protected override void Initialize()
		{
			base.Initialize();

			// Create one object derived from MenuCommand for each command defined in the VSCT file and add it to the command service.
			CommandID id = new CommandID(GuidsList.guidClientCmdSet, PkgCmdId.cmdidPersistedWindow);
			DefineCommandHandler(new EventHandler(ShowPersistedWindow), id);
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
            ToolWindowPane pane = FindToolWindow(typeof(PersistedWindowPane), 0, true);

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

//internal string GetResourceString(string resourceName)
//{
//    string resourceValue;
//    IVsResourceManager resourceManager = (IVsResourceManager)GetService(typeof(SVsResourceManager));
//    if (resourceManager == null)
//    {
//        throw new InvalidOperationException("Could not get SVsResourceManager service. Make sure the package is Sited before calling this method");
//    }
//    Guid packageGuid = GetType().GUID;
//    int hr = resourceManager.LoadResourceString(ref packageGuid, -1, resourceName, out resourceValue);
//    ErrorHandler.ThrowOnFailure(hr);
//    return resourceValue;
//}

