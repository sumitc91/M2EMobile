using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2EMobile.Models.Binding
{
    class UserAllTasksBinding
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string _title;
        public string _info;

        public String Title
        {
            set
            {
                if (_title != value)
                {
                    _title = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Title"));
                    }
                }
            }
            get
            {
                return _title;
            }
        }
        public String Info
        {
            set
            {
                if (_info != value)
                {
                    _info = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Info"));
                    }
                }
            }
            get
            {
                return _info;
            }
        }
        protected void FirePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
