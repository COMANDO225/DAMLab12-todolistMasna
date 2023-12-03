using Lab12TODOLIST.Models;
using Lab12TODOLIST.ViewModels;
using Lab12TODOLIST.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab12TODOLIST
{
    public partial class MainPage : ContentPage
    {
        TaskMasnaViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();

            viewModel = new TaskMasnaViewModel();

            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadTasks();
        }

        private async void onAgregarTaskClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateEditPage(new CreateEditViewModel()));
        }

        private async void onTaskSelecred(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("Evento de selección disparado");
            if (e.CurrentSelection.FirstOrDefault() is TaskMasna selectedTask)
            {
                Console.WriteLine("Tarea seleccionada: " + selectedTask.Title);
                await Navigation.PushAsync(new CreateEditPage(new CreateEditViewModel(selectedTask)));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
