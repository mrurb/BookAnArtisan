using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class MeetingDB : IDataAccess<Meeting>
    {
        public Meeting Create(Meeting t)
        {
            return null;
        }

        public bool Delete(Meeting t)
        {
            throw new NotImplementedException();
        }

        public Meeting Read(Meeting t)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Meeting Update(Meeting t)
        {
            throw new NotImplementedException();
        }
    }
}
