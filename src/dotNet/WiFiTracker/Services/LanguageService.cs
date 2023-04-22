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
			Download_Android_App,
			Name,
			Save,
			Cancel,
			Generate,
			Edit_Terminal,
			Update,
            Are_You_Sure_Delete,
            Pick_From_Map,
			Transmitter,
			Login,
            Acc_Login_Number,
            Field_Required,
			shouldnt_be_more_than,
			Characters,
			This_Field,
			UserName,
			Password,
			Repeat,
			Login_Error,
			Register,
			Hello,
			Logout,
			Account,
            Enter_Valid_Email,
            Passwords_should_same,
			Email,
			Admin_page,
			Allow_Terminals,
			Allow_Transmitters,
			Allow_Report_LiveView,
			Allow_Report_TrackHistory,
			Allow_DownloadApp,
			Add_New_UserRole,
			UserRole,
			UserRoles,
			Users,
			Admin,
			Add_New_User,
			User,
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
			{Words.Refresh, "Refresh" },
			{Words.Name, "Name" },
			{Words.Save, "Save" },
			{Words.Cancel, "Cancel" },
			{Words.Generate, "Generate" },
			{Words.Edit_Terminal, "Edit Terminal" },
			{Words.Update, "Update" },
			{Words.Are_You_Sure_Delete, "Are you sure you want to delete this " },
			{Words.Pick_From_Map, "Pick From Map " },
			{Words.Transmitter, "Transmitter" },
			{Words.Login, "Login" },
			{Words.Acc_Login_Number, "Account Login Number" },
			{Words.Field_Required, "This field is required" },
			{Words.shouldnt_be_more_than, "shouldn't be more than" },
			{Words.Characters, "characters" },
			{Words.This_Field, "This field" },
			{Words.UserName, "UserName" },
			{Words.Password, "Password" },
			{Words.Repeat, "Repeat" },
			{Words.Login_Error, "Login credentials do not match" },
			{Words.Register, "Register" },
			{Words.Hello, "Hello" },
			{Words.Logout, "Logout" },
			{Words.Account, "Account" },
			{Words.Enter_Valid_Email, "Enter a valid email" },
			{Words.Passwords_should_same, "Passwords should be the same" },
			{Words.Email, "Email" },
			{Words.Admin_page, "Admin Page" },
			{Words.Allow_Terminals, "Allow Terminals" },
			{Words.Allow_Transmitters, "Allow Transmitters" },
			{Words.Allow_Report_LiveView, "Allow Live View" },
			{Words.Allow_Report_TrackHistory, "Allow Track History" },
			{Words.Allow_DownloadApp, "Allow Download App" },
			{Words.Add_New_UserRole, "Add New UserRole" },
			{Words.UserRole, "UserRole" },
			{Words.UserRoles, "UserRoles" },
			{Words.Users, "Users" },
			{Words.Admin, "Admin" },
			{Words.Add_New_User, "Add New User" },
            {Words.User, "User" },
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
			{Words.WiFiTracker, "Уайфай Локатор" },
			{Words.Add_New_Terminal, "Добави Нов Терминал" },
			{Words.Welcome_into_WiFiTrackerSystem, "Добре дошли в Уайфай Локатор !" },
			{Words.Applications, "Апликации" },
			{Words.Check_Route, "Провери маршрута" },
			{Words.Track_History, "История на проследяване" },
			{Words.Download_Android_App, "Изтеглете Android Приложението" },
			{Words.From, "От" },
			{Words.To, "До" },
			{Words.Refresh, "Опресняване" },
			{Words.Name, "Име" },
            {Words.Save, "Запамети" },
            {Words.Cancel, "Отказ" },
            {Words.Generate, "Изгенерирай" },
            {Words.Edit_Terminal, "Редактирай Терминал" },
            {Words.Update, "Обнови" },
            {Words.Are_You_Sure_Delete, "Сигурни ли сте, че искате да изтриете този " },
            {Words.Pick_From_Map, "Избери от карта " },
            {Words.Transmitter, "Предавател" },
            {Words.Login, "Логин" },
            {Words.Acc_Login_Number, "Номер на акаунт" },
            {Words.Field_Required, "Това поле е задължително" },
            {Words.shouldnt_be_more_than, "не трябва да бъде повече от " },
            {Words.Characters, "символа" },
            {Words.This_Field, "Това поле " },
            {Words.UserName, "Потребителско име" },
            {Words.Password, "Парола" },
            {Words.Repeat, "Повтори" },
            {Words.Login_Error, "Грешен потребител или парола" },
			{Words.Register, "Регистрация" },
			{Words.Hello, "Здравей" },
			{Words.Logout, "Изход" },
            {Words.Account, "Акаунт" },
            {Words.Enter_Valid_Email, "Въведете валиден имейл адрес" },
            {Words.Passwords_should_same, "Паролата трябва да съвпада" },
            {Words.Email, "Имейл адрес" },
			{Words.Admin_page, "Admin панел" },
            {Words.Allow_Terminals, "Позволи Терминали" },
            {Words.Allow_Transmitters, "Позволи Предаватели" },
            {Words.Allow_Report_LiveView, "Позволи На Живо" },
            {Words.Allow_Report_TrackHistory, "Позволи История на проследяване" },
            {Words.Allow_DownloadApp, "Позволи Теглене на приложението" },
            {Words.Add_New_UserRole, "Добави Нова Роля" },
            {Words.UserRole, "Потребителска роля" },
            {Words.UserRoles, "Потребителски роли" },
			{Words.Users, "Потребители" },
			{Words.Admin, "Admin" },
            {Words.Add_New_User, "Добави Нов Потребител" },
            {Words.User, "Потребител" },
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
