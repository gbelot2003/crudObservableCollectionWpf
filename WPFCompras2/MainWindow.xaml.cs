using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCompras2.Models;
using WPFCompras2.Views;

namespace WPFCompras2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModelContext context = new ModelContext();

        public MainWindow()
        {
            InitializeComponent();
            cargarGrid();
        }

        public void cargarGrid()
        {
            // Le integré la búsqueda. Ahorita está en instantáneo...
            var search = searchTxt.Text.ToUpper();

            DataGridData.ItemsSource = !string.IsNullOrWhiteSpace(search)
                ? context.Personas
                    .Where(p=>p.FirstName.ToUpper().Contains(search))
                    .OrderByDescending(x => x.Id)
                    .Take(20)
                    .ToList()
                : context.Personas
                    .OrderByDescending(x => x.Id)
                    .Take(20)
                    .ToList();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridData.SelectedItem == null) return;
            var selectedPerson = DataGridData.SelectedItem as Models.Personas;
            PersonasEditView.getData(selectedPerson);

            // Esto lo agregué para realizar el guardado aquí
            // La idea de guardar afuera es para no tener que instanciar otro contexto.
            context.SaveChanges();
        }

        private void Btn_newPersonaClick(object sender, RoutedEventArgs e)
        {
            var persona = new Personas();
            PersonasEditView.getData(persona);

            // Esto tambien es nuevo...
            context.Personas.Add(persona);
            context.SaveChanges();

            // Un DbSet no es observable, entonces hay que volver a hacer la Query.
            cargarGrid();

            /*
             * Las cosas que son observables son los árboles de datos, donde 
             * agregas datos a una lista dentro de una entidad. Por eso hubo
             * que volver a hacer Query.
             *
             * Otra forma de verlo es que, la linea:
             *     context.Personas.OrderByDescending(x => x.Id).Take(10)
             * devuelve un IQueryable (o sea, un deffered query) que en si, no
             * contiene datos, pero cuando agregas:
             *     .ToList()
             * el QUery se ejecuta y se transforma en una lista estática, una
             * instancia comun y silvestre de List<T>, que NO es observable.
             *
             * Como el resultado no es observable, es necesario refrescar la
             * información manualmente.
             *
             * Esa es otra ventaja de trabajar con MVVM... podrías implementar
             * una "propiedad" que sea tu Query, que devuelva una colección
             * observable.
             */
        }

        private void SearchTxt_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            cargarGrid();
        }
    }
}