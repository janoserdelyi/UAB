using System;
using com.janoserdelyi.UAB;

namespace test
{
	class Program
	{
		static void Main (string[] args) {
			Console.WriteLine ("Hello World!");

			System.Collections.Generic.IList<string> uas = new System.Collections.Generic.List<string> ();

			uas.Add ("Mozilla/5.0 (Windows NT 10.0; WOW64; rv:77.0) Gecko/20100101 Firefox/77.0");
			uas.Add ("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.19582");
			uas.Add ("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36");
			uas.Add ("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A");
			uas.Add ("Mozilla/5.0 (compatible; MJ12bot/v1.2.4; http://www.majestic12.co.uk/bot.php?+)");
			uas.Add ("Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");
			uas.Add ("rando agent");
			uas.Add ("rando crawler +http://noreallyimlegit.com");
			uas.Add ("Googlebot/2.1 (+http://www.google.com/bot.html)");
			uas.Add ("Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)");
			uas.Add ("Mozilla/5.0 (compatible; YandexImages/3.0; +http://yandex.com/bots)");

			foreach (string ua in uas) {
				Browser b = Browser.Parse (ua);

				Console.WriteLine (ua);

				foreach (System.Collections.Generic.KeyValuePair<string, BrowserIdentity> identity in b.Identities) {
					Console.WriteLine ($"Name {identity.Value.Name}: , MajorVer {identity.Value.MajorVersion}: , MinorVer : {identity.Value.MinorVersion}");
				}

				Console.WriteLine ();
			}

			/*
            // this is closer to the more realistic day-to-day uses
			Console.WriteLine ($"is firefox ? {b.Is ("firefox")}");
			if (b.Is ("firefox")) {
				Console.WriteLine ($"MajorVer : {b.Get ("firefox").MajorVersion}, MinorVer : {b.Get ("firefox").MinorVersion}");
			}

            Browser b = Browser.Parse ("foo");

            if (b.Is("firefox") && b.Get("firefox").MajorVersion < 60) {
                Console.WriteLine("whoa now gramps! time to update!");
            }
            */

			Console.WriteLine ("thank you, come again");
		}
	}
}
