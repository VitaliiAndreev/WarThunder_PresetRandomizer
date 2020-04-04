using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.Enumerations;
using Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of a row or a column of <see cref="ToggleButton"/>s, indexed by <typeparamref name="T"/> values. </summary>
    /// <typeparam name="T"> The key type. </typeparam>
    public class ToggleButtonGroupControl<T> : LocalizedUserControl, IEnumerable<ToggleButton>
    {
        #region Fields

        /// <summary> The toggle-all button. </summary>
        protected ToggleButton _toggleAllButton;

        /// <summary> Toggle buttons grouped by the generic "<see cref="T"/>" key. </summary>
        protected internal readonly IDictionary<T, ToggleButton> Buttons;

        #endregion Fields
        #region Properties

        /// <summary> The reference to the static <see cref="ClickEvent"/>. </summary>
        internal RoutedEvent ClickEventReference { get; }

        #endregion Properties
        #region Events

        /// <summary> A routed event for <see cref="Click"/>. </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleButtonGroupControl<T>));

        /// <summary> An event for clicking toggle buttons. </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ToggleButtonGroupControl()
        {
            Buttons = new Dictionary<T, ToggleButton>();
            ClickEventReference = ClickEvent;
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Raises the <see cref="ClickEvent"/> for one of the toggle buttons. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        public virtual void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is ToggleButton toggleButton)
                RaiseClickEvent(toggleButton);
        }

        #endregion Methods: Event Handlers
        #region Methods: Overrides

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localize()
        {
            base.Localize();

            static void localizeButtonContent(ToggleButton toggleButton, string @string)
            {
                var localizedString = ApplicationHelpers.LocalizationManager.GetLocalizedString(@string);

                if (@string != localizedString)
                    toggleButton.Content = localizedString;
            }

            foreach (var button in Buttons.Values)
            {
                if (button.Content is string @string)
                {
                    localizeButtonContent(button, @string);
                }
                else if (button.Content is Enum enumerationItem)
                {
                    var enumerationItemString = enumerationItem.ToString();
                    localizeButtonContent(button, enumerationItemString);
                }
            }

            if (_toggleAllButton is ToggleButton)
                _toggleAllButton.Content = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.All);
        }

        #endregion Methods: Overrides

        /// <summary> Raises the <see cref="ClickEvent"/> for the specified toggle button. </summary>
        /// <param name="toggleButton"> The toggle button to raise the event for. </param>
        public void RaiseClickEvent(ToggleButton toggleButton) =>
            RaiseEvent(new RoutedEventArgs(ClickEvent, toggleButton));

        #region Methods: CreateToggleButton()

        /// <summary> Creates a toggle button for the given <paramref name="enumerationItem"/>. </summary>
        /// <param name="panel"> The panel to the button onto. </param>
        /// <param name="enumerationItem"> The enumeration item to create the toggle button for. </param>
        /// <param name="content"> Content displayed (for the designer). </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        /// <param name="horizontal"> Whether other buttons are arranged in a row or in a column. </param>
        private ToggleButton CreateToggleButton(Panel panel, T enumerationItem, object content, string styleKey, bool horizontal)
        {
            var toggleButton = new ToggleButton
            {
                Style = this.GetStyle(styleKey),
                Tag = enumerationItem,
                Content = content,
            };

            toggleButton.Click += OnClick;
            toggleButton.AddToPanel(panel, horizontal);

            Buttons.Add(enumerationItem, toggleButton);

            return toggleButton;
        }

        /// <summary> Creates a toggle-all button. </summary>
        /// <param name="panel"> The panel to the button onto. </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        /// <param name="horizontal"> Whether other buttons are arranged in a row or in a column. </param>
        protected void CreateToggleAllButton(Panel panel, string styleKey, bool horizontal)
        {
            var tagType = typeof(T);

            if (tagType.IsEnum)
            {
                var enumerationItem = typeof(T).GetEnumValues().OfType<T>().FirstOrDefault(item => item.ToString() == EWord.All);
                _toggleAllButton = CreateToggleButton(panel, enumerationItem, enumerationItem.ToString(), styleKey, horizontal);
            }
            else
            {
                _toggleAllButton = CreateToggleButton(panel, default, EWord.All, styleKey, horizontal);
            }
        }

        /// <summary> Creates a toggle-all button. </summary>
        /// <param name="panel"> The panel to add the button onto. </param>
        /// <param name="enumerationItem"> The enumeration item to create the toggle button for. </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        /// <param name="horizontal"> Whether other buttons are arranged in a row or in a column. </param>
        protected virtual void CreateToggleAllButton(Panel panel, T enumerationItem, string styleKey, bool horizontal)
        {
            _toggleAllButton = CreateToggleButton(panel, enumerationItem, typeof(T).IsEnum ? enumerationItem.ToString() : EWord.All, $"{styleKey}{EWord.All}", horizontal);
        }

        /// <summary> Creates toggle buttons for given <paramref name="enumerationItems"/>, with <paramref name="characterIcons"/>. </summary>
        /// <param name="panel"> The panel to add buttons onto. </param>
        /// <param name="enumerationItems"> Enumeration items to create toggle buttons for. </param>
        /// <param name="characterIcons"> Character icons for <paramref name="enumerationItems"/>. </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        /// <param name="horizontal"> Whether to arrange buttons in a row or in a column. </param>
        /// <param name="createToggleAllButton"> Whether to create the toggle-all button. </param>
        public void CreateToggleButtons(Panel panel, IEnumerable<T> enumerationItems, IDictionary<T, char> characterIcons, string styleKey, bool horizontal = true, bool createToggleAllButton = false)
        {
            if (createToggleAllButton)
                CreateToggleAllButton(panel, $"{styleKey}{EWord.All}", horizontal);

            foreach (var enumerationItem in enumerationItems)
            {
                try
                {
                    CreateToggleButton(panel, enumerationItem, (characterIcons is null || !characterIcons.TryGetValue(enumerationItem, out var characterIcon)) ? enumerationItem.ToString() : characterIcon.ToString(), styleKey, horizontal);
                }
                catch (Exception e)
                {
                    throw new Exception($"{e.Message} | {enumerationItem.ToString()} | {enumerationItem.GetType().ToStringLikeCode()}", e);
                }
            }
        }

        #endregion Methods: CreateToggleButton()

        /// <summary> Returns all <see cref="Buttons"/> on the control except <see cref="_toggleAllButton"/>. </summary>
        /// <returns></returns>
        public IEnumerable<ToggleButton> GetButtonsExceptToggleAll()
        {
            return Buttons.Values.Except(_toggleAllButton);
        }

        /// <summary> Changes the <see cref="UIElement.IsEnabled"/> status of a button corresponding to the specified key. </summary>
        /// <param name="key"> The key whose toggle button's state to change. </param>
        /// <param name="enable"> Whether to enable or disable the toggle button. </param>
        public void Enable(T key, bool enable)
        {
            if (Buttons.TryGetValue(key, out var toggleButton) && toggleButton.IsEnabled != enable)
                toggleButton.IsEnabled = enable;
        }

        #region Methods: AllButtonsMeetCondition()

        /// <summary> Checks whether all <see cref="Buttons"/> meet the given condition. </summary>
        /// <param name="predicate"> The predicate that the condition is defined by. </param>
        /// <returns></returns>
        private bool AllButtonsMeetCondition(Predicate<ToggleButton> predicate)
        {
            return Buttons
                .Values
                .Except(_toggleAllButton)
                .All(toggleButton => predicate(toggleButton))
            ;
        }

        /// <summary> Checks whether all <see cref="Buttons"/> are toggled on, except the <see cref="_toggleAllButton"/>. </summary>
        /// <returns></returns>
        public bool AllButtonsAreToggledOn() =>
            AllButtonsMeetCondition(toggleButton => toggleButton.IsChecked());

        /// <summary> Checks whether all <see cref="Buttons"/> are toggled off, except the <see cref="_toggleAllButton"/>. </summary>
        /// <returns></returns>
        public bool AllButtonsAreToggledOff() =>
            AllButtonsMeetCondition(toggleButton => !toggleButton.IsChecked());

        #endregion Methods: AllButtonsMeetCondition()
        #region Methods: Toggling()

        /// <summary> Toggles a button corresponding to the specified key. </summary>
        /// <param name="key"> The key whose button to toggle. </param>
        /// <param name="newState"> Whether to toggle the button on or off. </param>
        public void Toggle(T key, bool newState)
        {
            if (Buttons.TryGetValue(key, out var toggleButton) && toggleButton.IsChecked != newState)
                toggleButton.IsChecked = newState;
        }

        /// <summary> Toggles buttons corresponding to specified keys. </summary>
        /// <param name="keys"> The keys whose buttons to toggle. </param>
        /// <param name="newState"> Whether to toggle buttons on or off. </param>
        public void Toggle(IEnumerable<T> keys, bool newState)
        {
            foreach (var branch in keys)
                Toggle(branch, newState);
        }

        /// <summary> Toggles all <see cref="Buttons"/>. </summary>
        /// <param name="newState"> Whether to toggle buttons on or off. </param>
        public void ToggleAll(bool newState)
        {
            foreach (var button in Buttons.Values)
                if (button.IsChecked != newState)
                    button.IsChecked = newState;
        }

        #endregion Methods: Toggling()
        #region Implementation of IEnumerable<ToggleButton>

        public IEnumerator<ToggleButton> GetEnumerator() => Buttons.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion Implementation of IEnumerable<ToggleButton>
    }
}