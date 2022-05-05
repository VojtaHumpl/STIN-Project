﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace ChatBot {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		TCPServer server = new();

		public MainWindow() {
			InitializeComponent();
			server.OnMessageReceived += Server_OnMessageReceived;
		}

		private void Server_OnMessageReceived(object? sender, EventArgs e) {
			var data = (MessageEventArgs)e;
			var message = ProtocolParser.ParsePacket(data.Data);
			Dispatcher.BeginInvoke((Action)(() => listView.Items.Add(message)));
			server.StopClient();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

			//Task.Run(() => server.StartClient("40.122.30.213", 6969));
			//Task.Run(() => server.StartClient("127.0.0.1", 6969));
			
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			string txt = textBoxInput.Text;
			//server.StartClient("127.0.0.1", 6969);
			/*Task.Run(() => {
				server.StartClient("40.122.30.213", 6969);
				while ()
			});*/
			var res = server.StartClient("40.122.30.213", 6969);
			if (res) {
				server.SendData(ProtocolParser.CreatePacket(txt));
				listView.Items.Add(txt);
			} else
				listView.Items.Add("Failed to send");
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			server.Stop();
		}
	}
}
