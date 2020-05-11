using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMWPFUserInterface.Helper;

namespace TRMWPFUserInterface.ViewModels
{
    public class LoginUserViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;

        public LoginUserViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        public bool CanLogIn(string userName, string password)
        {
            bool output = false;
            //   if (userName?.Length > 0 && password?.Length > 0)
            if (!String.IsNullOrWhiteSpace(userName) && !String.IsNullOrWhiteSpace(password))
            {
                output = true;
            }
            return output;

        }
        public async Task LogIn(string userName, string password)
        {
            try
            {
                var result = await _apiHelper.Authenticate(userName, password);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
