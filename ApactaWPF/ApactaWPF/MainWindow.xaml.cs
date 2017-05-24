using System;
using System.Globalization;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using ApactaWPF.ServiceReferences;

namespace ApactaWPF
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly BookingServiceClient bookingServiceClient = new BookingServiceClient();
		private readonly MaterialServiceClient materialServiceClient = new MaterialServiceClient();
		private readonly UserServiceClient userServiceClient = new UserServiceClient();
		private Booking booking1;
		private Booking booking2;
		private Material material1;
		private User user1;

		public MainWindow()
		{
			InitializeComponent();

			var bookingsList = bookingServiceClient.ReadAllBooking();
			foreach (var booking in bookingsList)
				ShowAllView.Items.Add(booking);

			var usersList = userServiceClient.ReadAllUser();
			foreach (var user in usersList)
				ListViewUsers.Items.Add(user);

			var materialsList = materialServiceClient.ReadAllMaterial();
			foreach (var material in materialsList)
				ListViewMaterials.Items.Add(material);
		}

		private void GetAllBookings(object sender, RoutedEventArgs e)
		{
			try
			{
				ShowAllView.Items.Clear();
				var mylist = bookingServiceClient.ReadAllBooking();
				foreach (var stuff in mylist)
					ShowAllView.Items.Add(stuff);
			}
			catch (FaultException)
			{
				MessageBox.Show("A service error occured.", "Service Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception)
			{
				MessageBox.Show("An error occured.", "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void listView_Click(object sender, RoutedEventArgs e)
		{
			var item = (sender as ListView)?.SelectedItem;
			if (item == null) return;
			booking1 = (Booking)item;
			var bla = (Booking)item;
			bla = bookingServiceClient.ReadBooking(bla);
			MaterialTxt.Text = bla.Item.Name;
			RenterTxt.Text = bla.User.UserName;
			ToDateTimeTxt.Text = bla.EndTime.ToString(CultureInfo.CurrentCulture);
			FromDateTimeTxt.Text = bla.StartTime.ToString(CultureInfo.CurrentCulture);
			OwnerTxt.Text = bla.Item.Owner.UserName;
		}

		private void Delete(object sender, RoutedEventArgs e)
		{
			try
			{
				if (booking1 != null)
					bookingServiceClient.DeleteBooking(booking1);
			}
			catch (FaultException)
			{
				MessageBox.Show("A service error occured.", "Service Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception)
			{
				MessageBox.Show("An error occured.", "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Update(object sender, RoutedEventArgs e)
		{
			if (booking1 == null || FromDateTimeTxt.Value == null || ToDateTimeTxt.Value == null) return;
			try
			{
				var bla2 = new Booking
				{
					Id = booking1.Id,
					StartTime = (DateTime)FromDateTimeTxt.Value,
					EndTime = (DateTime)ToDateTimeTxt.Value,
					Updated = booking1.Updated,
					Item = booking1.Item,
					User = booking1.User
				};
				bookingServiceClient.UpdateBooking(bla2);
			}
			catch (FaultException)
			{
				MessageBox.Show("A service error occured.", "Service Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception)
			{
				MessageBox.Show("An error occured.", "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Create(object sender, RoutedEventArgs e)
		{
			try
			{
				if (material1 == null || user1 == null || FromDateTime.Value == null || ToDateTime.Value == null) return;
				booking2 = new Booking
				{
					Item = materialServiceClient.ReadMaterial(new Material { Id = Convert.ToInt32(HiddenIdMatLbl.Content) }),
					User = userServiceClient.ReadUser(new User { Id = HiddenIdRenLbl.Content.ToString() }),
					StartTime = Convert.ToDateTime(FromDateTime.Value),
					EndTime = Convert.ToDateTime(ToDateTime.Value)
				};
				bookingServiceClient.CreateBooking(booking2);
			}
			catch (FaultException)
			{
				MessageBox.Show("A service error occured.", "Service Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception)
			{
				MessageBox.Show("An error occured.", "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void GetAllUsers(object sender, RoutedEventArgs e)
		{
			try
			{
				ListViewUsers.Items.Clear();
				var mylist = userServiceClient.ReadAllUser();
				foreach (var stuff in mylist)
					ListViewUsers.Items.Add(stuff);
			}
			catch (FaultException)
			{
				MessageBox.Show("A service error occured.", "Service Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception)
			{
				MessageBox.Show("An error occured.", "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void GetAllMaterials(object sender, RoutedEventArgs e)
		{
			try
			{
				ListViewMaterials.Items.Clear();
				var mylist = materialServiceClient.ReadAllMaterial();
				foreach (var stuff in mylist)
					ListViewMaterials.Items.Add(stuff);
			}
			catch (FaultException)
			{
				MessageBox.Show("A service error occured.", "Service Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception)
			{
				MessageBox.Show("An error occured.", "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void listViewUsers_Click(object sender, RoutedEventArgs e)
		{
			var item = (sender as ListView)?.SelectedItem;
			if (item == null) return;
			user1 = (User)item;
			user1 = userServiceClient.ReadUser(user1);
			CreateRenterTxt.Text = user1.Email;
			HiddenIdRenLbl.Content = user1.Id;
		}

		private void listViewMaterial_Click(object sender, RoutedEventArgs e)
		{
			var item = (sender as ListView)?.SelectedItem;
			if (item == null) return;
			material1 = (Material)item;
			material1 = materialServiceClient.ReadMaterial(material1);
			CreateMaterialTxt.Text = material1.Name;
			HiddenIdMatLbl.Content = material1.Id;
		}
	}
}