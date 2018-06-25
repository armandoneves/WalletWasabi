﻿using AvalonStudio.Documents;
using AvalonStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WalletWasabi.Gui
{
	public static class Utils
	{
		public static string GetNextWalletName()
		{
			for (int i = 0; i < int.MaxValue; i++)
			{
				if (!File.Exists(Path.Combine(Global.WalletsDir, $"Wallet{i}.json")))
				{
					return $"Wallet{i}";
				}
			}

			throw new NotSupportedException("This is impossible.");
		}

		public static void AddOrSelectDocument<T>(this IShell me, T document) where T : IDocumentTabViewModel
		{
			IDocumentTabViewModel doc = me.Documents.FirstOrDefault(x => x.Equals(document));

			if (doc != null)
			{
				me.SelectedDocument = doc;
			}
			else
			{
				me.AddDocument(document);
			}
		}

		public static void AddOrSelectDocument<T>(this IShell me, Func<T> factory) where T : IDocumentTabViewModel
		{
			IDocumentTabViewModel doc = me.Documents.FirstOrDefault(x => x is T);
			if (doc != default)
			{
				me.SelectedDocument = doc;
			}
			else
			{
				me.AddDocument(factory());
			}
		}

		public static T GetOrCreate<T>(this IShell me) where T : IDocumentTabViewModel, new()
		{
			T document = default;
			IDocumentTabViewModel doc = me.Documents.FirstOrDefault(x => x is T);
			if (doc != default)
			{
				document = (T)doc;
				me.SelectedDocument = doc;
			}
			else
			{
				document = new T();
				me.AddDocument(document);
			}
			return document;
		}
	}
}