using Microsoft.AspNetCore.Components;
using Orders2024.Frontend.Repositories;
using Orders2024.Shared.Entities;

namespace Orders2024.Frontend.Pages.Countries;

public partial class Index
{
    [Inject] IRepository Repository { get; set; } = null!;
    public List<Country>? Countries { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var responseHttp = await Repository.GetAsyn<List<Country>>("api/countries");
        if (!responseHttp.Error)
        {
            Countries = responseHttp.Response;
        }
    }
}
