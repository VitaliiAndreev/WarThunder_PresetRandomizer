using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for FolderSelectorWithLabelControl.xaml. </summary>
    public partial class FolderSelectorWithLabelControl : System.Windows.Controls.UserControl
    {
        #region Fields

        /// <summary> Indicates whether the address in the <see cref="_textBox"/> is valid. Is used by the <see cref="AddressIsValid"/> property. </summary>
        private bool _addressIsValid;

        #endregion Fields
        #region Properties

        #region Label

        /// <summary> The width of the <see cref="_textBlock"/>. </summary>
        public GridLength LabelWidth
        {
            get => _labelColumn.Width;
            set => _labelColumn.Width = value;
        }

        /// <summary> The text in the <see cref="_textBlock"/>. </summary>
        public string LabelText
        {
            get => _label.Text;
            set => _label.Text = value;
        }

        #endregion Label
        #region TextBox

        /// <summary> The width of the <see cref="_textBox"/>. </summary>
        public GridLength TextBoxWidth
        {
            get => _textBoxColumn.Width;
            set => _textBoxColumn.Width = value;
        }

        /// <summary> The text in the <see cref="_textBox"/>. </summary>
        public string TextBoxText
        {
            get => _textBox.Text;
            set => _textBox.Text = value;
        }

        #endregion TextBox
        #region Button

        /// <summary> The width of the <see cref="_button"/>. </summary>
        public GridLength ButtonWidth
        {
            get => _buttonColumn.Width;
            set => _buttonColumn.Width = value;
        }

        #endregion Button

        /// <summary> The method used for validation of the address in the <see cref="_textBox"/>. </summary>
        public Predicate<string> AddressValidator { get; set; }

        /// <summary> Indicates whether the address in the <see cref="_textBox"/> is valid. </summary>
        public bool AddressIsValid
        {
            get => _addressIsValid;
            private set
            {
                _addressIsValid = value;
                RaiseAddressValidityChanged();
            }
        }

        #endregion Properties
        #region Events

        /// <summary> It is raised whenever the <see cref="AddressIsValid"/> property is changed. </summary>
        public event EventHandler AddressValidityChanged;

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public FolderSelectorWithLabelControl()
        {
            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Validates the address whenever it's changed. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnTextChanged(object sender, TextChangedEventArgs eventArguments)
        {
            ValidateAddress();
            UpdateTextBoxColor();
        }

        /// <summary> Opens a dialog for directory look-up. </summary>
        /// <param name="sender"> Not Used. </param>
        /// <param name="eventArguments"> Not Used. </param>
        private void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = false;
                dialog.SelectedPath = AddressIsValid
                    ? _textBox.Text
                    : string.Empty;

                if (dialog.ShowDialog() == DialogResult.OK)
                    _textBox.Text = dialog.SelectedPath;
            }
        }

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text on the control. </summary>
        public void Localize()
        {
            _button.Content = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.LookUp);
        }

        /// <summary> Raises <see cref="AddressValidityChanged"/>. </summary>
        private void RaiseAddressValidityChanged() =>
            AddressValidityChanged?.Invoke(_textBox, new EventArgs());

        /// <summary> Validates the address in the <see cref="_textBox"/> and updates the <see cref="AddressIsValid"/> property. </summary>
        private void ValidateAddress()
        {
            try
            {
                var newValue = AddressValidator(_textBox.Text);

                if (AddressIsValid != newValue)
                    AddressIsValid = newValue;
            }
            catch
            {
                if (AddressIsValid)
                    AddressIsValid = false;
            }
        }

        /// <summary> Applies color to the <see cref="_textBox"/> contents according to the value of the <see cref="AddressIsValid"/> property. </summary>
        private void UpdateTextBoxColor()
        {
            void applyStyle(string styleKey) => _textBox.Style = this.GetStyle(styleKey);

            if (AddressIsValid)
                applyStyle(EStyleKey.TextBox.ValidText);
            else
                applyStyle(EStyleKey.TextBox.InvalidText);
        }
    }
}