using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for FlagControl.xaml. </summary>
    public partial class FlagControl : LocalisedUserControl
    {
        #region Fields

        private Style _flagStyle;

        #endregion Fields
        #region Constructors

        public FlagControl()
        {
            InitializeComponent();
        }

        public FlagControl(ENation nation, Thickness margin, MouseButtonEventHandler mouseDownHandler, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center, double? panelWidth = null, bool setTag = true)
        {
            InitializeComponent();
            Initialise(nation, margin, mouseDownHandler, horizontalAlignment, panelWidth, setTag);
        }

        public FlagControl(ECountry country, Thickness margin, MouseButtonEventHandler mouseDownHandler, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center, double? panelWidth = null, bool setTag = true)
        {
            InitializeComponent();
            Initialise(country, margin, mouseDownHandler, horizontalAlignment, panelWidth, setTag);
        }

        #endregion Constructors

        private void Initialise<T>(T tag, Thickness margin, MouseButtonEventHandler mouseDownHandler, HorizontalAlignment horizontalAlignment, double? panelWidth, bool setTag) where T : struct
        {
            _flagStyle = this.GetStyle(EStyleKey.Image.FlagIcon16px);

            Margin = margin;
            HorizontalAlignment = horizontalAlignment;

            var image = CreateFlag(tag, horizontalAlignment);

            _panel.Children.Add(image);

            if (panelWidth.HasValue)
                _panel.Width = panelWidth.Value;

            if (mouseDownHandler is MouseButtonEventHandler)
                MouseDown += mouseDownHandler;

            if (setTag)
                SetTag(tag);
        }

        private Image CreateFlag<T>(T tag, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left) where T : struct
        {
            if (tag is ENation nation)
                return nation.CreateFlag(_flagStyle, default, horizontalAlignment);

            else if (tag is ECountry country)
                return country.CreateFlag(_flagStyle, default, horizontalAlignment);

            else
                throw new NotImplementedException();
        }

        private void SetTag<T>(T tag) where T : struct
        {
            if (tag is ENation nation)
                Tag = nation;

            else if (tag is ECountry country)
                Tag = country;

            else
                throw new NotImplementedException();
        }
    }
}
