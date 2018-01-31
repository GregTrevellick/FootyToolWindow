using FootieData.Vsix.Options;

namespace FootieData.Vsix
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("c53b01a9-7130-4b7f-957d-cdc8672fa6de")]
    public class ToolWindow1b : ToolWindowPane
    {
        public static GeneralOptions GeneralOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow1b"/> class.
        /// </summary>
        public ToolWindow1b() : base(null)
        {
            Caption = "League Tables";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            Content = new ToolWindow1Control(GeneralOptions);
        }
    }
}
