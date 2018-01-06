using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace FootieData.Ui
{
    /// <summary>
    /// Tool windows are composed of a frame (provided by Visual Studio) and a pane (provided by the package implementer). 
    /// The frame implements IVsWindowFrame while the pane implements IVsWindowPane.
    /// 
    /// PersistedWindowPane inherits the IVsWindowPane implementation from its base class (ToolWindowPane). 
    /// PersistedWindowPane will host a .NET UserControl (PersistedWindowControl). 
    /// The Package base class will/ get the user control by asking for the Window property on this class.
    /// </summary>
    [Guid("0A6F8EDC-5DDB-4aaa-A6B3-2AC1E319693E")]
	class PersistedFootieWindowPane : ToolWindowPane
	{
		// Control that will be hosted in the tool window
		private FootieUserControl control = null;

		public PersistedFootieWindowPane() : base(null)
		{
			// Add the toolbar by specifying the Guid/MenuID pair corresponding to the toolbar definition in the vsct file.
			ToolBar = new CommandID(GuidsList.guidClientCmdSet, PkgCmdId.IDM_MyToolbar);
			
            // Specify that we want the toolbar at the top of the window
			ToolBarLocation = (int)VSTWT_LOCATION.VSTWT_TOP;

			// Creating the user control that will be displayed in the window 
            control = new FootieUserControl();

            Content = control;

            this.Caption = "Gregt League Tables";
        }
        
		public override void OnToolWindowCreated()
		{
			base.OnToolWindowCreated();

            VSPackageToolWindow package = (VSPackageToolWindow)this.Package;

			CommandID id = new CommandID(GuidsList.guidClientCmdSet, PkgCmdId.cmdidRefreshWindowsList);

            OleMenuCommand command = DefineCommandHandler(new EventHandler(this.RefreshList), id);
        
            control.Dispatcher.BeginInvoke((Action)delegate
            {
                this.RefreshList(this, null);
            });
		}

		private void RefreshList(object sender, EventArgs arguments)
		{
			control.RefreshData();
		}

		private OleMenuCommand DefineCommandHandler(EventHandler handler, CommandID id)
		{
            // First add it to the package. This is to keep the visibility of the command on the toolbar constant when the tool window does not have focus. In addition, it creates the command object for us.
            VSPackageToolWindow package = (VSPackageToolWindow)this.Package;
            OleMenuCommand command = package.DefineCommandHandler(handler, id);

            if (command == null)
            {
                return command;
            }

			OleMenuCommandService menuService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
			
			if (null != menuService)
			{
				menuService.AddCommand(command);
			}

			return command;
		}
	}
}
