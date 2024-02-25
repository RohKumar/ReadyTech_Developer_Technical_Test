using System;
using BusinessEntities;

namespace Service
{
	public interface ICoffeeMachineService
	{
		BrewCoffeeResponse GetBrewCoffee();
	}
}

