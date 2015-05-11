using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTaiSan.Models
{
    [Table("tinhhaomon")]
    public class tinhhaomon
    {
        private int id;
        private string nhomtaisan;
        private int thoigiansudung;
        private double tylehaomon;

        public double Tylehaomon
        {
            get { return tylehaomon; }
            set { tylehaomon = value; }
        }

        public int Thoigiansudung
        {
            get { return thoigiansudung; }
            set { thoigiansudung = value; }
        }

        public string Nhomtaisan
        {
            get { return nhomtaisan; }
            set { nhomtaisan = value; }
        }
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
