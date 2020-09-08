using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonData;

namespace WebPushSample.Controllers
{
	public class SubscribeController : ApiController
	{
		[HttpPost]
		public void Post(NotificationTarget target)
		{
			var pushInformation = PushInformation.Load(AppDomain.CurrentDomain.BaseDirectory + "../info.xml");  // 保存先はてきとうに
			if (pushInformation == null) return;
			pushInformation.Targets.Remove(pushInformation.Targets.FirstOrDefault(t => t.EndPoint == target.EndPoint));
			pushInformation.Targets.Add(target);
			PushInformation.Save(pushInformation, AppDomain.CurrentDomain.BaseDirectory + "../info.xml");
		}
	}
}
