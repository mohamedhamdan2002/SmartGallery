namespace SmartGallery.Shared.ViewModels.ServiceViewModels;

public abstract record ServiceForManipulationViewModel
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}
