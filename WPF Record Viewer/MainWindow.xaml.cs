using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections;


namespace WPFrecordViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

            
            ArrayList listOfPerson = new ArrayList();

            foreach (string line in File.ReadLines(@"C:\\temp\\oscourse\\360-p6.txt"))
            {
                listOfPerson.Add(line);
            }

           
            foreach (string i in listOfPerson)
            {   
               
                string[] item = i.Split(',');

                string name = item[0].Trim('"').Replace(@"\", string.Empty);
                string name1 = item[1].Trim('"').Replace(@"\", string.Empty);
                name = name + name1;
                int first = int.Parse(item[2]);
                int second = int.Parse(item[3]);
                int third = int.Parse(item[4]);

                dataGrid.Items.Add(new { Name = name, Number1 = first, Number2 = second, Number3 = third });
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Items.Add(dataGrid.Items);
        }
    }
}
