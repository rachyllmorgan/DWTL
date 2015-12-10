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

        public List<Competition> GetUserCompetitions(string handle)
        {
            var query = from user in _context.DownUsers where user.Handle == handle select user;
            DownUser found_user = query.Single();
            return found_user.Competition; ;
        }
    }
}