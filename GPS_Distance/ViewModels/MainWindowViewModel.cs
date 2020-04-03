namespace GPS_Distance.ViewModels
{
    using CommonServiceLocator;
    using GPS_Distance.Events;
    using Prism.Events;
    using System.Windows.Input;

    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEventAggregator _eventEnableAggregator;

        private bool isEnable;
        private bool _isResultTabSelected;

        public bool IsEnabled
        {
            get => isEnable;
            set => SetProperty(ref isEnable, value);
        }
         public bool IsResultTabSelected
        {
            get => _isResultTabSelected;
            set => SetProperty(ref _isResultTabSelected, value);
          
        }


        public MainWindowViewModel()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _eventAggregator.GetEvent<DistanceResultEvent>().Subscribe(args => IsResultTabSelected = true);

            _eventEnableAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _eventEnableAggregator.GetEvent<ResultTabEnablerEvent>().Subscribe(EnableResultTabEventHandler);
         }

        
       
        void EnableResultTabEventHandler(ResultTabEnablerEventArgs obj)
        {
            IsEnabled = obj.Enable;
        }
    }
}
