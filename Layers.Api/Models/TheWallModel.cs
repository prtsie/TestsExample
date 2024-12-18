using Layers.Application.Models;

namespace TestsExample.Models;

public class TheWallModel
{
    public required IEnumerable<PostViewModel> Posts { get; init; }

    public Sort Sort { get; set; } = Sort.Date;
}