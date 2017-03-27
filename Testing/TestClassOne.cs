using System;
using BLL;
using WCF;
using Model;

namespace Testing
{
    public class TestClassOne
    {
        static BLL.Class1 controller = null;
    public static void setUpBeforeClass() 
        {
            try
            {
                controller = new BLL.Class1();
            } catch
            {
                throw new Exception();
            }
        }

    public static void tearDownAfterClass()
        {
            try
            {
                controller = null;
            } catch
            {
                throw new Exception();
            }
}

    public void setUp()
        {
            try
            {
                //do nothing
            } catch
            {
                throw new Exception();
    }
}
    public void tearDown()
    {
            try
            {
                //do nothing
            }
            catch
            {
                throw new Exception();
            }
        }

        public void TestFail()
        {
            try
            {
                //do nothing
            }
            catch
            {
                throw new Exception();
            }
        }

        public void TestSucceed()
        {
            try
            {
                //do nothing
            }
            catch
            {
                throw new Exception();
            }
        }
}
}
