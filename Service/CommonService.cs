using System;
using BusinessEntities;

namespace Service
{
	public static class CommonService
	{
		/// <summary>
		/// Check the date is 1'st April or not.
		/// </summary>
		/// <param name="today"></param>
		/// <returns></returns>
		public static bool IsFirstApril(DateTime? today) {
			if(today!=null)
				return today.Value.Date.Month == (int)Month.April && today.Value.Date.Day == 1;
			else
				return DateTime.Now.Date.Month == (int)Month.April && DateTime.Now.Date.Day == 1;
        }
	}
}

