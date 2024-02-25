using System;
using BusinessEntities;

namespace Service
{
	public interface ICoffeeMachineService
	{
		object GetBrewCoffee();
		double GetCurrentTempreture(string lat, string lon);
	}
}

