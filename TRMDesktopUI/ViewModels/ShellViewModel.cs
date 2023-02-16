using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Caliburn.Micro;

namespace TRMDesktopUI.ViewModels
{

    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM; 



        public ShellViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM;

        }

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await AttachLogin();
        }

        public async Task AttachLogin()
        {   
            await  ActivateItemAsync(_loginVM, new CancellationToken());    
        }

    }
}
