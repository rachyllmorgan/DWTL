using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DWTL.Models
{
    public class DownRepository
    {
        private DownContext _context;
        public DownContext Context { get { return _context; } }

        public DownRepository()
        {
            _context = new DownContext();
        }
        // construstor that allows us to inject context
        public DownRepository(DownContext a_content)
        {
            _context = a_content;
        }

        public List<DownUser> GetAllUsers()
        {
            var query = from users in _context.DownUsers select users;
            return query.ToList();
        }

        public DownUser GetUserByHandle(string handle)
        {
            var query = from user in _context.DownUsers where user.Handle == handle select user;
            return query.SingleOrDefault();
        }

        public bool IsHandleAvailable(string handle)
        {
            bool is_available = false;

            try
            {
                DownUser a_user = GetUserByHandle(handle);
                    if(a_user == null)
                {
                    is_available = true;
                }
            }
            catch (InvalidOperationException) { }

            return is_available;
        }

        public List<DownUser> SearchByHandle(string handle)
        {
            var query = from user in _context.DownUsers select user;
            List<DownUser> found_users = query.Where(user => Regex.IsMatch(user.Handle, handle, RegexOptions.IgnoreCase)).ToList();
            found_users.Sort();
            return found_users;
        }

        public List<DownUser> SearchByName(string name_search)
        {
            var query = from user in _context.DownUsers select user;
            List<DownUser> found_users = query.Where(user => Regex.IsMatch(user.FirstName, name_search, RegexOptions.IgnoreCase) || Regex.IsMatch(user.LastName, name_search, RegexOptions.IgnoreCase)).ToList();
            found_users.Sort();
            return found_users;
        }

        public List<Competition> GetAllCompetitions()
        {
            var query = from comps in _context.Competitions select comps;
            return query.ToList();
        }

        public Competition GetCompetitionByName(string comp_name)
        {
            var query = from comp in _context.Competitions where comp.Name == comp_name select comp;
            return query.SingleOrDefault();
        }

        public List<Competition> SearchCompetitionByName(string name_search)
        {
            var query = from comp in _context.Competitions select comp;
            List<Competition> found_comps = query.Where(comp => Regex.IsMatch(comp.Name, name_search, RegexOptions.IgnoreCase)).ToList();
            found_comps.Sort();
            return found_comps;
        }

        public bool IsCompetitionNameAvailable(string comp_name)
        {
            bool is_name_available = false;

            try
            {
                Competition a_comp = GetCompetitionByName(comp_name);
                if (a_comp == null)
                {
                    is_name_available = true;
                }
            }
            catch (InvalidOperationException) { }

            return is_name_available;
        }

        public List<Competition> GetUserCompetitions(string handle)
        {
            var query = from user in _context.DownUsers where user.Handle == handle select user;
            DownUser found_user = query.Single();
            return found_user.Competition;
        }

        public List<Post> GetPostsByCompetition(string comp_name)
        {
            var query = from post in _context.Posts select post;
            List<Post> found_post = query.Where(post => post.Competition.ToString() == comp_name).ToList();
            found_post.Sort();
            return found_post;
        }

        public bool CreatePost(DownUser down_user1, Competition comp_name, string content)
        {
            Post a_post = new Post { Content = content, Date = DateTime.Now, Author = down_user1, Competition = comp_name };
            bool is_added = true;
            try
            {
                Post added_post = _context.Posts.Add(a_post);
                _context.SaveChanges();
                //if (added_post == null)
                //{
                //    is_added = false;
                //}
            }
            catch (Exception)
            {
                is_added = false;
            }
            return is_added;
        }
    }
}