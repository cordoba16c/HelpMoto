using Countries.Prism.Droid;
using Xamarin.Forms;
using Interfaces;
using Resources;


namespace Countries.Web.Helpers
{
    public static class Languages
    {

    }
    static Languages()
    {
        var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        Resource.Culture = ci;
        DependencyService.Get<ILocalize>().SetLocale(ci);
    }

    public static string Accept
    {
        get { return Resource.Accept; }
    }


    public static string Error
    {
        get { return Resource.Error; }
    }

    public static string SomethingWrong
    {
        get { return Resource.SomethingWrong; }
    }




    public static string Countries
    {
        get { return Resource.Countries; }
    }

    public static string Search
    {
        get { return Resource.Search; }
    }

    public static string Country
    {
        get { return Resource.Country; }
    }

    public static string Information
    {
        get { return Resource.Information; }
    }

    public static string Capital
    {
        get { return Resource.Capital; }
    }

    public static string Population
    {
        get { return Resource.Population; }
    }

    public static string Area
    {
        get { return Resource.Area; }
    }

    public static string AlphaCode2
    {
        get { return Resource.AlphaCode2; }
    }

    public static string AlphaCode3
    {
        get { return Resource.AlphaCode3; }
    }

    public static string Region
    {
        get { return Resource.Region; }
    }

    public static string Subregion
    {
        get { return Resource.Subregion; }
    }

    public static string Demonym
    {
        get { return Resource.Demonym; }
    }

    public static string GINI
    {
        get { return Resource.GINI; }
    }

    public static string NativeName
    {
        get { return Resource.NativeName; }
    }

    public static string NumericCode
    {
        get { return Resource.NumericCode; }
    }

    public static string CIOC
    {
        get { return Resource.CIOC; }
    }

    public static string Borders
    {
        get { return Resource.Borders; }
    }

    public static string Currencies
    {
        get { return Resource.Currencies; }
    }

    public static string MyLanguages
    {
        get { return Resource.MyLanguages; }
    }
}
