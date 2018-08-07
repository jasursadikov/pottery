// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: plasticblock
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/plasticblock/Pottery)
// ----------------------------------------------------------------------- 

// NOTE: placeholder script for Demo

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PlasticBlock.Pottery.Demo
{
	public sealed class SavesManager : MonoBehaviour
	{
		[SerializeField]
		private PotteryGenerator _generator;

		[SerializeField]
		private InputField _fileName;

		[SerializeField]
		private Dropdown _selection;

		private IDictionary<string, MeshData> _bodies;

		private void Start() => LoadAll();

		private void LoadAll()
		{
			_selection.ClearOptions();

			_bodies = new Dictionary<string, MeshData>();

			foreach (string path in Directory.GetFiles(Application.persistentDataPath))
			{
				var body = JsonUtility.FromJson<MeshData>(File.ReadAllText(path));
				_bodies.Add(Path.GetFileNameWithoutExtension(path), body);
			}

			_selection.AddOptions(_bodies.Keys.ToList());
		}

		public void Load()
		{
			try
			{
				_generator.meshData = _bodies.ElementAt(_selection.value).Value;
			}
			catch
			{
				Message.Instance.PopUp("You have nothing to load");
			}
		}

		public void Delete()
		{
			try
			{
				foreach (string path in Directory.GetFiles(Application.persistentDataPath))
					if (_bodies.ElementAt(_selection.value).Key == Path.GetFileName(path).Replace(".json", ""))
					{
						File.Delete(path);
						break;
					}
			}
			catch (Exception exception)
			{
				Message.Instance.PopUp($"Oops!\n{exception.Message}");
			}

			LoadAll();
		}

		public void Save()
		{

			if (string.IsNullOrEmpty(_fileName.text))
			{
				Message.Instance.PopUp("File name is empty.");
				return;
			}

			var path = Path.Combine(Application.persistentDataPath, _fileName.text + ".json");
			File.WriteAllText(path, JsonUtility.ToJson(_generator.meshData));

			LoadAll();

			Message.Instance.PopUp("Saved successfully.");
		}
	}
}
