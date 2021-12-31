using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using EpamDashkevichLab9.Services;

namespace EpamDashkevichLab9.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DialogService _Dialog;
        private TextFileService _FileService;
        private string _Document;

        private string _Status;
        public string Document
        {
            get
            {
                return _Document;
            }
            set
            {
                _Document = value;
                OnPropertyChanged();
                Status = "Warning! Changes are not saved";
            }
        }
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            OpenFile = new Command(OnOpenFileExecuted, CanOpenFileExecute);
            SaveFile = new Command(OnSaveFileExecuted, CanSaveFileExecute);
            SaveFileAs = new Command(OnSaveFileAsExecuted, CanSaveFileAsExecute);
            _Dialog = new DialogService();
            _FileService = new TextFileService();
        }

        public ICommand OpenFile { get; }
        public void OnOpenFileExecuted(object parameter)
        {
            try
            {
                if (_Dialog.OpenFile() == true)
                {
                    Document = _FileService.Open(_Dialog.FilePath);
                    Status = $"File opened: {_Dialog.FilePath}";
                }
            }
            catch (Exception e)
            {
                _Dialog.ShowMessage(e.Message);
            }            
        }
        public bool CanOpenFileExecute(object parameter) => true;


        public ICommand SaveFile { get; }
        public void OnSaveFileExecuted(object parameter)
        {
            try
            {
                if (_Dialog.SaveFile() == true)
                {
                    _FileService.Save(_Dialog.FilePath, Document);
                    Status = $"File saved: {_Dialog.FilePath}";
                }
            }
            catch (Exception e)
            {
                _Dialog.ShowMessage(e.Message);
            }
        }
        public bool CanSaveFileExecute(object parameter) => true;


        public ICommand SaveFileAs { get; }
        public void OnSaveFileAsExecuted(object parameter)
        {
            try
            {
                if (_Dialog.SaveFileAs() == true)
                {
                    _FileService.Save(_Dialog.FilePath, Document);
                    Status = $"File saved: {_Dialog.FilePath}";
                }
            }
            catch (Exception e)
            {
                _Dialog.ShowMessage(e.Message);
            }
        }
        public bool CanSaveFileAsExecute(object parameter) => true;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
