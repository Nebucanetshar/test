using System.ComponentModel;
using wpf.Models;

namespace wpf.ViewModels;

public class PersonViewModel : INotifyPropertyChanged
{
	private Person _person;

	public PersonViewModel()
	{
		_person = new Person { Nom = "Dylan Minin", Age = 30 };
	}

	public string Nom
	{
		get { return _person.Nom; }
		set
		{
			if (_person.Nom != value)
			{
				_person.Nom = value;
				OnPropertyChanged("Name");
			}
		}
	}

	public int Age
	{
		get { return _person.Age; }
		set
		{
			if (_person.Age != value)
			{
				_person.Age = value;
				OnPropertyChanged("Age");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged(string propertyNom)
	{
		PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyNom));
	}
}
