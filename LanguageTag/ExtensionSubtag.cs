﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbbyyLS.Globalization
{
	public struct ExtensionSubtag : IEquatable<ExtensionSubtag>
	{
		public static readonly List<string> _empty = new List<string>(0);

		private List<string> _sequence;

		public Char Singleton { get; private set; }

		public IEnumerable<string> Sequence
		{
			get
			{
				return (_sequence ?? _empty).AsEnumerable();
			}
		}

		public ExtensionSubtag(Char singleton, string firstSubtag)
			:this()
		{
			Singleton = singleton;
			_sequence = new List<string>();
			_sequence.Add(firstSubtag);
		}

		public ExtensionSubtag(Char singleton, params string[] subtags)
			: this()
		{
			Singleton = singleton;
			_sequence = new List<string>();
			_sequence.AddRange(subtags);
		}

		internal void Append(string subtag)
		{
			_sequence.Add(subtag);
		}

		public bool Equals(ExtensionSubtag other)
		{
			return Singleton == other.Singleton &&
				_sequence.IsEquivalent(other._sequence);
		}

		public override int GetHashCode()
		{
			return Singleton.GetHashCode() ^ Sequence.GetHashCodeOfSequence();
		}

		public override bool Equals(object obj)
		{
			return obj is ExtensionSubtag &&
				Equals((ExtensionSubtag)obj);
		}

		public static bool operator ==(ExtensionSubtag x, ExtensionSubtag y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(ExtensionSubtag x, ExtensionSubtag y)
		{
			return !(x == y);
		}

		public IEnumerable<string> SubtagElements()
		{
			yield return Singleton.ToString();

			foreach (var el in Sequence)
				yield return el;
		}

		public override string ToString()
		{
			return string.Join(LanguageTag.TagSeparator.ToString(), SubtagElements());
		}

		public static readonly IComparer<ExtensionSubtag> SingletonComparer = new SingletonComparerImpl();

		private class SingletonComparerImpl: IComparer<ExtensionSubtag>
		{
			public int Compare(ExtensionSubtag x, ExtensionSubtag y)
			{
				return x.Singleton.CompareTo(y.Singleton);
			}
		}
	}
}
