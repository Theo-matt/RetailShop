using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.Helpers;

namespace TRMDesktopUI.ViewModels
{

    public class LoginViewModel : Screen
    {
        private string _userName;
		private string _password;
        private IAPIHelper _apiHelper;

        public LoginViewModel(IAPIHelper aPIHelper)
        {
            _apiHelper = aPIHelper;
        }

		public string UserName
		{
			get { return _userName; }
			set 
			{
				_userName = value;
				NotifyOfPropertyChange(() => UserName);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}	

		public string Password
		{
			get { return _password; }
			set 
			{ 
				_password = value;
				NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
		}

        //public bool CanLogIn(string userName, string password)
        //{
        //	bool output = false;
        //	if (userName.Length > 0 && password.Length > 0)
        //	{
        //		output = true;
        //	}

        //	return output;
        //}

        public bool CanLogIn
        {
            get
			{
                bool output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task LogIn()
		{
            var result = await _apiHelper.Authenticate(UserName, Password);
		}


	}
}
