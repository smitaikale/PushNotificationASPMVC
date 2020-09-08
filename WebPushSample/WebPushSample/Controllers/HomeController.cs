using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonData;
using WebPush;
using WebPushSample.Models;

namespace WebPushSample.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var model = new HomeModel();
			var pushInformation = PushInformation.Load(AppDomain.CurrentDomain.BaseDirectory + "../info.xml");	// 保存先はてきとうに
			if (pushInformation == null)    // VAPIDで使用する公開鍵を作成
			{
				pushInformation = new PushInformation();
				var vapidKeys = VapidHelper.GenerateVapidKeys();
				pushInformation.VapidPublic = vapidKeys.PublicKey;
				pushInformation.VapipPrivate = vapidKeys.PrivateKey;
				PushInformation.Save(pushInformation, AppDomain.CurrentDomain.BaseDirectory + "../info.xml");    // 作ったら保存しとく
			}
			model.VapidPublic = pushInformation.VapidPublic;
			model.Data = Request.QueryString.Get("data");
			return View(model);
		}
	}
}