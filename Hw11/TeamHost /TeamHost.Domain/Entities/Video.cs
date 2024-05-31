using TeamHost.Domain.Common;

namespace TeamHost.Domain.Entities;

public class Video : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string VideoUrl { get; set; }
}