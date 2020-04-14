using Client.Wpf.Enumerations;
using System.Diagnostics;

namespace Client.Wpf.Commands.MainWindow
{
    public class LinkToYouTubeCommand : Command
    {
        #region Constructors

        public LinkToYouTubeCommand()
            : base(ECommandName.LinkToYouTube)
        {
        }

        #endregion Constructors

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            Process.Start(EApplicationData.LinkToYouTubePlaylist);
        }
    }
}