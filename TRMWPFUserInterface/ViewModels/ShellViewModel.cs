using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private LoginUserViewModel _LoginVM;
        public ShellViewModel(LoginUserViewModel loginVM)
        {
            _LoginVM = loginVM;
            ActivateItem(_LoginVM);
        }
    }
}
