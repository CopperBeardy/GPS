namespace GPS_Distance.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ValidationBaseViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private readonly object _lock = new object();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _errors.Any(x => x.Value.Any());

        public IEnumerable? GetErrors(string propertyName)
        {
            if (propertyName is null)
                lock (_lock) return _errors.SelectMany(x => x.Value.ToList()).ToList();

            return _errors.TryGetValue(propertyName, out var values) ? values.ToList() : null;
        }

        public void OnErrorsChanged([CallerMemberName]string? propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool ValidateProperty<T>(T value, Predicate<T> validator, string error, [CallerMemberName]string? propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return false; // for _errors[propertyName]

            var valid = validator(value);
            lock (_lock) _errors[propertyName] = valid ? new List<string>() : new List<string>() { error };

            OnErrorsChanged(propertyName);
            return valid;
        }
    }
}
