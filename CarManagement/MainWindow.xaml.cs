using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows;

namespace CarManagement
{
  public partial class MainWindow : Window
  { 
	StreamWriter writeLog = new StreamWriter (@Environment.GetFolderPath (Environment.SpecialFolder.Desktop).ToString()+"log.txt");
	private void MainWindows_Loaded(object sender, RoutedEventArgs e)
	{
	  MessageBoxResult result;
	  ComboBoxAnalizator.SelectedItem = ComboBoxArduino.SelectedItem = null; 
	  ComboBoxArduino.Text = ComboBoxAnalizator.Text; 
	  if (PortsTextbox.Text != null)
		PortsTextbox.Text = null;
	  if (GetCOMs ().Count != 0)
	  {
		foreach (string PortName in GetCOMs ())
		{
		  PortsTextbox.Text += PortName;
		  ComboBoxAnalizator.Items.Add (PortName);
		  ComboBoxArduino.Items.Add (PortName);
		}
	  }
	  else
		result = MessageBox.Show ("Nu e conectat nici un dispozitiv", "Porturi libere", MessageBoxButton.OK , MessageBoxImage.Warning);
	}

	private void FormChange()
	{
	  MessageBoxResult result;
	  ComboBoxAnalizator.SelectedItem = ComboBoxArduino.SelectedItem = null;
	  ComboBoxArduino.Text = ComboBoxAnalizator.Text;
	  if (PortsTextbox.Text != null)
		PortsTextbox.Text = null;
	  if (GetCOMs ().Count != 0)
	  {
		foreach (string PortName in GetCOMs ())
		{
		  PortsTextbox.Text += PortName;
		  ComboBoxAnalizator.Items.Add (PortName);
		  ComboBoxArduino.Items.Add (PortName);
		}
	  }
	  else
		result = MessageBox.Show ("Nu e conectat nici un dispozitiv" , "Porturi libere" , MessageBoxButton.OK , MessageBoxImage.Warning);
	}

	private List<string> GetCOMs()
	{
	  SerialPort newSerial = new SerialPort();
	  List<string> ports = new List<string>();
	  foreach (var s in SerialPort.GetPortNames ())
	  {
		ports.Add (s);
	  }	  
	  return ports;						
	}

	public MainWindow()
	{
	  InitializeComponent ();
	  Loaded += MainWindows_Loaded;
	  while (MainForm.IsVisible)
	  {
		FormChange ();
	  }
	}

	private void ButtonExit_Click(object sender , RoutedEventArgs e)
	{
	  MainForm.Close ();
	}

	private void ButtonContinuare_Click(object sender , RoutedEventArgs e)
	{ 
	  Window1 newDetailsWindows = new Window1 ();
	  newDetailsWindows.Show ();
	  SerialPort arduinoPort = new SerialPort (ComboBoxArduino.SelectedValue.ToString(), 9600);
	  SerialPort analizatorPort = new SerialPort (ComboBoxAnalizator.SelectedValue.ToString () , 9600);
	  writeLog.WriteLine (DateTime.Now.ToString ("mm-DD-yyyy HH:mm:ss") + "User choosed arduino port: " + ComboBoxArduino.SelectedValue);
	  writeLog.WriteLine (DateTime.Now.ToString ("mm-DD-yyyy HH:mm:ss") + "User choosed analizator port: " + ComboBoxAnalizator.SelectedValue);
	  try
	  {
		arduinoPort.Open ();
	  }
	  catch (IOException artuinoException)
	  {
		writeLog.WriteLine (DateTime.Now.ToString ("mm-DD-yyyy HH:mm:ss") + "Arduino Port Open Operation" + artuinoException.ToString()); 
	  }
	 // try
	 // {
		//analizatorPort.Open ();
	 // }
	 // catch (IOException analizatorException)
	 // {
		//writeLog.WriteLine (DateTime.Now.ToString ("mm-DD-yyyy HH:mm:ss") + "Analizator Port Open Operation" + analizatorException.ToString ());
	 // }



	}
  }
}
