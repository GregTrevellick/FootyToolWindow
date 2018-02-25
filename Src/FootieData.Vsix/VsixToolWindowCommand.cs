using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.Shell.Interop;

namespace FootieData.Vsix
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class VsixToolWindowCommand
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("4d2eb9da-e750-4c37-b048-d8a9269e5431");
        //private readonly Package _package;//mine
        private static AsyncPackage _package;//mine

        /////////////////////////////////private readonly DTE2 _dte;
        public static VsixToolWindowCommand Instance { get; private set; }

        #region orig
        //private VsixToolWindowCommand(Package package)
        //{
        //    if (package == null)
        //    {
        //        throw new ArgumentNullException("package");
        //    }
        //    _package = package;
        //    OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
        //    if (commandService != null)
        //    {
        //        var menuCommandId = new CommandID(CommandSet, CommandId);
        //        var menuItem = new MenuCommand(ShowToolWindow, menuCommandId);
        //        commandService.AddCommand(menuItem);
        //    }
        //}
        #endregion

        public static async Task Initialize(AsyncPackage package)
        {//2nd
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }
            _package = package;

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            ///////////////var dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;//his
            Instance = new VsixToolWindowCommand(commandService);/////////////////////////////////, dte);
        }

        private VsixToolWindowCommand(OleMenuCommandService commandService)///////////////////////////////, DTE2 dte)
        {//3rd
            //////////////////_dte = dte;

            var commandId = new CommandID(CommandSet, CommandId);//mine
            //var commandId = new CommandID(PackageGuids.guidDiffFilesCmdSet, PackageIds.VsixToolWindowCommandId);//his

            
            //var menuItem = new MenuCommand(ShowToolWindow, commandId);//mine
            var menuItem = new OleMenuCommand(ShowToolWindow, commandId);//mine
            //var command = new OleMenuCommand(CommandCallback, commandId);//his

            //command.BeforeQueryStatus += BeforeQueryStatus;//his

            commandService.AddCommand(menuItem);//mine
            //commandService.AddCommand(command);//his
        }


        //his
        //private void BeforeQueryStatus(object sender, EventArgs e)
        //{
        //    var button = (OleMenuCommand)sender;
        //    var selectedFiles = GetSelectedFiles();
        //    Only show if 1 or 2 files are selected
        //    button.Visible = selectedFiles.Count() <= 2;
        //}
        //private void CommandCallback(object sender, EventArgs e)
        //{
        //    string file1, file2;
        //    if (CanFilesBeCompared(out file1, out file2))
        //    {
        //        if (!DiffFileUsingCustomTool(file1, file2))
        //        {
        //            DiffFilesUsingDefaultTool(file1, file2);
        //        }
        //    }
        //}






        //mine
        ///// <summary>
        ///// Gets the service provider from the owner package.
        ///// </summary>
        //private IServiceProvider ServiceProvider => _package;
        ///// <summary>
        ///// Initializes the singleton instance of the command.
        ///// </summary>
        ///// <param name="package">Owner package, not null.</param>
        //public static void Initialize(Package package)
        //{
        //    Instance = new VsixToolWindowCommand(package);
        //}




        /// <summary>
        /// Shows the tool window when the menu item is clicked.
        /// </summary>
        private void ShowToolWindow(object sender, EventArgs e)
        {//4th
            // Get the instance number 0 of this tool window. This window is single instance so this instance is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            var window = _package.FindToolWindow(typeof(VsixToolWindowPane), 0, true);
            if (window?.Frame == null)
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            var windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}







