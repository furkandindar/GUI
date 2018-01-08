using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class DataContent
    {
        public int line_number { get; set; }
        public string content { get; set; }
        public string suffix { get; set; }
    }
    public partial class MainWindow : Window
    {
        string fileName = "";
        Boolean file_exist = false;
        int file_counter = 0;
        ObservableCollection<DataContent> rows = new ObservableCollection<DataContent>();
        int scrollPos;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var parameter = textBox.Text;
            string[] parameters = parameter.Split(' ');
            if (file_exist)
            {
                StreamWriter outputFile = new StreamWriter(fileName);
                for (int i=1;i<rows.Count()-1;i++)
                {
                    outputFile.WriteLine(rows[i].content);
                }
                outputFile.Close();
                MessageBox.Show("File saved Successfully!","File Saving");
            }
            else
            {
                MessageBox.Show("You should open a file first !","File Saving Error!");
            }
        }

        private void save_as_Click(object sender, RoutedEventArgs e)
        {
            
            var parameter = textBox.Text;
            string[] parameters = parameter.Split(' ');
            if (!file_exist)
            {
                MessageBox.Show("You should open a file first !", "File Saving Error!");
            }
            else
            {
                string temp = Microsoft.VisualBasic.Interaction.InputBox("Give a name for your file", "Save As", "");
                StreamWriter outputFile = new StreamWriter(temp);
                for (int i = 1; i < rows.Count()-1; i++)
                {
                    outputFile.WriteLine(rows[i].content);
                }
                outputFile.Close();
                MessageBox.Show("File saved Successfully!", "File Saving");
            }
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome to Furkan Dindar's Text Editor Help Center!\n\nparameters tells you that what should you write in order to execute command in command line\nprequisite tells you that what you should do before using that command\n# sign tells you that you need to use a number\n\nOPEN: parameters: file path - prequisite : no\nSAVE: parameters: no - prequisite: open a file\nSAVE AS: parameters: new file name - prequisite: open a file\nDOWN: parameters: d # or down # - prequisite: open a file\nUP: parameters: u # or up # - prequisite: open a file\n#: parameters: # - prequisite: open a file\nLEFT: parameters: l # or left # - prequisite: open a file\nRIGHT: parameters r # or right # - prequisite: open a file\nFORWARD: parameters: f or forward - prequisite: open a file\nBACKWARD: parameters: b or back - prequisite: open a file\nSETCL: parameters: setcl # - prequisite: open a file\nSEARCH: parameters: s or search /string/#_lines_from_current_line - prequisite: open a file\nCHANGE: parameters: c or change /string_in_file/your_string/#_lines_from_current_line - prequisite: open a file", "Help Center");
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var parameter = textBox.Text;
                string[] parameters = parameter.Split(' ');
                if (parameters[0].ToLower() == "h" || parameters[0].ToLower() == "help")
                {
                    MessageBox.Show("Welcome to Furkan Dindar's Text Editor Help Center!\n\nparameters tells you that what should you write in order to execute command in command line\nprequisite tells you that what you should do before using that command\n# sign tells you that you need to use a number\n\nOPEN: parameters: file path - prequisite : no\nSAVE: parameters: no - prequisite: open a file\nSAVE AS: parameters: new file name - prequisite: open a file\nDOWN: parameters: d # or down # - prequisite: open a file\nUP: parameters: u # or up # - prequisite: open a file\n#: parameters: # - prequisite: open a file\nLEFT: parameters: l # or left # - prequisite: open a file\nRIGHT: parameters r # or right # - prequisite: open a file\nFORWARD: parameters: f or forward - prequisite: open a file\nBACKWARD: parameters: b or back - prequisite: open a file\nSETCL: parameters: setcl # - prequisite: open a file\nSEARCH: parameters: s or search /string/#_lines_from_current_line - prequisite: open a file\nCHANGE: parameters: c or change /string_in_file/your_string/#_lines_from_current_line - prequisite: open a file", "Help Center");
                }
                else if(parameters[0].ToLower() == "o" || parameters[0].ToLower() == "open")
                {
                    try
                    {
                        rows.Clear();
                        int line_counter = 0;
                        fileName = parameters[1];
                        scrollPos = 1;
                        StreamReader myFile = new StreamReader(fileName);
                        file_exist = true;
                        label.Content = "File Name: " + fileName;
                        label1.Content = "Current Line: " + 1;
                        string line;
                       rows.Add(new DataContent {content = "=============top of file===========" , suffix="====="});
                        while ((line = myFile.ReadLine()) != null)
                        {
                            line_counter = line_counter + 1;
                            rows.Add(new DataContent { line_number = line_counter, content = line, suffix = "=====" });
                        }
                        rows.Add(new DataContent { content = "=============end of file===========" ,suffix="====="});
                        dataGrid.ItemsSource = rows;

                        label2.Content = "Total Lines: " + line_counter;
                        FileInfo f = new FileInfo(fileName);
                        label5.Content = "File Size: " + f.Length + "Kb";

                        myFile.Close();
                        file_counter++;
                        label6.Content = "Files Edited: "+ file_counter;
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(SetCL));
                    }
                    catch {
                        file_exist = false;
                        label.Content = "File Name: ";
                        MessageBox.Show("File does not exist!\nCheck the file path or name again !", "File Error");
                    }
                    
                }
                else if (parameters[0].ToLower() == "save" && parameters.Count() == 1)
                {
                    if (file_exist)
                    {
                        StreamWriter outputFile = new StreamWriter(fileName);
                        for (int i = 1; i < rows.Count()-1; i++)
                        {
                            outputFile.WriteLine(rows[i].content);
                        }
                        outputFile.Close();
                        MessageBox.Show("File saved Successfully!", "File Saving");
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first !", "File Saving Error!");
                    }
                }
                else if (parameters.Count() == 3 && parameters[0]=="save" && parameters[1]=="as")
                {
                    if (file_exist)
                    {
                        StreamWriter outputFile = new StreamWriter(parameters[2]);
                        for (int i = 1; i < rows.Count()-1; i++)
                        {
                            outputFile.WriteLine(rows[i].content);
                        }
                        outputFile.Close();
                        MessageBox.Show("File saved Successfully!", "File Saving");
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first !", "File Saving Error!");
                    }
                }
                else if((parameters[0].ToLower() == "d" || parameters[0].ToLower() == "down") && parameters.Count() == 2)
                {
                    if (file_exist)
                    {
                        scrollViewer.ScrollToBottom();
                        int scroll_size = scrollPos + Int32.Parse(parameters[1]);

                        if(scroll_size > rows.Count() - 1)
                        {
                            scrollPos = rows.Count() - 1; 
                        }
                        else
                        {
                            scrollPos = scroll_size;
                        }
                        dataGrid.SelectedItem = dataGrid.Items[scrollPos];
                        dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                    
                }
                else if((parameters[0].ToLower() == "u" || parameters[0].ToLower() == "up") && parameters.Count() == 2)
                {
                    if (file_exist)
                    {
                        scrollViewer.ScrollToBottom();
                        int scroll_size = scrollPos - Int32.Parse(parameters[1]);

                        if (scroll_size < 1)
                        {
                            scrollPos = 0;
                        }
                        else
                        {
                            scrollPos = scroll_size;
                        }
                        dataGrid.SelectedItem = dataGrid.Items[scrollPos];
                        dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                }
                else if(parameters.Count() == 1 && !parameters[0].Contains("/") && (parameters[0].Contains("1") || parameters[0].Contains("2") || parameters[0].Contains("3") || parameters[0].Contains("4") || parameters[0].Contains("5") || parameters[0].Contains("6") || parameters[0].Contains("7") || parameters[0].Contains("8") || parameters[0].Contains("9")))
                {
                    if (file_exist)
                    {
                        scrollViewer.ScrollToBottom();
                        int scroll_size = Int32.Parse(parameters[0]);

                        for (int i=0; i < rows.Count(); i++)
                        {
                            if(rows[i].line_number == scroll_size)
                            {
                                scrollPos = i;
                                dataGrid.SelectedItem = dataGrid.Items[scrollPos];
                                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                                break;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                }
                else if (parameters[0].ToLower() == "l" || parameters[0].ToLower() == "left")
                {
                    if (file_exist)
                    {
                        int scroll_left = Int32.Parse(parameters[1]);
                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - (scroll_left * 10));
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                }
                else if(parameters[0].ToLower() == "r" || parameters[0].ToLower() == "right")
                {
                    if (file_exist)
                    {
                        int scroll_right = Int32.Parse(parameters[1]);
                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + (scroll_right * 10));
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                }
                else if (parameters[0].ToLower() == "f" || parameters[0].ToLower() == "forward")
                {
                    if (file_exist)
                    {
                        scrollViewer.ScrollToBottom();
                        int scroll_size = scrollPos + 9;

                        if (scroll_size > rows.Count() - 1)
                        {
                            scrollPos = rows.Count() - 1;
                        }
                        else
                        {
                            scrollPos = scroll_size;
                        }
                        dataGrid.ScrollIntoView(dataGrid.Items[scrollPos]);
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                }
                else if (parameters[0].ToLower() == "b" || parameters[0].ToLower() == "back")
                {
                    if (file_exist)
                    {
                        scrollViewer.ScrollToBottom();
                        int scroll_size = scrollPos - 9;

                        if (scroll_size < 1)
                        {
                            scrollPos = 0;
                        }
                        else
                        {
                            scrollPos = scroll_size;
                        }
                        dataGrid.ScrollIntoView(dataGrid.Items[scrollPos]);
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                }
                else if (parameters[0].ToLower()=="setcl")
                {
                    int lineNum = Int32.Parse(parameters[1]);
                    scrollPos = lineNum;
                    if(scrollPos > rows.Count() - 1)
                    {
                        scrollPos = rows.Count();
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(SetCL));
                        label1.Content = "Current Line: " + scrollPos;
                        //scrollPos = 0;
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(SetCL));
                        label1.Content = "Current Line: " + scrollPos;
                        //scrollPos = 0;
                    }
                }
                else if(parameters[0].ToLower() == "s" || parameters[0] == "search")
                {
                    if (file_exist)
                    {
                        if (parameters.Count() == 2)
                        {
                            string str = parameters[1].Split('/')[1];
                            int lineNum, found_counter = 0, found_position = 0;

                            if (parameters[1].Split('/')[2] == "*")
                            {
                                lineNum = rows.Count();
                            }
                            else
                            {
                                lineNum = Int32.Parse(parameters[1].Split('/')[2]) - 1;
                            }
                            for(int i = scrollPos-1; i < lineNum; i++)
                            {
                                if (rows[i].content.Contains(str))
                                {
                                    found_position = i;
                                    found_counter++;
                                }
                            }
                            
                            if (found_counter != 0)
                            {
                                MessageBox.Show(str + " found " + found_counter + " times", "Found Window");
                            }
                            else
                            {
                                MessageBox.Show(str + " not found!", "Not found Error");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Search command Error!\nCheck Help Center!","Search Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                }
                else if(parameters[0].ToLower() == "c" || parameters[0].ToLower() == "change")
                {
                    if (file_exist)
                    {
                        if(parameters.Count() == 2)
                        {
                            string str = parameters[1].Split('/')[1];
                            string change_str = parameters[1].Split('/')[2];
                            string last;
                            int lineNum, found_counter = 0, found_position = 0;

                            if (parameters[1].Split('/')[3] == "*")
                            {
                                lineNum = rows.Count();
                            }
                            else
                            {
                                lineNum = Int32.Parse(parameters[1].Split('/')[3]) - 1;
                            }
                            for (int i = scrollPos-1; i < lineNum; i++)
                            {
                                if (rows[i].content.Contains(str))
                                {
                                    found_position = i;
                                    found_counter++;

                                    last = rows[i].content.Replace(str, change_str);
                                    rows[i].content = last;
          
                                }
                            }

                            if (found_counter != 0)
                            {
                                dataGrid.ItemsSource = rows;
                                dataGrid.Items.Refresh();
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(SetCL));
                                MessageBox.Show(str + " found " + found_counter + " times," + " changed with " + change_str, "Found Window");
                            }
                            else
                            {
                                MessageBox.Show(str + " not found!", "Not found Error");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Change command Error!\nCheck Help Center!", "Change Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You should open a file first!", "File operations Error");
                    }
                }

                else
                {
                    MessageBox.Show("You entered a wrong command, please check Help if you have any trouble", "Wrong Command");
                }
                textBox.Clear();
            }
        }

        private void SetCL()
        {
            foreach(DataContent data in dataGrid.ItemsSource)
            {
                var row = dataGrid.ItemContainerGenerator.ContainerFromItem(data) as DataGridRow;
                if(data.line_number == scrollPos)
                {
                    row.Foreground = Brushes.Red;
                }
                else
                {
                    row.Foreground = Brushes.Black;
                }
            }
        }

        private void dataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (file_exist)
            {
                int mousePosition = Convert.ToInt32(Math.Floor(e.GetPosition(dataGrid).X));
                label4.Content = "Column: " + mousePosition/8;
            }
        }

        private void RowInfo(object sender, MouseEventArgs e)
        {
            if (file_exist)
            {
                int index = dataGrid.ItemContainerGenerator.IndexFromContainer((DataGridRow)sender);
                label3.Content = "Line: " + (index + 1);
            }
           
        }

        private void dataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            int counter = 0;
            Boolean itemFound = false;
            DataGridCell d = e.OriginalSource as DataGridCell;
            if (e.Key == Key.Enter)
            {
                if (d.Foreground.ToString() == "#FFFFFFFF")
                {
                    foreach (DataContent item in dataGrid.ItemsSource)
                    {
                        if (item.suffix[0] == 'i')
                        {
                            int numOfLines = (int)Char.GetNumericValue(item.suffix[1]);
                            for (int i = counter + 1; i < (counter + numOfLines + 1); i++) rows.Insert(i, new DataContent { line_number = i, content = "", suffix = "=====" });

                            itemFound = true;
                            break;
                        }

                        counter++;
                    }
                }
            }
            if (itemFound)
            {
                UpdateColumns();
            }
        }
        private void UpdateColumns()
        {
            int counter = 0;

            for (int i = 1; i < rows.Count-1; i++)
            {
                rows[i].suffix = "=====";
                rows[i].line_number =i + 1;
                counter++;
            }

            label2.Content = "Total Lines = " + counter;
            dataGrid.ItemsSource = rows;
            dataGrid.Items.Refresh();
        }
    }
}
