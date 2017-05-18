﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using Model;
using DAL;

namespace BLL
{
    public class RentingController : IController<Booking>
    {
        RentingDb rdb = new RentingDb();
        MaterialController mctr = new MaterialController();
        UserController uctr = new UserController();
        public Booking Create(Booking t)
        {
            if (t.StartTime >= t.EndTime)
            {
                throw new ApplicationException("Dates Overlapping");
            }
            var material = mctr.Read(t.Item);
            if (material.Name != t.Item.Name)
            {
                throw new ApplicationException("Material does not exist");
            }
            var theuser = uctr.Read(t.User);
            if (theuser.UserName != t.User.UserName)
            {
                throw new ApplicationException("User does not exist");
            }
            return rdb.Create(t);
        }

        public Booking Delete(Booking t)
        {
            rdb.Delete(t);
            return rdb.Read(t);
        }

        public Booking Read(Booking t)
        {
            return rdb.Read(t);
        }

        public List<Booking> ReadAll()
        {
            return rdb.ReadAll();
        }

        public Booking Update(Booking t)
        {
            if (t.StartTime >= t.EndTime)
            {
                throw new ApplicationException("Dates Overlapping");
            }
            var theuser = uctr.Read(t.User);
            if (theuser.UserName != t.User.UserName)
            {
                throw new ApplicationException("User does not exist");
            }
            return rdb.Update(t);
        }
    }
}
