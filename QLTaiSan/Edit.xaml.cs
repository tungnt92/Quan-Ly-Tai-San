using QLTaiSan.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Edit : Page
    {
        int thoigiansudung = 0;
        double _tylehaomon = 0;
        int id;
        public Edit()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            DateTime timeNow = DateTime.Now.Date;
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            var query = await conn.QueryAsync<tinhhaomon>(
                "SELECT Nhomtaisan from tinhhaomon");
            List<string> n = new List<string>();
            foreach (var g in query.ToList())
            {
                n.Add(g.Nhomtaisan);
            }
            cbTinhhaomon.ItemsSource = n;
            
            var taisanItem = (taisan)e.Parameter;
            int soNgay = (timeNow - taisanItem.datebuy).Days / 365;
            id = taisanItem.id;
            editTenTaiSan.Text = taisanItem.name;
            editNgayMua.Date = taisanItem.datebuy;
            editGiaMua.Text = taisanItem.price;
            editNhanHieu.Text = taisanItem.brand;
            editNoiMua.Text = taisanItem.store;
            cbTinhhaomon.SelectedValue = taisanItem.loaitaisan;
            editNgaysudung.Text = soNgay.ToString();
        }

        private async void saveTaiSanAppBar_Click(object sender, RoutedEventArgs e)
        {
            await UpdateTaiSanNameAsync(id, editTenTaiSan.Text, editNgayMua.Date.Date, editGiaMua.Text, editNhanHieu.Text, editNoiMua.Text, cbTinhhaomon.SelectedValue.ToString(), thoigiansudung, _tylehaomon);
            this.Frame.Navigate(typeof(MainPage));
        }
        private async Task UpdateTaiSanNameAsync(int oldid, string newName, DateTime newDayBuy, string newPrice, string newBrand, string newStore, string newLoai, int newthoigiasudung, double newtylehaomon)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            
            
            // Retrieve taisan
            var ptaisan = await conn.Table<taisan>().Where(x => x.id == oldid).FirstOrDefaultAsync();
            
            if (ptaisan != null)
            {
                // Modify taisan
                ptaisan.name = newName;
                ptaisan.price = newPrice;
                ptaisan.store = newStore;
                ptaisan.datebuy = newDayBuy;
                ptaisan.brand = newBrand;
                ptaisan.loaitaisan = newLoai;
                ptaisan.timeuse = newthoigiasudung;
                ptaisan.tylehaomon = newtylehaomon;
                // Update record
                await conn.UpdateAsync(ptaisan);
            }
        }
        private async Task DeleteTaiSanAsync(int oldid)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");

            // Retrieve taisan
            var pvtaisan = await conn.Table<taisan>().Where(x => x.id == oldid).FirstOrDefaultAsync();
            if (pvtaisan != null)
            {
                // Delete record
                await conn.DeleteAsync(pvtaisan);
            }
        }
        private async void deleteTaiSanAppBar_Click(object sender, RoutedEventArgs e)
        {
            await DeleteTaiSanAsync(id);
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void cbTinhhaomon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("quanlytaisan.db");
            var query = await conn.QueryAsync<tinhhaomon>("SELECT Thoigiansudung, Tylehaomon FROM tinhhaomon WHERE Nhomtaisan LIKE '" + cbTinhhaomon.SelectedValue.ToString() + "'");
            thoigiansudung = query.ElementAt(0).Thoigiansudung;
            _tylehaomon = query.ElementAt(0).Tylehaomon;
        }
    }
}
