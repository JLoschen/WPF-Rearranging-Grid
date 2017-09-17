using System;

namespace RearrangingGridDemo
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExtendedGrid_OnSwitchingToNarrow(object sender, EventArgs e)
        {
            Console.WriteLine("On switching to narrow");
        }
    }
}
