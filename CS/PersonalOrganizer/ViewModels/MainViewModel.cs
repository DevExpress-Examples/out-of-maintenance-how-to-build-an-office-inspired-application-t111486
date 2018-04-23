using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace PersonalOrganizer.ViewModels {
    [POCOViewModel]
    public class MainViewModel {
        protected virtual ICurrentWindowService CurrentWindowService { get { return null; } }
        public void Exit() {
            CurrentWindowService.Close();
        }
    }
}