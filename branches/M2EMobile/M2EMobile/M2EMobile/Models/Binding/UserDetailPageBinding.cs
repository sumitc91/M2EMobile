using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace M2EMobile.Models.Binding
{
    public class UserDetailPageBinding:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string _firstname;
        public string _lastname;
        public string _reputation;
        public string _totalEarning;
        public ImageSource _userProfilePicImageSource;

        public ImageSource UserProfilePicImageSource
        {
            set
            {
                if (_userProfilePicImageSource != value)
                {
                    _userProfilePicImageSource = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("UserProfilePicImageSource"));
                    }
                }
            }
            get
            {
                return _userProfilePicImageSource;
            }
        }
        public String TotalEarning
        {
            set
            {
                if (_totalEarning != value)
                {
                    _totalEarning = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("TotalEarning"));
                    }
                }
            }
            get
            {
                return _totalEarning;
            }
        }
        public String Reputation
        {
            set
            {
                if (_reputation != value)
                {
                    _reputation = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Reputation"));
                    }
                }
            }
            get
            {
                return _reputation;
            }
        }
        public String LastName
        {
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("LastName"));
                    }
                }
            }
            get
            {
                return _lastname;
            }
        }
        public String FirstName
        {
            set
            {
                if (_firstname != value)
                {
                    _firstname = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("FirstName"));
                    }
                }
            }
            get
            {
                return _firstname;
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
