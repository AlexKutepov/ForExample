// using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// кастомный словарь, делал для того,чтобы понять работу 
/// универсального параметра <T> , когда появится время - 
/// добавлю хэштаблицу
/// и работу в многопоточном режиме.
/// </summary>

namespace CustomCollections {

	class Program {

		static CustomCollection<int, string> NewListEmployers = new CustomCollection<int, string>();

		static void Main(string[] args) {

			NewListEmployers.Add(0, "Company");
			NewListEmployers.Add(1, "Company1");

			for (int i = 2; i < 10; i++) {
				NewListEmployers.Add(i, "Company" + (i + 1).ToString());
			}

			Console.WriteLine(NewListEmployers.Count);

			NewListEmployers.Add(0, "NewCompany");

			string NameComp = "";

			Console.WriteLine(NewListEmployers.TryGetValueNameById(5, out NameComp));
			Console.WriteLine(NameComp + " Test Get Value");

			for (int i = 0; i < NewListEmployers.Count; i++) {
				if (NewListEmployers.TryGetValueNameById(i, out NameComp)) {
					Console.WriteLine(NameComp);
				};

			}
			Console.WriteLine(NewListEmployers.Count);
			NewListEmployers.RemoveElementByID(0);

			Console.WriteLine(NewListEmployers.Count);

			foreach (int id in NewListEmployers) {
				Console.WriteLine(NewListEmployers.TryGetValueNameById(id, out NameComp));
				Console.WriteLine(NameComp);
			}

			Console.Read();

		}
	}

	public class CustomCollection<T_id, T_name> {

		List<T_id> id = new List<T_id>();
		List<T_name> name = new List<T_name>();
		
		public void Add(T_id Id, T_name Name) {
			if (!CheckIdOnExistForAdd(Id)) {
				id.Add(Id);
				name.Add(Name);
			}
		}

		public void RemoveElementByID(T_id Id) {
			if (CheckIdOnExistForAdd(Id)) {
				int index = IndexOfId(Id);
				id.RemoveAt(index);
				name.RemoveAt(index);
			}
		}

		public void Clear() {
			id.Clear();
			name.Clear();
		}

		public bool ContainsId(T_id Id) {
			if (CheckIdOnExistForAdd(Id)) {
				return true;
			}
			return false;
		}

		public bool ContainsName(T_name Name) {
			if (CheckNameExist(Name)) {
				return true;
			}
			return false;
		}

		public bool TryGetValueNameById(T_id Id, out T_name Name) {
			Name = default(T_name);
			if (Id == null) return false;
			if (CheckIdOnExistForAdd(Id)) {
				Name = name[IndexOfId(Id)];
				return true;
			}
			return false;
		}

		public bool TryGetValueIdByName(T_name Name, out T_id Id) {
			Id = default(T_id);
			if (Name == null) return false;
			if (CheckNameExist(Name)) {
				Id = id[FirstIndexOfName(Name)];
				return true;
			}
			return false;
		}

		public int Count { get { return id.Count; } }

		public void Reverse() {
			id.Reverse();
			name.Reverse();
		}

		bool CheckIdOnExistForAdd(T_id Id) {
			int indexId = IndexOfId(Id);
			if (indexId != -1) return true;
			return false;
		}

		bool CheckNameExist(T_name Name) {
			int indexName = FirstIndexOfName(Name);
			if (indexName != -1) return true;
			return false;
		}

		public int IndexOfId(T_id Id) {
			for (int i = 0; i < id.Count - 1; i++) {
				if (id[i].Equals(Id)) {
					return i;
				}
			}
			return -1;
		}

		public int FirstIndexOfName(T_name Name) {
			for (int i = 0; i < id.Count - 1; i++) {
				if (name[i].Equals(Name)) {
					return i;
				}
			}
			return -1;
		}

		public IEnumerator GetEnumerator() {
			return new CustomCollectionEnumerator(id);
		}

		class CustomCollectionEnumerator : IEnumerator<T_id> {

			List<T_id> id = new List<T_id>();

			int position = -1;

			public CustomCollectionEnumerator(List<T_id> Id) {
				id = Id;
			}

			public object Current {
				get {
					if (position == -1 || position >= id.Count)
						throw new InvalidOperationException();
					return id[position];
				}
			}


			public bool MoveNext() {
				if (position < id.Count - 1) {
					position++;
					return true;
				} else
					return false;
			}

			public void Reset() {
				position = -1;
			}

			public void Dispose() {

			}

			T_id IEnumerator<T_id>.Current => throw new NotImplementedException();

		}
	}
}