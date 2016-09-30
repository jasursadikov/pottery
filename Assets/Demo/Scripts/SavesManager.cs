using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Pottery.Demo
{
	public sealed class SavesManager : MonoBehaviour
	{
		public PotteryGenerator generator;

		public Text fileName;

		public IDictionary<string, Body> bodies;

		public Dropdown selection;

		private void Start()
		{
			LoadAll();
		}

		private void LoadAll()
		{
			bodies = new Dictionary<string, Body>();

			foreach (string path in Directory.GetFiles(Application.persistentDataPath))
			{
				Debug.Log(path);
			}
		}

		public void Load()
		{
			generator.body = bodies.ElementAt(selection.value).Value;
		}

		public void Save()
		{
			try
			{
				if (string.IsNullOrEmpty(fileName.text))
					throw new NullReferenceException();

				NetDataContractSerializer serializer = new NetDataContractSerializer();

				using (FileStream stream = File.Create(Path.Combine(Application.persistentDataPath, fileName.text + ".dat")))
					serializer.Serialize(stream, generator.body);

				LoadAll();
			}
			catch (Exception exception)
			{
				if (exception is NullReferenceException)
					Message.GetInstance().PopUp("You should write down the name.", delegate { });
				Debug.LogError(exception);
			}
		}
	}
}
