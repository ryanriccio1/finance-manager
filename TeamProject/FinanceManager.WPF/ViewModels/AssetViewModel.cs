using FinanceManager.Domain.Services;
using FinanceManager.FinancialModelAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.ViewModels
{
    public class AssetViewModel : ViewModelBase
    {
        private IStockPriceService _stockPriceService;
        
        private string _symbol;
        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value;
                GetStockPrice();
                OnPropertyChanged(nameof(Symbol));
            }
        }

        private int _shares;
        public int Shares
        {
            get
            {
                return _shares;
            }
            set
            {
                _shares = value;
                OnPropertyChanged(nameof(Shares));
            }
        }

        private double _value;
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                TotalValue = _shares * value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public double TotalValue { get; set; }

        private double _dayChange;
        public double DayChange
        {
            get
            {
                return _dayChange;
            }
            set
            {
                _dayChange = value;
                OnPropertyChanged(nameof(DayChange));
            }
        }

        private double _weekChange;
        public double WeekChange
        {
            get
            {
                return _weekChange;
            }
            set
            {
                _weekChange = value;
                OnPropertyChanged(nameof(WeekChange));
            }
        }

        private double _monthChange;
        public double MonthChange
        {
            get
            {
                return _monthChange;
            }
            set
            {
                _monthChange = value;
                OnPropertyChanged(nameof(MonthChange));
            }
        }

        private double _yearChange;
        public double YearChange
        {
            get
            {
                return _yearChange;
            }
            set
            {
                _yearChange = value;
                OnPropertyChanged(nameof(YearChange));
            }
        }

        public AssetViewModel(string symbol, int shares, IStockPriceService stockPriceService)
        {
            _stockPriceService = stockPriceService;
            Shares = shares;
            Symbol = symbol;
        }

        public async void GetStockPrice()
        {
            Value = await _stockPriceService.GetPrice(_symbol);
            DayChange = await _stockPriceService.GetDayChange(_symbol);
            WeekChange = await _stockPriceService.GetWeekChange(_symbol);
            MonthChange = await _stockPriceService.GetMonthChange(_symbol);
            YearChange = await _stockPriceService.GetYearChange(_symbol);
        }
    }
}
