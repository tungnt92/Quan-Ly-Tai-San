using QLTaiSan.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography.Certificates;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace QLTaiSan
{
    public sealed partial class MainPage : Page
    {
        public List<taisan> taisans { get; set; }
        public List<tinhhaomon> haomon { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            // Create Db if not exist
            bool dbExist = await CheckDbAsync("quanlytaisan.db");
            if (!dbExist)
            {
                await CreateDatabaseAsync();
                await CreateHaoMonDataBaseAsync();
                //await AddtaisansAsync();
                await AddHaoMonAsync();
            }

            // Get taisans
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            var query = conn.Table<taisan>();
            taisans = await query.ToListAsync();

            // Show taisans
            TaisanList.ItemsSource = taisans;
            
        }

        private async Task CreateHaoMonDataBaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            await conn.CreateTableAsync<tinhhaomon>();
        }

        private async Task AddHaoMonAsync()
        {
            var haoMonList = new List<tinhhaomon>()
            {
                new tinhhaomon()
                {
                    Nhomtaisan = "Xe ô-tô",
                    Thoigiansudung = 15,
                    Tylehaomon = 6.67
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Xe gắn máy",
                    Thoigiansudung = 10,
                    Tylehaomon = 10
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Phương tiện đường thủy",
                    Thoigiansudung = 10,
                    Tylehaomon = 10
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Máy vi tính",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Máy in",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Máy phô tô cóp py",
                    Thoigiansudung = 8,
                    Tylehaomon = 12.5
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Thiết bị lọc nước",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Máy hút bụi",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Ti Vi, DVD, CD, VCD,...",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Thiết bị hình ảnh",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Thiết bị âm thanh",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Thiết bị truyền thông",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Thiết bị gia dụng",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Đồ dùng gỗ",
                    Thoigiansudung = 8,
                    Tylehaomon = 12.5
                },
                new tinhhaomon()
                {
                    Nhomtaisan = "Thiết bị khác",
                    Thoigiansudung = 5,
                    Tylehaomon = 20
                }
            };

            // Add rows to the taisan Table
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            await conn.InsertAllAsync(haoMonList);
        }
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }
        private async void CheckDbAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            bool dbExist = await CheckDbAsync("quanlytaisan.db");
            string msg = "The database quanlytaisan.db " + (dbExist ? "is present" : "is not present");

            MessageDialog dialog = new MessageDialog(msg);
            await dialog.ShowAsync();
        }
        private void AddTaiSanAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(New));
        }

        private void DeleteTaiSanAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (taisans != null && taisans.Count > 0)
            {
                // get last inserted user
                taisan taisan = taisans.Last();

                // delete the row of the table
                // SQLite uses the User.Id to find witch row correspond to the user instance
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
                await conn.DeleteAsync(taisan);

                // delete the user from the user list
                taisans.RemoveAt(taisans.Count - 1);

                // Refresh user list
                TaisanList.ItemsSource = null;
                TaisanList.ItemsSource = taisans;
            }*/
        }
        #region SQLite utils
        private async Task<bool> CheckDbAsync(string dbName)
        {
            bool dbExist = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                dbExist = false;
            }

            return dbExist;
        }

        private async Task CreateDatabaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            await conn.CreateTableAsync<taisan>();
        }

        /*private async Task AddtaisansAsync()
        {
            // Create a taisans list
            var taisanList = new List<taisan>()
            {
                new taisan()
                {
                    name = "Pablo Picasso",
                    datebuy = "20/01/2015",
                    price = "120000",
                    brand = "Sony 1",
                    store = "Málaga"
                },
                new taisan()
                {
                    name = "Pablo Picasso 1",
                    datebuy = "20/01/2015",
                    price = "120000",
                    brand = "Sony 11",
                    store = "Málaga"
                },
                new taisan()
                {
                    name = "Pablo Picasso 2",
                    datebuy = "20/01/2015",
                    price = "120000",
                    brand = "Sony 111",
                    store = "Málaga"
                }
            };

            // Add rows to the taisan Table
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            await conn.InsertAllAsync(taisanList);
        }*/

        private async Task SearchtaisanByNameAsync(string name)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");

            var query = conn.Table<taisan>().Where(x => x.name.Contains("Pablo"));
            var result = await query.ToListAsync();
            foreach (var item in result)
            {
                // ...
            }

            var alltaisans = await conn.QueryAsync<taisan>("SELECT * FROM taisan");
            foreach (var taisan in alltaisans)
            {
                // ...
            }

            var citytaisans = await conn.QueryAsync<taisan>(
                "SELECT name FROM taisan WHERE brand = ?", new object[] { "Sony" });
            foreach (var taisan in citytaisans)
            {
                // ...
            }
        }

        private async Task UpdatetaisanNameAsync(string oldName, string newName)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");

            // Retrieve taisan
            var taisan = await conn.Table<taisan>().Where(x => x.name == oldName).FirstOrDefaultAsync();
            if (taisan != null)
            {
                // Modify taisan
                taisan.name = newName;

                // Update record
                await conn.UpdateAsync(taisan);
            }
        }

        private async Task DeletetaisanAsync(string name)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");

            // Retrieve taisan
            var taisan = await conn.Table<taisan>().Where(x => x.name == name).FirstOrDefaultAsync();
            if (taisan != null)
            {
                // Delete record
                await conn.DeleteAsync(taisan);
            }
        }

        private async Task DropTableAsync(string name)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            await conn.DropTableAsync<taisan>();
        }

        #endregion SQLite utils

        private void TaisanList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var taisanItem = (taisan)e.ClickedItem;
            this.Frame.Navigate(typeof(Edit), taisanItem);
        }

        private void khauHao_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
        
            Slider slider = (Slider)sender;
            taisan ts = (taisan)slider.Tag;

            DateTime timeNow = DateTime.Now.Date;
           
            int index = taisans.IndexOf(ts);
            int soNgay = (timeNow - taisans[index].datebuy).Days;
            taisans[index].priceSauKhauHao = double.Parse(taisans[index].price) - double.Parse(taisans[index].price) * (slider.Value/100) * (double)(soNgay/30);
            //time.Text = ((slider.Value/100)).ToString();

            //Debug.WriteLine(taisans[index].priceSauKhauHao);
        }

        private void introApp_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AppInfo));
        }
    }
}