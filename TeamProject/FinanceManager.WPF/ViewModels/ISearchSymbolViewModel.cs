namespace FinanceManager.WPF.ViewModels
{
    // sell view and buy view implement this
    // because they both need to give these messages
    // and they must share a type so 
    // that our stock search service can update them
    public interface ISearchSymbolViewModel
    {
        string ErrorMessage { set; }
        string SearchResultSymbol { set; }
        double StockPrice { set; }
        string Symbol { get; }
    }
}