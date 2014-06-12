/*
 * Created by SharpDevelop.
 * User: Ihatenames
 * Date: 12.06.2014
 * Time: 12:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OmgUtils.UserInteraction
{
	/// <summary>
	/// Methods to simplify communication with an application user.
	/// </summary>
	public class UserInteractionUtils
	{
		/// <summary>
		/// Called when the user has finished or aborted entering a string. Used by <see cref="AskUserToEnterString"/>
		/// If user aborted, result is cancel, else ok
		/// </summary>
		public delegate void UserFinishedEnteringStringDelegate(TextBox boxContainingText, MessageBoxResult result);
		
		
		/// <summary>
		/// Displays a message box with an OK button and an error image
		/// </summary>
		/// <param name="message">The message to be shown inside the box</param>
		/// <param name="caption">Caption of the error window</param>
		public static void ShowErrorMessageBox(string message, string caption = "Error")
		{
			MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
		}
		
		/// <summary>
		/// Displays a message box with an OK button and an exclamation image
		/// </summary>
		/// <param name="message">Message to be displayed inside the box</param>
		/// <param name="caption">Caption of the message box window</param>
		public static void ShowInfoMessageBox(string message, string caption = "Notice")
		{
			MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
		}
		
		/// <summary>
		/// Displays a new window with a richtextbox containing the specified string
		/// </summary>
		/// <param name="content">Text to be displayed</param>
		/// <param name="title">Title of the window</param>
		public static void DisplayRichTextBox(string content, string title = "Result") // LOCALIZE:
		{
			RichTextBox box = new RichTextBox();
			
			box.AppendText(content);
			box.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			box.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
			
			Window window = new Window();
			
			window.Content = box;
			window.Title = title;
			window.Width = 400;
			window.Height = 200;
			window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			
			window.Show();
		}
		
		/// <summary>
		/// Displays a textbox that asks the user to enter a string.
		/// </summary>
		/// <param name="windowTitle">The title of the window</param>
		/// <param name="sMessage">The message to be displayed</param>
		/// <param name="callbackOnFinished">Called once the user finished or abortet entering</param>
		/// <param name="sLocalizedOK">Text that is displayed on the OK button</param>
		/// <param name="sLocalizedCancel">Text that is displayed on the Cancel button</param>
		/// <param name="sPredefinedText">Initial text that the textbox should contain</param>
		public static void	AskUserToEnterString(string windowTitle, string sMessage, UserFinishedEnteringStringDelegate callbackOnFinished, string sLocalizedOK = "OK", string sLocalizedCancel = "Cancel", string sPredefinedText = "")
			
		{
			var window = new Window();
			
			window.Title = windowTitle;
			window.ResizeMode = ResizeMode.NoResize;
			window.SizeToContent = SizeToContent.WidthAndHeight;
			
			var box = new TextBox();
			box.Width = 300;
			box.Text = sPredefinedText;
			box.SelectAll();
			box.KeyDown += delegate(object sender, KeyEventArgs e)
			{
				if (e.Key == Key.Enter)
				{
					window.Close();
					callbackOnFinished(box, MessageBoxResult.OK);
					
				}
				else if (e.Key == Key.Escape)
				{
					window.Close();
					callbackOnFinished(box, MessageBoxResult.Cancel);
					
				}
			};
			
			var buttonOK = new Button();
			buttonOK.HorizontalAlignment = HorizontalAlignment.Stretch;
			buttonOK.Content = sLocalizedOK;
			buttonOK.Click += delegate
			{
				window.Close();
				callbackOnFinished(box, MessageBoxResult.OK);
				
			};
			
			var buttonCancel = new Button();
			buttonCancel.Content = sLocalizedCancel;
			buttonCancel.Click += delegate
			{
				window.Close();
				callbackOnFinished(box, MessageBoxResult.Cancel);
				
			};
			
			var messageLabel = new Label();
			messageLabel.Content = sMessage;
			
			Grid grid = new Grid();
			var rowDef = new RowDefinition();
			rowDef.Height = new GridLength(50, GridUnitType.Star);
			grid.RowDefinitions.Add(rowDef);
			rowDef = new RowDefinition();
			rowDef.Height = new GridLength(50, GridUnitType.Star);
			grid.RowDefinitions.Add(rowDef);
			rowDef = new RowDefinition();
			rowDef.Height = new GridLength(50, GridUnitType.Star);
			grid.RowDefinitions.Add(rowDef);
			
			
			var columnDef = new ColumnDefinition();
			columnDef.Width = new GridLength(50, GridUnitType.Star);
			grid.ColumnDefinitions.Add(columnDef);
			columnDef = new ColumnDefinition();
			columnDef.Width = new GridLength(50, GridUnitType.Star);
			grid.ColumnDefinitions.Add(columnDef);
			
			grid.Children.Add(box);
			grid.Children.Add(buttonOK);
			grid.Children.Add(buttonCancel);
			grid.Children.Add(messageLabel);
			
			messageLabel.SetValue(Grid.RowProperty, 0);
			messageLabel.SetValue(Grid.ColumnSpanProperty, 2);
			
			box.SetValue(Grid.RowProperty, 1);
			box.SetValue(Grid.ColumnSpanProperty, 2);
			
			buttonOK.SetValue(Grid.RowProperty, 2);
			buttonOK.SetValue(Grid.ColumnProperty, 0);
			
			buttonCancel.SetValue(Grid.RowProperty, 2);
			buttonCancel.SetValue(Grid.ColumnProperty, 1);
			
			window.Content = grid;
			window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			box.Focus();
			window.ShowDialog();
			
		}
		
		
		
	}
}