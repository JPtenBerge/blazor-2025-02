using DemoProject.Entities;

namespace DemoProject.Components.Pages
{
    public partial class Cursussen
    {
        public string Naam { get; set; } = "JP";

        public List<Course>? Courses { get; set; } = new()
        {
            new()
            {
                Title = "Awesome Blazorrrr",
                NrOfDays = 5,
                FunPhoto = "https://www.delta-n.nl/wp-content/uploads/2019/10/BrandBlazor_300.png"
            },
            new()
            {
                Title = "Awesome C#",
                NrOfDays = 5,
                FunPhoto = "https://miro.medium.com/v2/resize:fit:375/1*NhpIIUL7AFgKKn30gKoDUw.png"
            },
            new()
            {
                Title = "Matige React",
                NrOfDays = 2,
                FunPhoto = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/React.svg/1200px-React.svg.png"
            }
        };
    }
}
