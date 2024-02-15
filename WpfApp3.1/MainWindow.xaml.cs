using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using NoteApp.Models;
using NoteApp.Utilities;

namespace WpfApp3._1
{


    public partial class MainWindow : Window
    {



        private const string FilePath = "notes.json";
        private List<Note> notes;




        public ObservableCollection<string> Items { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Items = new ObservableCollection<string>();
            listBox.ItemsSource = Items;
            LoadNotes(DateTime.Today);

        }



        private void LoadNotes(DateTime date)
        {
            notes = JsonSerializer.Deserialize<Note>(FilePath)
                .Where(note => note.Date.Date == date.Date)
                .ToList();
            listBox.ItemsSource = notes.Select(note => note.Title);
        }

        private void DatePicker_SelectedDateChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DatePicker.SelectedDate.HasValue)
            {
                LoadNotes(DatePicker.SelectedDate.Value);
            }
        }


        private void NotesListBox_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                Note selectedNote = notes[listBox.SelectedIndex];
                NoteName.Text = selectedNote.Title;
                NoteDescription.Text = selectedNote.Description;
            }
        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string newItem = NoteName.Text;
            Items.Add(newItem);
            NoteName.Text = "";

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedItem != null)
            {
                string selectedItem = (string)listBox.SelectedItem;
                Items.Remove(selectedItem);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                Note selectedNote = notes[listBox.SelectedIndex];
                selectedNote.Title = NoteName.Text;
                selectedNote.Description = NoteDescription.Text;
                JsonSerializer.Serialize(notes, FilePath);
            }
        }

 
    }
}
