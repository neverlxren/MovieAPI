using System;
using System.Collections.Generic;

namespace MovieAPI.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public short ReleaseYear { get; set; }

    public string Director { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public short DurationMinutes { get; set; }

    public decimal Rating { get; set; }

    public string Country { get; set; } = null!;

    public string Language { get; set; } = null!;
}
