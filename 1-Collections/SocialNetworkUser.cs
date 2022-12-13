using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        Dictionary<String, IList<TUser>> _groups;

        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
            _groups = new Dictionary<String, IList<TUser>>();
        }

        public bool AddFollowedUser(string group, TUser user)
        {   
            if (_groups.ContainsKey(group))
            {
                if (_groups[group].Contains(user)) return false;
                _groups[group].Add(user);
                return true;
            }
            IList<TUser> newgroup = new List<TUser>();
            newgroup.Add(user);
            _groups.Add(group, newgroup);
            return true;
        }

        public IList<TUser> FollowedUsers
        {
            get
            {   
                List<TUser> retlist = new List<TUser>();
                foreach (String gr in _groups.Keys)
                {
                    foreach (TUser u in _groups[gr])
                    {
                        if(!retlist.Contains(u)) retlist.Add(u);
                    }
                }
                return retlist;
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            List<TUser> retlist = new List<TUser>();

            if (!_groups.ContainsKey(group)) return retlist;

            foreach (TUser u in _groups[group])
            {
                retlist.Add(u);
            }
            return retlist;
        }
    }
}
