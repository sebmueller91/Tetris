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
using System.Windows.Shapes;

namespace Tetris
{
  /// <summary>
  /// Interaction logic for Highscores.xaml
  /// </summary>
  public partial class Highscores : Window
  {
    

    private const string filename = "Highscores.txt";
    private char[] delimiter = new char[] { ',' };
    public Highscores()
    {
      InitializeComponent();

      CreateHighscoreList();
    }

    private void CreateHighscoreList()
    {
      string[] highscores = null;
      try
      {
        highscores = ParseFile();
      } 
      catch (System.IO.FileNotFoundException)
      {
        return;
      }

      for (int i = 0; i < highscores.Length; i++)
      {
        if (i > 10)
        {
          break;
        }

        String[] namepoints = highscores[i].Split(delimiter);

        StackPanel panel = new StackPanel();
        panel.Orientation = Orientation.Horizontal;
        HIGHSCORELIST.Children.Add(panel);

        TextBlock tb1 = new TextBlock(), tb2 = new TextBlock() ;
        tb1.Text = namepoints[0] + ":\t";
        tb1.TextAlignment = TextAlignment.Left;
        tb2.Text = namepoints[1];
        tb2.TextAlignment = TextAlignment.Right;

        panel.Children.Add(tb1);
        panel.Children.Add(tb2);
      }


    }

    public String[] ParseFile()
    {
      string path = Directory.GetCurrentDirectory();
      FileStream file = new FileStream(path + filename, FileMode.Open, FileAccess.Read);

      StreamReader reader = new StreamReader(file);

      List<string> list = new List<string>();

      while (reader.Peek() != -1)
      {
        list.Add(reader.ReadLine());
      }

      reader.Close();

      return list.ToArray();
    }

    public void WriteToFile(String[] names, int[] points)
    {
      string path = Directory.GetCurrentDirectory();
      FileStream file = new FileStream(path + filename, FileMode.Create, FileAccess.Write);

      StreamWriter writer = new StreamWriter(file);

      for (int i = 0; i < names.Length; i++)
      {
        writer.WriteLine(names + "," + points);
      }

      writer.Close();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.Key == Key.Enter)
      {
        //doStuff
        //replace text in textbox with textblock
      }
    }
  }

  
}
