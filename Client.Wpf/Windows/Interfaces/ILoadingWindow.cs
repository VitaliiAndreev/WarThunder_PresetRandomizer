namespace Client.Wpf.Windows.Interfaces
{
    /// <summary> The loading window. </summary>
    public interface ILoadingWindow : IBaseWindow
    {
        /// <summary> Indicates whether the initialization has been completed. </summary>
        public bool InitializationComplete { get; set; }
    }
}