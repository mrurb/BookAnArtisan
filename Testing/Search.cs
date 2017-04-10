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
        string input_tag = "Kitchen";
        /*
         * Test: Only input tags. Tested with simple tag initially. Created (10/04-17)
         * Success: Something was found.
         */
        [TestMethod]
        public void TestMethod1(string input_tag)
        {
            List<Project> results = pc.search_by_tag(input_tag);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         * Created (10/04-17) 
         * Test: only project name. Tested with standard input name initially.
         * Success: Something was found.
         */
         [TestMethod]
         public void Test_Search_Project_Name(string pname)
        {
            List<Project> results = pc.search_by_project_name(pname);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         * Created (10/04-17)
         * Test: only project status
         * Success: Something was found.
         */
         [TestMethod]
         public void Test_Search_Project_Status(bool status)
        {
            List<Project> results = pc.search_by_project_status(status);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         *  Created (10/04-17)
         * Test: only project artisan
         * Success: Something was found. 
         */
         [TestMethod]
        public void Test_Search_Project_Artisan(Artisan art)
        {
            List<Project> results = pc.search_by_project_user(art);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         *  Created (10/04-17)
         * Test: only project client
         * Success: Something was found. 
         */
        [TestMethod]
        public void Test_Search_Project_Client(Client client)
        {
            List<Project> results = pc.search_by_project_user(client);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }

        /*
         *  Created (10/04-17)
         * Test: only project client
         * Success: Something was found. 
         */
        [TestMethod]
        public void Test_Search_Project_Client_Address(string address)
        {
            List<Project> results = pc.search_by_project_address(address);
            Assert.AreNotEqual(0, results.ToArray().Length);
        }
    }
}
