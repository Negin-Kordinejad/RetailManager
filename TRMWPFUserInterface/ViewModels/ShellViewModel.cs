﻿using Caliburn.Micro;
using DesktopUILibrary.Api;
using DesktopUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMWPFUserInterface.EventModels;

namespace TRMWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>,IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelpder;


        public ShellViewModel(IEventAggregator events,SalesViewModel salesVM,
            ILoggedInUserModel user,IAPIHelper apiHelpder)
        { 
            _events = events;
            _salesVM = salesVM;
            _user = user;
            _apiHelpder = apiHelpder;
            _events.Subscribe(this);
            ActivateItem(IoC.Get<LoginUserViewModel>());
           
        }
        public bool IsLoggedIn
        {
            get {
                bool output = false;
                if(String.IsNullOrWhiteSpace(_user.Token)==false)
                {
                    output = true;
                }
                return output; }
            
        }

        public void ExitApplication()
        {
            TryClose();
        }
        public void LogOut()
        {
            _user.ResetUserModel();
            _apiHelpder.LogOffUsser();
            ActivateItem(IoC.Get<LoginUserViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
