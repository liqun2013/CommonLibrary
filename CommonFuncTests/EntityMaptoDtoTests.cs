using CommonLibrary;
using CommonLibrary.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonLibrary.Tests
{
	[TestClass()]
	public class EntityMaptoDtoTests
	{
		[TestMethod()]
		public void ToDtoTest()
		{
			var entity = new TestEntity
			{
				Api_Account = "efg324e", Api_Available = 2, Api_CreateTime = DateTime.Now, Api_Description = "23rrfw4erf32r234rf4tg34tfg34wtfg4f34tgf34tgf",
				CompanyId = "er23er23r32r", Key = Guid.NewGuid(), Api_Deleted = 213, Api_SecretKey = 'f', P1 = 4444444444444444.3452345324, P2 = true
			};

			var dto = entity.ToDto<TestDto>();

			Assert.AreEqual(entity.Api_Account, dto.ApiAccount);
			Assert.AreEqual(entity.Api_Available, dto.Api_Available);
			Assert.AreEqual(entity.Api_CreateTime.Value.ToString(), dto.Api_CreateTime);
			Assert.AreEqual(entity.Api_Deleted, dto.Api_Deleted.Value);
			Assert.AreEqual(entity.Api_Description, dto.Api_Description);
			Assert.AreEqual(entity.Api_SecretKey, dto.ApiSecretKey);
			Assert.AreEqual(entity.Api_UpdateTime, dto.Api_UpdateTime);
			Assert.AreEqual(entity.CompanyId, dto.CompanyId);
			Assert.AreEqual(entity.Key.ToString(), dto.Key);
			Assert.AreEqual(entity.P1, dto.P1);
			Assert.AreEqual(entity.P2.ToString(), dto.P2);
		}

	}

	public class TestEntity : BaseEntity
	{
		public Guid Key { get; set; }

		public string Api_Account { get; set; }

		public char Api_SecretKey { get; set; }

		public string Api_Description { get; set; }

		public DateTime? Api_CreateTime { get; set; }

		public DateTime? Api_UpdateTime { get; set; }

		public int? Api_Available { get; set; }

		public int Api_Deleted { get; set; }

		public string CompanyId { get; set; }
		public double P1 { get; set; }
		public bool P2 { get; set; }
		public bool P4 { get; set; }
	}

	public class TestDto : BaseDto
	{
		public string Key { get; set; }

		[DtoMapperAttrbute(Mapto = "Api_Account")]
		public string ApiAccount { get; set; }

		[DtoMapperAttrbute(Mapto = "Api_SecretKey")]
		public char ApiSecretKey { get; set; }

		public string Api_Description { get; set; }

		public string Api_CreateTime { get; set; }

		public string Api_UpdateTime { get; set; }

		public int? Api_Available { get; set; }

		[DtoMapperAttrbute(Mapto = "Api_Deleted")]
		public int? Api_Deleted { get; set; }

		public string CompanyId { get; set; }
		public double P1 { get; set; }
		public string P2 { get; set; }

		public string P3 => Api_Deleted.ToString();
	}
}