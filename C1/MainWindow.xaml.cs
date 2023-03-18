using C1.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace C1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PE_PRN_Fall22B1Context _context;

        List<Star> ListStars = new List<Star>();
        public class Country
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
        public MainWindow(PE_PRN_Fall22B1Context context)
        {
            _context = context;
            InitializeComponent();
            nationality.ItemsSource = new List<Country>()
            {
                new Country { Name = "Vietnam", Code = "VN" },
                new Country { Name = "United States", Code = "US" },
                new Country { Name = "Japan", Code = "JP" }
            };
        }
        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }
        private Star GetProductObject()
        {

            Star star = new Star
            {
                FullName = starName.Text,
                Male = male.IsChecked == true ? true : false,
                Dob = dob.SelectedDate,
                Description = StringFromRichTextBox(description),
                Nationality = nationality.Text
            };
            return star;
        }


        private void UpdateGridView()
        {
            lvStars.ItemsSource = null;
            lvStars.ItemsSource = ListStars;
        }

        private void Btn_add(object sender, RoutedEventArgs e)
        {
            ListStars.Add(GetProductObject());
            UpdateGridView();
        }

        private void Btn_import(object sender, RoutedEventArgs e)
        {
            // Create an OpenFileDialog to allow the user to select a JSON file
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                // Get the path of the selected JSON file
                string jsonFilePath = openFileDialog.FileName;

                // Read the JSON file and deserialize it into a list of objects
                string json = File.ReadAllText(jsonFilePath);
                List<Star> myObjects = JsonConvert.DeserializeObject<List<Star>>(json);
                ListStars.AddRange(myObjects);
                UpdateGridView();
                // Add code here to process the deserialized objects
            }
        }

        private void Btn_save(object sender, RoutedEventArgs e)
        {
            _context.Stars.AddRange(ListStars);
            if (_context.SaveChanges() > 0)
            {
                MessageBox.Show("Insert Success");
                ListStars.Clear();
                UpdateGridView();
            }
        }
    }
}
