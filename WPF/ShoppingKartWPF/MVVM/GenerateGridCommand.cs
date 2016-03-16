#region Copyright
// <copyright file="GenerateGridCommand.cs" company="Open Energi">
// ShoppingKart - [ShoppingKart.WPF]
// Copyright (c) 2012 All Right Reserved, http://openenergi.com/
// </copyright>
// 
// <author>Dinesh Tripathi</author>
// <date>2016-03-15</date>
// <summary>
// 
// </summary>
#endregion
using System;
using System.Windows.Input;
namespace ShoppingKartWPF.MVVM
{
	
	public class GenerateGridCommand:ICommand
	{
		public bool CanExecute(object parameter)
		{
			return false;
		}

		public void Execute(object parameter)
		{
		}

		public event EventHandler CanExecuteChanged;
	}
}