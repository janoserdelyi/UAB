using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace com.janoserdelyi.UAB
{
	public class Browser
	{
		// https://tools.ietf.org/html/rfc7231#page-46
		// bahhh. so user agents are not terribly strict and they seem to be interpreted in many ways
		public static Browser Parse (string ua) {
			if (string.IsNullOrEmpty (ua)) {
				throw new ArgumentNullException ("ua");
			}

			ua = ua.ToLower ();

			MatchCollection mc = Regex.Matches (ua, @"(?<name>\w+)(/|:){1}(?<major>\d+)(\.(?<minor>\d+)?)");

			Browser b = new Browser ();

			foreach (Match m in mc) {
				string minor = m.Groups["minor"].Value;
				if (string.IsNullOrEmpty (minor)) {
					minor = "0";
				}
				BrowserIdentity bi = new BrowserIdentity () {
					Name = m.Groups["name"].Value,
					MajorVersion = Convert.ToInt32 (m.Groups["major"].Value),
					MinorVersion = Convert.ToInt32 (minor)
				};
				b.AddIdentity (bi);
			}

			// always return something. "garbage" in this case!
			if (b.Identities == null || b.Identities.Count == 0) {

				// will test for crawler format
				// example : Googlebot/2.1 (+http://www.google.com/bot.html) 
				// example : Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm) 
				// example : Mozilla/5.0 (compatible; YandexImages/3.0; +http://yandex.com/bots) 

				bool isCrawler = Regex.IsMatch (ua, @"\w+(/|:){1}(\d+)(\.(\d+)?)\s+\(([^\+]*)\+http[s]{0,1}://[^\)]+\)");

				// not going to get into crawler identity at this point

				b.AddIdentity (new BrowserIdentity () {
					Name = (isCrawler ? "crawler" : "garbage"),
					MajorVersion = 1,
					MinorVersion = 0
				});
			}

			return b;
		}

		public IDictionary<string, BrowserIdentity> Identities { get; set; }

		public void AddIdentity (BrowserIdentity identity) {
			if (this.Identities == null) {
				this.Identities = new Dictionary<string, BrowserIdentity> ();
			}
			if (this.Identities.ContainsKey (identity.Name.ToLower ())) {
				this.Identities[identity.Name.ToLower ()] = identity;
			} else {
				this.Identities.Add (identity.Name.ToLower (), identity);
			}
		}

		public bool Is (string name) {
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}
			return this.Identities.ContainsKey (name.ToLower ());
		}

		public BrowserIdentity Get (string name) {
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}
			if (this.Identities.ContainsKey (name.ToLower ())) {
				return this.Identities[name.ToLower ()];
			}
			return null;
		}
	}
	public class BrowserIdentity
	{
		public string Name { get; set; }
		public int MajorVersion { get; set; }
		public int MinorVersion { get; set; }
	}
}
