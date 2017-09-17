using System.ComponentModel;
using RearrangingGrid;

namespace RearrangingGridDemo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int NarrowThreshold
        {
            get { return _narrowThreshold; }
            set
            {
                if (_narrowThreshold == value) return;
                _narrowThreshold = value;
                OnPropertyChanged("NarrowThreshold");
            }
        }
        private int _narrowThreshold = 500;

        public int ShortThreshold
        {
            get { return _shortThreshold; }
            set
            {
                if (_shortThreshold == value) return;
                _shortThreshold = value;
                OnPropertyChanged("ShortThreshold");
            }
        }
        private int _shortThreshold = 500;

        public int NarrowRow
        {
            get { return _narrowRow; }
            set
            {
                if (_narrowRow == value || value < 0) return;
                _narrowRow = value;
                OnPropertyChanged("NarrowRow");
            }
        }

        private int _narrowRow;

        public int NarrowColumn
        {
            get { return _narrowColumn; }
            set
            {
                if (_narrowColumn == value || value < 0) return;
                _narrowColumn = value;
                OnPropertyChanged("NarrowColumn");
            }
        }

        private int _narrowColumn = 1;

        public int NarrowRowSpan
        {
            get { return _narrowRowSpan; }
            set
            {
                if (_narrowRowSpan == value || value <= 0) return;
                _narrowRowSpan = value;
                OnPropertyChanged("NarrowRowSpan");
            }
        }

        private int _narrowRowSpan = 1;

        public int NarrowColumnSpan
        {
            get { return _narrowColumnSpan; }
            set
            {
                if (_narrowColumnSpan == value || value <= 0) return;
                _narrowColumnSpan = value;
                OnPropertyChanged("NarrowColumnSpan");
            }
        }

        private int _narrowColumnSpan = 2;

        public int ShortRow
        {
            get { return _shortRow; }
            set
            {
                if (_shortRow == value || value < 0) return;
                _shortRow = value;
                OnPropertyChanged("ShortRow");
            }
        }

        private int _shortRow = 1;

        public int ShortColumn
        {
            get { return _shortColumn; }
            set
            {
                if (_shortColumn == value || value < 0) return;
                _shortColumn = value;
                OnPropertyChanged("ShortColumn");
            }
        }

        private int _shortColumn = 2;

        public int ShortRowSpan
        {
            get { return _shortRowSpan; }
            set
            {
                if (_shortRowSpan == value || value <= 0) return;
                _shortRowSpan = value;
                OnPropertyChanged("ShortRowSpan");
            }
        }

        private int _shortRowSpan = 2;

        public int ShortColumnSpan
        {
            get { return _shortColumnSpan; }
            set
            {
                if (_shortColumnSpan == value || value <= 0) return;
                _shortColumnSpan = value;
                OnPropertyChanged("ShortColumnSpan");
            }
        }

        private int _shortColumnSpan = 1;

        public LayoutMode LayoutMode
        {
            get { return _layoutMode; }
            set
            {
                if (_layoutMode == value) return;
                _layoutMode = value;
                OnPropertyChanged("LayoutMode");
                CanEditValues = _layoutMode == LayoutMode.Regular;
            }
        }
        private LayoutMode _layoutMode = LayoutMode.Regular;

        public bool CanEditValues
        {
            get { return _canEditValues; }
            set
            {
                if (_canEditValues == value) return;
                _canEditValues = value;
                OnPropertyChanged("CanEditValues");
            }
        }
        private bool _canEditValues = true;
    }
}
