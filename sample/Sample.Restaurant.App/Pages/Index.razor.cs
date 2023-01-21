using Sample.Restaurant.Application;

namespace Sample.Restaurant.App.Pages;

public partial class Index
{
    private TableDto[]? tables;

    protected override async Task OnInitializedAsync()
    {
        var result = await this.Mediator.Send(new GetTablesQuery(Guid.NewGuid()));

        if (!result.IsSuccess || result.Data == null)
        {
            // TODO: error handling
            return;
        }

        this.tables = result.Data.ToArray();
    }

    private async Task UseTable(int tableNumber)
    {
        var result = await this.Mediator.Send(new UseTableCommand(Guid.NewGuid(), tableNumber));

        if (!result.IsSuccess)
        {
            // TODO: error handling
        }

        this.ViewTable(tableNumber);
    }

    private void ViewTable(int tableNumber)
    {
        this.NavigationManager.NavigateTo($"tables/{tableNumber}");
    }
}