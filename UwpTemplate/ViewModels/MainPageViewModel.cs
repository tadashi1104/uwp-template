using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace UwpTemplate.ViewModels
{
    public class MainPageViewModel : Helpers.Observable
    {

        public Views.MainPage View { get; private set; } = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainPageViewModel()
        {

        }

        public void Initialize(Views.MainPage mainPage)
        {
            View = mainPage;
        }

        #region Id
        public string Id
        {
            get => this.Id_;
            set => Set(ref this.Id_, value, nameof(Id));
        }
        private string Id_;
        #endregion Id

        #region List
        public List<string> List { get; private set; } = new List<string> { "value 1", "value 2" };

        public string Value
        {
            get => this.Value_;
            set
            {
                Set(ref this.Value_, value, nameof(Value));
            }
        }
        private string Value_;
        #endregion Value


        #region Output
        public string Output
        {
            get => this.Output_;
            set => Set(ref this.Output_, value, nameof(Output));
        }
        private string Output_;
        #endregion Output

        private Helpers.RelayCommand OutputTextCommand_;
        public Helpers.RelayCommand OutputTextCommand
        {
            get { return OutputTextCommand_ = OutputTextCommand_ ?? new Helpers.RelayCommand(OutputText); }
        }

        private void OutputText()
        {
            // API呼び出し
            //try
            //{
            //    //string url = $@"https://example.com";
            //    //var responce = await new Helpers.ApiSend().SendGet<List<Models.Example>>(url);
            //}
            //catch (Exception ex)
            //{
            //    var msg = new MessageDialog(ex.Message);
            //    await msg.ShowAsync();
            //}

            this.Output = $@"{this.Id} : {this.Value}";
        }

    }
}
