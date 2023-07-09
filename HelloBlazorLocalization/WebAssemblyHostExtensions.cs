using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;

namespace HelloBlazorLocalization
{

    public static class WebAssemblyHostExtensions
    {
        public async static Task SetDefaultCultureAsync(this ServiceProvider serviceProvider)
        {
            var navigationManager = serviceProvider.GetService<NavigationManager>(); 
            var uri = navigationManager!.ToAbsoluteUri(navigationManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            var localStorage = serviceProvider.GetRequiredService<ILocalStorageService>();

            if (queryStrings.TryGetValue("culture", out var selectedCulture))
            {
                await localStorage.SetItemAsStringAsync("culture", selectedCulture);
            }

            var cultureString = await localStorage.GetItemAsync<string>("culture");
            CultureInfo cultureInfo;

            if (!string.IsNullOrWhiteSpace(cultureString))
            {
                cultureInfo = new CultureInfo(cultureString);
            }
            else
            {
                cultureInfo = new CultureInfo("en-US");
            }

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }


}
