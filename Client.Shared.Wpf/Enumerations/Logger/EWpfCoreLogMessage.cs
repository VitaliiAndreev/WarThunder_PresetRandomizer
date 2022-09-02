using Core;

namespace Client.Shared.Wpf.Enumerations.Logger
{
    public class EWpfCoreLogMessage : CoreLogMessage
    {
        public static readonly string StackPanelCantBeHorizontal = $"{_Stack} {_panel} {_cant} {_be} {_horizontal}.";
        public static readonly string WrapPanelCantBeVertical = $"{_Wrap} {_panel} {_cant} {_be} {_vertical}.";
    }
}