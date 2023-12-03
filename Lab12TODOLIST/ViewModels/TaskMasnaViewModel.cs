using Lab12TODOLIST.Models;
using Lab12TODOLIST.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab12TODOLIST.ViewModels
{
    internal class TaskMasnaViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ApiService apiService = new ApiService();
        private ObservableCollection<TaskMasna> tasks;

        public ObservableCollection<TaskMasna> Tasks
        {
            get => tasks;
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public TaskMasnaViewModel()
        {
            Tasks = new ObservableCollection<TaskMasna>();
            LoadTasks();
        }

        public async Task ReloadTasks()
        {
            await LoadTasks();
        }

        public async Task LoadTasks()
        {
            try
            {
                var taskData = await apiService.GetTasksAsync();
                Tasks.Clear();
                foreach (var task in taskData)
                {
                    Tasks.Add(task);
                }
            }
            catch (Exception error) 
            { 
                Console.WriteLine(error.Message);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
