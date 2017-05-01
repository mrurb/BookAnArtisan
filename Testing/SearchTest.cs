using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using Model;
using DAL;
using WCF;

namespace Testing
{
    /*
     * Created: (10/04-17) Jira ID: AA-37
     * Test Class for search feature. Contains tests for search by: 
     * Tag, project name, project status, artisan, client and address of client/project.
     */


    [TestClass]
    public class SearchTest
    {
        static ProjectSearch ps;
        static string input_tag;
        static string address;
        static User art;

        [ClassInitialize]
        public static void setUpBeforeClass(TestContext tc)
        {
            try
            {
                input_tag = "VVS";
                art = new User("1", "Laurids", "Andersen", "lauridsandersen2013@gmail.com", "12345678", "1234", "østre allé 58");
                address = "stuf";
            }
            catch
            {
                throw new Exception();
            }
        }

        [ClassCleanup]
        public static void tearDownAfterClass()
        {
            try
            {
                ps = null;
                art = null;
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestInitialize]
        public void setUp()
        {
            try
            {
                ps = new ProjectSearch();
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestCleanup]
        public void tearDown()
        {
            try
            {
                ps = null;
            }
            catch
            {
                throw new Exception();
            }
        }


        /*
         * Created (10/04-17)
         * Test of connection to database. Success if there is a connection.
         */
        [TestMethod]
        public void TestDBCon()
        {
            SearchDB dba = new SearchDB();
            bool worked = dba.DBConnectionTest();
            Assert.AreEqual(true, worked);
        }
        /*
         * Test: Only input tags. Tested with simple tag initially. Created (10/04-17)
         * Success: Something was found.
         */
        [TestMethod]
        public void TestSearchByTag()
        {
            IList<Project> results = ps.SearchByTag(input_tag);
            List<Project> resultsList = (List<Project>)results;
            Project[] parray = resultsList.ToArray();
            Assert.AreNotEqual(0, parray[0].tags.Count);
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

        /*
         * Created (24/04-17)
         * Test: Ensure the correct object is taken from the DB
         * Success: The *correct* object was returned
         */
         [TestMethod]
         public void TestSearchIntegrationTest()
        {
            IList<Project> results = ps.SearchByProjectAddress(address);
            List<Project> resultsList = (List<Project>)results;
            Assert.AreEqual("stuff 21", resultsList[0].address);
        }
    }
}