// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Plastic Block <admin@plasticblock.xyz>
// Skype: plasticblock, email: support@plasticblock.xyz
// Project: Pottery. (https://github.com/PlasticBlock/Pottery)
// ----------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Pottery.Demo
{
	public sealed class SavesManager : MonoBehaviour
	{
		public PotteryGenerator generator;

		public InputField fileName;

		public IDictionary<string, Body> bodies;

		public Dropdown selection;

		private void Start()
		{
			LoadAll();
		}

		private void LoadAll()
		{
			selection.ClearOptions();

			bodies = new Dictionary<string, Body>();

			foreach (string path in Directory.GetFiles(Application.persistentDataPath))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Body));

				string name = Path.GetFileName(path).Replace(".dat", "");

				Body body = null;

				using (FileStream stream = File.OpenRead(path))
				{
					body = (Body) serializer.Deserialize(stream);
					bodies.Add(name, body);
				}

				body.vertices = new Vertex[body.faces, body.heightSegments];
				body.UpdateVertices();
			}
			selection.AddOptions(bodies.Keys.ToList());
		}

		public void Load()
		{
			try
			{
				generator.body = bodies.ElementAt(selection.value).Value;
			}
			catch
			{
				Message.GetInstance().PopUp("You have nothing to load", delegate { });
			}
		}

		public void Delete()
		{
			try
			{
				foreach (string path in Directory.GetFiles(Application.persistentDataPath))
					if (bodies.ElementAt(selection.value).Key == Path.GetFileName(path).Replace(".dat", ""))
					{
						File.Delete(path);
						break;
					}
			}
			catch (Exception)
			{
				Message.GetInstance().PopUp("Oops! Something went wrong...", delegate { });
			}
			LoadAll();
		}

		public void Save()
		{
			try
			{
				if (string.IsNullOrEmpty(fileName.text))
					throw new NullReferenceException();

				XmlSerializer serializer = new XmlSerializer(typeof(Body));

				using (FileStream stream = File.Create(Path.Combine(Application.persistentDataPath, fileName.text + ".dat")))
					serializer.Serialize(stream, generator.body);

				LoadAll();

				Message.GetInstance().PopUp("Saved successfully.", delegate { });
			}
			catch (Exception exception)
			{
				if (exception is NullReferenceException)
					Message.GetInstance().PopUp("You should write down the name.", delegate { });
			}
		}
	}
}
