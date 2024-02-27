using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Shared.ViewModels.ReviewViewModels
{
    public record ReviewViewModel(
            int Id,
            string CustomerId,
            int ServiceId,
            int Rating, 
            string Comment
        );
    public record ReviewDetailsVM(
        int Id,
        string CustomerEmail,
        int Rating,
        string Comment
    );
}
