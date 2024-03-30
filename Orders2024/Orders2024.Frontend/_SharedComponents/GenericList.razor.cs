using Microsoft.AspNetCore.Components;

namespace Orders2024.Frontend._SharedComponents;

public partial class GenericList<Titem>
{
    [Parameter] public RenderFragment? Loading { get; set; }
    [Parameter] public RenderFragment? NoRecords { get; set; }
    [Parameter, EditorRequired] public RenderFragment Body { get; set; } = null!;
    [Parameter, EditorRequired] public List<Titem> DataSource { get; set; } = null!;
}
