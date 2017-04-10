using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using Model;

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
        ProjectController pc = new ProjectController();
        string input_tag = "Kitchen"; // TODO : add stuff here
        /*
         * Test: Only input tags. Tested with simple tag initially. Created (10/04-17)
         * Success: Something was found.
         */
        [TestMethod]
        public void TestSearchByTag(string input_tag)
        {
            List<Project> results = pc.SearchByTag(input_tag);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         * Created (10/04-17) 
         * Test: only project name. Tested with standard input name initially.
         * Success: Something was found.
         */
         [TestMethod]
         public void TestSearchProjectName(string pname)
        {
            List<Project> results = pc.SearchByProjectName(pname);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         * Created (10/04-17)
         * Test: only project status
         * Success: Something was found.
         */
         [TestMethod]
         public void TestSearchProjectStatus(bool status)
        {
            List<Project> results = pc.SearchByProjectStatus(status);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         *  Created (10/04-17)
         * Test: only project user
         * Success: Something was found. 
         */
         [TestMethod]
        public void TestSearchProjectUser(User art)
        {
            List<Project> results = pc.SearchByProjectUser(art);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         *  Created (10/04-17)
         * Test: only project client
         * Success: Something was found. 
         */
        [TestMethod]
        public void TestSearchProjectClientAddress(string address)
        {
            List<Project> results = pc.SearchByProjectAddress(address);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }
    }
}
