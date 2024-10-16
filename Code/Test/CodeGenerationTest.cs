using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helpers.Test
{
	[TestClass]
	public class CodeGenerationTest
	{
		[TestMethod]
		public void TestEvenCodeLength()
		{
			// Arrange
			int actualCodeLength = 4;
			CodeGenerationHelper codeGenerationHelper = new CodeGenerationHelper();

			// Act
			string randomCode = codeGenerationHelper.GenerateRandomCode(actualCodeLength);
			int expectedCodeLength = randomCode.Length;

			// Assert
			Assert.AreEqual(expectedCodeLength, actualCodeLength, 
				"Expected code length = " + expectedCodeLength + ", While actual code length = " + actualCodeLength);
		}

		[TestMethod]
		public void TestOddCodeLength()
		{
			// Arrange
			int actualCodeLength = 5;
			CodeGenerationHelper codeGenerationHelper = new CodeGenerationHelper();

			// Act
			string randomCode = codeGenerationHelper.GenerateRandomCode(actualCodeLength);
			int expectedCodeLength = randomCode.Length;

			// Assert
			Assert.AreEqual(expectedCodeLength, actualCodeLength,
				"Expected code length = " + expectedCodeLength + ", While actual code length = " + actualCodeLength);
		}
	}
}
