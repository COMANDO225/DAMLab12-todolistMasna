using Lab12TODOLIST.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab12TODOLIST.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEditPage : ContentPage
    {
        public CreateEditPage(CreateEditViewModel createEditViewModel)
        {
            InitializeComponent();

            BindingContext = createEditViewModel;
            Console.WriteLine("CreateEditPage iniciada con tarea: ");

        }
    }
}