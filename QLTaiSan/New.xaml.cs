using QLTaiSan.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace QLTaiSan
{
    public sealed partial class New : Page
    {
        int thoigiansudung = 0;
        double _tylehaomon = 0;
        public List<taisan> taisans { get; set; }
        public List<tinhhaomon> phaomon { get; set; }
        public New()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            var query = await conn.QueryAsync<tinhhaomon>(
                "SELECT Nhomtaisan from tinhhaomon");
            List<string> n = new List<string>();
            foreach(var g in query.ToList())
            {
                n.Add(g.Nhomtaisan);
            }
            cbTinhhaomon.ItemsSource = n;
        }
        private async void addTaiSanAppBar_Click(object sender, RoutedEventArgs e)
        {
            if(TenTaiSan.Text == "")
            {
                MessageDialog messageDialog = new MessageDialog("Bạn chưa nhập tên tài sản.");
                await messageDialog.ShowAsync();
                return;
            }
            if(GiaMua.Text == "")
            {
                MessageDialog messageDialog = new MessageDialog("Bạn chưa nhập giá trị tài sản.");
                await messageDialog.ShowAsync();
                return;
            }
            if (cbTinhhaomon.SelectedIndex == -1)
            {
                MessageDialog messageDialog = new MessageDialog("Bạn chưa chọn loại tài sản.");
                await messageDialog.ShowAsync();
                return;
            }
            taisan newTaisan = new taisan()
            {
                name = TenTaiSan.Text,
                datebuy = new DateTime(NgayMua.Date.Date.Year, NgayMua.Date.Date.Month, NgayMua.Date.Date.Day),
                price = GiaMua.Text,
                brand = NhanHieu.Text,
                store = NoiMua.Text,
                loaitaisan = (string)cbTinhhaomon.SelectedValue,
                timeuse = thoigiansudung,
                tylehaomon = _tylehaomon
                
            };

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            await conn.InsertAsync(newTaisan);

            this.Frame.Navigate(typeof(MainPage));
        }

        private async void cbTinhhaomon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            var query = await conn.QueryAsync<tinhhaomon>("SELECT Thoigiansudung, Tylehaomon FROM tinhhaomon WHERE Nhomtaisan LIKE '" + cbTinhhaomon.SelectedValue.ToString() +"'");
            thoigiansudung = query.ElementAt(0).Thoigiansudung;
            _tylehaomon = query.ElementAt(0).Tylehaomon;

            //Debug.WriteLine(_tylehaomon);
        }
    }
}
