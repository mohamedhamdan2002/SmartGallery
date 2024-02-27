namespace SmartGallery.Shared.ViewModels.ServiceViewModels;

public abstract record ServiceForManipulationViewModel
{
    public string? Icon { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
