﻿using Edumenu.Models;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Edumenu.ViewModels
{
    public class DayViewModel
    {
        public ObservableCollection<Day> daysOfWeek { get; set; }

        public DayViewModel()
        {
            daysOfWeek = new ObservableCollection<Day>()
            {
                new Day() { Name = "Maanantai" },
                new Day() { Name = "Tiistai" },
                new Day() { Name = "Keskiviikko" },
                new Day() { Name = "Torstai" },
                new Day() { Name = "Perjantai" },
                new Day() { Name = "Lauantai" }
            };

            if (!DateTime.Today.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                SelectDay(new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek));
            }
            else
            {
                // Show Monday menus on Sunday
                SelectDay(new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DayOfWeek.Monday));
            }
        }

        public void SelectDay(string selectThisDay)
        {
            // Set isSelected property of the day to be selected to true.
            // Set isSelected property of other days to false.
            foreach (Day day in daysOfWeek)
            {
                // This used to be:
                // if (day.Name.ToLower().Equals(selectThisDay.ToLower()))
                // but Windows 10 Mobile broke the names of the days of the week,
                // producing, for example, "Maanantaina" instead if "Maanantai" when
                // executing:
                // new CultureInfo("fi-FI").DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek).
                // That is why below "Contains" statement is used now.
                if (selectThisDay.ToLower().Contains(day.Name.ToLower()))
                {
                    day.IsSelected = true;
                    continue;
                }
                day.IsSelected = false;
            }
        }

        public string GetSelectedDay()
        {
            // Returns the name of the selected day
            foreach (Day day in daysOfWeek)
            {
                if (day.IsSelected)
                {
                    return day.Name;
                }
            }
            return string.Empty;
        }
    }
}
