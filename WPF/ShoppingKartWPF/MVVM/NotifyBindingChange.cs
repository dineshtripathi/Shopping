#region Copyright
// <copyright file="NotifyBindingChange.cs" company="Open Energi">
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
using System.ComponentModel;
namespace ShoppingKartWPF.MVVM
{
	

	public class NotifyBindingChange :INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
	}
}