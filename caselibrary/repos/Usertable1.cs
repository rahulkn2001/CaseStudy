using caselibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caselibrary.repos
{
    public class Usertable1 : IUser
    {
        private readonly RetailapplicationContext _context;

       public  Usertable1(RetailapplicationContext context)
        {
            _context = context;
        }

        public Usertable Userdetails1(int id)
        {
            Usertable user = _context.Usertables.Where(u => u.UserId == id).FirstOrDefault();
            return user;
        }
    }
}
