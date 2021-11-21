using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskList.Models;
using Xamarin.Forms;

namespace TaskList.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        #region Atributes
        private string _descriptionForm;
        private bool _isCompleteForm = false;
        private bool _isVisibleDelete = false;
        private object _listViewSource;
        private ToDo _toDoSelected;
        private int _idToDo = 0;
        private string _insertUpdateButton = "Crear";
        #endregion

        #region Properties
        public string DescriptionForm
        {
            get => _descriptionForm;
            set => SetProperty(ref _descriptionForm, value);
        }

        public bool IsCompleteForm
        {
            get => _isCompleteForm;
            set => SetProperty(ref _isCompleteForm, value);
        }

        public bool IsVisibleDelete
        {
            get => _isVisibleDelete;
            set => SetProperty(ref _isVisibleDelete, value);
        }

        public object ListViewSource
        {
            get => _listViewSource;
            set => SetProperty(ref _listViewSource, value);
        }

        public ToDo ToDoSelected
        {
            get => _toDoSelected;
            set
            {
                if (_toDoSelected != value)
                {
                    SetProperty(ref _toDoSelected, value);
                    ChargeToDo();
                }
            }
        }

        public int IdToDo
        {
            get => _idToDo;
            set => SetProperty(ref _idToDo, value);
        }

        public string InsertUpdateButton
        {
            get => _insertUpdateButton;
            set => SetProperty(ref _insertUpdateButton, value);
        }

        public DelegateCommand InsertUpdateToDoCommand { get; set; }

        public DelegateCommand DeleteToDoCommand { get; set; }
        #endregion

        #region Methods
        public async void LoadData()
        {
            ListViewSource = await App.Database.GetToDoAsync();
        }

        public async void InsertUpdateToDo()
        {
            if (!string.IsNullOrWhiteSpace(_descriptionForm))
            {
                if (_idToDo != 0)
                {
                    ToDo toDo = new ToDo
                    {
                        Id = _toDoSelected.Id,
                        Description = _descriptionForm,
                        IsComplete = _isCompleteForm
                    };

                    await App.Database.SaveUpdateToDoAsync(toDo);

                    IdToDo = 0;
                    IsVisibleDelete = false;
                }
                else
                {
                    ToDo toDo = new ToDo
                    {
                        Description = _descriptionForm,
                        IsComplete = _isCompleteForm
                    };

                    await App.Database.SaveUpdateToDoAsync(toDo);
                }

                DescriptionForm = "";
                IsCompleteForm = false;
                InsertUpdateButton = "Crear";

                LoadData();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay tarea para registrar", "Ok");
            }
        }

        public async void DeleteToDo()
        {
            await App.Database.DeleteToDoAsync(_toDoSelected);

            DescriptionForm = "";
            IsCompleteForm = false;
            IdToDo = 0;
            IsVisibleDelete = false;
            InsertUpdateButton = "Crear";

            LoadData();
        }

        public void ChargeToDo()
        {
            DescriptionForm = _toDoSelected.Description;
            IsCompleteForm = _toDoSelected.IsComplete;
            IdToDo = _toDoSelected.Id;
            IsVisibleDelete = true;
            InsertUpdateButton = "Editar";
        }
        #endregion

        public IndexViewModel()
        {
            LoadData();

            InsertUpdateToDoCommand = new DelegateCommand(InsertUpdateToDo);
            DeleteToDoCommand = new DelegateCommand(DeleteToDo);
        }
    }
}
