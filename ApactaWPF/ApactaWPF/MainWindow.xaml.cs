using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ApactaWPF.ServiceReferences;
using System.ComponentModel;
using Xceed.Wpf.Toolkit;

namespace ApactaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Booking bla1 = null;
        private Booking blacreate = null;
        private Material blacreate2 = null;
        private User blacreate3 = null;
        RentingServiceClient rCl = new RentingServiceClient();
        UserServiceClient sCl = new UserServiceClient();
        MaterialServiceClient mCl = new MaterialServiceClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Get_All_Bookings(object sender, RoutedEventArgs e)
        {
            showAllView.Items.Clear();
            var mylist = rCl.ReadAllBooking();
            foreach (var stuff in mylist)
            {
                showAllView.Items.Add(stuff);
            }
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                bla1 = (Booking)item;
                Booking bla = (Booking)item;
                bla = rCl.ReadBooking(bla);
                //add data to labels
                materialtxt.Text = bla.Item.Name;
                rentertxt.Text = bla.User.UserName;
                totxt.Text = bla.EndTime.ToString();
                fromtxt.Text = bla.StartTime.ToString();
                ownertxt.Text = bla.Item.Owner.UserName;
            }
        }

        private void showAllView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //listView_Click(sender, e);
        }

        private void btn_delete(object sender, RoutedEventArgs e)
        {
            if (bla1 != null)
            {
                rCl.DeleteBooking(bla1);
            }
        }

        private void SUBMITBTN_Click(object sender, RoutedEventArgs e)
        {
            if (bla1 != null)
            {
                Booking bla2 = new Booking()
                {
                    Id = bla1.Id,
                    StartTime = Convert.ToDateTime(fromtxt.Text),
                    EndTime = Convert.ToDateTime(totxt.Text),
                    //User = sCl.ReadUser(new User() { Id = bla1.User.Id})
                    Item = bla1.Item,
                    User = bla1.User
                };
                rCl.UpdateBooking(bla2);
            }
        }

        private void create(object sender, RoutedEventArgs e)
        {
            if (blacreate2 != null && blacreate3 !=null)
            {
                blacreate = new Booking()
                {
                    Item = mCl.ReadMaterial(new Material() { Id = Convert.ToInt32(materialtxt_create.Text) }),
                    User = sCl.ReadUser(new User() { Id = rentertxt_create.Text}),
                    StartTime = (DateTime)fromdatetime.Value,
                    EndTime = (DateTime)todatetime.Value
                };
                rCl.CreateBooking(blacreate);
            }
        }

        private void GetAllUsers_Click(object sender, RoutedEventArgs e)
        {
            listView_Users.Items.Clear();
            var mylist = sCl.ReadAllUser();
            foreach (var stuff in mylist)
            {
                listView_Users.Items.Add(stuff);
            }
        }

        private void GetAllMaterials_Click(object sender, RoutedEventArgs e)
        {
            listView_Materials.Items.Clear();
            var mylist = mCl.ReadAllMaterial();
            foreach (var stuff in mylist)
            {
                listView_Materials.Items.Add(stuff);
            }
        }

        private void listViewUsers_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                blacreate3 = (User)item;
                blacreate3 = sCl.ReadUser(blacreate3);
                //add data to labels
                rentertxt_create.Text = blacreate3.Id.ToString();
            }
        }

        private void listViewMaterial_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                blacreate2 = (Material)item;
                blacreate2 = mCl.ReadMaterial(blacreate2);
                //add data to labels
                materialtxt_create.Text = blacreate2.Id.ToString();
            }
        }
    }
}
