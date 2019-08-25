using Core.NLogExtensions.Objects;
using Core.NLogExtensions.Targets;
using NLog;
using NLog.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Client.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for ListViewNLog.xaml.
    /// This is a simplified version of the NLogViewer (https://github.com/erizet/NlogViewer).
    /// </summary>
    public partial class ListViewNLog : UserControl
    {
        #region Constructors

        public ListViewNLog()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                foreach (var target in LogManager.Configuration.AllTargets.OfType<WpfTarget>())
                    target.Log += LogReceived;
            }
        }

        #endregion Constructors

        protected void LogReceived(AsyncLogEventInfo log)
        {
            var eventInfo = new LogEventInfoLaidOutForWpf(log.LogEvent);

            Action<AsyncLogEventInfo, LogEventInfoLaidOutForWpf> AddNewEntry = (asynchLogEventInfo, logEventInfo) =>
            {
                _listView.Items.Add(eventInfo.LaidOutMessage);
                ScrollToLast();
            };

            Dispatcher.BeginInvoke(AddNewEntry, log, eventInfo);
        }

        private delegate void EmptyDelegate();

        public void ScrollToLast()
        {
            if (_listView.Items.IsEmpty)
                return;

            _listView.SelectedIndex = _listView.Items.Count - 1;
            _listView.ScrollIntoView(_listView.SelectedItem);
        }
    }
}