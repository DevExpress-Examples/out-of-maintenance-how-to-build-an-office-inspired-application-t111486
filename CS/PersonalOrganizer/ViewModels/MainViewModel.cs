using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace PersonalOrganizer.ViewModels {
    [POCOViewModel]
    public class MainViewModel {
        protected ICurrentWindowService CurrentWindowService { get { return this.GetService<ICurrentWindowService>(); } }
        public void Exit() {
            CurrentWindowService.Close();
        }
    }
}