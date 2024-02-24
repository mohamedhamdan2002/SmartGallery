namespace SmartGallery.Shared.ViewModels.ServiceViewModels;

public abstract record ServiceForManipulationViewModel
{
    public string? Icon { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}
