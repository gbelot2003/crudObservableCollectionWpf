using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFCompras2.Models;

namespace WPFCompras2.Views
{
    /// <summary>
    /// Interaction logic for PersonasEditView.xaml
    /// </summary>
    public partial class PersonasEditView : Window
    {
        //private ModelContext repo = new ModelContext();
        //private Personas innerPersonas = new Personas();
        //private int iniId;

        public PersonasEditView(Personas persona)
        {
            InitializeComponent();
            this.DataContext = persona;
            //iniId = persona.Id;
        }

        public static void getData(Personas person)
        {
            var x = new PersonasEditView(person);
            x.ShowDialog();
        }

        private void BtnEditOnClick(object sender, RoutedEventArgs e)
        {
            //innerPersonas.Id = iniId;
            //innerPersonas.FirstName = txtFirstName.Text;
            //innerPersonas.LastName = txtLastName.Text;

            //repo.Personas.AddOrUpdate(innerPersonas);
            //repo.SaveChanges();

            this.Close();
        }
    }
}