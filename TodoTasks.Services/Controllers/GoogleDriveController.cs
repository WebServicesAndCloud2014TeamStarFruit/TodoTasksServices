namespace TodoTasks.Services.Controllers
{
	using Microsoft.AspNet.Identity;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Http;
	using System.Web.Http.Cors;
	using TodoTasks.Data;
	using TodoTasks.FileExporter;

	//[Authorize]
	public class GoogleDriveController : ApiController
	{
		private readonly ITodoTasksData data;

		public GoogleDriveController()
			: this(new TodoTasksData())
		{
		}

		public GoogleDriveController(ITodoTasksData data)
		{
			this.data = data;
		}

		[HttpGet]
		public IHttpActionResult Create()
		{
			var userId = User.Identity.GetUserId();
			ExcelFileCreator.ExportReportToXlsxFile(this.data, userId);
			GoogleDriveFileExporter.UploadFile();
			return Ok();
		}
	}
}