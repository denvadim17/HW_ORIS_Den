using TeamHost.Domain.Entities;

namespace TeamHost.Web.Areas.Friends.Models;

public class FriendsViewModel
{
    public List<User> Friends { get; set; } = new List<User>();
    public List<User> SearchResults { get; set; } = new List<User>();
    public List<Friendship> FriendRequests { get; set; } = new List<Friendship>();
    public string SearchTerm { get; set; }
}