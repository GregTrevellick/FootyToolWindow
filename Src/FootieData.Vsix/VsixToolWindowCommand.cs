using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace FootieData.Vsix
{
    internal sealed class VsixToolWindowCommand
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("4d2eb9da-e750-4c37-b048-d8a9269e5431");
        private static AsyncPackage _asyncPackage;

        public static VsixToolWindowCommand Instance { get; private set; }

        public static async Task InitializeCommandAsync(AsyncPackage asyncPackage)
        {
            if (asyncPackage == null)
            {
                throw new ArgumentNullException(nameof(asyncPackage));
            }
            _asyncPackage = asyncPackage;

            OleMenuCommandService commandService = await asyncPackage.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new VsixToolWindowCommand(commandService);
        }

        private VsixToolWindowCommand(OleMenuCommandService commandService)
        {
            var commandId = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(CommandEventHandler, commandId);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Is hit when user selects Tools > Windows > VS Sports Desk
        /// </summary>
        private async void CommandEventHandler(object sender, EventArgs e)
        {

            //AsyncBindingProperty.SlowSource source = new AsyncBindingProperty.SlowSource();
            //source.FetchNewData();







            //////////////////////////////////// Get the instance number 0 of this tool window. This window is single instance so this instance is actually the only one.
            //////////////////////////////////// The last flag is set to true so that if the tool window does not exists it will be created.
            ////////////////////////////////////var window = _asyncPackage.FindToolWindow(typeof(VsixToolWindowPane), 0, true);
            ////////////////////////////////////if (window?.Frame == null)
            ////////////////////////////////////{
            ////////////////////////////////////    throw new NotSupportedException("Cannot create tool window");
            ////////////////////////////////////}
            ////////////////////////////////////var windowFrame = (IVsWindowFrame)window.Frame;
            ////////////////////////////////////Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());

            // Get the instance number 0 of this tool window. This window is single instance so this instance is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            await _asyncPackage.JoinableTaskFactory.RunAsync(async delegate
            {
                /////////////////////////////////////////////////////////////var window = await _asyncPackage.ShowToolWindowAsync(typeof(VsixToolWindowPane), 0, true, _asyncPackage.DisposalToken);
                /////var window = _asyncPackage.FindToolWindow(typeof(VsixToolWindowPane), 0, true);
                var window = await _asyncPackage.FindToolWindowAsync(typeof(VsixToolWindowPane), 0, true, _asyncPackage.DisposalToken);
                if (window?.Frame == null)
                {
                    throw new NotSupportedException("Cannot create tool window");
                }
                await _asyncPackage.JoinableTaskFactory.SwitchToMainThreadAsync();
                var windowFrame = (IVsWindowFrame)window.Frame;
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
            });
        }
    }
}