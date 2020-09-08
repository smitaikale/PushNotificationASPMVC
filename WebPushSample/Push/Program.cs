using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonData;
using WebPush;

namespace Push
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length <= 0) return;
			var pushInformation = PushInformation.Load(AppDomain.CurrentDomain.BaseDirectory + "../../../info.xml");  // ASP.NETが保存しているファイルを見ます

			var webPushClient = new WebPushClient();
			var vapidDetails = new VapidDetails("mailto:smita.ikale@ness.com", pushInformation.VapidPublic, pushInformation.VapipPrivate);
			var targetList = pushInformation.Targets.ToArray();
			foreach (var target in targetList)
			{
				try
				{
					var subscription = new PushSubscription(target.EndPoint, target.PublicKey, target.AuthSecret);
					webPushClient.SendNotification(subscription, @"test application", vapidDetails);
				}
				catch (Exception exp)
				{
					if (exp.Message == "Subscription no longer valid")  // 購読者がいなくなるとこんな感じの例外を吐くので送信先から消しておこう
					{
						pushInformation.Targets.Remove(target);
					}
				}
			}
			PushInformation.Save(pushInformation, AppDomain.CurrentDomain.BaseDirectory + "../../../info.xml");
			Console.ReadKey();
		}
	}
}
