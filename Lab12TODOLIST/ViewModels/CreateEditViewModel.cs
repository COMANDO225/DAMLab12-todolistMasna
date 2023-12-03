using Lab12TODOLIST.Models;
using Lab12TODOLIST.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lab12TODOLIST.ViewModels
{
    public class CreateEditViewModel: INotifyPropertyChanged
    {
        private TaskMasna currentTask;
        private ApiService apiservice;
        public bool IsEdit => !string.IsNullOrEmpty(currentTask.Id);
        public string Title
        {
            get => currentTask.Title;
            set
            {
                currentTask.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get => currentTask.Description;
            set
            {
                currentTask.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public bool IsCompleted
        {
            get => currentTask.IsCompleted;
            set
            {
                currentTask.IsCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public CreateEditViewModel(TaskMasna task = null)
        {
            apiservice = new ApiService();
            currentTask = task ?? new TaskMasna();
            SaveCommand = new Command(async () => await SaveTask());
            DeleteCommand = new Command(async () => await DeleteTask());
        }

        private async Task SaveTask()
        {
            Console.WriteLine("JSON antes de enviar: " + JsonConvert.SerializeObject(currentTask));
            try
            {
                TaskMasna resultTask = null;
                if (!string.IsNullOrEmpty(currentTask.Id))
                {
                    // aca actualizaremos el task
                    //Console.WriteLine(currentTask.Id);
                    //Console.WriteLine(currentTask.Title);
                    //Console.WriteLine(currentTask.Description);
                    //Console.WriteLine(currentTask.IsCompleted);

                    resultTask = await apiservice.UpdateTaskAsync(currentTask);
                }
                else
                {
                    Console.WriteLine("Creando tarea" + currentTask);
                    resultTask = await apiservice.CreateTaskAsync(currentTask);
                }
                if (resultTask != null)
                {
                    string message = !string.IsNullOrEmpty(currentTask.Id) ? "Task Actualizado correctamente" : "Ya se creo la tarea";
                    await Application.Current.MainPage.DisplayAlert("Un Aviso mortal", message, "Ya papi");

                    // Volver a MainPage
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                await Application.Current.MainPage.DisplayAlert("Un Aviso mortal", "Error al crear task", "Ya papi");
            }
        }

        private async Task DeleteTask()
        {
            if (!string.IsNullOrEmpty(currentTask.Id))
            {
                var result = await apiservice.DeleteTaskAsync(currentTask.Id);
                if (!string.IsNullOrEmpty(result))
                {
                    await Application.Current.MainPage.DisplayAlert("Task Eliminado", result, "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
