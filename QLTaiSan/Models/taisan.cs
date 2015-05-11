using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.ComponentModel;

namespace QLTaiSan.Models
{
    [Table("taisan")]
    public class taisan : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private DateTime _datebuy;
        private string _price;
        private string _brand;
        private string _store;
        private double _priceSauKhauHao;
        private string _loaitaisan;
        private int _timeuse;
        private double _tylehaomon;

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { 
            get{ return _id;}
            set { _id = value;
            NotifyPropertyChanged("id");
            }
        }
        public int timeuse
        {
            get { return _timeuse; }
            set
            {
                _timeuse = value;
                NotifyPropertyChanged("timeuse");
            }
        }
        public double tylehaomon
        {
            get { return _tylehaomon; }
            set
            {
                _tylehaomon = value;
                NotifyPropertyChanged("tylehaomon");
            }
        }
        public string loaitaisan
        {
            get { return _loaitaisan; }
            set
            {
                _loaitaisan = value;
                NotifyPropertyChanged("loaitaisan");
            }
        }
        public string name {
            get { return _name; }
            set { _name = value;
            NotifyPropertyChanged("name");
            }
        }

        public DateTime datebuy 
        {
            get { return _datebuy; }
            set { _datebuy = value;
            NotifyPropertyChanged("datebuy");
            }
        }

        public string price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged("price");
            }
        }

        public string brand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                NotifyPropertyChanged("brand");
            }
        }

        public string store
        {
            get { return _store; }
            set
            {
                _store = value;
                NotifyPropertyChanged("store");
            }
        }

        [SQLite.Ignore]
        public double priceSauKhauHao
        {
            get { return _priceSauKhauHao; }
            set
            {
                _priceSauKhauHao = value;
                NotifyPropertyChanged("priceSauKhauHao");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
