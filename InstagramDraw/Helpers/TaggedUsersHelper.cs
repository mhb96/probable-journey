using InstagramDraw.Models;
using System.Collections.Generic;
using System.Linq;

namespace InstagramDraw.Helpers
{
    public static class TaggedUsersHelper
    {
        /// <summary>
        /// Values the tuple.
        /// </summary>
        /// <typeparam name="dynamic">The type of the ynamic.</typeparam>
        /// <returns></returns>
        public static List<UserModel> GetUsers(dynamic data, int personToTicketRatio)
        {
            var users = new List<UserModel>();
            foreach (var edge in data.graphql.shortcode_media.edge_media_preview_comment.edges)
            {
                var comment = (string)edge.node.text;
                var taggedUsers = GetTaggedUsers(comment);

                var existingUser = users.FirstOrDefault(u => u.UserId == (string)edge.node.owner.id);

                if (existingUser != null)
                {
                    var newUsers = taggedUsers.Where(tu => !existingUser.UniqueTaggedUsers.Contains(tu));
                    existingUser.UniqueTaggedUsers.AddRange(newUsers);
                    existingUser.Score = existingUser.UniqueTaggedUsers.Count / personToTicketRatio;
                }
                else
                {
                    var user = new UserModel
                    {
                        UserName = (string)edge.node.owner.username,
                        UserId = (string)edge.node.owner.id,
                        UniqueTaggedUsers = taggedUsers,
                        Score = taggedUsers.Count/personToTicketRatio
                    };
                    users.Add(user);
                }
            }

            return users;
        }

        private static List<string> GetTaggedUsers(string comment)
        {
            List<string> taggedUsers = new List<string>();

            var splitTexts = comment.Split().ToList();
            var taggedTexts = splitTexts.Where(t => t.Contains("@")).Select(t => t.Split("@").Where(x => !string.IsNullOrWhiteSpace(x)).ToList()).ToList();
            foreach (var taggedText in taggedTexts)
                foreach (var t in taggedText)
                    taggedUsers.Add(t);

            return taggedUsers;
        }
    }
}
