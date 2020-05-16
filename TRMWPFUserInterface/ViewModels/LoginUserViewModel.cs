using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DesktopUILibrary.Api;
using TRMWPFUserInterface.EventModels;
using TRMWPFUserInterface.Helper;

namespace TRMWPFUserInterface.ViewModels
{
    public class LoginUserViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public LoginUserViewModel(IAPIHelper apiHelper,IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
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


        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }

        }
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }


        public bool CanLogIn//(string userName, string password)
        {
            get
            {
                bool output = false;
                //   if (userName?.Length > 0 && password?.Length > 0)
                if (!String.IsNullOrWhiteSpace(UserName) && !String.IsNullOrWhiteSpace(Password))
                {
                    output = true;
                }
                return output;
            }
        }
        public async Task LogIn()
        {
            try
            {
                var result = await _apiHelper.Authenticate(UserName, Password);
                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);
                _events.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

    }
}
