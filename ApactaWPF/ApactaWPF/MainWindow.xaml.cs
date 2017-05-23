using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ApactaWPF.ServiceReferences;

namespace ApactaWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private Booking booking1;
		private Booking booking2;
		private Material material1;
		private User user1;

		private readonly BookingServiceClient rCl = new BookingServiceClient();
		private readonly UserServiceClient sCl = new UserServiceClient();
		private readonly MaterialServiceClient mCl = new MaterialServiceClient();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Get_All_Bookings(object sender, RoutedEventArgs e)
		{
			ShowAllView.Items.Clear();
			var mylist = rCl.ReadAllBooking();
			foreach (var stuff in mylist)
			{
				ShowAllView.Items.Add(stuff);
			}
		}

		private void listView_Click(object sender, RoutedEventArgs e)
		{
			var item = (sender as ListView)?.SelectedItem;
			if (item == null) return;
			booking1 = (Booking)item;
			var bla = (Booking)item;
			bla = rCl.ReadBooking(bla);
			MaterialTxt.Text = bla.Item.Name;
			RenterTxt.Text = bla.User.UserName;
			ToDateTimeTxt.Text = bla.EndTime.ToString(CultureInfo.InvariantCulture);
			FromDateTimeTxt.Text = bla.StartTime.ToString(CultureInfo.InvariantCulture);
			OwnerTxt.Text = bla.Item.Owner.UserName;
		}

		private void btn_delete(object sender, RoutedEventArgs e)
		{
			if (booking1 != null)
			{
				rCl.DeleteBooking(booking1);
			}
		}

		private void SubmitBtn_Click(object sender, RoutedEventArgs e)
		{
			if (booking1 == null || FromDateTimeTxt.Value == null || ToDateTimeTxt.Value == null) return;
			var bla2 = new Booking
			{
				Id = booking1.Id,
				StartTime = (DateTime)FromDateTimeTxt.Value,
				EndTime = (DateTime)ToDateTimeTxt.Value,
				Updated = booking1.Updated,
				Item = booking1.Item,
				User = booking1.User
			};
			rCl.UpdateBooking(bla2);
		}

		private void Create(object sender, RoutedEventArgs e)
		{
			if (material1 == null || user1 == null || FromDateTime.Value == null || ToDateTime.Value == null) return;
			booking2 = new Booking
			{
				Item = mCl.ReadMaterial(new Material { Id = Convert.ToInt32(CreateMaterialTxt.Text) }),
				User = sCl.ReadUser(new User { Id = CreateRenterTxt.Text}),
				StartTime = (DateTime)FromDateTime.Value,
				EndTime = (DateTime)ToDateTime.Value
			};
			rCl.CreateBooking(booking2);
		}

		private void GetAllUsers_Click(object sender, RoutedEventArgs e)
		{
			ListViewUsers.Items.Clear();
			var mylist = sCl.ReadAllUser();
			foreach (var stuff in mylist)
			{
				ListViewUsers.Items.Add(stuff);
			}
		}

		private void GetAllMaterials_Click(object sender, RoutedEventArgs e)
		{
			ListViewMaterials.Items.Clear();
			var mylist = mCl.ReadAllMaterial();
			foreach (var stuff in mylist)
			{
				ListViewMaterials.Items.Add(stuff);
			}
		}

		private void listViewUsers_Click(object sender, RoutedEventArgs e)
		{
			var item = (sender as ListView)?.SelectedItem;
			if (item == null) return;
			user1 = (User)item;
			user1 = sCl.ReadUser(user1);
			CreateRenterTxt.Text = user1.Id;
		}

		private void listViewMaterial_Click(object sender, RoutedEventArgs e)
		{
			var item = (sender as ListView)?.SelectedItem;
			if (item == null) return;
			material1 = (Material)item;
			material1 = mCl.ReadMaterial(material1);
			CreateMaterialTxt.Text = material1.Id.ToString();
		}
	}
}
