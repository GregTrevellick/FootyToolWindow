using FootieData.Common;
using FootieData.Common.Options;
using FootieData.Entities.ReferenceData;
using FootieData.Vsix.Options;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
//using Microsoft.VisualStudio;
using VsixRatingChaser.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace FootieData.Vsix
{
    #region AsyncPackage
    [PackageRegistration(UseManagedResourcesOnly = true)]//, AllowsBackgroundLoading = true)]//previously just [PackageRegistration(UseManagedResourcesOnly = true)]
    //////[ProvideService(typeof(ISFootieService), IsAsyncQueryable = true)]//gregt or should ISFootieService in fact be OleMenuCommandService or IMenuCommandService ?
    //[ProvideAutoLoad(UIContextGuids.NoSolution)]//his
    [ProvideAutoLoad(UIContextGuids.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]//mine //gregt UIContextGuids.NoSolution vs VSConstants.UICONTEXT.NoSolution_string
    #endregion
    [ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, "General", 0, 0, true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]//[ProvideMenuResource(1000, 1)]
    [ProvideToolWindow(typeof(VsixToolWindowPane), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    [Guid(ToolWindow1Package.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed partial class ToolWindow1Package : AsyncPackage
    {
        public const string PackageGuidString = "4431588e-199d-477f-b3c4-c0b9603602b0";

        //public ToolWindow1Package()
        //{//0th
        //    if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
        //    {
        //        ChaseRating();
        //    }

        //    InitializeFootie();
        //}

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {//1st
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                ChaseRating();
            }

            InitializeFootie();

            await VsixToolWindowCommand.Initialize(this);
        }



        //#region from https://stackoverflow.com/questions/40345756/does-visual-studio-extension-packages-support-asyncronous-operations
        ////protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> serviceProgressData)
        ////{

        ////    InitializeFootie();

        ////    this.AddService(typeof(ISFootieService), CreateService);//gregt is this required ? perhaps, but 'FootieService' contains solely 'GetAwaiter'
        ////    await base.InitializeAsync(cancellationToken, serviceProgressData);

        ////    //var textService = await this.GetServiceAsync(typeof(ISTextWriterService)) as ITextWriterService;
        ////    //var commandService = await this.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;

        ////    //await textService.WriteLineAsync(@"c:\windows\temp\async_service.txt", "this is a test");
        ////    //await commandService.AddCommand();
        ////}

        //public async System.Threading.Tasks.Task<object> CreateService(IAsyncServiceContainer asyncServiceContainer, CancellationToken cancellationToken, Type serviceType)
        //{
        //    FootieService service = null;
        //    await System.Threading.Tasks.Task.Run(() =>
        //    {                
        //        service = new FootieService();
        //    });

        //    return service;
        //}
        //#endregion

        #region existing code
        internal void InitializeFootie()
        {//0th
            /////////////////////////////////////////////////base.Initialize();

            //VsixToolWindowCommand.Initialize(this);            

            VsixToolWindowPane.GetOptionsFromStoreAndMapToInternalFormatMethod =
                any
                    => 
                {
                    var generalOptions = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));
                    ToolWindow1Control.LeagueGeneralOptions = GetLeagueGeneralOptions(generalOptions);
                };

            VsixToolWindowPane.UpdateLastUpdatedDate =
                any
                    =>
                {
                    var hiddenOptions = (HiddenOptions)GetDialogPage(typeof(HiddenOptions));
                    hiddenOptions.LastUpdated = DateTime.Now;
                    hiddenOptions.SaveSettingsToStorage();
                };

            VsixToolWindowPane.GetLastUpdatedDate =
                any
                    =>
                {
                    var hiddenOptions = (HiddenOptions)GetDialogPage(typeof(HiddenOptions));
                    return hiddenOptions.LastUpdated;
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

        private void ChaseRating()
        {
            var hiddenChaserOptions = (IRatingDetailsDto)GetDialogPage(typeof(HiddenRatingDetailsDto));
            var packageRatingChaser = new PackageRatingChaser();
            packageRatingChaser.Hunt(hiddenChaserOptions);
        }
        #endregion

        //#region VsixToolWindowCommand
        //public const int CommandId = 0x0100;
        //public static readonly Guid CommandSet = new Guid("4d2eb9da-e750-4c37-b048-d8a9269e5431");
        ////private readonly Package _package;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="VsixToolWindowCommand"/> class.
        ///// Adds our command handlers for menu (commands must exist in the command table file)
        ///// </summary>
        ///// <param name="package">Owner package, not null.</param>
        ////private void VsixToolWindowCommand(Package package)
        //private void VsixToolWindowCommand3()
        //{
        //    //_package = package;

        //    //OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
        //    OleMenuCommandService commandService = this.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

        //    if (commandService != null)
        //    {
        //        var menuCommandId = new CommandID(CommandSet, CommandId);
        //        var menuItem = new MenuCommand(ShowToolWindow, menuCommandId);
        //        commandService.AddCommand(menuItem);
        //    }
        //}

        ////public static VsixToolWindowCommand Instance
        ////{
        ////    get;
        ////    private set;
        ////}

        ///// <summary>
        ///// Gets the service provider from the owner package.
        ///// </summary>
        ////private IServiceProvider ServiceProvider => _package;

        ///// <summary>
        ///// Initializes the singleton instance of the command.
        ///// </summary>
        ///// <param name="package">Owner package, not null.</param>
        ////public static void Initialize(Package package)
        //public void Initialize3()
        //{
        //    //Instance = new VsixToolWindowCommand(package);
        //    VsixToolWindowCommand3();
        //}

        ///// <summary>
        ///// Shows the tool window when the menu item is clicked.
        ///// </summary>
        //private void ShowToolWindow(object sender, EventArgs e)
        //{
        //    // Get the instance number 0 of this tool window. This window is single instance so this instance is actually the only one.
        //    // The last flag is set to true so that if the tool window does not exists it will be created.
        //    //var window = _package.FindToolWindow(typeof(VsixToolWindowPane), 0, true);
        //    var window = this.FindToolWindow(typeof(VsixToolWindowPane), 0, true);
        //    if (window?.Frame == null)
        //    {
        //        throw new NotSupportedException("Cannot create tool window");
        //    }

        //    var windowFrame = (IVsWindowFrame)window.Frame;
        //    Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        //}
        //#endregion
    }
}
