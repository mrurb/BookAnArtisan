using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using Model;
using WCF;
namespace Testing
{
    /*
     * Created: (10/04-17) Jira ID: AA-37
     * Test Class for search feature. Contains tests for search by: 
     * Tag, project name, project status, artisan, client and address of client/project.
     */

    [TestClass]
    public class Search
    {
        ProjectSearch ps = new ProjectSearch();
        string input_tag = "Kitchen"; // TODO : add stuff here
        string pname = "asdasdadasdasdsad";
        bool status = true;
        User art = new User("1", "Laurids Andersen", "lauridsandersen2013@gmail.com", "12345678", "1234", "østre allé 58");
        string address = "østre allé 58";
        /*
         * Test: Only input tags. Tested with simple tag initially. Created (10/04-17)
         * Success: Something was found.
         */
        [TestMethod]
        public void TestSearchByTag()
        {
            IList<Project> results = ps.SearchByTag(input_tag);
            List<Project> resultsList = (List<Project>)results;
            Assert.AreNotEqual(0, resultsList.ToArray().Length);
        }

        /*
         * Created (10/04-17) 
         * Test: only project name. Tested with standard input name initially.
         * Success: Something was found.
         */
         [TestMethod]
         public void TestSearchProjectName()
        {
            IList<Project> results = ps.SearchByProjectName(pname);
            List<Project> resultsList = (List<Project>)results;
            Assert.AreNotEqual(0, resultsList.ToArray().Length);
        }

        /*
         * Created (10/04-17)
         * Test: only project status
         * Success: Something was found.
         */
         [TestMethod]
         public void TestSearchProjectStatus()
        {
            IList<Project> results = ps.SearchByProjectStatus(status);
            List<Project> resultsList = (List<Project>)results;
            Assert.AreNotEqual(0, resultsList.ToArray().Length);
        }

        /*
         *  Created (10/04-17)
         * Test: only project user
         * Success: Something was found. 
         */
         [TestMethod]
        public void TestSearchProjectUser()
        {
            IList<Project> results = ps.SearchByProjectUser(art);
            List<Project> resultsList = (List<Project>)results;
            Assert.AreNotEqual(0, resultsList.ToArray().Length);
        }

        /*
         *  Created (10/04-17)
         * Test: only project client
         * Success: Something was found. 
         */
        [TestMethod]
        public void TestSearchProjectClientAddress()
        {
            IList<Project> results = ps.SearchByProjectAddress(address);
            List<Project> resultsList = (List<Project>)results;
            Assert.AreNotEqual(0, resultsList.ToArray().Length);
        }
    }
}
