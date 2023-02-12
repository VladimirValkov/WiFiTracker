using System.Collections.Generic;

namespace WiFiTracker.Services
{
	public class LanguageService
	{

		//All words/senteces to translate
		public enum Words
		{
			WiFiTracker,
			Home,
			Terminals,
			Transmitters,
			Reports,
			Applications,
			Welcome_into_WiFiTrackerSystem,
			Add_New_Terminal,
			Edit,
			Delete,
			Add_New_Transmitter,
			Live_View,
			Track_History,
			Refresh,
			Terminal,
			From,
			To,
			Check_Route,
			Download_Android_App
		}

		//All supported languages
		public enum Languages
		{
			en,
			bg
		}

		//English Translations
		private Dictionary<Words, string> en = new Dictionary<Words, string> 
		{
			{Words.Delete, "Delete" },
			{Words.Edit, "Edit" },
			{Words.Home, "Home"},
			{Words.Reports, "Reports" },
			{Words.Add_New_Transmitter, "Add New Transmitter" },
			{Words.Live_View, "Live View" },
			{Words.Transmitters, "Transmitters" },
			{Words.Terminals, "Terminals" },
			{Words.Terminal, "Terminal" },
			{Words.WiFiTracker, "WiFiTracker" },
			{Words.Add_New_Terminal, "Add New Terminal" },
			{Words.Welcome_into_WiFiTrackerSystem, "Welcome To WiFiTracker System !" },
			{Words.Applications, "Applications" },
			{Words.Check_Route, "Check Route" },
			{Words.Track_History, "Track History" },
			{Words.Download_Android_App, "Download Android App" },
			{Words.From, "From" },
			{Words.To, "To" },
			{Words.Refresh, "Refresh" }
		};

		//Bulgarian Translations
		private Dictionary<Words, string> bg = new Dictionary<Words, string>
		{
			{Words.Delete, "Изтрий" },
			{Words.Edit, "Редактиране" },
			{Words.Home, "Начало"},
			{Words.Reports, "Доклади" },
			{Words.Add_New_Transmitter, "Добави Нов Предавател" },
			{Words.Live_View, "На Живо" },
			{Words.Transmitters, "Предаватели" },
			{Words.Terminals, "Терминали" },
			{Words.Terminal, "Терминал" },
			{Words.WiFiTracker, "WiFiTracker" },
			{Words.Add_New_Terminal, "Добави Нов Терминал" },
			{Words.Welcome_into_WiFiTrackerSystem, "Добре дошли в WiFiTracker !" },
			{Words.Applications, "Апликации" },
			{Words.Check_Route, "Провери маршрута" },
			{Words.Track_History, "История на проследяване" },
			{Words.Download_Android_App, "Изтеглете Android Приложението" },
			{Words.From, "От" },
			{Words.To, "До" },
			{Words.Refresh, "Опресняване" }
		};

		//Default Language
		public Languages lang { get; set; } = Languages.bg;


		//Get Word Function
		public string get(Words word)
		{
			switch (lang)
			{
				case Languages.en:
					return en[word];
				case Languages.bg:
					return bg[word];
				default:
					return "";
			}
		}

	}
}
