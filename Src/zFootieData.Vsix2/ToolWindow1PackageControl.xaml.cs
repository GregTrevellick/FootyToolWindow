namespace FootieData.Vsix
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ToolWindow1PackageControl.
    /// </summary>
    public partial class ToolWindow1PackageControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow1PackageControl"/> class.
        /// </summary>
        public ToolWindow1PackageControl()
        {
            Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId);
            this.InitializeComponent();
            SomeLongRunningCode();
        }

        private void SomeLongRunningCode()
        {
            //for (int i = 0; i < 10_000_000; i++)//gregt long running code - circa 12/13 seconds
            //{
            //    DateTime.Now.ToString();
            //}
            var source = new SlowSource();
            this.DataContext = source;
            source.FetchNewData();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "ToolWindow1Package GregtA");
        }
    }
}